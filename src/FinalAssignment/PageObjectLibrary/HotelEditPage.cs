using Bogus;
using FinalAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Click("xpath=//span[text()='Enter Location']");

            return this;
        }

        public HotelEditPage SetHotelLocation(string location)
        {
            SetText("xpath=//div[@id='select2-drop']/div/input", location);

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
            Click("id=add");

            return this;
        }
    }
}
