using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.Core.Models
{
    public class Employee
    {
        public Employee()
        {
            var Projects = new List<Project>();
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public int SkillLevel { get; set; }
        public Gender Gender { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}