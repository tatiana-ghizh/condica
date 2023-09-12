﻿// <auto-generated />
using System;
using CVU.CONDICA.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CVU.CONDICA.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.Blob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BlobType")
                        .HasColumnType("int");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Blobs");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.BlobUser", b =>
                {
                    b.Property<int>("BlobId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BlobId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("BlobUsers");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.CompanyProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDay")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CompanyProjects");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.DepartmentRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DepartmentId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("DepartmentRoleCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("DepartmentRoles");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Idnp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("SecurityCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SecurityCodeExpiresAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.UserCompanyProject", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyProjectId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "CompanyProjectId");

                    b.HasIndex("CompanyProjectId");

                    b.ToTable("UserCompanyProjects");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.UserDepartmentRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentRoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "DepartmentRoleId");

                    b.HasIndex("DepartmentRoleId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserDepartmentRoles");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.Vacation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mentions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfDays")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Vacations");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.BlobUser", b =>
                {
                    b.HasOne("CVU.CONDICA.Persistence.Entities.Blob", "Blob")
                        .WithMany("BlobUsers")
                        .HasForeignKey("BlobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CVU.CONDICA.Persistence.Entities.User", "User")
                        .WithMany("BlobUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blob");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.DepartmentRole", b =>
                {
                    b.HasOne("CVU.CONDICA.Persistence.Entities.Department", "Department")
                        .WithMany("DepartmentRoles")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.UserCompanyProject", b =>
                {
                    b.HasOne("CVU.CONDICA.Persistence.Entities.CompanyProject", "CompanyProject")
                        .WithMany("UserCompanyProjects")
                        .HasForeignKey("CompanyProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CVU.CONDICA.Persistence.Entities.User", "User")
                        .WithMany("UserCompanyProjects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CompanyProject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.UserDepartmentRole", b =>
                {
                    b.HasOne("CVU.CONDICA.Persistence.Entities.DepartmentRole", "DepartmentRole")
                        .WithMany("UserDepartmentRoles")
                        .HasForeignKey("DepartmentRoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CVU.CONDICA.Persistence.Entities.User", "User")
                        .WithOne("UserDepartmentRole")
                        .HasForeignKey("CVU.CONDICA.Persistence.Entities.UserDepartmentRole", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("DepartmentRole");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.Vacation", b =>
                {
                    b.HasOne("CVU.CONDICA.Persistence.Entities.User", "User")
                        .WithMany("Vacations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.Blob", b =>
                {
                    b.Navigation("BlobUsers");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.CompanyProject", b =>
                {
                    b.Navigation("UserCompanyProjects");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.Department", b =>
                {
                    b.Navigation("DepartmentRoles");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.DepartmentRole", b =>
                {
                    b.Navigation("UserDepartmentRoles");
                });

            modelBuilder.Entity("CVU.CONDICA.Persistence.Entities.User", b =>
                {
                    b.Navigation("BlobUsers");

                    b.Navigation("UserCompanyProjects");

                    b.Navigation("UserDepartmentRole")
                        .IsRequired();

                    b.Navigation("Vacations");
                });
#pragma warning restore 612, 618
        }
    }
}
