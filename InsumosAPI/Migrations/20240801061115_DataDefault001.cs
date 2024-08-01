using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsumosAPI.Migrations
{
    /// <inheritdoc />
    public partial class DataDefault001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NombreCompleto",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "IdCliente", "Correo", "Direccion", "Estado", "FechaCreacion", "FechaEliminacion", "FechaModificacion", "Identificacion", "NombreCompleto", "Telefono", "UsuarioCreacion", "UsuarioEliminacion", "UsuarioModificacion" },
                values: new object[,]
                {
                    { 1L, "ana.rodriguez@example.com", "Av. Quito 123, Quito, Ecuador", "A", new DateTime(2024, 8, 1, 6, 11, 15, 0, DateTimeKind.Utc).AddTicks(4268), null, null, "0998765432", "Ana María Rodríguez", "+593987654321", "SYSTEM", null, null },
                    { 2L, "carlos.fernandez@example.com", "Calle Guayaquil 456, Guayaquil, Ecuador", "A", new DateTime(2024, 8, 1, 6, 11, 15, 0, DateTimeKind.Utc).AddTicks(4270), null, null, "0987654321", "Carlos Fernández", "+593987654322", "SYSTEM", null, null },
                    { 3L, "lucia.morales@example.com", "Av. Cuenca 789, Cuenca, Ecuador", "A", new DateTime(2024, 8, 1, 6, 11, 15, 0, DateTimeKind.Utc).AddTicks(4272), null, null, "0976543210", "Lucía Morales", "+593987654323", "SYSTEM", null, null }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "IdUsuario", "Apellidos", "Contraseña", "Correo", "Estado", "FechaCreacion", "FechaEliminacion", "FechaModificacion", "Identificacion", "IntentosFallidos", "Nombres", "Rol", "Username", "UsuarioCreacion", "UsuarioEliminacion", "UsuarioModificacion" },
                values: new object[,]
                {
                    { 1L, "Farmacia", "$2a$11$KFUx83w07FBBg1TOZ01t9.JIPIKlxIZ55O8cnK7l/rFiY/DuUlXHS", null, "A", new DateTime(2024, 8, 1, 6, 11, 15, 0, DateTimeKind.Utc).AddTicks(4027), null, null, "0999999999", null, "Admin", "Admin", "admin", "SYSTEM", null, null },
                    { 2L, "Orellana Maridueña", "$2a$11$KFUx83w07FBBg1TOZ01t9.JIPIKlxIZ55O8cnK7l/rFiY/DuUlXHS", null, "A", new DateTime(2024, 8, 1, 6, 11, 15, 0, DateTimeKind.Utc).AddTicks(4032), null, null, "0955416755", null, "Milena Saray", "Vendedor", "morella", "SYSTEM", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 2L);

            migrationBuilder.DropColumn(
                name: "Correo",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "NombreCompleto",
                table: "Clientes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
