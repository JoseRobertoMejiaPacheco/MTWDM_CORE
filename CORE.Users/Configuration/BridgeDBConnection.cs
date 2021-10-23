
using Net.Core.Connection.Interfaces;
using Net.Core.Connection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Users.Tools;
using Net.Core.Connection;

namespace CORE.Users.Configuration
{
    public class BridgeDBConnection<T>
    {
        public static IConnectionDB<T> Create(string ConnectionString, DataBaseEnum DB)
        {
            //Se descifra la cadena de Conexion
            //return Factorizer<T>.Create(EncryptTool.Decrypt(ConnectionString), DB);
            return Factorizer<T>.Create(ConnectionString, DB);
        }
    }
}
    

