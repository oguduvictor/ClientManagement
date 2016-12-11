using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientManagement.Core.Models
{
    public class Project
    {
        public Project()
        {
            Employees = new List<Employee>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public virtual ProjectStatus ProjectStatus { get; set; }
        public Client Client { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}