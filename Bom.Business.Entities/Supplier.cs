using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;

namespace Bom.Business.Entities
{
    [DataContract]
    public class Supplier : BaseEntity, IIdentifiableEntity
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Contact { get; set; }
        [DataMember]
        public string Phone { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
