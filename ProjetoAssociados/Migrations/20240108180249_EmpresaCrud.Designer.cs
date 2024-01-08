﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoAssociados.Data;

#nullable disable

namespace ProjetoAssociados.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240108180249_EmpresaCrud")]
    partial class EmpresaCrud
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AssociadoModelEmpresaModel", b =>
                {
                    b.Property<int>("AssociadosId")
                        .HasColumnType("int");

                    b.Property<int>("EmpresasId")
                        .HasColumnType("int");

                    b.HasKey("AssociadosId", "EmpresasId");

                    b.HasIndex("EmpresasId");

                    b.ToTable("AssociadoModelEmpresaModel");
                });

            modelBuilder.Entity("ProjetoAssociados.Models.AssociadoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime>("DtNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.ToTable("Associados");
                });

            modelBuilder.Entity("ProjetoAssociados.Models.AssociadoModelEmpresaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AssociadoId")
                        .HasColumnType("int");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssociadoId");

                    b.HasIndex("EmpresaId");

                    b.ToTable("AssociadosEmpresa");
                });

            modelBuilder.Entity("ProjetoAssociados.Models.EmpresaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Cnpj")
                        .IsUnique();

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("AssociadoModelEmpresaModel", b =>
                {
                    b.HasOne("ProjetoAssociados.Models.AssociadoModel", null)
                        .WithMany()
                        .HasForeignKey("AssociadosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoAssociados.Models.EmpresaModel", null)
                        .WithMany()
                        .HasForeignKey("EmpresasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjetoAssociados.Models.AssociadoModelEmpresaModel", b =>
                {
                    b.HasOne("ProjetoAssociados.Models.AssociadoModel", "Associado")
                        .WithMany()
                        .HasForeignKey("AssociadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoAssociados.Models.EmpresaModel", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Associado");

                    b.Navigation("Empresa");
                });
#pragma warning restore 612, 618
        }
    }
}
