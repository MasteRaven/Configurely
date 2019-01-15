using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Configurely.Entities
{
    public class Employee
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Department EmployeeDepartment { get; set; }
        [DataMember]
        public List<EmployeeData> EmployeeData { get; set; }
        [DataMember]
        [Editable(false)]
        public DateTime DateCreated { get; set; }
    }
}
