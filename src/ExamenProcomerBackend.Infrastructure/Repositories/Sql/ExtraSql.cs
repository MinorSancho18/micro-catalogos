namespace ExamenProcomerBackend.Infrastructure.Repositories.Sql;

internal static class ExtraSql
{
    internal const string ListarTodos = @"
        SELECT 
            ID_EXTRA AS IdExtra,
            DESCRIPCION AS Descripcion,
            COSTO AS Costo
        FROM T_EXTRAS
        ORDER BY DESCRIPCION;
    ";

    internal const string ObtenerPorId = @"
        SELECT 
            ID_EXTRA AS IdExtra,
            DESCRIPCION AS Descripcion,
            COSTO AS Costo
        FROM T_EXTRAS
        WHERE ID_EXTRA = @IdExtra;
    ";

    internal const string Crear = @"
        INSERT INTO T_EXTRAS (DESCRIPCION, COSTO)
        VALUES (@Descripcion, @Costo);
        SELECT CAST(SCOPE_IDENTITY() AS INT);
    ";

    internal const string Actualizar = @"
        UPDATE T_EXTRAS
        SET DESCRIPCION = @Descripcion,
            COSTO = @Costo
        WHERE ID_EXTRA = @IdExtra;
    ";

    internal const string Eliminar = @"
        DELETE FROM T_EXTRAS
        WHERE ID_EXTRA = @IdExtra;
    ";
}
