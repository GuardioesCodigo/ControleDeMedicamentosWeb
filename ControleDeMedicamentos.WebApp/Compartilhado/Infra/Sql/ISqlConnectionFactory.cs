using Microsoft.Data.SqlClient;

namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Sql;

public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection();
}
