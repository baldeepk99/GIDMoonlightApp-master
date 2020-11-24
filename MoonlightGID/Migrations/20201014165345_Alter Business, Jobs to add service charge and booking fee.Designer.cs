﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoonlightGID.Models;

namespace MoonlightGID.Migrations
{
    [DbContext(typeof(MoonLightContext))]
    [Migration("20201014165345_Alter Business, Jobs to add service charge and booking fee")]
    partial class AlterBusinessJobstoaddservicechargeandbookingfee
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MoonlightGID.Models.Businesses", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyLogin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfficeHours")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("date");

                    b.Property<string>("WorkingDays")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("MoonlightGID.Models.Customers", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("CityAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserLogin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MoonlightGID.Models.Jobs", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BookingFee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceCharge")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("JobId")
                        .HasName("PK__Jobs__056690C2EB07A580");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("MoonlightGID.Models.Reviews", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ReviewID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnName("CompanyID")
                        .HasColumnType("int");

                    b.Property<int>("JobId")
                        .HasColumnName("JobID")
                        .HasColumnType("int");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("ReviewContent")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.HasKey("ReviewId");

                    b.HasIndex("CompanyId")
                        .HasName("FK_Company");

                    b.HasIndex("JobId")
                        .HasName("FK_Jobs");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("MoonlightGID.Models.Services", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOrder")
                        .HasColumnType("datetime2");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ServiceId")
                        .HasName("PK__Services__C51BB00AA0F3198A");

                    b.HasIndex("CustomerId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("MoonlightGID.Models.Jobs", b =>
                {
                    b.HasOne("MoonlightGID.Models.Businesses", "Company")
                        .WithMany("Jobs")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("FK_Jobs_Businesses")
                        .IsRequired();

                    b.HasOne("MoonlightGID.Models.Services", "Service")
                        .WithMany("Jobs")
                        .HasForeignKey("ServiceId");
                });

            modelBuilder.Entity("MoonlightGID.Models.Reviews", b =>
                {
                    b.HasOne("MoonlightGID.Models.Businesses", "Company")
                        .WithMany("Reviews")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("FK_BusinessesID")
                        .IsRequired();

                    b.HasOne("MoonlightGID.Models.Jobs", "Job")
                        .WithMany("Reviews")
                        .HasForeignKey("JobId")
                        .HasConstraintName("FK_Job")
                        .IsRequired();
                });

            modelBuilder.Entity("MoonlightGID.Models.Services", b =>
                {
                    b.HasOne("MoonlightGID.Models.Customers", "Customer")
                        .WithMany("Services")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Services_Customers")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
