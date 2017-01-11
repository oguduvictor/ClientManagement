using System;
using System.Collections.Generic;

namespace ClientManagement.Core.Models
{
    public class Project
    {
        public Project()
        {
            Employees = new List<Employee>();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ClientId { get; set; }
        public virtual ProjectStatus ProjectStatus { get; set; }
        public Client Client { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}