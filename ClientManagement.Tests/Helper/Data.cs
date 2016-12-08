using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;

namespace ClientManagement.Tests.Core
{
    public class Data
    {
        public static List<Project> project
        {
            get
            {
                return new List<Project>
                {
                   new Project {Id = 1, Title = "Client Management", Description = "Client management software for consulting firm",
                   ProjectStatus = ProjectStatus.NotStarted},
                   new Project {Id = 2, Title = "Accounting Software", Description = "Accounting management software for consulting firm",
                   ProjectStatus = ProjectStatus.InProgress},
                   new Project {Id = 3, Title = "Hospital Software", Description = "Software to manage hospital database",
                   ProjectStatus = ProjectStatus.Completed}
                };
            }
        }
        public static List<Client> client
        {
            get
            {
                return new List<Client>
                {
                    new Client {Id = Guid.NewGuid(), Name = "Microsoft", Email = "microsoft@live.com"},
                    new Client {Id = Guid.NewGuid(), Name = "Globacom", Email = "glo@gloworld.com"},
                    new Client {Id = Guid.NewGuid(), Name = "Google", Email = "google@google.com"}
                };
            }
        }
        public static Guid User1Id
        {
            get
            {
                return new Guid("ab0db2b1-2b62-4aef-8781-454fe93e7f85");
            }
        }

        public static Guid User2Id
        {
            get
            {
                return new Guid("a40d9803-b4fc-4ddc-9fbb-6dd99c41760f");
            }
        }
        

        public static List<Employee> Employees
        {
            get
            {
                return new List<Employee>
                {
                    new Employee {Id = Guid.NewGuid(), FirstName = "James", LastName = "Fox", Gender = Gender.Male, Salary = 200000, SkillLevel = 3,
                    Projects = new List<Project>
                    {
                        new Project {Id = 5, Title = "List", Description = "Just starting", ProjectStatus = ProjectStatus.InProgress },
                        new Project {Id = 3, Title = "Hospital Software", Description = "Software to manage hospital database",
                   ProjectStatus = ProjectStatus.Completed}
                    }
                    },
                    new Employee {Id = User1Id, FirstName = "hello", LastName = "world", Gender = Gender.Female, Salary = 240900, SkillLevel = 2 },
                    new Employee {Id = User2Id, FirstName = "James", LastName = "Fox", Gender = Gender.Male, Salary = 200000, SkillLevel = 3 }
                };
            }
        }
    }
}
