using System.Linq;
using System.Net;
using System.Web.Mvc;
using ClientManagement.Core.Models;
using ClientManagement.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace ClientManagement.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IClientService _clientService;
       
        public ProjectController(IProjectService projectService, IClientService clientService)
        {
            _projectService = projectService;
            _clientService = clientService;
        }
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: Project
        public async Task<ActionResult> Index()
        {
            var projects = await _projectService.GetAllProjects();
            return View(projects);
        }

        // GET: Project/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = await _projectService.GetProject(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var clients = await _clientService.GetAllClients();
            ViewBag.ClientName = clients.FirstOrDefault(x => x.Id == project.ClientId).Name;
            return View(project);
        }

        public async Task<ActionResult> ProjectEmployees(Guid id)
        {
            var project = await _projectService.GetProject(id);
            var projectEmployees = await _projectService.GetEmployeeListForProject(id);
            ViewBag.Project = project.Title;
            return View(projectEmployees);
        }

        // GET: Project/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.ClientId = new SelectList(await _clientService.GetAllClients(), "Id", "Name");
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                project.Id = Guid.NewGuid();

                await _projectService.Save(project);

                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(await _clientService.GetAllClients(), "Id", "Name", project.ClientId);

            return View(project);
        }

        // GET: Project/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = await _projectService.GetProject(id);

            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(await _clientService.GetAllClients(), "Id", "Name", project.ClientId);
            return View(project);
        }
        // POST: Project/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Title,Description,ClientId,ProjectStatus")] Project project)
        {
            if (ModelState.IsValid)
            {
                await _projectService.Save(project);
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(await _clientService.GetAllClients(), "Id", "Name", project.ClientId);
            return View(project);
        }

        // GET: Project/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = await _projectService.GetProject(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await _projectService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}