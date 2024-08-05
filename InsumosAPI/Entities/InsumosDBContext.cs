using InsumosAPI.Repositories.LoginRepository;
using InsumosAPI.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InsumosAPI.Entities
{
    public class InsumosDBContext : DbContext
    {
        private readonly CRUDInterceptor _crudInterceptor;

        public InsumosDBContext(DbContextOptions<InsumosDBContext> options, CRUDInterceptor crudInterceptor)
            : base(options)
        {
            _crudInterceptor = crudInterceptor;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaDetalle> VentaDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Cliente>()
                .ToTable("Clientes")
                .HasKey(c => c.IdCliente);

            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios")
                .HasKey(u => u.IdUsuario);

            modelBuilder.Entity<Laboratorio>()
                .ToTable("Laboratorio")
                .HasKey(u => u.IdLaboratorio);

            modelBuilder.Entity<Producto>()
                .ToTable("Producto")
                .HasKey(u => u.IdProducto);

            modelBuilder.Entity<Proveedor>()
                .ToTable("Proveedor")
                .HasKey(u => u.IdProveedor);

            modelBuilder.Entity<Venta>()
                .ToTable("Venta")
                .HasKey(u => u.IdVenta);

            modelBuilder.Entity<VentaDetalle>()
                .ToTable("VentaDetalle")
                .HasKey(u => u.IdVentaDetalle);

            #region DATA DEFAULT

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    IdUsuario = 1,
                    Identificacion = "0999999999",
                    Nombres = "Admin",
                    Apellidos = "Farmacia",
                    Username = "admin",
                    Contraseña = "$2a$11$KFUx83w07FBBg1TOZ01t9.JIPIKlxIZ55O8cnK7l/rFiY/DuUlXHS",
                    Rol = "Admin",
                    Estado = "A",
                    FechaCreacion =DateTime.UtcNow,
                    UsuarioCreacion="SYSTEM"
                },

                new Usuario
                {
                    IdUsuario = 2,
                    Identificacion = "0955416755",
                    Nombres = "Milena Saray",
                    Apellidos = "Orellana Maridueña",
                    Username = "morella",
                    Contraseña = "$2a$11$KFUx83w07FBBg1TOZ01t9.JIPIKlxIZ55O8cnK7l/rFiY/DuUlXHS",
                    Rol = "Vendedor",
                    Estado = "A",
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreacion = "SYSTEM"
                }
            );

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente
                {
                    IdCliente = 1,
                    Identificacion = "0998765432",
                    NombreCompleto = "Ana María Rodríguez",
                    Telefono = "+593987654321",
                    Direccion = "Av. Quito 123, Quito, Ecuador",
                    Correo = "ana.rodriguez@example.com",
                    Estado = "A",
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreacion = "SYSTEM"
                },
                new Cliente
                {
                    IdCliente = 2,
                    Identificacion = "0987654321",
                    NombreCompleto = "Carlos Fernández",
                    Telefono = "+593987654322",
                    Direccion = "Calle Guayaquil 456, Guayaquil, Ecuador",
                    Correo = "carlos.fernandez@example.com",
                    Estado = "A",
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreacion = "SYSTEM"
                },
                new Cliente
                {
                    IdCliente = 3,
                    Identificacion = "0976543210",
                    NombreCompleto = "Lucía Morales",
                    Telefono = "+593987654323",
                    Direccion = "Av. Cuenca 789, Cuenca, Ecuador",
                    Correo = "lucia.morales@example.com",
                    Estado = "A",
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreacion = "SYSTEM"
                }
            );

            modelBuilder.Entity<Laboratorio>().HasData(
                new Laboratorio
                {
                    IdLaboratorio = 1,
                    Nombre = "Génerico",
                    Estado = "A",
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreacion = "SYSTEM",
                    Telefono = "+1-800-123-4567",
                    Direccion = "123 Calle Principal, Ciudad, País"
                },

                new Laboratorio
                {
                    IdLaboratorio = 2,
                    Nombre = "MK",
                    Estado = "A",
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreacion = "SYSTEM",
                    Telefono = "+1-800-987-6543",
                    Direccion = "456 Avenida Secundaria, Ciudad, País"
                },

                new Laboratorio
                {
                    IdLaboratorio = 3,
                    Nombre = "Genfar",
                    Estado = "A",
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreacion = "SYSTEM",
                    Telefono = "+1-800-555-1212",
                    Direccion = "789 Calle Terciaria, Ciudad, País"
                }
            );

            modelBuilder.Entity<Proveedor>().HasData(
                new Proveedor
                {
                    IdProveedor = 1,
                    Nombre = "Farmacéutica ABC",
                    Telefono = "0918456789",
                    Direccion = "Av. Central 123, Ciudad",
                    Estado = "A",
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreacion = "SYSTEM"
                },
                new Proveedor
                {
                    IdProveedor = 2,
                    Nombre = "Distribuidora XYZ",
                    Telefono = "0978986756",
                    Direccion = "Calle de la Salud 456, Ciudad",
                    Estado = "A",
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreacion = "SYSTEM"
                },
                new Proveedor
                {
                    IdProveedor= 3,
                    Nombre = "Laboratorios DEF",
                    Telefono = "0912345678",
                    Direccion = "Paseo de los Medicamentos 789, Ciudad",
                    Estado = "A",
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreacion = "SYSTEM"
                }
             );
            #endregion

        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _crudInterceptor.OnBeforeSaveChanges(this);
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _crudInterceptor.OnBeforeSaveChanges(this);
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
