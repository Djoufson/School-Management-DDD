﻿// <auto-generated />
using System;
using Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("Api.Domain.AcademicAggregate.Entities.Discipline", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SemesterId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SemesterId");

                    b.HasIndex("Title");

                    b.ToTable("Disciplines", (string)null);
                });

            modelBuilder.Entity("Api.Domain.AcademicAggregate.Entities.Semester", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("SemesterNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Semester");
                });

            modelBuilder.Entity("Api.Domain.SchoolAggregate.Entities.SchoolClass", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("AdminId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Specialization")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TeacherAdvisorId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("Specialization");

                    b.HasIndex("TeacherAdvisorId");

                    b.HasIndex("Year");

                    b.ToTable("SchoolClass");
                });

            modelBuilder.Entity("Api.Domain.SchoolAggregate.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SchoolClassStudent", b =>
                {
                    b.Property<string>("ClassesId")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentsId")
                        .HasColumnType("TEXT");

                    b.HasKey("ClassesId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("SchoolClassStudent");
                });

            modelBuilder.Entity("Api.Domain.SchoolAggregate.Entities.Admin", b =>
                {
                    b.HasBaseType("Api.Domain.SchoolAggregate.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("Api.Domain.SchoolAggregate.Entities.Student", b =>
                {
                    b.HasBaseType("Api.Domain.SchoolAggregate.User");

                    b.Property<string>("AdminId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Specialization")
                        .HasColumnType("INTEGER");

                    b.HasIndex("AdminId");

                    b.ToTable("Users", t =>
                        {
                            t.Property("AdminId")
                                .HasColumnName("Student_AdminId");
                        });

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("Api.Domain.SchoolAggregate.Entities.TeacherAdvisor", b =>
                {
                    b.HasBaseType("Api.Domain.SchoolAggregate.User");

                    b.Property<string>("AdminId")
                        .HasColumnType("TEXT");

                    b.HasIndex("AdminId");

                    b.HasDiscriminator().HasValue("TeacherAdvisor");
                });

            modelBuilder.Entity("Api.Domain.AcademicAggregate.Entities.Discipline", b =>
                {
                    b.HasOne("Api.Domain.AcademicAggregate.Entities.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("Api.Domain.SchoolAggregate.Entities.SchoolClass", b =>
                {
                    b.HasOne("Api.Domain.SchoolAggregate.Entities.Admin", null)
                        .WithMany("Classes")
                        .HasForeignKey("AdminId");

                    b.HasOne("Api.Domain.SchoolAggregate.Entities.TeacherAdvisor", "TeacherAdvisor")
                        .WithMany("Classes")
                        .HasForeignKey("TeacherAdvisorId");

                    b.Navigation("TeacherAdvisor");
                });

            modelBuilder.Entity("SchoolClassStudent", b =>
                {
                    b.HasOne("Api.Domain.SchoolAggregate.Entities.SchoolClass", null)
                        .WithMany()
                        .HasForeignKey("ClassesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Domain.SchoolAggregate.Entities.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Domain.SchoolAggregate.Entities.Student", b =>
                {
                    b.HasOne("Api.Domain.SchoolAggregate.Entities.Admin", null)
                        .WithMany("Students")
                        .HasForeignKey("AdminId");

                    b.OwnsMany("Api.Domain.AcademicAggregate.Entities.Notation", "Notations", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("TEXT");

                            b1.Property<string>("StudentId")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("SubjectId")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<decimal?>("Value")
                                .HasColumnType("TEXT");

                            b1.HasKey("Id");

                            b1.HasIndex("StudentId");

                            b1.HasIndex("SubjectId");

                            b1.ToTable("Notations", (string)null);

                            b1.WithOwner("Student")
                                .HasForeignKey("StudentId");

                            b1.HasOne("Api.Domain.AcademicAggregate.Entities.Discipline", "Subject")
                                .WithMany()
                                .HasForeignKey("SubjectId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.Navigation("Student");

                            b1.Navigation("Subject");
                        });

                    b.Navigation("Notations");
                });

            modelBuilder.Entity("Api.Domain.SchoolAggregate.Entities.TeacherAdvisor", b =>
                {
                    b.HasOne("Api.Domain.SchoolAggregate.Entities.Admin", null)
                        .WithMany("Teachers")
                        .HasForeignKey("AdminId");
                });

            modelBuilder.Entity("Api.Domain.SchoolAggregate.Entities.Admin", b =>
                {
                    b.Navigation("Classes");

                    b.Navigation("Students");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("Api.Domain.SchoolAggregate.Entities.TeacherAdvisor", b =>
                {
                    b.Navigation("Classes");
                });
#pragma warning restore 612, 618
        }
    }
}
