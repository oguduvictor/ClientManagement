using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientManagement.Core.Models
{
    public class Project
    {
        public Project()
        {
            Employees = new List<Employee>();
        }
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public virtual ProjectStatus Status { get; set; }

        public Client Client { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}