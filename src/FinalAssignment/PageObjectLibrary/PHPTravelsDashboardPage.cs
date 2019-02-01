﻿using FinalAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FinalAssignment.PageObjectLibrary
{
    class PhpTravelsDashboardPage : SeleniumActions
    {
        public PhpTravelsDashboardPage ExpandHotelsDropdown()
        {            
            Click("xpath=//a[@href='#Hotels']");            

            return this;
        }

        public PhpTravelsDashboardPage ClickHotelsLink()
        {
            Click("xpath=//a[text()='Hotels']");

            return this;
        }

        public PhpTravelsDashboardPage ClickRoomsLink()
        {
            Click("xpath=//a[text()='Rooms']");

            return this;
        }

        public PhpTravelsDashboardPage LogOut()
        {
            Click("xpath=//a[text()='Log Out']");

            return this;
        }

        public PhpTravelsDashboardPage Add()
        {
            Click("xpath=//button[contains(text(),'Add')]");

            return this;
        }

        public PhpTravelsDashboardPage DeleteFirst()
        {
            Click("xpath=//table/tbody/tr[1]/td[12]/span/a[3]/i");            
            AcceptAlert();            

            return this;
        }
    }
}
