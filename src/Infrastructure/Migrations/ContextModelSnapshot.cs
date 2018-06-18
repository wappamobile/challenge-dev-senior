﻿// <auto-generated />
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Infra.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationCore.Entity.Carro", b =>
                {
                    b.Property<int>("CarroId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<DateTime>("DataCadastro");

                    b.Property<string>("Marca")
                        .IsRequired();

                    b.Property<string>("Modelo")
                        .IsRequired();

                    b.Property<string>("Placa")
                        .IsRequired();

                    b.HasKey("CarroId");

                    b.ToTable("Carro");
                });

            modelBuilder.Entity("ApplicationCore.Entity.Endereco", b =>
                {
                    b.Property<int>("EnderecoId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<string>("CEP")
                        .IsRequired();

                    b.Property<string>("Cidade")
                        .IsRequired();

                    b.Property<string>("Complemento")
                        .IsRequired();

                    b.Property<DateTime>("DataCadastro");

                    b.Property<int?>("GeoLocationId");

                    b.Property<string>("Logradouro")
                        .IsRequired();

                    b.Property<int>("Numero");

                    b.Property<string>("UF")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.HasKey("EnderecoId");

                    b.HasIndex("GeoLocationId");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("ApplicationCore.Entity.GeoLocation", b =>
                {
                    b.Property<int>("GeoLocationId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.HasKey("GeoLocationId");

                    b.ToTable("GeoLocation");
                });

            modelBuilder.Entity("ApplicationCore.Entity.Motorista", b =>
                {
                    b.Property<int>("MotoristaId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<int>("CarroId");

                    b.Property<DateTime>("DataCadastro");

                    b.Property<int>("EnderecoId");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("Sobrenome")
                        .IsRequired();

                    b.HasKey("MotoristaId");

                    b.HasIndex("CarroId");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Motorista");
                });

            modelBuilder.Entity("ApplicationCore.Entity.Endereco", b =>
                {
                    b.HasOne("ApplicationCore.Entity.GeoLocation", "GeoLocation")
                        .WithMany()
                        .HasForeignKey("GeoLocationId");
                });

            modelBuilder.Entity("ApplicationCore.Entity.Motorista", b =>
                {
                    b.HasOne("ApplicationCore.Entity.Carro", "Carro")
                        .WithMany()
                        .HasForeignKey("CarroId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplicationCore.Entity.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
