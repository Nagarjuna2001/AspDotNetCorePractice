using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using FetchingEmployeeDataWebApplication.Models;
using FetchingEmployeeDataWebApplication.ViewModels;

namespace FetchingEmployeeDataWebApplication.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [Route("~/")]
        [Route("[action]")]

        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }

        [Route("[action]")]
        [Route("[action]/{id?}")]
        public ViewResult Details(int? id)
        {
            //Employee model = _employeeRepository.GetEmployee(1);

            //Normal way of sending the data to view
            /*return View(model);*/

            //Passing data to the view using ViewData
            /*ViewData["Employee"] = model;
            ViewData["PageTitle"] = "Employee Details";
            return View();*/

            //Passing data to the view using ViewData
            /* ViewBag.Employee = model;
             ViewBag.PageTitle = "Employee Details";
             return View();*/

            //Passing data using Strongly typed view
            /*ViewBag.PageTitle = "Employee Details";
            return View(model);*/

            //Passing data using ViewModel - differnt types od data can be wrapped and sent together using view model
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel
            {
                Employee = _employeeRepository.GetEmployee(id ?? 1),
                PageTitle = "Details Of Employee"
            };
            return View(homeDetailsViewModel);
        }
    }
}
