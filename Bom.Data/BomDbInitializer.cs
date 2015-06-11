using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Business.Entities;
using Bom.Common;

namespace Bom.Data
{
    public class BomDbInitializer : DropCreateDatabaseIfModelChanges<BomContext>
    {
        protected override void Seed(BomContext context)
        {
            context.Suppliers.Add(new Supplier
            {
                Id = 1,
                Name = "Name 1",
                Contact = "Ted",
                Notes = "Notes 1",
                Phone = "123456789"
            });
            context.Suppliers.Add(new Supplier
            {
                Id = 2,
                Name = "Name 2",
                Contact = "John",
                Notes = "Notes 2",
                Phone = "22334455"
            });
            var part1 = new Part
            {
                Id = 1,
                Cost = 99,
                Notes = "Notes 1",
                Length = 10,
                Description = "Desc 1",
                IsOwnMake = false,
                Type = PartType.Flat,
                Number = "N1"
            };
            var part2 = new Part
            {
                Id = 2,
                Cost = 49,
                Notes = "Notes 2",
                Length = 20,
                Description = "Desc 2",
                IsOwnMake = false,
                Type = PartType.Pipe,
                Number = "N2"
            };
            var part3 = new Part
            {
                Id = 3,
                Cost = 149,
                Notes = "",
                Length = 0,
                Description = "Assembly",
                IsOwnMake = true,
                Type = PartType.Assembly,
                Number = "N3"
            };
            context.Parts.Add(part1);
            context.Parts.Add(part2);
            context.Parts.Add(part3);

            context.Stocks.Add(new Stock
            {
                Id = 1,
                Part = part1,
                Count = 1,
                CountDate = new DateTime(2015, 1, 1)
            });
            context.Stocks.Add(new Stock
            {
                Id = 2,
                Part = part2,
                Count = 2,
                CountDate = new DateTime(2015, 1, 1)
            });
            context.Stocks.Add(new Stock
            {
                Id = 3,
                Part = part3,
                Count = 3,
                CountDate = new DateTime(2015, 1, 1)
            });

            base.Seed(context);
        }
    }
}
