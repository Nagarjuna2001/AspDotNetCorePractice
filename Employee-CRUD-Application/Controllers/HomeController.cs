using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Employee_CRUD_Application.Models;
using Employee_CRUD_Application.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Employee_CRUD_Application.Controllers
{
    //[Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository,IWebHostEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        /*[Route("~/")]
        [Route("[action]")]*/
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }

        /*[Route("[action]")]
        [Route("[action]/{id?}")]*/
        public ViewResult Details(int? id)
        {
            //throw new Exception("hi");
            //Employee model = _employeeRepository.GetEmployee(1);

            //Normal way of sending the data to view
            /*return View(model);*/

            //Passing data to the view using ViewData
            /*ViewData["Employee"] = model;
            ViewData["PageTitle"] = "Employee Details";
            return View();*/

            //Passing data to the view using viewBag
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

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int Id)
        {
            Employee employee = _employeeRepository.GetEmployee(Id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath

            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            //if block condition is used to check if the validation and mapping to employee parameter is successfull
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Photo != null)
                {
                    if(model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);
                }
                Employee updatedEmployee = _employeeRepository.Update(employee);
                return RedirectToAction("index");
            }
            return View(model);
            
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            //if block condition is used to check if the validation and mapping to employee parameter is successfull
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };

                _employeeRepository.AddEmployee(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }


            //if validation not successful then it redirects the same create view to allow us to create new employee
            return View();
        }
        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
