using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.ProductoRepository;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using InsumosAPI.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InsumosAPI.Repositories.ProductoRepository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly InsumosDBContext _contexto;

        public ProductoRepository(InsumosDBContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<ProductoDTO>> GetAll()
        {
            return await _contexto.Productos
                .Where(p => p.Estado == Globales.ACTIVO)
                .Include(p => p.MovimientosInventario)
                 .Select(p => new ProductoDTO
                 {
                     IdProducto = p.IdProducto,
                     Nombre = p.Nombre,
                     IdLaboratorio = p.IdLaboratorio,
                     LaboratorioName =  p.Laboratorio.Nombre,
                     PrecioCompra = p.PrecioCompra,
                     PrecioVenta = p.PrecioVenta,
                     RutaImg = p.RutaImg,
                     Stock = p.MovimientosInventario
                                .Select(m => m.StockProducto).FirstOrDefault()
                 })
                .ToListAsync() ?? throw new NotFoundException("No se encuentran productos activos.");
        }

        public async Task<ProductoDTO> GetById(long id)
        {
            var producto = await _contexto.Productos
                .Where(p => p.IdProducto == id && p.Estado == Globales.ACTIVO)
                .Include(p => p.MovimientosInventario)
                .Select(p => new ProductoDTO
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    IdLaboratorio = p.IdLaboratorio,
                    LaboratorioName = p.Laboratorio.Nombre,
                    PrecioCompra = p.PrecioCompra,
                    PrecioVenta = p.PrecioVenta,
                    RutaImg = p.RutaImg,
                    Stock = p.MovimientosInventario
                                .Select(m => m.StockProducto).FirstOrDefault()
                })
                .FirstOrDefaultAsync()
                ?? throw new NotFoundException("No se encuentra el producto.");

            return producto;
        }

        public async Task<Producto> GetByIdAsync(long id)
        {
            return await _contexto.Productos.FirstOrDefaultAsync(p => p.IdProducto == id && p.Estado == Globales.ACTIVO)
                                             ?? throw new NotFoundException("No se encuentra el producto.");
        }

        public async Task<ProductoDTO> ObtenerPorNombreAsync(string nombre)
        {
            var producto = await _contexto.Productos
                .Where(p => p.Nombre.Contains(nombre)
                        && p.Estado == Globales.ACTIVO)
                .Include(p => p.MovimientosInventario)
                .Select(p => new ProductoDTO
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    IdLaboratorio = p.IdLaboratorio,
                    LaboratorioName = p.Laboratorio.Nombre,
                    PrecioCompra = p.PrecioCompra,
                    PrecioVenta = p.PrecioVenta,
                    RutaImg = p.RutaImg,
                    Stock = p.MovimientosInventario
                                .Select(m => m.StockProducto).FirstOrDefault()
                })
                .FirstOrDefaultAsync()
                ?? throw new NotFoundException("No se encuentra el producto.");

            return producto;
        }

        public async Task<MessageInfoDTO> CrearNuevoProducto(ProductoDTO producto)
        {
            try
            {
                var productoSave = new Producto
                {
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    IdLaboratorio = producto.IdLaboratorio,
                    PrecioCompra = producto.PrecioCompra,
                    PrecioVenta = producto.PrecioVenta,
                    RutaImg = producto.RutaImg
                };
                await _contexto.Productos.AddAsync(productoSave);
                await _contexto.SaveChangesAsync();

                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.Created,
                    Message = "Producto creado exitosamente.",
                    Success = "true"
                };
            }
            catch (Exception ex)
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Success = "false"
                };
            }
        }

        public async Task ModificarProductoAsync(Producto producto)
        {
            try
            {
                _contexto.Productos.Update(producto);
                await _contexto.SaveChangesAsync();

            } catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al actualizar.");
            }
        }

        public async Task EliminarProductoAsync(long id)
        {
            var producto = await GetByIdAsync(id);
            if (producto == null)
            {
                throw new NotFoundException("Producto no encontrado.");
            }

            // Marcar la entidad como eliminada lógicamente
            producto.Estado = Globales.INACTIVO;
            producto.FechaEliminacion = DateTime.Now;

            // Actualizar la entidad en el contexto
            _contexto.Productos.Update(producto);

            // Guardar los cambios
            await _contexto.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
