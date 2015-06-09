using System.Runtime.Serialization;

namespace Bom.Business.Entities
{
    [DataContract]
    public enum PartType : int
    {
        [EnumMember]
        Undefined = 0,
        [EnumMember]
        RHS,
        [EnumMember]
        Pipe,
        [EnumMember]
        Flat,
        [EnumMember]
        Assembly
    }
}
