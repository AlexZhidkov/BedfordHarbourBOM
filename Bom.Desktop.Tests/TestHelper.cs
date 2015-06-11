using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Client.Entities;

namespace Bom.Desktop.Tests
{
    public static class TestHelper
    {
        public static Supplier GetTestSupplier()
        {
            return new Supplier
            {
                Id = 1,
                Name = "Test Name",
                Contact = "Test Contact",
                Phone = "Test Phone",
                Notes = "Test Notes"
            };
        }

        public static Stock GetTestStock()
        {
            return new Stock()
            {
                Id = 1,
                Notes = "Test Notes",
                Suppliers = new[] { new Supplier { Id = 1, Name = "Test Supplier 1" } },
                Part = new Part { Id = 1, Description = "Test Part 1" },
                Cost = 99,
                Count = 5,
                CountDate = new DateTime(2010, 1, 21)
            };
        }
    }
}
