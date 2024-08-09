using Azure.Core;
using InsumosAPI.DTOs;
using InsumosAPI.Middleware.Exceptions.BadRequest;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.ClienteRepository;
using InsumosAPI.Repositories.MovimientoInventarioRepository;
using InsumosAPI.Services.ProductoService;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace InsumosAPI.Services.MovimientoInventarioService
{
    public class MovimientoInventarioService : IMovimientoInventarioService
    {
        private readonly IMovimientoInventarioRepository _movimientoInventarioRepository;
        private readonly IProductoService _productoService;

        public MovimientoInventarioService(IMovimientoInventarioRepository movimientoInventarioRepository,
                                            IProductoService productoService)
        {
            _movimientoInventarioRepository = movimientoInventarioRepository;
            _productoService = productoService;
        }

        public async Task<List<MovimientoInventarioDTO>> GetAll()
        {
            var movimientos = await _movimientoInventarioRepository.GetAll();
            return movimientos.Select(m => new MovimientoInventarioDTO
            {
                IdMovimiento = m.IdMovimiento,
                TipoMovimiento = m.TipoMovimiento,
                IdProducto = m.IdProducto,
                Producto = m.Producto,
                StockProducto = m.StockProducto,
                CantidadMovimiento = m.CantidadMovimiento
            }).ToList();
        }

        public async Task<MessageInfoDTO> Insert(MovimientoInventarioDTO movimientoInventarioDTO)
        {

            try
            {
                await ValidarInsert(movimientoInventarioDTO);

                await _movimientoInventarioRepository.Create(movimientoInventarioDTO);

                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.Created,
                    Message = "Cliente creado exitosamente.",
                    Success = "true"
                };
            }
            catch (Exception ex)
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Success = "false"
                };
            }
        }

        public async Task ValidarInsert(MovimientoInventarioDTO movimientoInventarioDTO)
        {

            var productoExiste = await _productoService.GetById(movimientoInventarioDTO.IdProducto);
            if (productoExiste != null)
            {
                throw new NotFoundException("El producto no existe.");
            }

        }
    }

}
