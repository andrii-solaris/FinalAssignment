using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FinalAssignment.Utils;
using WindowsInput.Native;

namespace FinalAssignment.PageObjectLibrary
{
    class RoomEditPage : SeleniumActions
    {
        public RoomEditPage ExpandRoomTypeDropdown()
        {
            Click("xpath=//label[text()='Room Type']/following-sibling::div/div/a/span[1]");

            return this;
        }

        public RoomEditPage SetRoomType(string roomType)
        {
            SetText("xpath=//div[@id='select2-drop']/div/input", roomType);
            Thread.Sleep(2000);
            PressKey(VirtualKeyCode.RETURN);

            return this;
        }

        public RoomEditPage SetRoomDescription(string description)
        {
            SwitchToFrame("xpath=//div[@id='cke_1_contents']/iframe");
            SetText("xpath=//body", description);
            SwitchToDefaultContent();

            return this;
        }

        public RoomEditPage SetRoomPrice(string price)
        {
            SetText("name=basicprice", price);

            return this;
        }

        public RoomEditPage SetRoomQuantity(string quantity)
        {
            SetText("name=quantity", quantity);

            return this;
        }

        public RoomEditPage SetRoomMinStay(string duration)
        {
            SetText("name=minstay", duration);

            return this;
        }

        public RoomEditPage SetRoomMaxAdults (string adultsQuantity)
        {
            SetText("name=adults", adultsQuantity);

            return this;
        }

        public RoomEditPage SetRoomMaxChildren(string childrenQuantity)
        {
            SetText("name=children", childrenQuantity);

            return this;
        }

        public RoomEditPage SetRoomExtraBeds(string extraBeds)
        {
            SetText("name=extrabeds", extraBeds);

            return this;
        }

        public RoomEditPage SetRoomExtraBedCharges(string extraBedCharges)
        {
            SetText("name=bedcharges", extraBedCharges);

            return this;
        }

        public RoomEditPage ClickSubmit()
        {
            Click("xpath=//button[text()='Submit']");

            return this;
        }

    }
}
