using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment.Utils
{
    class HotelDataCreator
    {   
        
        public string HotelName { get; set; }
        public string HotelDescription { get; set; }
        public string HotelStars { get; set; }
        public string HotelType { get; set; }
        public string HotelLocation { get; set; }

        public static List<HotelDataCreator> GetCreator()
        {

            var hotelType = new[] { "Apartment", "Hotel", "Guest House", "Motel", "Residence", "Resort" };
            var hotelLocation = new[] { "London", "Ternopol", "Glasgow", "Bristol", "Madrid", "Canberra" };

            var userFaker =  new Faker<HotelDataCreator>()
                 .StrictMode(false)
                 .RuleFor(o => o.HotelName, f => f.Company.CompanyName())
                 .RuleFor(o => o.HotelDescription, f => f.Lorem.Paragraph())                 
                 .RuleFor(o => o.HotelStars, f => (f.Random.Number(4) + 1).ToString())
                 .RuleFor(o => o.HotelType, f => f.PickRandom(hotelType))
                 .RuleFor(o => o.HotelLocation, f => f.PickRandom(hotelLocation));

            return userFaker.Generate(5);
        }
    }
}