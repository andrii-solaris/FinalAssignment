using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment.Utils
{
    class UserDataCreator
    {
        private static UserDataCreator instance;
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Company { get; set; }
        public string Password { get; set; }

        private UserDataCreator()
        {
            new Faker<UserDataCreator>()
                .StrictMode(false)
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.SecondName, f => f.Name.LastName())
                .RuleFor(o => o.Email, (f, a) => f.Internet.Email(a.FirstName, a.SecondName))
                .RuleFor(o => o.Company, f => f.Company.CompanyName())
                .RuleFor(o => o.Password, f => f.Internet.Password(15))
                .Populate(this);
        }

        public static UserDataCreator GetCreator()
        {
            if (instance == null)
            {
                instance = new UserDataCreator();
            }

            return instance;
        }
    }
}