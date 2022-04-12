using Employee_CRUD_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_CRUD_Application.ViewModels
{
    public class HomeDetailsViewModel
    {
        public Employee Employee { get; set; }

        public string PageTitle { get; set; }

    }
}
