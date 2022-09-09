﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using certificationRegister.Models;

namespace certificationRegister.Migrations
{
    [DbContext(typeof(AppDbcontext))]
    partial class AppDbcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("certificationRegister.Models.Certification", b =>
                {
                    b.Property<int>("certificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("libelle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("sigle")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.HasKey("certificationId");

                    b.ToTable("certifications");
                });

            modelBuilder.Entity("certificationRegister.Models.Student", b =>
                {
                    b.Property<int>("studentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("studentId");

                    b.ToTable("students");
                });

            modelBuilder.Entity("certificationRegister.Models.StudentCertification", b =>
                {
                    b.Property<int>("StudId")
                        .HasColumnType("int");

                    b.Property<int>("CertId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCertified")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("certificationId")
                        .HasColumnType("int");

                    b.Property<int?>("studentId")
                        .HasColumnType("int");

                    b.HasKey("StudId", "CertId");

                    b.HasIndex("certificationId");

                    b.HasIndex("studentId");

                    b.ToTable("StudentCertification");
                });

            modelBuilder.Entity("certificationRegister.Models.StudentCertification", b =>
                {
                    b.HasOne("certificationRegister.Models.Certification", "Certification")
                        .WithMany("StudentsLink")
                        .HasForeignKey("certificationId");

                    b.HasOne("certificationRegister.Models.Student", "Student")
                        .WithMany("CertificationsLink")
                        .HasForeignKey("studentId");

                    b.Navigation("Certification");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("certificationRegister.Models.Certification", b =>
                {
                    b.Navigation("StudentsLink");
                });

            modelBuilder.Entity("certificationRegister.Models.Student", b =>
                {
                    b.Navigation("CertificationsLink");
                });
#pragma warning restore 612, 618
        }
    }
}
