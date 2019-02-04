using FinalAssignment.Utils;
using WindowsInput.Native;
using System.Threading;

namespace FinalAssignment.PageObjectLibrary
{
    //This class represents hotel edit page and its functionality for PhpTravels.net.
    class HotelEditPage : SeleniumActions
    {            
        //Populate "Hotel Name" text input .
        public HotelEditPage SetHotelName(string name)
        {
            SetText("name=hotelname", name);

            return this;
        }

        //Populate "Hotel Description" text input. 
        public HotelEditPage SetHotelDescription(string description)
        {
            SwitchToFrame("xpath=//div[@id='cke_1_contents']/iframe");
            SetText("xpath=//body", description);
            SwitchToDefaultContent();

            return this;
        }

        //Select random value from "Stars" dropdown.
        public HotelEditPage SetHotelStars(string option)
        {
            SelectDropdownElement("name=hotelstars", option);

            return this;
        }

        //Select random value from "Hotel Type" dropdown.
        public HotelEditPage SetHotelType(string option)
        {
            SelectDropdownElementByText("name=hoteltype", option);

            return this;
        }

        //Click "Location" dropdown's icon to expand it.
        public HotelEditPage ExpandLocationDropdown()
        {
            Click("xpath=//div[@id='s2id_searching']/a/span[1]");

            return this;
        }

        //Populate "Location" input with text data.
        public HotelEditPage SetHotelLocation(string location)
        {
            SetText("xpath=//div[@id='select2-drop']/div/input", location);
            Thread.Sleep(2000);            
            PressKey(VirtualKeyCode.RETURN);
            

            return this;
        }

        //Populate "Date From" input with text data.
        public HotelEditPage SetDateFrom(string date)
        {
            SetText("name=ffrom", date);

            return this;
        }

        //Populate "Date To" input with text data.
        public HotelEditPage SetDateTo(string date)
        {
            SetText("name=fto", date);

            return this;
        }

        //Click "Submit" button to submit form.
        public HotelEditPage ClickSubmit()
        {
            Click("xpath=//button[text()='Submit']");     
            
            return this;
        }
    }
}
