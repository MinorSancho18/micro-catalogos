namespace ExamenProcomerBackend.Infrastructure.Repositories.Sql;

internal static class VehiculoSql
{
    internal const string ListarTodos = @"
        SELECT 
            v.ID_VEHICULO AS IdVehiculo,
            v.ID_CATEGORIA AS IdCategoria,
            v.DESCRIPCION AS Descripcion,
            v.COSTO AS Costo,
            c.DESCRIPCION AS CategoriaDescripcion,
            c.CODIGO AS CategoriaCodigo
        FROM T_VEHICULO v
        INNER JOIN T_CATEGORIA_VEHICULO c ON v.ID_CATEGORIA = c.ID_CATEGORIA
        ORDER BY v.DESCRIPCION;
    ";

    internal const string ObtenerPorId = @"
        SELECT 
            v.ID_VEHICULO AS IdVehiculo,
            v.ID_CATEGORIA AS IdCategoria,
            v.DESCRIPCION AS Descripcion,
            v.COSTO AS Costo,
            c.DESCRIPCION AS CategoriaDescripcion,
            c.CODIGO AS CategoriaCodigo
        FROM T_VEHICULO v
        INNER JOIN T_CATEGORIA_VEHICULO c ON v.ID_CATEGORIA = c.ID_CATEGORIA
        WHERE v.ID_VEHICULO = @IdVehiculo;
    ";

    internal const string ListarPorCategoria = @"
        SELECT 
            v.ID_VEHICULO AS IdVehiculo,
            v.ID_CATEGORIA AS IdCategoria,
            v.DESCRIPCION AS Descripcion,
            v.COSTO AS Costo,
            c.DESCRIPCION AS CategoriaDescripcion,
            c.CODIGO AS CategoriaCodigo
        FROM T_VEHICULO v
        INNER JOIN T_CATEGORIA_VEHICULO c ON v.ID_CATEGORIA = c.ID_CATEGORIA
        WHERE v.ID_CATEGORIA = @IdCategoria
        ORDER BY v.DESCRIPCION;
    ";

    internal const string Crear = @"
        INSERT INTO T_VEHICULO (ID_CATEGORIA, DESCRIPCION, COSTO)
        VALUES (@IdCategoria, @Descripcion, @Costo);
        SELECT CAST(SCOPE_IDENTITY() AS INT);
    ";

    internal const string Actualizar = @"
        UPDATE T_VEHICULO
        SET ID_CATEGORIA = @IdCategoria,
            DESCRIPCION = @Descripcion,
            COSTO = @Costo
        WHERE ID_VEHICULO = @IdVehiculo;
    ";

    internal const string Eliminar = @"
        DELETE FROM T_VEHICULO
        WHERE ID_VEHICULO = @IdVehiculo;
    ";

    internal const string ExisteVehiculoPorCategoria = @"
        SELECT CASE WHEN EXISTS (
            SELECT 1 FROM T_VEHICULO 
            WHERE ID_CATEGORIA = @IdCategoria
        ) THEN 1 ELSE 0 END;
    ";
}
