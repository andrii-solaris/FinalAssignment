using System.Configuration;
using FinalAssignment.Utils;
using Serilog;

namespace FinalAssignment.PageObjectLibrary
{
    class PhpTravelsAdminLoginPage : SeleniumActions
    {
        //Populate sign in form with valid username and email and submit.
        public void SubmitSignInForm()
        {            
            Log.Information("Logging into PHP Travels as demo admin...");
            base.SetText("xpath=//input[not(@id='resetemail') and @name='email']", ConfigurationManager.AppSettings["Username"]);
            base.SetText("name=password", ConfigurationManager.AppSettings["Password"]).Submit();            
        }
    }
}
