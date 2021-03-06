using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD_Application.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int Id);
        IEnumerable<Employee> GetAllEmployees();

        Employee AddEmployee(Employee employee);

        Employee Update(Employee employeeChanges);

        Employee Delete(int Id);
    }
}
