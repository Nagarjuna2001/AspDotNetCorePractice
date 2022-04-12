using EmployeeCRUDWebApplicationWithEFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FetchingEmployeeDataWebApplication.ViewModels
{
    public class HomeDetailsViewModel
    {
        public Employee Employee { get; set; }

        public string PageTitle { get; set; }

    }
}
