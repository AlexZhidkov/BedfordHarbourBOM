using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Client.Contracts;
using Bom.Client.Entities;
using Bom.Common;

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
                Suppliers = new[] {new Supplier {Id = 1, Name = "Test Supplier 1"}},
                PartId = 1,
                Cost = 99,
                Count = 5,
                CountDate = new DateTime(2010, 1, 21)
            };
        }

        public static StockItemData GetTestStockItemData()
        {
            return new StockItemData
            {
                StockId = 1,
                PartId = 2,
                PartDescription = "Test Part Description",
                Count = 5,
                Cost = 99,
                CountDate = new DateTime(2010, 1, 21)
            };
        }

        public static Part GetTestPart()
        {
            return new Part
            {
                Id = 1,
                Description = "Test Part Description",
                Number = "12345",
                Type = PartType.Assembly,
                ComponentsCost = 99,
                IsOwnMake = true,
                Length = 55,
                Count = 5,
                Notes = "Test Notes",
                Components = new[]
                {
                    new SubassemblyData
                    {
                        AssemblyId = 1,
                        SubassemblyId = 2,
                        PartDescription = "Subassembly 2",
                        CostContribution = 33,
                        Notes = "Notes"
                    },
                    new SubassemblyData
                    {
                        AssemblyId = 1,
                        SubassemblyId = 2,
                        PartDescription = "Subassembly 2",
                        CostContribution = 33,
                        Notes = "Notes"
                    }
                }
            };
        }

        public static SubassemblyData GetTestComponent()
        {
            return new SubassemblyData
            {
                Id = 1,
                AssemblyId = 1,
                SubassemblyId = 2,
                PartDescription = "Subassembly 2",
                CostContribution = 50,
                Notes = "Test Notes",
            };
        }
    }
}
