namespace ExamenProcomerBackend.Infrastructure.Repositories.Sql;

internal static class CategoriaVehiculoSql
{
    internal const string ListarTodos = @"
        SELECT 
            ID_CATEGORIA AS IdCategoria,
            DESCRIPCION AS Descripcion,
            CODIGO AS Codigo
        FROM T_CATEGORIA_VEHICULO
        ORDER BY DESCRIPCION;
    ";

    internal const string ObtenerPorId = @"
        SELECT 
            ID_CATEGORIA AS IdCategoria,
            DESCRIPCION AS Descripcion,
            CODIGO AS Codigo
        FROM T_CATEGORIA_VEHICULO
        WHERE ID_CATEGORIA = @IdCategoria;
    ";

    internal const string ExisteCodigo = @"
        SELECT CASE WHEN EXISTS(
            SELECT 1 FROM T_CATEGORIA_VEHICULO 
            WHERE CODIGO = @Codigo 
            AND (@IdCategoriaExcluir IS NULL OR ID_CATEGORIA != @IdCategoriaExcluir)
        ) THEN 1 ELSE 0 END;
    ";

    internal const string Crear = @"
        INSERT INTO T_CATEGORIA_VEHICULO (DESCRIPCION, CODIGO)
        VALUES (@Descripcion, @Codigo);
        SELECT CAST(SCOPE_IDENTITY() AS INT);
    ";

    internal const string Actualizar = @"
        UPDATE T_CATEGORIA_VEHICULO
        SET DESCRIPCION = @Descripcion,
            CODIGO = @Codigo
        WHERE ID_CATEGORIA = @IdCategoria;
    ";

    internal const string Eliminar = @"
        DELETE FROM T_CATEGORIA_VEHICULO
        WHERE ID_CATEGORIA = @IdCategoria;
    ";
}
