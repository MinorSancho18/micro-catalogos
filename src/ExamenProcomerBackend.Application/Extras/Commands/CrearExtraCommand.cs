namespace ExamenProcomerBackend.Application.Extras.Commands;

public sealed record CrearExtraCommand(
    string Descripcion,
    decimal Costo
);
