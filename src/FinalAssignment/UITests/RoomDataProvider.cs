using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalAssignment.Utils;

namespace FinalAssignment.UITests
{
    class RoomDataProvider : IEnumerable
    {

        public IEnumerator GetEnumerator()
        {
            foreach (var item in RoomDataCreator.GetCreator())
            {
                yield return new string[] {

                    item.RoomType,
                    item.RoomDescription,
                    item.RoomPrice.ToString(),
                    item.RoomQuantity.ToString(),
                    item.RoomMinStay.ToString(),
                    item.RoomMaxAdults.ToString(),
                    item.RoomMaxChildren.ToString(),
                    item.RoomExtraBeds.ToString(),
                    item.RoomExtraBedCharges.ToString()

                };
            }

        }
    }
}
