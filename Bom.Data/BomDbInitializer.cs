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
            var topRing = new Part
            {
                Id = 2,
                Cost = 258.6M,
                Length = 19,
                Description = "Top Ring - 65 x 74 x 2.5 Silo Tube - 19M",
                IsOwnMake = true,
                Type = PartType.Assembly,
                Number = ""
            };
            var rm1 = new Part
            {
                Id = 3,
                Cost = 163.32M,
                Length = 12,
                Description = "74.6 X 65 X 2.5MM X 12M",
                IsOwnMake = false,
                Type = PartType.RHS,
                Number = ""
            };

            var part4 = new Part
            {
                Id = 4,
                Cost = 231.9M,
                Notes = "",
                Length = 20,
                Description = "32NB @ 1875mm Trusses",
                IsOwnMake = false,
                Type = PartType.Undefined,
                Number = "4"
            };

            var rm32NB = new Part
            {
                Id = 5,
                Cost = 31.62M,
                Notes = "",
                Length = 6,
                Description = "32NB MED X 6M",
                IsOwnMake = false,
                Type = PartType.Pipe,
            };

            context.Parts.Add(rm1);
            context.Parts.Add(topRing);
            context.Parts.Add(mainFrame);
            context.Parts.Add(part4);
            context.Parts.Add(rm32NB);

            context.Subassemblies.Add(new Subassembly
            {
                Id = 1,
                AssemblyId = mainFrame.Id,
                SubassemblyId = topRing.Id,
                CostContribution = 1
            });
            context.Subassemblies.Add(new Subassembly
            {
                Id = 2,
                AssemblyId = topRing.Id,
                SubassemblyId = rm1.Id,
                CostContribution = 1.583333333M,
            });
            context.Subassemblies.Add(new Subassembly
            {
                Id = 3,
                AssemblyId = mainFrame.Id,
                SubassemblyId = part4.Id,
                CostContribution = 22
            });
            context.Subassemblies.Add(new Subassembly
            {
                Id = 4,
                AssemblyId = part4.Id,
                SubassemblyId = rm32NB.Id,
                CostContribution = 3
            });

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
                PartId = topRing.Id,
                Count = 3,
                CountDate = new DateTime(2015, 1, 1)
            });
            context.Stocks.Add(new Stock
            {
                Id = 4,
                PartId = rm1.Id,
                Count = 13,
                CountDate = new DateTime(2015, 1, 1)
            });

            base.Seed(context);
        }
    }
}
