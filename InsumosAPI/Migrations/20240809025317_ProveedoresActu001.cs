using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsumosAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProveedoresActu001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdProveedor",
                table: "Compra",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Compra_IdProveedor",
                table: "Compra",
                column: "IdProveedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Proveedor_IdProveedor",
                table: "Compra",
                column: "IdProveedor",
                principalTable: "Proveedor",
                principalColumn: "IdProveedor",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Proveedor_IdProveedor",
                table: "Compra");

            migrationBuilder.DropIndex(
                name: "IX_Compra_IdProveedor",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "IdProveedor",
                table: "Compra");
        }
    }
}
