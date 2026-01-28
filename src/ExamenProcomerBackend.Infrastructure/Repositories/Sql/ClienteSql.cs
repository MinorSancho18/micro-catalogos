namespace ExamenProcomerBackend.Infrastructure.Repositories.Sql;

internal static class ClienteSql
{
    public const string GetAll = @"
        SELECT ID_CLIENTE AS IdCliente, NOMBRE AS Nombre, NUMERO_CEDULA AS NumeroCedula
        FROM T_CLIENTE
        ORDER BY NOMBRE";

    public const string GetById = @"
        SELECT ID_CLIENTE AS IdCliente, NOMBRE AS Nombre, NUMERO_CEDULA AS NumeroCedula
        FROM T_CLIENTE
        WHERE ID_CLIENTE = @IdCliente";

    public const string GetByCedula = @"
        SELECT ID_CLIENTE AS IdCliente, NOMBRE AS Nombre, NUMERO_CEDULA AS NumeroCedula
        FROM T_CLIENTE
        WHERE NUMERO_CEDULA = @NumeroCedula";

    public const string ExistsCedula = @"
        SELECT CASE WHEN EXISTS (
            SELECT 1 FROM T_CLIENTE 
            WHERE NUMERO_CEDULA = @NumeroCedula 
            AND (@IdClienteExcluir IS NULL OR ID_CLIENTE != @IdClienteExcluir)
        ) THEN 1 ELSE 0 END";

    public const string Insert = @"
        INSERT INTO T_CLIENTE (NOMBRE, NUMERO_CEDULA)
        VALUES (@Nombre, @NumeroCedula);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

    public const string Update = @"
        UPDATE T_CLIENTE
        SET NOMBRE = @Nombre,
            NUMERO_CEDULA = @NumeroCedula
        WHERE ID_CLIENTE = @IdCliente";

    public const string Delete = @"
        DELETE FROM T_CLIENTE
        WHERE ID_CLIENTE = @IdCliente";
}
