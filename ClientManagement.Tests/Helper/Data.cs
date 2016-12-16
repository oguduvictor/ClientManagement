using ClientManagement.Core.Models;
using System.Collections.Generic;

namespace ClientManagement.Tests.Core
{
    public class Data
    {
        public static List<Project> projects
        {
            get
            {
                return new List<Project>
                {
                   new Project { Id = 12, Title = "Client Management", Description = "Client management software for consulting firm",
                   ProjectStatus = ProjectStatus.NotStarted},
                   new Project { Id = 11, Title = "Health Tutor", Description = "Software for health-related issues",
                   ProjectStatus = ProjectStatus.InProgress},
                   new Project { Id = 10, Title = "Accounting Software", Description = "Accounting management software for consulting firm",
                   ProjectStatus = ProjectStatus.InProgress, Employees = new List<Employee> { Employees[0], Employees[2] }
                       }
                };
            }
        }
        public static List<Project> project
        {
            get
            {
                return new List<Project>
                {
                   new Project { Id = 12, Title = "Client Management", Description = "Client management software for consulting firm",
                   ProjectStatus = ProjectStatus.NotStarted,
                       Client = new Client { Id = 21, Name = "Globacom", EmailAddress = "glo@gloworld.com"} },
                   new Project { Id = 10, Title = "Accounting Software", Description = "Accounting management software for consulting firm",
                   ProjectStatus = ProjectStatus.InProgress, Client = new Client {Id = 22, Name = "Microsoft", EmailAddress = "msn@live.com" } }
                };
            }
        }
        public static List<Client> clients
        {
            get
            {
                return new List<Client>
                {
                    new Client { Id = 1, Name = "Microsoft", EmailAddress = "msn@live.com", Projects = new List<Project>
                      {
                        new Project {Id = 1, Title= "Time tracking app", Description = "Time track payment", ProjectStatus = ProjectStatus.InProgress },
                        new Project {Id = 2, Title= "Calculator app", Description = "Calculate anything", ProjectStatus = ProjectStatus.Completed }
                      }
                    }
                };
            }
        }

        public static List<Employee> Employees
        {
            get
            {
                return new List<Employee>
                {
                    new Employee { Id = 30, FirstName = "James", LastName = "Fox", Gender = Gender.Male, Salary = 200000, SkillLevel = 3
                    , Projects = new List<Project>() { } },
                    new Employee { Id = 31, FirstName = "Banke", LastName = "Bola", Gender = Gender.Female, Salary = 349400, SkillLevel = 2,
                        Projects = new List<Project> { project[0], project[1] },
                    },
                    new Employee { Id = 20, FirstName = "Bello", LastName = "Bimbo", Gender = Gender.Male, Salary = 524900, SkillLevel = 5,
                        Projects = new List<Project>() }
                };
            }
        }
    }
}
