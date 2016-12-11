using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientManagement.Core.Models
{
    public class Employee
    {
        public Employee()
        {
            var Projects = new List<Project>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None) ]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public int SkillLevel { get; set; }
        public Gender Gender { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}