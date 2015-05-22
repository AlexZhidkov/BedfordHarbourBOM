using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.Collections.Generic;
using System.IO;

namespace Tests
{
    [TestClass]
    public class DataTests
    {
        [TestInitialize]
        public void Init()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));
        }

        [Ignore]
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

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
