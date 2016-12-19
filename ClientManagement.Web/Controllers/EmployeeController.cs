using ClientManagement.Core.Models;
using ClientManagement.Core.Services;
using ClientManagement.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManagement.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: Employee
        public ActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            var employee = _employeeService.GetEmployee(id);
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignProject(int employeeId, [Bind(Include ="Id, Title, Description, ProjectStatus")] Project project)
        {
            _employeeService.AssignProjectToEmployee(employeeId, project.Id);
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="Id,FirstName,LastName,Salary,Gender,SkillLevel,UserId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.Save(employee);

                return RedirectToAction("Details/" + employee.Id);
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Salary,Gender,SkillLevel")] Employee employee)
        {
            try
            {
                _employeeService.Save(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        [Authorize]
        public ActionResult EmployeeProjects(int id)
        {
            var employee = _employeeService.GetEmployee(id);
            var projects = employee.Projects.ToList();
            return View(projects);
        }
    }
}
