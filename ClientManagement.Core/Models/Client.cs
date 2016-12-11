using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientManagement.Core.Models
{
    public class Client
    {
        public Client()
        {
            Projects = new List<Project>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None) ]
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}