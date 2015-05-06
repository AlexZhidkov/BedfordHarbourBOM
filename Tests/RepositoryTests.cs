using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.Collections.Generic;
using System.IO;

namespace Tests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestInitialize]
        public void Init()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));
        }

        [TestMethod]
        public void CreateDb()
        {
            using (var db = new BomContext())
            {
                var supplier = new Supplier
                {
                    Name = "Test"
                };

                db.Suppliers.Add(supplier);
                db.SaveChanges();
            }
        }

        //[TestMethod]
        //public void BaseReqTwoLengthsOfRawMaterial()
        //{
        //    var basePart = new Part
        //    {
        //        Description = "Base"
        //    };
        //    var material1 = new RawMaterial
        //    {
        //        Description = "65 X 65 X 2.5MM X 8M",
        //        Length = 8000
        //    };
        //    var materialToPart1 = new RawMaterialToPart
        //    {
        //        Part = basePart,
        //        RawMaterial = material1,
        //        Length = 16000
        //    };
        //    var materialToPart1 = new RawMaterialToPart
        //    {
        //        Part = basePart,
        //        RawMaterial = material1,
        //        Length = 17000
        //    };
        //}

        //[TestMethod]
        //public void TestMethod1()
        //{
        //    var oneSteel = new Supplier
        //    {
        //        Name = "OneSteel"
        //    };

        //    var p75x4 = new Part
        //    {
        //        Description = "75x4 SHS @ 1500 slotted",
        //        Number = "14",
        //        Suppliers = new List<Supplier> { oneSteel }
        //    };

        //    var structureBase = new MasterStructure
        //    {
        //        ProductDefinition = "Base",
        //        Subassemblies = new List<Item>
        //        {
        //            new Item 
        //            {
        //                Part = new Part
        //                {
        //                    Number = "A",
        //                    Description = "Main Frame"                          
        //                },
        //                Count = 1,
        //                Subassemblies = new List<Item>
        //                {
        //                    new Item 
        //                    {
        //                        Part = new Part
        //                        {
        //                            Description = "Top Ring - 65 x 74 x 2.5 Silo Tube - 19M",
        //                            Suppliers = new List<Supplier> { oneSteel }
        //                        },
        //                        Count = 1
        //                    }
        //                }
        //            },
        //            new Item 
        //            {
        //                Part = new Part
        //                {
        //                    Number = "B",
        //                    Description = "Axel Frame"                            
        //                },
        //                Count = 2,
        //                Subassemblies = new List<Item>
        //                {
        //                    new Item {
        //                        Part = p75x4,
        //                        Count = 2
        //                    }
        //                }
        //            },
                    
        //        },

        //    };
        //}
    }
}
