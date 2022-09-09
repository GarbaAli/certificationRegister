using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace certificationRegister.Models
{
    public class AppDbcontext: DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCertification>()
                .HasKey(x => new { x.StudId, x.CertId });
        }

        public DbSet<Certification> certifications { get; set; }
        public DbSet<Student> students { get; set; }
    }
}
