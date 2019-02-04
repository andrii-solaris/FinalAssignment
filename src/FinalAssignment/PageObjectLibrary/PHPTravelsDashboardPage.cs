using FinalAssignment.Utils;

namespace FinalAssignment.PageObjectLibrary
{
    //This class represents dashboard page and its functionality for PhpTravels.com
    class PhpTravelsDashboardPage : SeleniumActions
    {
        //Click "Hotels" dropdown's icon to expand it.
        public PhpTravelsDashboardPage ExpandHotelsDropdown()
        {            
            Click("xpath=//a[@href='#Hotels']");            

            return this;
        }

        //Click "Hotels" link to navigate to the corresponding section.
        public PhpTravelsDashboardPage ClickHotelsLink()
        {
            Click("xpath=//a[text()='Hotels']");

            return this;
        }

        //Click "Rooms" link to navigate to the corresponding section.
        public PhpTravelsDashboardPage ClickRoomsLink()
        {
            Click("xpath=//a[text()='Rooms']");

            return this;
        }

        //Click "Log out" to terminate session fro signed in user.
        public PhpTravelsDashboardPage LogOut()
        {
            Click("xpath=//a[text()='Log Out']");

            return this;
        }

        //Click "Add" to add new item.
        public PhpTravelsDashboardPage Add()
        {
            Click("xpath=//button[contains(text(),'Add')]");

            return this;
        }

        //Click "delete" icon in the first row of the table to delete current hotel record.
        public PhpTravelsDashboardPage DeleteFirstHotel()
        {
            Click("xpath=//table/tbody/tr[1]/td[12]/span/a[3]/i");            
            AcceptAlert();            

            return this;
        }

        //Click "edit" icon in the first row of the table to edit current hotel record.
        public PhpTravelsDashboardPage EditFirstHotel()
        {
            Click("xpath=//table/tbody/tr[1]/td[12]/span/a[2]/i");            

            return this;
        }

        //Click "delete" icon in the first row of the table to delete current room record.
        public PhpTravelsDashboardPage DeleteFirstRoom()
        {
            Click("xpath=//table/tbody/tr[1]/td[11]/span/a[2]");
            AcceptAlert();

            return this;
        }

        //Click "edit" icon in the first row of the table to edit current room record.
        public PhpTravelsDashboardPage EditFirstRoom()
        {
            Click("xpath=//table/tbody/tr[1]/td[11]/span/a[1]");

            return this;
        }
    }
}
