using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1
{
    public class MarkerModule : IHttpModule
    {

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            //throw new NotImplementedException();
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpContext ctx = app.Context;
            ctx.Response.AppendHeader("Auther", "Bhumip");
            ctx.Response.Write("begin request");
            //throw new NotImplementedException();
        }
    }
}