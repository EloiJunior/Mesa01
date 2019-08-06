﻿// <auto-generated />
using System;
using Mesa01.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mesa01.Migrations
{
    [DbContext(typeof(Mesa01Context_context))]
    [Migration("20190806215902_CriarTabelaClientNoDB")]
    partial class CriarTabelaClientNoDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Mesa01.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BirthDate")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<int>("RegistroNacional");

                    b.Property<int>("TipoRegistroNacional");

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Mesa01.Models.Departamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Departamento");
                });

            modelBuilder.Entity("Mesa01.Models.Fechamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Banco")
                        .IsRequired();

                    b.Property<int?>("ClientId");

                    b.Property<DateTime>("Data");

                    b.Property<double>("Despesa");

                    b.Property<string>("Empresa")
                        .IsRequired();

                    b.Property<int>("Fluxo");

                    b.Property<int>("OperadorId");

                    b.Property<int>("Status");

                    b.Property<double>("Taxa");

                    b.Property<int>("TipoId");

                    b.Property<double>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("OperadorId");

                    b.HasIndex("TipoId");

                    b.ToTable("Fechamento");
                });

            modelBuilder.Entity("Mesa01.Models.Operador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("BaseSalary");

                    b.Property<DateTime>("BirthDate");

                    b.Property<int>("DepartamentoId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("Operador");
                });

            modelBuilder.Entity("Mesa01.Models.Tipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Tipo");
                });

            modelBuilder.Entity("Mesa01.Models.Fechamento", b =>
                {
                    b.HasOne("Mesa01.Models.Client")
                        .WithMany("Fechamentos")
                        .HasForeignKey("ClientId");

                    b.HasOne("Mesa01.Models.Operador")
                        .WithMany("Fechamentos")
                        .HasForeignKey("OperadorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Mesa01.Models.Tipo")
                        .WithMany("Fechamentos")
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Mesa01.Models.Operador", b =>
                {
                    b.HasOne("Mesa01.Models.Departamento", "Departamento")
                        .WithMany("Operadores")
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
