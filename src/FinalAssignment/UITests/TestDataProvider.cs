using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FinalAssignment.Tests
{
    class TestDataProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new string[] { "1", "John Smith", "admin@mail.com", "London", "Male" };
            yield return new string[] { "2", "Jane Smith", "jane.smith@mail.com", "Tokio", "Female" };
        }
    }
}
