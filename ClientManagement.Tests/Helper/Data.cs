using ClientManagement.Core.Models;
using System.Collections.Generic;
using System;

namespace ClientManagement.Tests.Core
{
    public class Data
    {
        public static Guid Employee1Id
        {
            get
            {
                return new Guid("2246C4FE-FA5A-4183-A5C3-AEF275F50B38");
            }
        }

        public static Guid Employee2Id
        {
            get
            {
                return new Guid("19BFD2E8-FC1F-49BF-ADAD-E709E236ACDD");
            }
        }

        public static Guid Employee3Id
        {
            get
            {
                return new Guid("EB682C07-FDB2-46BF-8837-2168F5916FE2");
            }
        }

        public static Guid Client1Id
        {
            get
            {
                return new Guid("378B463A-C186-406B-8A1D-18FD941C9CB0");
            }
        }

        public static Guid Client2Id
        {
            get
            {
                return new Guid("5104CEE5-1291-4749-8E31-DEC981DD0E3A");
            }
        }
        
        public static List<Project> Projects
        {
            get
            {
                return new List<Project>
                {
                   new Project { Id = new Guid ("6E1AF9E0-E868-4DEA-9DB6-600960D5DB3A") , Title = "Client Management", Description = "Client management software for consulting firm",
                   Status = ProjectStatus.NotStarted, Client = Clients[0] },
                   new Project { Id = new Guid("AED89B56-9AAC-4EF3-A2DA-9CB386DB5064"), Title = "Health Tutor", Description = "Software for health-related issues",
                   Status = ProjectStatus.InProgress, Client = Clients[1] },
                   new Project { Id = new Guid("CEFA576C-C325-4C9F-AE20-7FAB97E1C319"), Title = "Accounting Software", Description = "Accounting management software for consulting firm",
                   Status = ProjectStatus.InProgress, Client = new Client
                   { Id = new Guid("AED89B56-9AAC-4C9F-AE20-7FAB97E1C319"), Name = "HP", EmailAddress = "hp@hp.com" } }
                };
            }
        }

        public static List<Client> Clients
        {
            get
            {
                return new List<Client>
                {
                    new Client { Id = Client1Id, Name = "NexTekk", EmailAddress = "info@nextekk.com" },
                    new Client { Id = Client2Id, Name = "Microsoft", EmailAddress = "msn@live.com" }
                };
            }
        }

        public static List<Employee> Employees
        {
            get
            {
                return new List<Employee>
                {
                    new Employee { Id = Employee1Id, FirstName = "James", LastName = "Fox", Gender = Gender.Male},
                    new Employee { Id = Employee2Id, FirstName = "Banke", LastName = "Bola", Gender = Gender.Female},
                    new Employee { Id = Employee3Id, FirstName = "Bello", LastName = "Bimbo", Gender = Gender.Male}
                };
            }
        }
    }
}
