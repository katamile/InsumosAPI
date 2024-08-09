using InsumosAPI.DTOs;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Services.VentasService
{
    public interface IVentasService
    {
        Task<List<VentaDTO>> GetAll();
        Task<VentaDTO> GetById(long idVenta);
        Task<MessageInfoDTO> Insert(VentaDTO ventaDTO);
    }
}
