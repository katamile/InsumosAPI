using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsumosAPI.Migrations
{
    /// <inheritdoc />
    public partial class NuevasTablas001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RazonSocial",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Laboratorio",
                columns: table => new
                {
                    IdLaboratorio = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioEliminacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratorio", x => x.IdLaboratorio);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    IdProveedor = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioEliminacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.IdProveedor);
                });

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    IdVenta = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaVenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCliente = table.Column<long>(type: "bigint", nullable: false),
                    IdUsuario = table.Column<long>(type: "bigint", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IvaPor = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Iva = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioEliminacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.IdVenta);
                    table.ForeignKey(
                        name: "FK_Venta_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venta_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    IdProducto = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdLaboratorio = table.Column<long>(type: "bigint", nullable: false),
                    PrecioCompra = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RutaImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioEliminacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK_Producto_Laboratorio_IdLaboratorio",
                        column: x => x.IdLaboratorio,
                        principalTable: "Laboratorio",
                        principalColumn: "IdLaboratorio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    IdCompra = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProveedor = table.Column<long>(type: "bigint", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IvaPor = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Iva = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioEliminacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.IdCompra);
                    table.ForeignKey(
                        name: "FK_Compra_Proveedor_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "IdProveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovimientoInventario",
                columns: table => new
                {
                    IdMovimiento = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<long>(type: "bigint", nullable: false),
                    TipoMovimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockProducto = table.Column<int>(type: "int", nullable: false),
                    CantidadMovimiento = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioEliminacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientoInventario", x => x.IdMovimiento);
                    table.ForeignKey(
                        name: "FK_MovimientoInventario_Producto_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VentaDetalle",
                columns: table => new
                {
                    IdVentaDetalle = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVenta = table.Column<long>(type: "bigint", nullable: false),
                    IdProducto = table.Column<long>(type: "bigint", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PrecioTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioEliminacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaDetalle", x => x.IdVentaDetalle);
                    table.ForeignKey(
                        name: "FK_VentaDetalle_Producto_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VentaDetalle_Venta_IdVenta",
                        column: x => x.IdVenta,
                        principalTable: "Venta",
                        principalColumn: "IdVenta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompraDetalle",
                columns: table => new
                {
                    IdCompraDetalle = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCompra = table.Column<long>(type: "bigint", nullable: false),
                    IdProducto = table.Column<long>(type: "bigint", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PrecioTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioEliminacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraDetalle", x => x.IdCompraDetalle);
                    table.ForeignKey(
                        name: "FK_CompraDetalle_Compra_IdCompra",
                        column: x => x.IdCompra,
                        principalTable: "Compra",
                        principalColumn: "IdCompra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraDetalle_Producto_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 1L,
                columns: new[] { "FechaCreacion", "RazonSocial" },
                values: new object[] { new DateTime(2024, 8, 9, 7, 47, 32, 929, DateTimeKind.Local).AddTicks(823), "Natural" });

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 2L,
                columns: new[] { "FechaCreacion", "RazonSocial" },
                values: new object[] { new DateTime(2024, 8, 9, 7, 47, 32, 929, DateTimeKind.Local).AddTicks(826), "Natural" });

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 3L,
                columns: new[] { "FechaCreacion", "RazonSocial" },
                values: new object[] { new DateTime(2024, 8, 9, 7, 47, 32, 929, DateTimeKind.Local).AddTicks(830), "Natural" });

            migrationBuilder.InsertData(
                table: "Laboratorio",
                columns: new[] { "IdLaboratorio", "Direccion", "Estado", "FechaCreacion", "FechaEliminacion", "FechaModificacion", "Nombre", "Telefono", "UsuarioCreacion", "UsuarioEliminacion", "UsuarioModificacion" },
                values: new object[,]
                {
                    { 1L, "123 Calle Principal, Ciudad, País", "A", new DateTime(2024, 8, 9, 7, 47, 32, 929, DateTimeKind.Local).AddTicks(864), null, null, "Génerico", "+1-800-123-4567", "SYSTEM", null, null },
                    { 2L, "456 Avenida Secundaria, Ciudad, País", "A", new DateTime(2024, 8, 9, 7, 47, 32, 929, DateTimeKind.Local).AddTicks(867), null, null, "MK", "+1-800-987-6543", "SYSTEM", null, null },
                    { 3L, "789 Calle Terciaria, Ciudad, País", "A", new DateTime(2024, 8, 9, 7, 47, 32, 929, DateTimeKind.Local).AddTicks(870), null, null, "Genfar", "+1-800-555-1212", "SYSTEM", null, null }
                });

            migrationBuilder.InsertData(
                table: "Proveedor",
                columns: new[] { "IdProveedor", "Direccion", "Estado", "FechaCreacion", "FechaEliminacion", "FechaModificacion", "Nombre", "Telefono", "UsuarioCreacion", "UsuarioEliminacion", "UsuarioModificacion" },
                values: new object[,]
                {
                    { 1L, "Av. Central 123, Ciudad", "A", new DateTime(2024, 8, 9, 7, 47, 32, 929, DateTimeKind.Local).AddTicks(911), null, null, "Farmacéutica ABC", "0918456789", "SYSTEM", null, null },
                    { 2L, "Calle de la Salud 456, Ciudad", "A", new DateTime(2024, 8, 9, 7, 47, 32, 929, DateTimeKind.Local).AddTicks(913), null, null, "Distribuidora XYZ", "0978986756", "SYSTEM", null, null },
                    { 3L, "Paseo de los Medicamentos 789, Ciudad", "A", new DateTime(2024, 8, 9, 7, 47, 32, 929, DateTimeKind.Local).AddTicks(916), null, null, "Laboratorios DEF", "0912345678", "SYSTEM", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1L,
                columns: new[] { "Contraseña", "FechaCreacion", "IntentosFallidos" },
                values: new object[] { "$2a$11$KFUx83w07FBBg1TOZ01t9.JIPIKlxIZ55O8cnK7l/rFiY/DuUlXHS", new DateTime(2024, 8, 9, 7, 47, 32, 929, DateTimeKind.Local).AddTicks(586), 0 });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 2L,
                columns: new[] { "Contraseña", "FechaCreacion", "IntentosFallidos" },
                values: new object[] { "$2a$11$KFUx83w07FBBg1TOZ01t9.JIPIKlxIZ55O8cnK7l/rFiY/DuUlXHS", new DateTime(2024, 8, 9, 7, 47, 32, 929, DateTimeKind.Local).AddTicks(601), 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Compra_IdProveedor",
                table: "Compra",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_CompraDetalle_IdCompra",
                table: "CompraDetalle",
                column: "IdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_CompraDetalle_IdProducto",
                table: "CompraDetalle",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoInventario_IdProducto",
                table: "MovimientoInventario",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_IdLaboratorio",
                table: "Producto",
                column: "IdLaboratorio");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_IdCliente",
                table: "Venta",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_IdUsuario",
                table: "Venta",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalle_IdProducto",
                table: "VentaDetalle",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalle_IdVenta",
                table: "VentaDetalle",
                column: "IdVenta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompraDetalle");

            migrationBuilder.DropTable(
                name: "MovimientoInventario");

            migrationBuilder.DropTable(
                name: "VentaDetalle");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "Laboratorio");

            migrationBuilder.DropColumn(
                name: "RazonSocial",
                table: "Clientes");

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 6, 11, 15, 0, DateTimeKind.Utc).AddTicks(4268));

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 6, 11, 15, 0, DateTimeKind.Utc).AddTicks(4270));

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 6, 11, 15, 0, DateTimeKind.Utc).AddTicks(4272));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1L,
                columns: new[] { "Contraseña", "FechaCreacion", "IntentosFallidos" },
                values: new object[] { "12345", new DateTime(2024, 8, 1, 6, 11, 15, 0, DateTimeKind.Utc).AddTicks(4027), null });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 2L,
                columns: new[] { "Contraseña", "FechaCreacion", "IntentosFallidos" },
                values: new object[] { "12345", new DateTime(2024, 8, 1, 6, 11, 15, 0, DateTimeKind.Utc).AddTicks(4032), null });
        }
    }
}
