using ClientManagement.Core.Models;
using ClientManagement.Core.Services;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
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
        public EmployeeController(IEmployeeService employeeService, IProjectService projectService)
        {
            _employeeService = employeeService;
            _projectService = projectService;
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
                var employeeId = _employeeService.GetAllEmployees().Find(x => x.UserId == User.Identity.GetUserId()).Id;
                return RedirectToAction("EmployeeProjects/" + employeeId, "Employee");
            }
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                id = _employeeService.GetAllEmployees().Find(x => x.UserId == User.Identity.GetUserId()).Id;
            }
            var employee = _employeeService.GetEmployee(id.Value);
            ViewBag.Employee = User.Identity.GetUserName();
            return View(employee);
        }

        public ActionResult AssignProject(int? id)
        {
            if (id == null)
            {
                id = _employeeService.GetAllEmployees().Find(x => x.UserId == User.Identity.GetUserId()).Id;
            }
            var projects = _projectService.GetAllProjects();
            var employee = _employeeService.GetEmployee(id.Value);
            ViewBag.EmployeeName = string.Format("{0} {1}", employee.FirstName,employee.LastName);
            ViewBag.EmployeId = id.Value;
            ViewBag.Projects = new SelectList(projects, "Id", "Title");
            return View(projects);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignProject(int? employeeId, int? projectId)
        {
            if (employeeId == null)
            {
                employeeId = _employeeService.GetAllEmployees().Find(x => x.UserId == User.Identity.GetUserId()).Id;
            }
            if(projectId == null)
            {
                var projects = _projectService.GetAllProjects();
            }
            _employeeService.AssignProjectToEmployee(employeeId.Value, projectId.Value);

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
                employee.Projects = new List<Project>();
                _employeeService.Save(employee);

                return RedirectToAction("Details/" + employee.Id);
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = _employeeService.GetEmployee(id.Value);
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
        
        [Authorize]
        public ActionResult EmployeeProjects(int id)
        {
            var employee = _employeeService.GetEmployee(id);
            var projects = employee.Projects.ToList();
            return View(projects);
        }
    }
}
