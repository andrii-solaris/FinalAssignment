using System.Threading;
using FinalAssignment.Utils;
using WindowsInput.Native;

namespace FinalAssignment.PageObjectLibrary
{
    //This class represents room edit page and its functionality for PhpTravels.net.
    class RoomEditPage : SeleniumActions
    {
        //Click "Room Type" dropdown's icon to expand it.
        public RoomEditPage ExpandRoomTypeDropdown()
        {
            Click("xpath=//label[text()='Room Type']/following-sibling::div/div/a/span[1]");

            return this;
        }

        //Populate "Room Type" input with text data.
        public RoomEditPage SetRoomType(string roomType)
        {
            SetText("xpath=//div[@id='select2-drop']/div/input", roomType);
            Thread.Sleep(2000);
            PressKey(VirtualKeyCode.RETURN);

            return this;
        }

        //Click "Hotel" dropdown's icon to expand it.
        public RoomEditPage ExpandHotelDropdown()
        {
            Click("xpath=//label[text()='Hotel']/following-sibling::div/div/a/span[1]");

            return this;
        }

        //Populate "Hotel" input with text data.
        public RoomEditPage SetHotel(string hotel)
        {
            SetText("xpath=//div[@id='select2-drop']/div/input", hotel);
            Thread.Sleep(2000);
            PressKey(VirtualKeyCode.RETURN);

            return this;
        }

        //Populate "Room Description" text input. 
        public RoomEditPage SetRoomDescription(string description)
        {
            SwitchToFrame("xpath=//div[@id='cke_1_contents']/iframe");
            SetText("xpath=//body", description);
            SwitchToDefaultContent();

            return this;
        }

        //Populate "Price" input with text data.
        public RoomEditPage SetRoomPrice(string price)
        {
            SetText("name=basicprice", price);

            return this;
        }

        //Populate "Quantity" input with text data.
        public RoomEditPage SetRoomQuantity(string quantity)
        {
            SetText("name=quantity", quantity);

            return this;
        }

        //Populate "Minimum Stay" input with text data.
        public RoomEditPage SetRoomMinStay(string duration)
        {
            SetText("name=minstay", duration);

            return this;
        }

        //Populate "Maximum Adults" input with text data.
        public RoomEditPage SetRoomMaxAdults (string adultsQuantity)
        {
            SetText("name=adults", adultsQuantity);

            return this;
        }

        //Populate "Maximum Children" input with text data.
        public RoomEditPage SetRoomMaxChildren(string childrenQuantity)
        {
            SetText("name=children", childrenQuantity);

            return this;
        }

        //Populate "Extra Beds" input with text data.
        public RoomEditPage SetRoomExtraBeds(string extraBeds)
        {
            SetText("name=extrabeds", extraBeds);

            return this;
        }

        //Populate "Extra Bed Charges" input with text data.
        public RoomEditPage SetRoomExtraBedCharges(string extraBedCharges)
        {
            SetText("name=bedcharges", extraBedCharges);

            return this;
        }

        //Click "Submit" button to submit form.
        public RoomEditPage ClickSubmit()
        {
            Click("xpath=//button[text()='Submit']");

            return this;
        }

    }
}
