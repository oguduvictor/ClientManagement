using System.Collections.Generic;

namespace ClientManagement.Core.Models
{
    public class Employee
    {
        public Employee()
        {
            var Projects = new List<Project>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public int SkillLevel { get; set; }
        public Gender Gender { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}