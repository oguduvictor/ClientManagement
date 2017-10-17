using System.Net;
using System.Web.Mvc;
using ClientManagement.Core.Models;
using System;
using ClientManagement.Core.Interfaces;
using System.Threading.Tasks;

namespace ClientManagement.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        // GET: Client
        public async Task<ActionResult> Index()
        {
            var clients = await _clientService.GetAllClients();
            return View(clients);
        }

        // GET: Client/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await _clientService.GetClient(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        public async Task<ActionResult> ClientProjects(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projects = await _clientService.GetClientProjects(id);
            ViewBag.ClientName = (await _clientService.GetClient(id)).Name;
            return View(projects);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,EmailAddress")] Client client)
        {
            if (ModelState.IsValid)
            {
                await _clientService.SaveClient(client);
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Client/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = await _clientService.GetClient(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Client/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,EmailAddress")] Client client)
        {
            if (ModelState.IsValid)
            {
                await _clientService.SaveClient(client);
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Client/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = await _clientService.GetClient(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await _clientService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
