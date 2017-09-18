using ClientManagement.Core.Models;
using ClientManagement.Core.Interfaces;
using ClientManagement.Web.Models;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using static ClientManagement.Core.Constants.RolesConstants;

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

        [Authorize(Roles = Manager)]
        public async Task<ActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployees();

            return View(employees);
        }

        // GET: Employee/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                employee = await _employeeService.GetEmployee(id);
            }
            if (employee == null)
            {
                return RedirectToAction("Create", "Employee");
            }

            return View(employee);
        }

        [HttpGet]
        [Authorize(Roles = Manager)]
        public async Task<ActionResult> AssignProject(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            employee = await _employeeService.GetEmployee(id);
            if (employee == null)
            {
                throw new Exception("Employee does not Exist!");
            }

            var projects = await _projectService.GetAllProjects();

            ViewBag.Employee = employee;
            ViewBag.ProjectId = new SelectList(projects, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Manager)]
        public async Task<ActionResult> AssignProject(EmployeeProject employeeProject)
        {
            if (ModelState.IsValid)
            {
                var employeeId = employeeProject.EmployeeId;
                var projectId = employeeProject.ProjectId;
                await _employeeService.AssignProjectToEmployee(employeeId, projectId);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = Manager)]
        public async Task<ActionResult> RemoveProject(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee = await _employeeService.GetEmployee(id);
            if (employee == null)
            {
                throw new Exception("Employee does not Exist!");
            }
            ViewBag.Employee = employee;
            var projects = (await _employeeService.GetEmployee(id)).Projects.ToList();
            ViewBag.ProjectId = new SelectList(projects, "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Manager)]
        public async Task<ActionResult> RemoveProject(EmployeeProject employeeProject)
        {
            var employeeId = employeeProject.EmployeeId;
            var projectId = employeeProject.ProjectId;
            await _employeeService.RemoveProjectFromEmployee(employeeId, projectId);
            return RedirectToAction("Index");
        }

        // GET: Employee/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee = await _employeeService.GetEmployee(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Salary,Gender,SkillLevel")] Employee employee)
        {
            try
            {
                await _employeeService.Save(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> EmployeeProjects(Guid id)
        {
            employee = await _employeeService.GetEmployee(id);
            if (employee == null)
            {
                throw new Exception("Employee does not Exist!");
            }
            var projects = employee.Projects.ToList();
            return View(projects);
        }
    }
}
