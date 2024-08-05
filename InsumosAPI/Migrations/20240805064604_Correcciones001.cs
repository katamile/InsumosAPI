using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsumosAPI.Migrations
{
    /// <inheritdoc />
    public partial class Correcciones001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8180));

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8183));

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(8185));

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
                columns: new[] { "Contraseña", "FechaCreacion", "IntentosFallidos" },
                values: new object[] { "$2a$11$KFUx83w07FBBg1TOZ01t9.JIPIKlxIZ55O8cnK7l/rFiY/DuUlXHS", new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(7932), 0 });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 2L,
                columns: new[] { "Contraseña", "FechaCreacion", "IntentosFallidos" },
                values: new object[] { "$2a$11$KFUx83w07FBBg1TOZ01t9.JIPIKlxIZ55O8cnK7l/rFiY/DuUlXHS", new DateTime(2024, 8, 5, 6, 46, 3, 801, DateTimeKind.Utc).AddTicks(7935), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1717));

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1719));

            migrationBuilder.UpdateData(
                table: "Laboratorio",
                keyColumn: "IdLaboratorio",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1721));

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1751));

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1756));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1L,
                columns: new[] { "Contraseña", "FechaCreacion", "IntentosFallidos" },
                values: new object[] { "12345", new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1436), null });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 2L,
                columns: new[] { "Contraseña", "FechaCreacion", "IntentosFallidos" },
                values: new object[] { "12345", new DateTime(2024, 8, 1, 7, 15, 49, 91, DateTimeKind.Utc).AddTicks(1440), null });
        }
    }
}
