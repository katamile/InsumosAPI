using InsumosAPI.DTOs;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Services.ComprasService
{
    public interface IComprasService
    {
        Task<List<CompraDTO>> GetAll();
        Task<CompraDTO> GetById(long idCompra);
        Task<MessageInfoDTO> Insert(CompraDTO compraDTO);
    }
}
