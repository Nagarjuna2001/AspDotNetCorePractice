﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchingEmployeeDataWebApplication.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> employeeList;
        public MockEmployeeRepository()
        {
            employeeList = new List<Employee>
            {
                new Employee(){Id = 1, Name = "Mary", Department = Dept.HR, Email = "mary@gmail.com" },
                new Employee(){Id = 2, Name = "Steve", Department = Dept.IT, Email = "steve@gmail.com" },
                new Employee(){Id = 3, Name = "Bob", Department = Dept.IT, Email = "bob@gmail.com" },
            };
        }
        public Employee GetEmployee(int Id)
        {
            return employeeList.FirstOrDefault(employee => employee.Id == Id);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return employeeList;
        }

        public Employee AddEmployee(Employee employee)
        {
            employee.Id = employeeList.Max(employee => employee.Id) + 1;
            employeeList.Add(employee);
            return employee;
        }
    }
}
