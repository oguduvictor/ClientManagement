using ClientManagement.Core.Interfaces;
using ClientManagement.Web.App_Start;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ClientManagement.Web.Helpers
{
    public class ViewHelper
    {
        public static IEnumerable<SelectListItem> GetProjectsList()
        {
            var service = GetProjectService();
            var options = new List<SelectListItem>();
            var task = service.GetAllProjects(true);
            task.Wait();
            
            foreach (var item in task.Result)
            {
                options.Add(new SelectListItem { Text = item.Title, Value = item.Id.ToString() });
            }

            return options;
        }

        public static IEnumerable<SelectListItem> GetEmployeesList()
        {
            var service = GetEmployeeService();
            var options = new List<SelectListItem>();
            var task = service.GetAllEmployees(true);
            task.Wait();

            foreach (var item in task.Result)
            {
                options.Add(new SelectListItem { Text = item.FullName, Value = item.Id.ToString() });
            }

            return options;
        }

        private static IProjectService GetProjectService()
        {
            return IoC.GetInstance<IProjectService>();
        }

        private static IEmployeeService GetEmployeeService()
        {
            return IoC.GetInstance<IEmployeeService>();
        }
    }
}