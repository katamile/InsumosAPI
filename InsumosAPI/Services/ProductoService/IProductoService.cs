using InsumosAPI.DTOs;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Services.ProductoService
{
    public interface IProductoService
    {
        Task<MessageInfoDTO> CrearProductoAsync(ProductoDTO request);
        Task<MessageInfoDTO> EliminarProductoAsync(long id);
        Task<List<ProductoDTO>> GetAll();
        Task<ProductoDTO> GetById(long id);
        Task<ProductoDTO> GetByNombre(string nombre);
        Task<MessageInfoDTO> ModificarProductoAsync(ProductoDTO request);
    }
}
