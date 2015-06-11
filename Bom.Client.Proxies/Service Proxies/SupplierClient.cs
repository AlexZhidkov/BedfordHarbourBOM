using System.ComponentModel.Composition;
using System.ServiceModel;
using Bom.Client.Contracts;
using Bom.Client.Entities;

namespace Bom.Client.Proxies
{
    [Export(typeof(ISupplierService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SupplierClient : ClientBase<ISupplierService>, ISupplierService
    {
        public Supplier GetSupplier(int supplierId)
        {
            return Channel.GetSupplier(supplierId);
        }

        public Supplier[] GetAllSuppliers()
        {
            return Channel.GetAllSuppliers();
        }

        public Supplier UpdateSupplier(Supplier supplier)
        {
            return Channel.UpdateSupplier(supplier);
        }

        public void DeleteSupplier(int supplierId)
        {
            Channel.DeleteSupplier(supplierId);
        }
    }
}
