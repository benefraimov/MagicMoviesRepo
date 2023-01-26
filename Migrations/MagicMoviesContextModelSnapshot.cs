﻿// <auto-generated />
using System;
using MagicMoviesBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MagicMoviesBackend.Migrations
{
    [DbContext(typeof(MagicMoviesContext))]
    partial class MagicMoviesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MagicMoviesBackend.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Genres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Premiered")
                        .HasColumnType("datetime2");

                    b.HasKey("MovieId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MagicMoviesBackend.Models.MovieSubscriber", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("SubscriberId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "SubscriberId");

                    b.HasIndex("SubscriberId");

                    b.ToTable("MovieSubscribers");
                });

            modelBuilder.Entity("MagicMoviesBackend.Models.Permissions", b =>
                {
                    b.Property<int>("PermissionsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CreateMovies")
                        .HasColumnType("bit");

                    b.Property<bool>("CreateSubs")
                        .HasColumnType("bit");

                    b.Property<bool>("DeleteMovies")
                        .HasColumnType("bit");

                    b.Property<bool>("DeleteSubs")
                        .HasColumnType("bit");

                    b.Property<bool>("UpdateMovies")
                        .HasColumnType("bit");

                    b.Property<bool>("UpdateSubs")
                        .HasColumnType("bit");

                    b.Property<bool>("WatchMovies")
                        .HasColumnType("bit");

                    b.Property<bool>("WatchSubs")
                        .HasColumnType("bit");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("PermissionsId");

                    b.HasIndex("WorkerId")
                        .IsUnique();

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("MagicMoviesBackend.Models.Subscriber", b =>
                {
                    b.Property<int>("SubscriberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubscriberId");

                    b.ToTable("Subscribers");
                });

            modelBuilder.Entity("MagicMoviesBackend.Models.Worker", b =>
                {
                    b.Property<int>("WorkerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkerId");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("MagicMoviesBackend.Models.MovieSubscriber", b =>
                {
                    b.HasOne("MagicMoviesBackend.Models.Movie", "Movie")
                        .WithMany("MovieSubscribers")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MagicMoviesBackend.Models.Subscriber", "Subscriber")
                        .WithMany("MovieSubscribers")
                        .HasForeignKey("SubscriberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MagicMoviesBackend.Models.Permissions", b =>
                {
                    b.HasOne("MagicMoviesBackend.Models.Worker", "Worker")
                        .WithOne("Permissions")
                        .HasForeignKey("MagicMoviesBackend.Models.Permissions", "WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
