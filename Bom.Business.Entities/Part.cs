using System.Collections.Generic;
using System.Runtime.Serialization;
using Bom.Common;
using Core.Common.Contracts;

namespace Bom.Business.Entities
{
    [DataContract]
    public class Part : BaseEntity, IIdentifiableEntity
    {
        [DataMember]
        public PartType Type { get; set; }
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public IEnumerable<Part> Components { get; set; }
        [DataMember]
        public bool IsOwnMake { get; set; }
        [DataMember]
        public int Length { get; set; }
        [DataMember]
        public int Cost { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
