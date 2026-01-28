using ExamenProcomerBackend.Application.Extras.DTOs;
using ExamenProcomerBackend.Application.Extras.Queries;
using ExamenProcomerBackend.Application.Common;
using ExamenProcomerBackend.Application.Interfaces;

namespace ExamenProcomerBackend.Application.Extras.Handlers;

public sealed class ExtraQueryHandler
{
    private readonly IExtraQueryRepository _queryRepository;

    public ExtraQueryHandler(IExtraQueryRepository queryRepository)
    {
        _queryRepository = queryRepository;
    }

    public async Task<OperationResult<IEnumerable<ExtraDto>>> HandleListarAsync(ListarExtrasQuery query)
    {
        var extras = await _queryRepository.ListarTodosAsync();
        return OperationResult<IEnumerable<ExtraDto>>.Ok(extras);
    }

    public async Task<OperationResult<ExtraDto>> HandleObtenerPorIdAsync(ObtenerExtraPorIdQuery query)
    {
        var extra = await _queryRepository.ObtenerPorIdAsync(query.IdExtra);
        if (extra == null)
            return OperationResult<ExtraDto>.Fail("El extra no existe.");

        return OperationResult<ExtraDto>.Ok(extra);
    }
}
