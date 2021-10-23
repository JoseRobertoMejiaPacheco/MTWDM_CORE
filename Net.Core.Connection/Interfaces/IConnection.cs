using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Core.Connection.Interfaces
{
    public interface IConnectionDB<T> : IDisposable
    {
        public void PrepararProcedimiento(string nombreProcedimiento, List<Dapper.DynamicParameters> dynParameters, System.Data.CommandType enuTipoComando = System.Data.CommandType.StoredProcedure);
        public long ExecuteDapper();
        public T QueryFirstOrDefaultDapper();
        public IEnumerable<T> Query();
    }
}
