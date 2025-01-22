using InsumosAPI.Repositories.LoginRepository;
using InsumosAPI.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq.Expressions;

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
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaDetalle> VentaDetalles { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<CompraDetalle> CompraDetalles { get; set; }
        public DbSet<MovimientoInventario> MovimientoInventarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var property = entity.ClrType.GetProperty("Estado");
                if (property != null)
                {
                    var parameter = Expression.Parameter(entity.ClrType, "e");
                    var filter = Expression.Lambda(
                        Expression.Equal(Expression.Property(parameter, property), Expression.Constant(Globales.ACTIVO)),
                        parameter);
                    entity.SetQueryFilter(filter);
                }
            }

            modelBuilder.Entity<Cliente>()
                .ToTable("Clientes")
                .HasKey(c => c.IdCliente);

            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios")
                .HasKey(u => u.IdUsuario);

            modelBuilder.Entity<Marca>()
                .ToTable("Marca")
                .HasKey(u => u.IdMarca);

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

            modelBuilder.Entity<MovimientoInventario>()
                .ToTable("MovimientoInventario")
                .HasKey(u => u.IdMovimiento);

            modelBuilder.Entity<Compra>()
                .ToTable("Compra")
                .HasKey(u => u.IdCompra);

            modelBuilder.Entity<CompraDetalle>()
                .ToTable("CompraDetalle")
                .HasKey(u => u.IdCompraDetalle);

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
                    FechaCreacion =DateTime.Now,
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
                    FechaCreacion = DateTime.Now,
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
                    RazonSocial="Natural",
                    Estado = "A",
                    FechaCreacion = DateTime.Now,
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
                    RazonSocial = "Natural",
                    Estado = "A",
                    FechaCreacion = DateTime.Now,
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
                    RazonSocial = "Natural",
                    Estado = "A",
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = "SYSTEM"
                }
            );

            modelBuilder.Entity<Marca>().HasData(
                new Marca
                {
                    IdMarca = 1,
                    Nombre = "PRONACA",
                    Estado = "A",
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = "SYSTEM",
                    Telefono = "+59323976400",
                    Direccion = "De los Naranjos N44-15, Quito, Ecuador"
                },

                new Marca
                {
                    IdMarca = 2,
                    Nombre = "La Europea",
                    Estado = "A",
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = "SYSTEM",
                    Telefono = "072860770 ext. 3322 / 3021",
                    Direccion = "Av. Pampite, Edificio AMC Business Center 2do. Piso, frente al YOO de Cumbayá, Quito, Ecuador"
                },

                new Marca
                {
                    IdMarca = 3,
                    Nombre = "El Rancho",
                    Estado = "A",
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = "SYSTEM",
                    Telefono = "+593996086316",
                    Direccion = "Pedro Pablo Gómez y Los Ríos matriz, Guayaquil, Ecuador"
                }
            );

            modelBuilder.Entity<Proveedor>().HasData(
                new Proveedor
                {
                    IdProveedor = 1,
                    Nombre = "La Vienesa",
                    Telefono = "+59343810320",
                    Direccion = "Km 10 vía Durán Yaguachi, Duran, Ecuador",
                    Estado = "A",
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = "SYSTEM"
                },
                new Proveedor
                {
                    IdProveedor = 2,
                    Nombre = "Avícola Fernandez",
                    Telefono = "0978986756",
                    Direccion = "Garzota, mz.149, v.9, Guayaquil, Guayas 593, EC",
                    Estado = "A",
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = "SYSTEM"
                },
                new Proveedor
                {
                    IdProveedor= 3,
                    Nombre = "Supermercado de Carnes La Española",
                    Telefono = "0998519628",
                    Direccion = "Cdla. La Puntilla, hasta Ciudad Celeste, Guayaquil",
                    Estado = "A",
                    FechaCreacion = DateTime.Now,
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
