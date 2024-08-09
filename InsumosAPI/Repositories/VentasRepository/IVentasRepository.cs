using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Repositories.VentasRepository
{
    public interface IVentasRepository
    {
        Task<MessageInfoDTO> Create(VentaDTO ventaDTO);
        Task<List<Venta>> GetAll();
        Task<Venta> GetById(long idCompra);
    }
}
