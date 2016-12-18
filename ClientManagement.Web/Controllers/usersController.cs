using ClientManagement.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;

namespace ClientManagement.Web.Controllers
{
    [Authorize]
    public class usersController : Controller
    {
        // GET: users
        public ActionResult Index()
        {
            if (User.IsInRole("Manager"))
            {
                ViewBag.displayMenu = "Yes";
                return RedirectToAction("Index", "Employee");
            }
            if (User.IsInRole("Employee"))
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";
                return RedirectToAction("Create", "Employee");
            }
            else
            {
                ViewBag.Name = "You Are Not Logged IN";
            }
            return View();
        }

        public bool isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var userRole = UserManager.GetRoles(user.GetUserId());
                return (userRole[0].ToString() == "Manager");
            }
            return false;
        }
    }
}
