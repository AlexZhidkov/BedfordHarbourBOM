using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
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
            DropCreateStoredProcedures(context);

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

            var bin = new Part
            {
                Id = 1,
                Notes = "Main product",
                Length = 0,
                Description = "Bin",
                IsOwnMake = true,
                Type = PartType.Assembly,
                Number = "Bin"
            };
            var topRing = new Part
            {
                Id = 2,
                Length = 19,
                Description = "Top Ring - 65 x 74 x 2.5 Silo Tube - 19M",
                IsOwnMake = true,
                Type = PartType.Assembly,
                Number = ""
            };
            var rm1 = new Part
            {
                Id = 3,
                OwnCost = 163.32M,
                Length = 12,
                Description = "74.6 X 65 X 2.5MM X 12M",
                IsOwnMake = false,
                Type = PartType.RHS,
                Number = ""
            };
            var part4 = new Part
            {
                Id = 4,
                Notes = "",
                Length = 20,
                Description = "32NB @ 1875mm Trusses",
                IsOwnMake = false,
                Type = PartType.Undefined,
                Number = "4"
            };
            var part36 = new Part
            {
                Id = 5,
                Notes = "",
                Length = 1700,
                Description = "32NB Drawbar Brackets@ 1700mm",
                IsOwnMake = true,
                Type = PartType.Undefined,
                Number = "36"
            };
            var rm32NB = new Part
            {
                Id = 6,
                OwnCost = 31.62M,
                Notes = "",
                Length = 6,
                Description = "32NB MED X 6M",
                IsOwnMake = false,
                Type = PartType.Pipe,
            };
            var aFrame = new Part
            {
                Id = 7,
                Notes = "",
                Length = 0,
                Description = "A-Frame Galvanised",
                IsOwnMake = true,
                Type = PartType.Assembly,
                Number = "D"
            };
            var mainFrame = new Part
            {
                Id = 8,
                Notes = "",
                Length = 0,
                Description = "Main Frame",
                IsOwnMake = true,
                Type = PartType.Assembly,
                Number = "A"
            };

            context.Parts.Add(rm1);
            context.Parts.Add(topRing);
            context.Parts.Add(mainFrame);
            context.Parts.Add(part4);
            context.Parts.Add(rm32NB);
            context.Parts.Add(part36);
            context.Parts.Add(aFrame);

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
                CostContribution = 0.333333M
            });
            context.Subassemblies.Add(new Subassembly
            {
                Id = 5,
                AssemblyId = part36.Id,
                SubassemblyId = rm32NB.Id,
                CostContribution = 0.333333M
            });
            context.Subassemblies.Add(new Subassembly
            {
                Id = 6,
                AssemblyId = aFrame.Id,
                SubassemblyId = part36.Id,
                CostContribution = 2M
            });
            context.Subassemblies.Add(new Subassembly
            {
                Id = 7,
                AssemblyId = bin.Id,
                SubassemblyId = mainFrame.Id,
                CostContribution = 1
            });
            context.Subassemblies.Add(new Subassembly
            {
                Id = 8,
                AssemblyId = bin.Id,
                SubassemblyId = aFrame.Id,
                CostContribution = 1
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

        private void DropCreateStoredProcedures(BomContext context)
        {
/*
            // Delete all stored procs, views
            foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sql\\Seed"), "*.sql"))
            {
                context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
            }
*/
            foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQL\\StoredProcedures"), "*.sql"))
            {
                context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
            }
        }
    }
}
