package Transcripts::Controller::MSDS;

use strict;
use warnings;
use base 'Transcripts::Controller::CrudBase';
use HTML::Element;
use Data::Dumper;

use DateTime;
use Date::Parse;
use DateTime::Duration;
use Data::Dumper;
use DateTime::Format::Strptime;


sub misc : Local {
    my ( $self, $c, $file ) = @_ ;    
    if (defined $file)
    {
        $c->response->header('Content-Type' => 'application/x-apple-diskimage');
        $c->response->header('Content-Disposition' => 'attachment; filename=' . $file);           
        $c->serve_static_file($c->config->{msds_upload_path} . '/' . $file);
    } 
}

sub list : Local {
	my ( $self, $c, @args ) = @_;	
   $c->stash->{'template'} = '/lib/crudbase/list';	
	$c->stash->{'rows'} = $c->forward('get_rs')->search_literal('status = ?', (1),{order_by=>{-desc => [qw/id/]}});
}

sub list_columns : Private {
    my ($self, $c) = @_;    
    return [
			{name=>"Material Name",eval=>'$row->material_name',type=>'String'},
         {name=>"Next Update",eval=>' $row->next_update->ymd()' ,type=>'String'},         
     ];
}


sub list_actions : Private {
    my ($self, $c, $row) = @_;

    return (
        new HTML::Element('a', href=>$c->uri_for("/msds/misc", $row->file_name, {}) )->push_content("> Download"),
    );
}

sub edit : Local Form {
    my ($self, $c, $id) = @_;

    my $form = $self->formbuilder;

    $self->SUPER::edit($c, $id);
    $c->stash->{'msds'} = $c->stash->{'row'};
    $c->stash->{'EDIT'} = 1;
    
    my $msds = $c->stash->{'msds'};
    
     $form->field(		
        name    => 'file_name',
        label   => 'File Upload',
        type	 => 'file',
        comment => defined $msds->file_name ? '<a href="/msds/misc/' . $msds->file_name . '" >Download File</a>' : '',
    );    

    my @tbl_locations = $c->model("DB::MSDS")->search(
                                                {'LOWER(location)' => {'not in' => ['warehouse', 'plant', 'office']}},
                                                {
                                                   select       => [qw/location/],                                                 
                                                   'order by'   => 'location',
                                                   distinct     => 1,                                                   
                                                });
    my  @location = ('Warehouse','Plant','Office');
    foreach my $loc (@tbl_locations) {
      push @location, $loc->location;
    }
    
    $form->field(		
        name        => 'location',
        label       => 'Location',
        type	     => 'select',
        options     => \@location,
        value 	     => defined $msds ? $msds->location : '',
        other       => 1, 
        required    => 1,
    );
        
    my @status = ({'1'=>'Active'},{'0'=>'Inactive'});
    $form->field(		
        name        => 'status',
        label       => 'Status',
        type	     => 'select',
        options     => \@status,
        value 	     => defined $msds ? $msds->status : '',
        required    => 1,
    );
   
    $form->field(		
        name    => 'file_location_hidden',	
        type	 => 'hidden',		
        value 	 => defined $msds ? $msds->file_location : '',		
    );
    
    $form->field(		
        name    => 'file_name_hidden',	
        type	 => 'hidden',		
        value 	 => defined $msds ? $msds->file_name : '',		
    );
    
   
	 my $filename='';
    my $target='';
    my $next_date='';
    
   
    if( my $upload = $c->req->upload('file_name')) {
                      
        $filename = $upload->filename;
        my ($ext) = $filename =~ /(\.[^.]+)$/;                
        $filename = $msds->id . $ext;
        $target   = $c->config->{msds_upload_path} . "/$filename";                            

        unless ( $upload->link_to($target) || $upload->copy_to($target) ) {
            die( "Failed to copy '$filename' to '$target': $!" );
        }
        
    } else {
        
        $filename = $c->req->param('file_name_hidden');
        $target =  $c->req->param('file_location_hidden');
    }
    
    if($c->request->method eq 'POST') {
                
        my $parser =  DateTime::Format::Strptime->new( pattern => '%Y-%m-%d' );
        
        my $nu = $parser->parse_datetime( $c->request->param('date_added') );
        $nu->add(years => $c->request->param('update_after_years'));
        
        my $dt = DateTime->now;
        
        $msds->update({
            status          => $c->request->param('status'),
            location        => defined $c->request->param('_other_location') ? $c->request->param('_other_location') :  $c->request->param('location'),
            file_name       => $filename,
            file_location   => $target,
            next_update     => $nu->year. '-'.$nu->month.'-'.$nu->day,
            date_updated    => $dt->year. '-'.$dt->month.'-'.$dt->day,
        });
    }

        
    if(defined $msds) {
        if (($id == 0) && ($msds->id)) {
            $c->res->redirect($c->uri_for('/msds/edit',$msds->id));
            $c->detach();
        }
    } 
}

=comment
sub list_columns : Private {
    my ($self, $c) = @_;
    
    return [
			{name=>"material_name",eval=>'$row->full_name',type=>'String'},
			
     ];
}
=cut


1;