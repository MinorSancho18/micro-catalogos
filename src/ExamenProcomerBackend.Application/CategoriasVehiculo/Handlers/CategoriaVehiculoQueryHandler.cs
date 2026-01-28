using ExamenProcomerBackend.Application.CategoriasVehiculo.DTOs;
using ExamenProcomerBackend.Application.CategoriasVehiculo.Queries;
using ExamenProcomerBackend.Application.Common;
using ExamenProcomerBackend.Application.Interfaces;

namespace ExamenProcomerBackend.Application.CategoriasVehiculo.Handlers;

public sealed class CategoriaVehiculoQueryHandler
{
    private readonly ICategoriaVehiculoQueryRepository _queryRepository;

    public CategoriaVehiculoQueryHandler(ICategoriaVehiculoQueryRepository queryRepository)
    {
        _queryRepository = queryRepository;
    }

    public async Task<OperationResult<IEnumerable<CategoriaVehiculoDto>>> HandleListarAsync(ListarCategoriasVehiculoQuery query)
    {
        var categorias = await _queryRepository.ListarTodosAsync();
        return OperationResult<IEnumerable<CategoriaVehiculoDto>>.Ok(categorias);
    }

    public async Task<OperationResult<CategoriaVehiculoDto>> HandleObtenerPorIdAsync(ObtenerCategoriaVehiculoPorIdQuery query)
    {
        var categoria = await _queryRepository.ObtenerPorIdAsync(query.IdCategoria);
        if (categoria == null)
            return OperationResult<CategoriaVehiculoDto>.Fail("La categoría no existe.");

        return OperationResult<CategoriaVehiculoDto>.Ok(categoria);
    }
}
