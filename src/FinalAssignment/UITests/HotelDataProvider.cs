using System.Collections;
using FinalAssignment.Utils;

namespace FinalAssignment.Tests
{
    //This class serves as a data provider for hotel-keyword UI tests. It is set up with randomly generated test data. 
    class HotelDataProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            
            foreach(var item in HotelDataCreator.GetCreator())
            {                
                yield return new string[] {

                    item.HotelName,
                    item.HotelDescription,
                    item.HotelStars,
                    item.HotelType,
                    item.HotelLocation

                };
            }                      
            
        }
    }
}
