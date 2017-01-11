using ClientManagement.Core.Models;
using ClientManagement.Core.Services;
using ClientManagement.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ClientManagement.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IProjectService _projectService;
        private Employee employee;
        public EmployeeController(IEmployeeService employeeService, IProjectService projectService)
        {
            _employeeService = employeeService;
            _projectService = projectService;
        }
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        // GET: Employee
        public ActionResult Index()
        {
            if (User.IsInRole("Manager"))
            {
                var employees = _employeeService.GetAllEmployees();
                return View(employees);
            }
            else
            {
                employee = _employeeService.GetAllEmployees().FirstOrDefault(x => x.UserId == User.Identity.GetUserId());
                if (employee == null)
                    return RedirectToAction("Create", "Employee");

                var employeeId = employee.Id;
                return RedirectToAction("EmployeeProjects/" + employeeId, "Employee");
            }
        }

        // GET: Employee/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                employee = _employeeService.GetAllEmployees().Find(x => x.UserId == User.Identity.GetUserId());
            }
            if (employee == null)
            {
                return RedirectToAction("Create", "Employee");
            }
            ViewBag.Employee = User.Identity.GetUserName();
            return View(employee);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult AssignProject(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            employee = _employeeService.GetEmployee(id);
            if (employee == null)
            {
                throw new Exception("Employee does not Exist!");
            }

            var projects = _projectService.GetAllProjects();

            ViewBag.Employee = employee;
            ViewBag.ProjectId = new SelectList(projects, "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult AssignProject(EmployeeProject employeeProject)
        {
            if (ModelState.IsValid)
            {
                var employeeId = employeeProject.EmployeeId;
                var projectId = employeeProject.ProjectId;
                _employeeService.AssignProjectToEmployee(employeeId, projectId);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult RemoveProject(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee = _employeeService.GetEmployee(id);
            if (employee == null)
            {
                throw new Exception("Employee does not Exist!");
            }
            ViewBag.Employee = employee;
            var projects = _employeeService.GetEmployee(id).Projects.ToList();
            ViewBag.ProjectId = new SelectList(projects, "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult RemoveProject(EmployeeProject employeeProject)
        {
            var employeeId = employeeProject.EmployeeId;
            var projectId = employeeProject.ProjectId;
            _employeeService.RemoveProjectFromEmployee(employeeId, projectId);
            return RedirectToAction("Index");
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
                employee.Id = Guid.NewGuid();
                _employeeService.Save(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee = _employeeService.GetEmployee(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
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
        
        public ActionResult EmployeeProjects(Guid id)
        {
            employee = _employeeService.GetEmployee(id);
            if (employee == null)
            {
                throw new Exception("Employee does not Exist!");
            }
            var projects = employee.Projects.ToList();
            return View(projects);
        }
    }
}
