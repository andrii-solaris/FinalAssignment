using FinalAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment.PageObjectLibrary
{
    class PHPTravelsDashboardPage : SeleniumActions
    {
        public void ExpandHotelsDropdown()
        {
            Click("xpath=//a[@href='#Hotels']");
        }

        public void ClickHotelsLink()
        {
            Click("xpath=//a[text()='Hotels']");
        }

        public void ClickRoomsLink()
        {
            Click("xpath=//a[text()='Rooms']");
        }

        public void LogOut()
        {
            Click("xpath=//a[text()='Log Out']");
        }

        public void Add()
        {
            Click("xpath=//button[contains(text(),'Add')]");
        }
    }
}
