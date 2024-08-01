using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsumosAPI.Migrations
{
    /// <inheritdoc />
    public partial class NuevasTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Stock = table.Column<int>(type: "int", nullable: false),
                    PrecioCompra = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
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
                    VentaIdVenta = table.Column<long>(type: "bigint", nullable: true),
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
                        name: "FK_VentaDetalle_Venta_VentaIdVenta",
                        column: x => x.VentaIdVenta,
                        principalTable: "Venta",
                        principalColumn: "IdVenta");
                });

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1677));

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1683));

            migrationBuilder.InsertData(
                table: "Laboratorio",
                columns: new[] { "IdLaboratorio", "Direccion", "Estado", "FechaCreacion", "FechaEliminacion", "FechaModificacion", "Nombre", "Telefono", "UsuarioCreacion", "UsuarioEliminacion", "UsuarioModificacion" },
                values: new object[,]
                {
                    { 1L, null, "A", new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1717), null, null, "Génerico", null, "SYSTEM", null, null },
                    { 2L, null, "A", new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1719), null, null, "MK", null, "SYSTEM", null, null },
                    { 3L, null, "A", new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1721), null, null, "Genfar", null, "SYSTEM", null, null }
                });

            migrationBuilder.InsertData(
                table: "Proveedor",
                columns: new[] { "IdProveedor", "Direccion", "Estado", "FechaCreacion", "FechaEliminacion", "FechaModificacion", "Nombre", "Telefono", "UsuarioCreacion", "UsuarioEliminacion", "UsuarioModificacion" },
                values: new object[,]
                {
                    { 1L, "Av. Central 123, Ciudad", "A", new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1751), null, null, "Farmacéutica ABC", "0918456789", "SYSTEM", null, null },
                    { 2L, "Calle de la Salud 456, Ciudad", "A", new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1754), null, null, "Distribuidora XYZ", "0978986756", "SYSTEM", null, null },
                    { 3L, "Paseo de los Medicamentos 789, Ciudad", "A", new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1756), null, null, "Laboratorios DEF", "0912345678", "SYSTEM", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1436));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1440));

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
                name: "IX_VentaDetalle_VentaIdVenta",
                table: "VentaDetalle",
                column: "VentaIdVenta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "VentaDetalle");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "Laboratorio");

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
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 6, 11, 15, 0, DateTimeKind.Utc).AddTicks(4027));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 6, 11, 15, 0, DateTimeKind.Utc).AddTicks(4032));
        }
    }
}
