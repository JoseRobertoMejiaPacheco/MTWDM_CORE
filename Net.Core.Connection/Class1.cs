using Net.Core.Connection.Connections;
using Net.Core.Connection.Interfaces;

namespace Net.Core.Connection
{
    public class Factorizer<T>
    {
        public static IConnectionDB<T> Create(string ConnectionString, Models.DataBaseEnum DB)
        {
            return DB switch
            {
                Models.DataBaseEnum.Sql => SqlServer<T>.Conectar(ConnectionString),
                Models.DataBaseEnum.MySql => MySql<T>.Conectar(ConnectionString),
                _ => SqlServer<T>.Conectar(ConnectionString),
            };
        }
    }
}