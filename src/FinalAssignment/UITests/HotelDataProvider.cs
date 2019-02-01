using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using FinalAssignment.Utils;

namespace FinalAssignment.Tests
{
    class TestDataProvider : IEnumerable
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
