using Aptitude.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aptitude.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Question> Question { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<CV> CV { get; set; }
        public DbSet<HR> HR { get; set; }
        public DbSet<Maintance> Maintance { get; set; }
        public DbSet<Exam> Exam { get; set; }
    }
}
