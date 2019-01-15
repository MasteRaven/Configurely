using Configurely.Data.Model;
using Configurely.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;

namespace Configurely.Data.Repository
{
    public class ConfigurelyDbRepository
    {
        #region Employee

        /// <summary>
        /// Returns all the created employees
        /// </summary>
        /// <returns>List of Employees</returns>
        public List<Entities.Employee> GetEmployees()
        {
            List<Entities.Employee> employees = new List<Entities.Employee>();

            try
            {
                using (var context = new ConfigurelyContext())
                {
                    var query = context.Employee.Include(x => x.Department)
                                                .Include(x => x.Department.Field)
                                                .Include(x => x.Department.Field.Select(y => y.FieldType))
                                                .Include(x => x.EmployeeData)
                                                .Include(x => x.EmployeeData.Select(y => y.Field))
                                                .Include(x => x.EmployeeData.Select(y => y.Field.FieldType));

                    foreach (Employee dbEmployee in query)
                    {
                        employees.Add(GetResponseEmployee(dbEmployee));
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }

            return employees;
        }

        /// <summary>
        /// Return a Employee by Id
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Employee</returns>
        public Entities.Employee GetEmployee(int id)
        {
            Entities.Employee responseEmployee = null;

            try
            {
                using (var context = new ConfigurelyContext())
                {
                    var query = context.Employee.Include(x => x.Department)
                                                .Include(x => x.Department.Field)
                                                .Include(x => x.Department.Field.Select(y => y.FieldType))
                                                .Include(x => x.EmployeeData)
                                                .Include(x => x.EmployeeData.Select(y => y.Field))
                                                .Include(x => x.EmployeeData.Select(y => y.Field.FieldType))
                                                .SingleOrDefault(x => x.ID == id);

                    if (query != null)
                    {
                        responseEmployee = GetResponseEmployee(query);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return responseEmployee;
        }

        /// <summary>
        /// Creates a new Employee
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <returns>Operation result</returns>
        public Result CreateEmployee(Entities.Employee employee)
        {
            Result response = new Result();

            try
            {
                using (var context = new ConfigurelyContext())
                {
                    Employee newEmployee = new Employee {
                        Name = employee.Name,
                        DepartmentID = employee.EmployeeDepartment.ID,
                        DateCreated = DateTime.Now,
                        EmployeeData = employee.EmployeeData.Select(x => new EmployeeData()
                        {
                            FieldID = x.Field.ID,
                            Value = x.Value
                        }).ToList()
                    };

                    context.Employee.Add(newEmployee);

                    context.SaveChanges();

                    response.isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.Error = ex.Message;
            }

            return new Result();
        }

        /// <summary>
        /// Changes a Employee
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <returns>Operation result</returns>
        public Result EditEmployee(Entities.Employee employee)
        {
            Result oResult = new Result();

            try
            {
                using (var context = new ConfigurelyContext())
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var dbEmployee = context.Employee.Include(x => x.Department)
                                                .Include(x => x.EmployeeData)
                                                .Include(x => x.EmployeeData.Select(y => y.Field))
                                                .Include(x => x.EmployeeData.Select(y => y.Field.FieldType))
                                                .SingleOrDefault(x => x.ID == employee.ID);

                            dbEmployee.Name = employee.Name;

                            if (dbEmployee.DepartmentID != employee.EmployeeDepartment.ID)
                            {
                                dbEmployee.DepartmentID = employee.EmployeeDepartment.ID;
                            }

                            dbEmployee.EmployeeData.Clear();

                            employee.EmployeeData.ForEach(x => dbEmployee.EmployeeData.Add(new EmployeeData()
                            {
                                FieldID = x.Field.ID,
                                Value = x.Value
                            }));

                            context.SaveChanges();

                            dbContextTransaction.Commit();

                            oResult.isSuccess = true;
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();

                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oResult.isSuccess = false;
                oResult.Error = ex.Message;
            }

            return oResult;
        }

        /// <summary>
        /// Converts the Employee object from the BD to the Employee object that will be returned
        /// </summary>
        /// <param name="dbEmployee">DB Employee</param>
        /// <returns>Response Employee</returns>
        private Entities.Employee GetResponseEmployee(Employee dbEmployee)
        {
            Entities.Employee oEmployee = null;

            oEmployee = new Entities.Employee
            {
                ID = dbEmployee.ID,
                Name = dbEmployee.Name,
                DateCreated = dbEmployee.DateCreated
            };

            oEmployee.EmployeeDepartment = new Entities.Department
            {
                ID = dbEmployee.Department.ID,
                Name = dbEmployee.Department.Name
            };

            oEmployee.EmployeeDepartment.Fields = dbEmployee.Department.Field.Select(x => new Entities.Field
            {
                ID = x.ID,
                Name = x.Name,
                Type = new Entities.FieldType { ID = x.FieldType.ID, Type = x.FieldType.Type },
                DefaultValue = x.DefaultValue,
                Sort = x.Sort,
                Value = x.Value
            }).ToList();

            oEmployee.EmployeeData = new List<Entities.EmployeeData>();

            foreach (EmployeeData data in dbEmployee.EmployeeData)
            {
                oEmployee.EmployeeData.Add(new Entities.EmployeeData()
                {
                    EmployeeID = data.EmployeeID,
                    Value = data.Value,
                    Field = new Entities.Field
                    {
                        ID = data.Field.ID,
                        Name = data.Field.Name,
                        Type = new Entities.FieldType { ID = data.Field.FieldType.ID, Type = data.Field.FieldType.Type },
                        DefaultValue = data.Field.DefaultValue,
                        Sort = data.Field.Sort,
                        Value = data.Field.Value
                    }
                });
            }

            return oEmployee;
        }

        #endregion

        #region Department

        /// <summary>
        /// Returns all the created departments and the corresponding Fields
        /// </summary>
        /// <returns>Departments</returns>
        public ICollection<Entities.Department> GetDepartments()
        {
            List<Entities.Department> departments = new List<Entities.Department>();

            try
            {
                using (var context = new ConfigurelyContext())
                {
                    var query = context.Department.Include(x => x.Field)
                                                  .Include(x => x.Field)
                                                  .Include(x => x.Field.Select(y => y.FieldType)).ToList();

                    foreach(Department dbDepartment in query)
                    {
                        departments.Add(GetResponseDepartment(dbDepartment));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return departments;
        }

        /// <summary>
        /// Return a single Department
        /// </summary>
        /// <param name="id">Department Id</param>
        /// <returns>Department</returns>
        public Entities.Department GetDepartment(int id)
        {
            Entities.Department responseDepartment = null;

            try
            {
                using (var context = new ConfigurelyContext())
                {
                    var query = context.Department.Include(x => x.Field)
                                                  .Include(x => x.Field)
                                                  .Include(x => x.Field.Select(y => y.FieldType))
                                                  .SingleOrDefault(x => x.ID == id);

                    if (query != null)
                    {
                        responseDepartment = GetResponseDepartment(query);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return responseDepartment;
        }

        /// <summary>
        /// Converts the Department object from the BD to the Department object that will be returned
        /// </summary>
        /// <param name="dbDepartment">DB Department</param>
        /// <returns>Response Department</returns>
        private Entities.Department GetResponseDepartment(Department dbDepartment)
        {
            Entities.Department oDepartment = null;

            oDepartment = new Entities.Department
            {
                ID = dbDepartment.ID,
                Name = dbDepartment.Name
            };

            oDepartment.Fields = dbDepartment.Field.Select(x => new Entities.Field
            {
                ID = x.ID,
                Name = x.Name,
                Type = new Entities.FieldType { ID = x.FieldType.ID, Type = x.FieldType.Type },
                DefaultValue = x.DefaultValue,
                Sort = x.Sort,
                Value = x.Value
            }).ToList();

            return oDepartment;
        }

        #endregion

        #region Fields

        /// <summary>
        /// Return all the existing Fields
        /// </summary>
        /// <returns>Fields List</returns>
        public List<Entities.Field> GetFields()
        {
            List<Entities.Field> responseField = null;

            try
            {
                using (var context = new ConfigurelyContext())
                {
                    responseField = context.Field.Select(x => new Entities.Field
                    {
                        ID = x.ID,
                        Name = x.Name
                    }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return responseField;
        }

        /// <summary>
        /// Adds a new fields to the respective department
        /// </summary>
        /// <param name="department">Department</param>
        /// <returns>Operation result</returns>
        public Result AddDepartmentField(Entities.Department department)
        {
            Result response = new Result();

            try
            {
                using (var context = new ConfigurelyContext())
                {
                    Department dbDepartment = new Department { ID = department.ID };
                    context.Department.Add(dbDepartment);
                    context.Department.Attach(dbDepartment);

                    Field dbField = new Field { ID = department.Fields.Single().ID };
                    context.Field.Add(dbField);
                    context.Field.Attach(dbField);

                    dbDepartment.Field.Add(dbField);

                    context.SaveChanges();

                    response.isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.Error = ex.Message;
            }

            return new Result();
        }

        /// <summary>
        /// Removes a fields from the respective department
        /// </summary>
        /// <param name="department">Department</param>
        /// <returns>Operation result</returns>
        public Result DeleteDepartmentField(Entities.Department department)
        {
            Result response = new Result();

            try
            {
                using (var context = new ConfigurelyContext())
                {
                    Department dbDepartment = context.Department.Include(x => x.Field).Single(x => x.ID == department.ID);

                    Field fieldToDelete = dbDepartment.Field.Where(x => x.ID == department.Fields.Single().ID).SingleOrDefault();

                    if (fieldToDelete != null)
                    {
                        dbDepartment.Field.Remove(fieldToDelete);

                        context.SaveChanges();

                        response.isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.Error = ex.Message;
            }

            return new Result();
        }

        #endregion
    }
}
