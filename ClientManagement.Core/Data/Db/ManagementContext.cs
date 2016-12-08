namespace ClientManagement.Core.Data.Db
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;

    public partial class ManagementContext : DbContext
    {
        public ManagementContext()
            : base("name=ClientsManagementDB")
        {
        }

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(19, 2);


            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Projects)
                .WithMany(e => e.Employees)
                .Map(m => m.ToTable("EmployeeProject").MapLeftKey("EmployeeId").MapRightKey("ProjectId"));
        }
    }
}
