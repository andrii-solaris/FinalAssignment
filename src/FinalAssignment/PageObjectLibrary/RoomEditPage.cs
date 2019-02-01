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
    }
}
