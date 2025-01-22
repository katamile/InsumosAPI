using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsumosAPI.Migrations
{
    /// <inheritdoc />
    public partial class Correcciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Laboratorio_IdLaboratorio",
                table: "Producto");

            migrationBuilder.DropTable(
                name: "Laboratorio");

            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    IdMarca = table.Column<long>(type: "bigint", nullable: false)
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
                    table.PrimaryKey("PK_Marca", x => x.IdMarca);
                });

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2025, 1, 21, 20, 22, 14, 990, DateTimeKind.Local).AddTicks(7361));

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2025, 1, 21, 20, 22, 14, 990, DateTimeKind.Local).AddTicks(7364));

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2025, 1, 21, 20, 22, 14, 990, DateTimeKind.Local).AddTicks(7366));

            migrationBuilder.InsertData(
                table: "Marca",
                columns: new[] { "IdMarca", "Direccion", "Estado", "FechaCreacion", "FechaEliminacion", "FechaModificacion", "Nombre", "Telefono", "UsuarioCreacion", "UsuarioEliminacion", "UsuarioModificacion" },
                values: new object[,]
                {
                    { 1L, "De los Naranjos N44-15, Quito, Ecuador", "A", new DateTime(2025, 1, 21, 20, 22, 14, 990, DateTimeKind.Local).AddTicks(7390), null, null, "PRONACA", "+59323976400", "SYSTEM", null, null },
                    { 2L, "Av. Pampite, Edificio AMC Business Center 2do. Piso, frente al YOO de Cumbayá, Quito, Ecuador", "A", new DateTime(2025, 1, 21, 20, 22, 14, 990, DateTimeKind.Local).AddTicks(7396), null, null, "La Europea", "072860770 ext. 3322 / 3021", "SYSTEM", null, null },
                    { 3L, "Pedro Pablo Gómez y Los Ríos matriz, Guayaquil, Ecuador", "A", new DateTime(2025, 1, 21, 20, 22, 14, 990, DateTimeKind.Local).AddTicks(7399), null, null, "El Rancho", "+593996086316", "SYSTEM", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 1L,
                columns: new[] { "Direccion", "FechaCreacion", "Nombre", "Telefono" },
                values: new object[] { "Km 10 vía Durán Yaguachi, Duran, Ecuador", new DateTime(2025, 1, 21, 20, 22, 14, 990, DateTimeKind.Local).AddTicks(7427), "La Vienesa", "+59343810320" });

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 2L,
                columns: new[] { "Direccion", "FechaCreacion", "Nombre" },
                values: new object[] { "Garzota, mz.149, v.9, Guayaquil, Guayas 593, EC", new DateTime(2025, 1, 21, 20, 22, 14, 990, DateTimeKind.Local).AddTicks(7430), "Avícola Fernandez" });

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 3L,
                columns: new[] { "Direccion", "FechaCreacion", "Nombre", "Telefono" },
                values: new object[] { "Cdla. La Puntilla, hasta Ciudad Celeste, Guayaquil", new DateTime(2025, 1, 21, 20, 22, 14, 990, DateTimeKind.Local).AddTicks(7432), "Supermercado de Carnes La Española", "0998519628" });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2025, 1, 21, 20, 22, 14, 990, DateTimeKind.Local).AddTicks(7186));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2025, 1, 21, 20, 22, 14, 990, DateTimeKind.Local).AddTicks(7198));

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Marca_IdLaboratorio",
                table: "Producto",
                column: "IdLaboratorio",
                principalTable: "Marca",
                principalColumn: "IdMarca",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Marca_IdLaboratorio",
                table: "Producto");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.CreateTable(
                name: "Laboratorio",
                columns: table => new
                {
                    IdLaboratorio = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioEliminacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratorio", x => x.IdLaboratorio);
                });

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 9, 8, 0, 11, 992, DateTimeKind.Local).AddTicks(4134));

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 9, 8, 0, 11, 992, DateTimeKind.Local).AddTicks(4139));

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "IdCliente",
                keyValue: 3L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 9, 8, 0, 11, 992, DateTimeKind.Local).AddTicks(4142));

            migrationBuilder.InsertData(
                table: "Laboratorio",
                columns: new[] { "IdLaboratorio", "Direccion", "Estado", "FechaCreacion", "FechaEliminacion", "FechaModificacion", "Nombre", "Telefono", "UsuarioCreacion", "UsuarioEliminacion", "UsuarioModificacion" },
                values: new object[,]
                {
                    { 1L, "123 Calle Principal, Ciudad, País", "A", new DateTime(2024, 8, 9, 8, 0, 11, 992, DateTimeKind.Local).AddTicks(4178), null, null, "Génerico", "+1-800-123-4567", "SYSTEM", null, null },
                    { 2L, "456 Avenida Secundaria, Ciudad, País", "A", new DateTime(2024, 8, 9, 8, 0, 11, 992, DateTimeKind.Local).AddTicks(4182), null, null, "MK", "+1-800-987-6543", "SYSTEM", null, null },
                    { 3L, "789 Calle Terciaria, Ciudad, País", "A", new DateTime(2024, 8, 9, 8, 0, 11, 992, DateTimeKind.Local).AddTicks(4184), null, null, "Genfar", "+1-800-555-1212", "SYSTEM", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 1L,
                columns: new[] { "Direccion", "FechaCreacion", "Nombre", "Telefono" },
                values: new object[] { "Av. Central 123, Ciudad", new DateTime(2024, 8, 9, 8, 0, 11, 992, DateTimeKind.Local).AddTicks(4227), "Farmacéutica ABC", "0918456789" });

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 2L,
                columns: new[] { "Direccion", "FechaCreacion", "Nombre" },
                values: new object[] { "Calle de la Salud 456, Ciudad", new DateTime(2024, 8, 9, 8, 0, 11, 992, DateTimeKind.Local).AddTicks(4230), "Distribuidora XYZ" });

            migrationBuilder.UpdateData(
                table: "Proveedor",
                keyColumn: "IdProveedor",
                keyValue: 3L,
                columns: new[] { "Direccion", "FechaCreacion", "Nombre", "Telefono" },
                values: new object[] { "Paseo de los Medicamentos 789, Ciudad", new DateTime(2024, 8, 9, 8, 0, 11, 992, DateTimeKind.Local).AddTicks(4233), "Laboratorios DEF", "0912345678" });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 9, 8, 0, 11, 992, DateTimeKind.Local).AddTicks(3891));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 2L,
                column: "FechaCreacion",
                value: new DateTime(2024, 8, 9, 8, 0, 11, 992, DateTimeKind.Local).AddTicks(3907));

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Laboratorio_IdLaboratorio",
                table: "Producto",
                column: "IdLaboratorio",
                principalTable: "Laboratorio",
                principalColumn: "IdLaboratorio",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
