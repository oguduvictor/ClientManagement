using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.Core.Models
{
    public class Client
    {
        public Client()
        {
            Projects = new List<Project>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}