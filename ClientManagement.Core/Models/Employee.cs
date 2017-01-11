using System;
using System.Collections.Generic;

namespace ClientManagement.Core.Models
{
    public class Employee
    {
        public Employee()
        {
            Projects = new List<Project>();
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}