using System.Collections.Generic;

namespace ClientManagement.Core.Models
{
    public class Client
    {
        public Client()
        {
            Projects = new List<Project>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}