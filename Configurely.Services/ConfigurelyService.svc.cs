using Configurely.Data.Repository;
using Configurely.Entities.Response;
using System;
using System.Collections.Generic;

namespace Configurely.Services
{
    public class ConfigurelyService : IConfigurelyService
    {
        #region Employee

        public ICollection<Entities.Employee> GetEmployees() => new ConfigurelyDbRepository().GetEmployees();

        public Entities.Employee GetEmployee(string id) => new ConfigurelyDbRepository().GetEmployee(Convert.ToInt32(id));

        public Result CreateEmployee(Entities.Employee employee) => new ConfigurelyDbRepository().CreateEmployee(employee);

        public Result EditEmployee(Entities.Employee employee) => new ConfigurelyDbRepository().EditEmployee(employee);

        #endregion

        #region Department

        public ICollection<Entities.Department> GetDepartments() => new ConfigurelyDbRepository().GetDepartments();

        public Entities.Department GetDepartment(string id) => new ConfigurelyDbRepository().GetDepartment(Convert.ToInt32(id));

        public Result AddDepartmentField(Entities.Department department) => new ConfigurelyDbRepository().AddDepartmentField(department);

        public Result DeleteDepartmentField(Entities.Department department) => new ConfigurelyDbRepository().DeleteDepartmentField(department);

        #endregion

        #region Fields

        public ICollection<Entities.Field> GetFields() => new ConfigurelyDbRepository().GetFields();

        #endregion
    }
}
