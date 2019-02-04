using System.Collections;
using FinalAssignment.Utils;

namespace FinalAssignment.Tests
{
    //This class serves as a data provider for room-keyword UI tests. It is set up with randomly generated test data. 
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
