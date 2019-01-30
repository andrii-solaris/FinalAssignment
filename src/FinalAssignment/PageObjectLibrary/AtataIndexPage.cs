using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using FinalAssignment.Utils;

namespace FinalAssignment.PageObjectLibrary
{
    class AtataIndexPage : SeleniumActions

    {
        public void ClickSignInButton()
        {
            Log.Information("Navigating to Atata sign-in window...");
            base.Click("id=sign-in");
        }
    }
}
