using Bogus;
using System.Collections.Generic;

namespace FinalAssignment.Utils
{
    class RoomDataCreator
    {

        public string RoomType { get; set; }
        public string RoomDescription { get; set; }
        public int RoomPrice { get; set; }
        public int RoomQuantity { get; set; }
        public int RoomMinStay { get; set; }
        public int RoomMaxAdults { get; set; }
        public int RoomMaxChildren { get; set; }
        public int RoomExtraBeds { get; set; }
        public int RoomExtraBedCharges { get; set; }

        public static List<RoomDataCreator> GetCreator()
        {

            var roomType = new[] { "One-Bedroom Apartment", "Triple Rooms", "Junior Suites", "Delux Room", "Double Deluxe Rooms", "Double or Twin Rooms" };

            var userFaker = new Faker<RoomDataCreator>()
                 .StrictMode(false)
                 .RuleFor(o => o.RoomType, f => f.PickRandom(roomType))
                 .RuleFor(o => o.RoomDescription, f => f.Lorem.Paragraph())
                 .RuleFor(o => o.RoomPrice, f => (f.Random.Number(999) + 1))
                 .RuleFor(o => o.RoomQuantity, f => (f.Random.Number(4) + 1))
                 .RuleFor(o => o.RoomMinStay, f => (f.Random.Number(6) + 1))
                 .RuleFor(o => o.RoomMaxAdults, f => (f.Random.Number(3) + 1))
                 .RuleFor(o => o.RoomMaxChildren, f => (f.Random.Number(3) + 1))
                 .RuleFor(o => o.RoomExtraBeds, f => (f.Random.Number(2) + 1))
                 .RuleFor(o => o.RoomExtraBedCharges, f => (f.Random.Number(199) + 1));

            return userFaker.Generate(5);
        }
    }
}