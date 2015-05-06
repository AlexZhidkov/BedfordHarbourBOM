using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Data
{
    public class BomDbInitializer : DropCreateDatabaseIfModelChanges<BomContext>
    {
        protected override void Seed(BomContext context)
        {
            //Suppliers
            var oneSteel = new Supplier { Name = "One Steel", Contact = "Fred", Phone = "0420420420" };
            context.Suppliers.Add(new Supplier { Name = "Action Laser", Contact = "John", Phone = "0420420421" });
            context.Suppliers.Add(new Supplier { Name = "Agshop", Contact = "Bob", Phone = "0420420422" });
            context.Suppliers.Add(new Supplier { Name = "Murray George", Contact = "Murray", Phone = "0420420423" });
            context.Suppliers.Add(new Supplier { Name = "Tyrepower", Contact = "Paul", Phone = "0420420424" });
            context.Suppliers.Add(new Supplier { Name = "Bare-Co", Contact = "Chris", Phone = "0420420425" });

            //Raw Material - stuff we buy from suppliers.
            var rm_74x65x2 = new RawMaterial
            {
                Type = RawMaterialType.RHS,
                Description = "74.6 X 65 X 2.5MM X 12M",
                Length = 12000,
                Suppliers = new List<Supplier> {oneSteel},
                Price = 1100
            };
            var rm_65x65x2 = new RawMaterial
            {
                Type = RawMaterialType.RHS,
                Description = "65 X 65 X 2.5MM X 8M",
                Length = 8000,
                Suppliers = new List<Supplier> {oneSteel},
                Price = 1000
            };
            var rm_32NBx6 = new RawMaterial
            {
                Type = RawMaterialType.Pipe,
                Description = "32NB MED X 6M",
                Length = 6000,
                Suppliers = new List<Supplier> {oneSteel},
                Price = 599
            };

            //Items. Item is a simpliest part. It doesn't have any subassemblies. It's made from one piece of raw material.
            var topRing = new Item 
            {
                Description = "Top Ring - 65 x 74 x 2.5 Silo Tube - 19M",
                IsOwnMake = true,
                RawMaterial = rm_74x65x2,
                Length = 19000,
                Count = 1
            };
            var innerRing = new Item 
            {
                Description = "Inner Ring - 65x2.5 SHS - 17M",
                IsOwnMake = true,
                RawMaterial = rm_74x65x2,
                Length = 17000,
                Count = 1
            };
            var outerRing = new Item 
            {
                Description = "Outer Ring - 65x2.5 SHS - 16M",
                IsOwnMake = true,
                RawMaterial = rm_74x65x2,
                Length = 16000,
                Count = 1
            };
            var item_32NB_150mm = new Item 
            {
                Description = "32NB @ 150mm ",
                IsOwnMake = true,
                RawMaterial = rm_32NBx6,
                Length = 150,
                Count = 4
            };

            //Parts (sub-assembies)
            var basePart = new Part 
            {
                Description = "Base",
                Items = new List<Item> { topRing, innerRing, outerRing },
                IsOwnMake = true
            };
            var conePart = new Part 
            {
                Description = "Cone"
            };
            var wallPart = new Part 
            {
                Description = "Wall"
            };
            var topPart = new Part 
            {
                Description = "Top"
            };

            //Product
            context.MasterStructures.Add(new MasterStructure 
            { 
                ProductDefinition = "Field Bin", 
                Subassemblies = new List<Part> { basePart, conePart, wallPart, topPart }
            });

            //Stock - contains only raw material
            context.Stocks.Add(new Stock
            {
                RawMaterial = rm_74x65x2,
                Count = 10,
                //Actual price we bought this material for.
                Price = 1100
            });
            context.Stocks.Add(new Stock
            {
                RawMaterial = rm_65x65x2,
                Count = 20,
                //Actual price we bought this material for.
                Price = 1000
            });
            context.Stocks.Add(new Stock
            {
                RawMaterial = rm_32NBx6,
                Count = 30,
                //Actual price we bought this material for.
                Price = 600
            });

            //Product - contains parts made by us.
            context.Products.Add(new Product
            {
                Item = topRing,
                Count = 3,
                //Cost of one.
                Cost = 3500
            });

            base.Seed(context);
        }
    }
}