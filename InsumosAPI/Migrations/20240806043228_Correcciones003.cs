using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsumosAPI.Migrations
{
    /// <inheritdoc />
    public partial class Correcciones003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Producto");

            migrationBuilder.AddColumn<long>(
                name: "IdMovimiento",
                table: "Producto",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "RazonSocial",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    IdCompra = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IvaPor = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Iva = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.IdCompra);
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
                    CompraIdCompra = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraDetalle", x => x.IdCompraDetalle);
                    table.ForeignKey(
                        name: "FK_CompraDetalle_Compra_CompraIdCompra",
                        column: x => x.CompraIdCompra,
                        principalTable: "Compra",
                        principalColumn: "IdCompra");
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
                values: new object[] { new DateTime(2024, 8, 6, 4, 32, 27, 599, DateTimeKind.Utc).AddTicks(8001), "Natural" });

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 2L,
                columns: new[] { "FechaCreacion", "RazonSocial" },
                values: new object[] { new DateTime(2024, 8, 6, 4, 32, 27, 599, DateTimeKind.Utc).AddTicks(8008), "Natural" });

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 3L,
                columns: new[] { "FechaCreacion", "RazonSocial" },
                values: new object[] { new DateTime(2024, 8, 6, 4, 32, 27, 599, DateTimeKind.Utc).AddTicks(8015), "Natural" });

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 6, 4, 32, 27, 599, DateTimeKind.Utc).AddTicks(8090));

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 6, 4, 32, 27, 599, DateTimeKind.Utc).AddTicks(8095));

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 6, 4, 32, 27, 599, DateTimeKind.Utc).AddTicks(8099));

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 6, 4, 32, 27, 599, DateTimeKind.Utc).AddTicks(8166));

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 6, 4, 32, 27, 599, DateTimeKind.Utc).AddTicks(8172));

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 6, 4, 32, 27, 599, DateTimeKind.Utc).AddTicks(8176));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 6, 4, 32, 27, 599, DateTimeKind.Utc).AddTicks(7581));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 6, 4, 32, 27, 599, DateTimeKind.Utc).AddTicks(7588));

            migrationBuilder.CreateIndex(
                name: "IX_CompraDetalle_CompraIdCompra",
                table: "CompraDetalle",
                column: "CompraIdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_CompraDetalle_IdProducto",
                table: "CompraDetalle",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoInventario_IdProducto",
                table: "MovimientoInventario",
                column: "IdProducto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompraDetalle");

            migrationBuilder.DropTable(
                name: "MovimientoInventario");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropColumn(
                name: "IdMovimiento",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "RazonSocial",
                table: "Clientes");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Producto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 7, 46, 21, 33, DateTimeKind.Utc).AddTicks(8880));

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 7, 46, 21, 33, DateTimeKind.Utc).AddTicks(8883));

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 7, 46, 21, 33, DateTimeKind.Utc).AddTicks(8885));

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 7, 46, 21, 33, DateTimeKind.Utc).AddTicks(8909));

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 7, 46, 21, 33, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 7, 46, 21, 33, DateTimeKind.Utc).AddTicks(8914));

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 7, 46, 21, 33, DateTimeKind.Utc).AddTicks(8939));

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 7, 46, 21, 33, DateTimeKind.Utc).AddTicks(8941));

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 7, 46, 21, 33, DateTimeKind.Utc).AddTicks(8943));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 7, 46, 21, 33, DateTimeKind.Utc).AddTicks(8686));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 7, 46, 21, 33, DateTimeKind.Utc).AddTicks(8688));
        }
    }
}
