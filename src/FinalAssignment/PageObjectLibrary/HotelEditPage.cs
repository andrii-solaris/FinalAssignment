using Bogus;
using FinalAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using System.Threading;

namespace FinalAssignment.PageObjectLibrary
{
    class HotelEditPage : SeleniumActions
    {            
        public HotelEditPage SetHotelName(string name)
        {
            SetText("name=hotelname", name);

            return this;
        }

        public HotelEditPage SetHotelDescription(string description)
        {
            SwitchToFrame("xpath=//div[@id='cke_1_contents']/iframe");
            SetText("xpath=//body", description);
            SwitchToDefaultContent();

            return this;
        }

        public HotelEditPage SetHotelStars(string option)
        {
            SelectDropdownElement("name=hotelstars", option);

            return this;
        }

        public HotelEditPage SetHotelType(string option)
        {
            SelectDropdownElementByText("name=hoteltype", option);

            return this;
        }

        public HotelEditPage ExpandLocationDropdown()
        {
            Click("xpath=//div[@id='s2id_searching']/a/span[1]");

            return this;
        }

        public HotelEditPage SetHotelLocation(string location)
        {
            SetText("xpath=//div[@id='select2-drop']/div/input", location);
            Thread.Sleep(2000);            
            PressKey(VirtualKeyCode.RETURN);
            

            return this;
        }

        public HotelEditPage SetDateFrom(string date)
        {
            SetText("name=ffrom", date);

            return this;
        }

        public HotelEditPage SetDateTo(string date)
        {
            SetText("name=fto", date);

            return this;
        }

        public HotelEditPage ClickSubmit()
        {
            Click("xpath=//button[text()='Submit']");     
            
            return this;
        }
    }
}
