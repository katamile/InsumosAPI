using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsumosAPI.Migrations
{
    /// <inheritdoc />
    public partial class MovInvent002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimientoInventario_Producto_ProductoIdProducto",
                table: "MovimientoInventario");

            migrationBuilder.DropIndex(
                name: "IX_MovimientoInventario_ProductoIdProducto",
                table: "MovimientoInventario");

            migrationBuilder.DropColumn(
                name: "ProductoIdProducto",
                table: "MovimientoInventario");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoInventario_IdProducto",
                table: "MovimientoInventario",
                column: "IdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimientoInventario_Producto_IdProducto",
                table: "MovimientoInventario",
                column: "IdProducto",
                principalTable: "Producto",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimientoInventario_Producto_IdProducto",
                table: "MovimientoInventario");

            migrationBuilder.DropIndex(
                name: "IX_MovimientoInventario_IdProducto",
                table: "MovimientoInventario");

            migrationBuilder.AddColumn<long>(
                name: "ProductoIdProducto",
                table: "MovimientoInventario",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoInventario_ProductoIdProducto",
                table: "MovimientoInventario",
                column: "ProductoIdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimientoInventario_Producto_ProductoIdProducto",
                table: "MovimientoInventario",
                column: "ProductoIdProducto",
                principalTable: "Producto",
                principalColumn: "IdProducto");
        }
    }
}
