using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using FinalAssignment.Utils;
using Serilog;

namespace FinalAssignment.PageObjectLibrary
{
    class AtataSignInPage : SeleniumActions
    {
        public void SubmitSignInForm()
        {
            Log.Information("Logging in Atata web app...");
            base.SetText("id=email", ConfigurationManager.AppSettings["Username"]);
            base.SetText("id=password", ConfigurationManager.AppSettings["Password"]);
            base.Click("xpath=//input[@value='Sign In']");
        }
    }
}
