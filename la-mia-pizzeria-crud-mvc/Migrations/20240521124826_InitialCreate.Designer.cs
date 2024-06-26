﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using la_mia_pizzeria_crud_mvc.Context;

#nullable disable

namespace la_mia_pizzeria_crud_mvc.Migrations
{
    [DbContext(typeof(PizzaContext))]
    [Migration("20240521124826_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("IngredientPizza", b =>
                {
                    b.Property<int>("Ingredientsid")
                        .HasColumnType("int");

                    b.Property<int>("Pizzasid")
                        .HasColumnType("int");

                    b.HasKey("Ingredientsid", "Pizzasid");

                    b.HasIndex("Pizzasid");

                    b.ToTable("IngredientPizza");
                });

            modelBuilder.Entity("la_mia_pizzeria_crud_mvc.Models.Category", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("la_mia_pizzeria_crud_mvc.Models.Ingredient", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Quantita")
                        .HasColumnType("real");

                    b.HasKey("id");

                    b.ToTable("Ingrediente");
                });

            modelBuilder.Entity("la_mia_pizzeria_crud_mvc.Models.Pizza", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("Categoryid")
                        .HasColumnType("int");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Foto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Prezzo")
                        .HasColumnType("real");

                    b.HasKey("id");

                    b.HasIndex("Categoryid");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("Pizza");
                });

            modelBuilder.Entity("IngredientPizza", b =>
                {
                    b.HasOne("la_mia_pizzeria_crud_mvc.Models.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("Ingredientsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("la_mia_pizzeria_crud_mvc.Models.Pizza", null)
                        .WithMany()
                        .HasForeignKey("Pizzasid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("la_mia_pizzeria_crud_mvc.Models.Pizza", b =>
                {
                    b.HasOne("la_mia_pizzeria_crud_mvc.Models.Category", "Category")
                        .WithMany("Pizzas")
                        .HasForeignKey("Categoryid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("la_mia_pizzeria_crud_mvc.Models.Category", b =>
                {
                    b.Navigation("Pizzas");
                });
#pragma warning restore 612, 618
        }
    }
}
