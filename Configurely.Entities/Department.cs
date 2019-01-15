using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Configurely.Entities
{
    public class Department
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<Field> Fields { get; set; }
}
}
