using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MyTC.Models;

namespace MyTC.Migrations
{
    [DbContext(typeof(MyTCContext))]
    [Migration("20160623151639_ThirdMigration")]
    partial class ThirdMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyTC.Models.Attractions", b =>
                {
                    b.Property<int>("AttractionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country");

                    b.Property<string>("Description");

                    b.Property<int>("GenreId");

                    b.Property<string>("Hours");

                    b.Property<string>("Image");

                    b.Property<string>("Name");

                    b.Property<int>("PostalCode");

                    b.Property<string>("StreetAddress");

                    b.HasKey("AttractionId");

                    b.ToTable("Attractions");
                });

            modelBuilder.Entity("MyTC.Models.HotButtons", b =>
                {
                    b.Property<int>("ButtonId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GenreId");

                    b.Property<string>("Name");

                    b.Property<string>("Translation");

                    b.HasKey("ButtonId");

                    b.ToTable("HotButtons");
                });

            modelBuilder.Entity("MyTC.Models.LocaleGenre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("GenreId");

                    b.ToTable("LocaleGenre");
                });

            modelBuilder.Entity("MyTC.Models.Travelers", b =>
                {
                    b.Property<int>("TravelerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddress");

                    b.Property<string>("HomeAddress");

                    b.Property<string>("Name");

                    b.Property<int>("TravelRating");

                    b.Property<string>("Username");

                    b.HasKey("TravelerId");

                    b.ToTable("Travelers");
                });

            modelBuilder.Entity("MyTC.Models.VisitedAttractions", b =>
                {
                    b.Property<int>("VisitedId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AttractionId");

                    b.Property<int>("AttractionRating");

                    b.Property<string>("Comments");

                    b.Property<int>("TravelerId");

                    b.HasKey("VisitedId");

                    b.ToTable("VisitedAttractions");
                });
        }
    }
}
