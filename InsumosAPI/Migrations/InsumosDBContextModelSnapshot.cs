﻿// <auto-generated />
using System;
using InsumosAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InsumosAPI.Migrations
{
    [DbContext(typeof(InsumosDBContext))]
    partial class InsumosDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InsumosAPI.Entities.Cliente", b =>
                {
                    b.Property<long>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdCliente"));

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Identificacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioEliminacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCliente");

                    b.ToTable("Clientes", (string)null);

                    b.HasData(
                        new
                        {
                            IdCliente = 1L,
                            Correo = "ana.rodriguez@example.com",
                            Direccion = "Av. Quito 123, Quito, Ecuador",
                            Estado = "A",
                            FechaCreacion = new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8148),
                            Identificacion = "0998765432",
                            NombreCompleto = "Ana María Rodríguez",
                            Telefono = "+593987654321",
                            UsuarioCreacion = "SYSTEM"
                        },
                        new
                        {
                            IdCliente = 2L,
                            Correo = "carlos.fernandez@example.com",
                            Direccion = "Calle Guayaquil 456, Guayaquil, Ecuador",
                            Estado = "A",
                            FechaCreacion = new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8151),
                            Identificacion = "0987654321",
                            NombreCompleto = "Carlos Fernández",
                            Telefono = "+593987654322",
                            UsuarioCreacion = "SYSTEM"
                        },
                        new
                        {
                            IdCliente = 3L,
                            Correo = "lucia.morales@example.com",
                            Direccion = "Av. Cuenca 789, Cuenca, Ecuador",
                            Estado = "A",
                            FechaCreacion = new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8153),
                            Identificacion = "0976543210",
                            NombreCompleto = "Lucía Morales",
                            Telefono = "+593987654323",
                            UsuarioCreacion = "SYSTEM"
                        });
                });

            modelBuilder.Entity("InsumosAPI.Entities.Laboratorio", b =>
                {
                    b.Property<long>("IdLaboratorio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdLaboratorio"));

                    b.Property<string>("Direccion")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioEliminacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdLaboratorio");

                    b.ToTable("Laboratorio", (string)null);

                    b.HasData(
                        new
                        {
                            IdLaboratorio = 1L,
                            Estado = "A",
                            FechaCreacion = new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8180),
                            Nombre = "Génerico",
                            UsuarioCreacion = "SYSTEM"
                        },
                        new
                        {
                            IdLaboratorio = 2L,
                            Estado = "A",
                            FechaCreacion = new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8183),
                            Nombre = "MK",
                            UsuarioCreacion = "SYSTEM"
                        },
                        new
                        {
                            IdLaboratorio = 3L,
                            Estado = "A",
                            FechaCreacion = new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8185),
                            Nombre = "Genfar",
                            UsuarioCreacion = "SYSTEM"
                        });
                });

            modelBuilder.Entity("InsumosAPI.Entities.Producto", b =>
                {
                    b.Property<long>("IdProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdProducto"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<long>("IdLaboratorio")
                        .HasColumnType("bigint");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PrecioCompra")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioVenta")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioEliminacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProducto");

                    b.HasIndex("IdLaboratorio");

                    b.ToTable("Producto", (string)null);
                });

            modelBuilder.Entity("InsumosAPI.Entities.Proveedor", b =>
                {
                    b.Property<long>("IdProveedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdProveedor"));

                    b.Property<string>("Direccion")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioEliminacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProveedor");

                    b.ToTable("Proveedor", (string)null);

                    b.HasData(
                        new
                        {
                            IdProveedor = 1L,
                            Direccion = "Av. Central 123, Ciudad",
                            Estado = "A",
                            FechaCreacion = new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8213),
                            Nombre = "Farmacéutica ABC",
                            Telefono = "0918456789",
                            UsuarioCreacion = "SYSTEM"
                        },
                        new
                        {
                            IdProveedor = 2L,
                            Direccion = "Calle de la Salud 456, Ciudad",
                            Estado = "A",
                            FechaCreacion = new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8215),
                            Nombre = "Distribuidora XYZ",
                            Telefono = "0978986756",
                            UsuarioCreacion = "SYSTEM"
                        },
                        new
                        {
                            IdProveedor = 3L,
                            Direccion = "Paseo de los Medicamentos 789, Ciudad",
                            Estado = "A",
                            FechaCreacion = new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8217),
                            Nombre = "Laboratorios DEF",
                            Telefono = "0912345678",
                            UsuarioCreacion = "SYSTEM"
                        });
                });

            modelBuilder.Entity("InsumosAPI.Entities.Usuario", b =>
                {
                    b.Property<long>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdUsuario"));

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Identificacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IntentosFallidos")
                        .HasColumnType("int");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioEliminacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUsuario");

                    b.ToTable("Usuarios", (string)null);

                    b.HasData(
                        new
                        {
                            IdUsuario = 1L,
                            Apellidos = "Farmacia",
                            Contraseña = "$2a$11$KFUx83w07FBBg1TOZ01t9.JIPIKlxIZ55O8cnK7l/rFiY/DuUlXHS",
                            Estado = "A",
                            FechaCreacion = new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(7932),
                            Identificacion = "0999999999",
                            IntentosFallidos = 0,
                            Nombres = "Admin",
                            Rol = "Admin",
                            Username = "admin",
                            UsuarioCreacion = "SYSTEM"
                        },
                        new
                        {
                            IdUsuario = 2L,
                            Apellidos = "Orellana Maridueña",
                            Contraseña = "$2a$11$KFUx83w07FBBg1TOZ01t9.JIPIKlxIZ55O8cnK7l/rFiY/DuUlXHS",
                            Estado = "A",
                            FechaCreacion = new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(7935),
                            Identificacion = "0955416755",
                            IntentosFallidos = 0,
                            Nombres = "Milena Saray",
                            Rol = "Vendedor",
                            Username = "morella",
                            UsuarioCreacion = "SYSTEM"
                        });
                });

            modelBuilder.Entity("InsumosAPI.Entities.Venta", b =>
                {
                    b.Property<long>("IdVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdVenta"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaVenta")
                        .HasColumnType("datetime2");

                    b.Property<long>("IdCliente")
                        .HasColumnType("bigint");

                    b.Property<long>("IdUsuario")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Iva")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IvaPor")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<decimal>("Subtotal")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioEliminacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdVenta");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Venta", (string)null);
                });

            modelBuilder.Entity("InsumosAPI.Entities.VentaDetalle", b =>
                {
                    b.Property<long>("IdVentaDetalle")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdVentaDetalle"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<long>("IdProducto")
                        .HasColumnType("bigint");

                    b.Property<long>("IdVenta")
                        .HasColumnType("bigint");

                    b.Property<decimal>("PrecioTotal")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioUnitario")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioEliminacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioModificacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("VentaIdVenta")
                        .HasColumnType("bigint");

                    b.HasKey("IdVentaDetalle");

                    b.HasIndex("IdProducto");

                    b.HasIndex("VentaIdVenta");

                    b.ToTable("VentaDetalle", (string)null);
                });

            modelBuilder.Entity("InsumosAPI.Entities.Producto", b =>
                {
                    b.HasOne("InsumosAPI.Entities.Laboratorio", "Laboratorio")
                        .WithMany()
                        .HasForeignKey("IdLaboratorio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Laboratorio");
                });

            modelBuilder.Entity("InsumosAPI.Entities.Venta", b =>
                {
                    b.HasOne("InsumosAPI.Entities.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InsumosAPI.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("InsumosAPI.Entities.VentaDetalle", b =>
                {
                    b.HasOne("InsumosAPI.Entities.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InsumosAPI.Entities.Venta", null)
                        .WithMany("VentaDetalles")
                        .HasForeignKey("VentaIdVenta");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("InsumosAPI.Entities.Venta", b =>
                {
                    b.Navigation("VentaDetalles");
                });
#pragma warning restore 612, 618
        }
    }
}
