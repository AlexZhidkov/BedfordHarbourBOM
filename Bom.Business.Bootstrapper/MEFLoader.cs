using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Bom.Data;
using Bom.Data.Data_Repositories;

namespace Bom.Business.Bootstrapper
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(SupplierRepository).Assembly));
            
            CompositionContainer container = new CompositionContainer(catalog);

            return container;
        }
    }
}
