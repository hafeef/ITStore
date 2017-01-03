using Inventory.Contracts.Administration;
using System;
using System.Collections.Generic;
using Inventory.ViewModels.Administration;
using Inventory.DomainClasses.Administration;
using Inventory.Data.Administration;
using AutoMapper;

namespace Inventory.Repositories.Administration
{
    public class EmployeeRepository : DataRepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AdminContext context) : base(context)
        {
        }

        public void CreateEmployee(EmployeeVM newEmployee)
        {
            try
            {
                Insert(Mapper.Map<Employee>(newEmployee));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void DeleteEmployee(int employeeID)
        {
            try
            {
                Delete(employeeID);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<EmployeeVM> GetAllEmployees()
        {
            try
            {
                var employees = FindBy(emplyee => emplyee.IsActive == true);
                return Mapper.Map<List<Employee>, List<EmployeeVM>>(employees);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<EmployeeVM> SearchEmployeeByName(string employeeName)
        {
            try
            {
                var employees = FindBy(employee => employee.IsActive == true && employee.Name.Contains(employeeName));
                return Mapper.Map<List<Employee>, List<EmployeeVM>>(employees);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void UpdateEmployee(EmployeeVM employee)
        {
            try
            {
                var dbEmployee = FindByKey(employee.EmployeeID);
                dbEmployee = Mapper.Map(employee, dbEmployee);
                Update(dbEmployee);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
