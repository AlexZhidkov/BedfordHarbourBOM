using System.ComponentModel.Composition;
using System.ServiceModel;
using Bom.Client.Contracts;
using Bom.Client.Entities;

namespace Bom.Client.Proxies
{
    [Export(typeof(IPartService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PartClient : ClientBase<IPartService>, IPartService
    {
        public Part[] GetAllParts()
        {
            return Channel.GetAllParts();
        }

        public Part UpdatePart(Part part)
        {
            return Channel.UpdatePart(part);
        }

        public void DeletePart(int partId)
        {
            Channel.DeletePart(partId);
        }
    }
}
