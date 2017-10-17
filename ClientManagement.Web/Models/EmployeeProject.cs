using System;

namespace ClientManagement.Web.Models
{
    public class EmployeeProject
    {
        public string Employee { get; set; }

        public Guid EmployeeId { get; set; }
        public Guid ProjectId { get; set; }
    }
}