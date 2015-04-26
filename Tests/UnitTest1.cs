using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var oneSteel = new Supplier
            {
                Name = "OneSteel"
            };

            var p75x4 = new Part
            {
                Description = "75x4 SHS @ 1500 slotted",
                Number = "14",
                Suppliers = new List<Supplier> { oneSteel }
            };

            var structureBase = new MasterStructure
            {
                ProductDefinition = "Base",
                Subassemblies = new List<Item>
                {
                    new Item 
                    {
                        Part = new Part
                        {
                            Number = "A",
                            Description = "Main Frame"                          
                        },
                        Count = 1,
                        Subassemblies = new List<Item>
                        {
                            new Item 
                            {
                                Part = new Part
                                {
                                    Description = "Top Ring - 65 x 74 x 2.5 Silo Tube - 19M",
                                    Suppliers = new List<Supplier> { oneSteel }
                                },
                                Count = 1
                            }
                        }
                    },
                    new Item 
                    {
                        Part = new Part
                        {
                            Number = "B",
                            Description = "Axel Frame"                            
                        },
                        Count = 2,
                        Subassemblies = new List<Item>
                        {
                            new Item {
                                Part = p75x4,
                                Count = 2
                            }
                        }
                    },
                    
                },

            };
        }
    }
}
