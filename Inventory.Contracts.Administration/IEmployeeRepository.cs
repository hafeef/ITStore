using Inventory.ViewModels.Administration;
using System.Collections.Generic;

namespace Inventory.Contracts.Administration
{
    public interface IEmployeeRepository
    {
        List<EmployeeVM> GetAllEmployees();
        void CreateEmployee(EmployeeVM newEmployee);
        void DeleteEmployee(int employeeID);
        List<EmployeeVM> SearchEmployeeByName(string employeeName);
        void UpdateEmployee(EmployeeVM employee);
    }
}
