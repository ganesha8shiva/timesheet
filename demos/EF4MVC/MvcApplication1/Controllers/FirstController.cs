using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class FirstController : Controller
    {
        //
        // GET: /First/

        public ActionResult Index()
        {

            return View();
        }
        public string GetStatus()
        {
         
                return "Status OK at " + DateTime.Now.ToLongTimeString();         
        }

        public string UpdateForm(string textBox1)
        {
            if (textBox1 != "Enter text")
            {
                return "You entered: \"" + textBox1.ToString() + "\" at " +
                    DateTime.Now.ToLongTimeString();
            }

            return String.Empty;
        }
    }
}
