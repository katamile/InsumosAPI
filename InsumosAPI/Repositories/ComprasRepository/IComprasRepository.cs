using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Repositories.ComprasRepository
{
    public interface IComprasRepository
    {
        Task<List<Compra>> GetAll();
        Task<MessageInfoDTO> Create(CompraDTO compraDTO);
        Task<Compra> GetById(long idCompra);
    }
}
