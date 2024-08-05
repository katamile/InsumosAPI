using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsumosAPI.Migrations
{
    /// <inheritdoc />
    public partial class Correcciones002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "Direccion", "FechaCreacion", "Telefono" },
                values: new object[] { "123 Calle Principal, Ciudad, País", new DateTime(2024, 8, 5, 7, 46, 21, 33, DateTimeKind.Utc).AddTicks(8909), "+1-800-123-4567" });

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 2L,
                columns: new[] { "Direccion", "FechaCreacion", "Telefono" },
                values: new object[] { "456 Avenida Secundaria, Ciudad, País", new DateTime(2024, 8, 5, 7, 46, 21, 33, DateTimeKind.Utc).AddTicks(8912), "+1-800-987-6543" });

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 3L,
                columns: new[] { "Direccion", "FechaCreacion", "Telefono" },
                values: new object[] { "789 Calle Terciaria, Ciudad, País", new DateTime(2024, 8, 5, 7, 46, 21, 33, DateTimeKind.Utc).AddTicks(8914), "+1-800-555-1212" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8148));

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8151));

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8153));

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 1L,
                columns: new[] { "Direccion", "FechaCreacion", "Telefono" },
                values: new object[] { null, new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8180), null });

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 2L,
                columns: new[] { "Direccion", "FechaCreacion", "Telefono" },
                values: new object[] { null, new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8183), null });

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 3L,
                columns: new[] { "Direccion", "FechaCreacion", "Telefono" },
                values: new object[] { null, new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8185), null });

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8213));

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8215));

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8217));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(7932));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(7935));
        }
    }
}
