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

            var mainFrame = new Part
            {
                Id = 1,
                Cost = 99M,
                Notes = "",
                Length = 0,
                Description = "Main Frame",
                IsOwnMake = true,
                Type = PartType.Assembly,
                Number = "A"
            };
            var part4 = new Part
            {
                Id = 2,
                Cost = 231.9M,
                Notes = "",
                Length = 20,
                Description = "32NB @ 1875mm Trusses",
                IsOwnMake = false,
                Type = PartType.Undefined,
                Number = "4"
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
            context.Parts.Add(mainFrame);
            context.Parts.Add(part4);
            context.Parts.Add(part3);

            var subassembly1 = new Subassembly
            {
                Id = 1,
                AssemblyId = 3,
                SubassemblyId = 1,
                CostContribution = 33,
                Notes = "Component 1"
            };

            var subassembly2 = new Subassembly
            {
                Id = 2,
                AssemblyId = 3,
                SubassemblyId = 2,
                CostContribution = 50,
                Notes = "Component 2"
            };

            context.Subassemblies.Add(subassembly1);
            context.Subassemblies.Add(subassembly2);

            context.Stocks.Add(new Stock
            {
                Id = 1,
                PartId = mainFrame.Id,
                Count = 1,
                CountDate = new DateTime(2015, 1, 1)
            });
            context.Stocks.Add(new Stock
            {
                Id = 2,
                PartId = part4.Id,
                Count = 2,
                CountDate = new DateTime(2015, 1, 1)
            });
            context.Stocks.Add(new Stock
            {
                Id = 3,
                PartId = part3.Id,
                Count = 3,
                CountDate = new DateTime(2015, 1, 1)
            });

            base.Seed(context);
        }
    }
}
