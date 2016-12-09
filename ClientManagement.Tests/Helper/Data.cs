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
                   new Project { Title = "Client Management", Description = "Client management software for consulting firm",
                   ProjectStatus = ProjectStatus.NotStarted,
                       Employees = {Employees[0], Employees[1]}
                       },
                   new Project { Title = "Accounting Software", Description = "Accounting management software for consulting firm",
                   ProjectStatus = ProjectStatus.InProgress, Employees = { Employees[2], Employees[0] } },
                   new Project { Title = "Hospital Software", Description = "Software to manage hospital database",
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
                    new Client { Id = 15, Name = "Microsoft", EmailAddress = "microsoft@live.com", Projects = { project[0] } },
                    new Client { Id = 21, Name = "Globacom", EmailAddress = "glo@gloworld.com", Projects = { project[1], project[0] }},
                    new Client { Id = 30, Name = "Youtube", EmailAddress = "youtube@google.com", Projects = { project[1], project[2] } }
                };
            }
        }

        public static List<Employee> Employees
        {
            get
            {
                return new List<Employee>
                {
                    new Employee { Id = 21, FirstName = "James", LastName = "Fox", Gender = Gender.Male, Salary = 200000, SkillLevel = 3 },
                    new Employee { Id = 30, FirstName = "Banke", LastName = "Lola", Gender = Gender.Female, Salary = 240900, SkillLevel = 2 },
                    new Employee { Id = 10, FirstName = "John", LastName = "Gates", Gender = Gender.Male, Salary = 902000, SkillLevel = 7 }
                };
            }
        }
    }
}
