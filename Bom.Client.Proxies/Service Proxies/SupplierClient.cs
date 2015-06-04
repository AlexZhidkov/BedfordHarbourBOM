using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Bom.Client.Contracts;

namespace Bom.Client.Proxies
{
    [Export(typeof(ISupplierService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SupplierClient : ClientBase<ISupplierService>, ISupplierService
    {
        public Entities.Supplier GetSupplier(int supplierId)
        {
            return Channel.GetSupplier(supplierId);
        }

        public Entities.Supplier[] GetAllSuppliers()
        {
            return Channel.GetAllSuppliers();
        }

        public Entities.Supplier UpdateSupplier(Entities.Supplier supplier)
        {
            return Channel.UpdateSupplier(supplier);
        }

        public void DeleteSupplier(int supplierId)
        {
            Channel.DeleteSupplier(supplierId);
        }
    }
}
