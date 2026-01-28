namespace ExamenProcomerBackend.Application.Extras.DTOs;

public sealed record ExtraDto(
    int IdExtra,
    string Descripcion,
    decimal Costo
);
