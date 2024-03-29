﻿// <auto-generated />
using GraphQLOne.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GraphQLOne.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("GraphQLOne.Models.Command", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CommandLine")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("HowTo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PlateformID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlateformID");

                    b.ToTable("Commands");
                });

            modelBuilder.Entity("GraphQLOne.Models.Platform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LicenseKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("GraphQLOne.Models.Command", b =>
                {
                    b.HasOne("GraphQLOne.Models.Platform", "Platform")
                        .WithMany("Commands")
                        .HasForeignKey("PlateformID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("GraphQLOne.Models.Platform", b =>
                {
                    b.Navigation("Commands");
                });
#pragma warning restore 612, 618
        }
    }
}
