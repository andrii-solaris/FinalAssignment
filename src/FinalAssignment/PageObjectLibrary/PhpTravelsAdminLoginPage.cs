using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalAssignment.Utils;
using Serilog;
using System.Threading;

namespace FinalAssignment.PageObjectLibrary
{
    class PhpTravelsAdminLoginPage : SeleniumActions
    {
        public void SubmitSignInForm()
        {            
            Log.Information("Logging into PHP Travels as demo admin...");
            base.SetText("xpath=//input[not(@id='resetemail') and @name='email']", ConfigurationManager.AppSettings["Username"]);
            base.SetText("name=password", ConfigurationManager.AppSettings["Password"]).Submit();            
        }
    }
}
