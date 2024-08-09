using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Repositories.ProductoRepository
{
    public interface IProductoRepository
    {
        Task<MessageInfoDTO> CrearNuevoProducto(ProductoDTO producto);
        Task EliminarProductoAsync(long id);
        Task<List<ProductoDTO>> GetAll();
        Task<ProductoDTO> GetById(long id);
        Task<Producto> GetByIdAsync(long id);
        Task ModificarProductoAsync(Producto producto);
        Task<ProductoDTO> ObtenerPorNombreYLaboratorioAsync(string nombre, long idLaboratorio);
    }
}
