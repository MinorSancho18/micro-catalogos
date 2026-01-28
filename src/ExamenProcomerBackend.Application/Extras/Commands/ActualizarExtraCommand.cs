namespace ExamenProcomerBackend.Application.Extras.Commands;

public sealed record ActualizarExtraCommand(
    int IdExtra,
    string Descripcion,
    decimal Costo
);
