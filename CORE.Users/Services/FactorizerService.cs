using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Users.Configuration;
using CORE.Users.Interfaces;
using CORE.Users.Models;

namespace CORE.Users.Services
{
    public class FactorizerService
    {
        public static IUser Inicializar(EServer typeServer)
        {
            return typeServer switch
            {
                EServer.UDEFINED => throw new NullReferenceException(),
                EServer.LOCAL => new UserService(BridgeDBConnection<UserModel>.Create(ConnectionStrings.LocalServer, Net.Core.Connection.Models.DataBaseEnum.Sql)),
                EServer.CLOUD => new UserService(BridgeDBConnection<UserModel>.Create(ConnectionStrings.CloudServer, Net.Core.Connection.Models.DataBaseEnum.Sql)),
                _ => throw new NullReferenceException(),
            };

        }

        public static ILogin Login(EServer typeServer)
        {
            return typeServer switch
            {
                EServer.UDEFINED => throw new NullReferenceException(),
                EServer.LOCAL => new LoginService(BridgeDBConnection<LoginModel>.Create(ConnectionStrings.LocalServer, Net.Core.Connection.Models.DataBaseEnum.Sql)),
                EServer.CLOUD => new LoginService(BridgeDBConnection<LoginModel>.Create(ConnectionStrings.CloudServer, Net.Core.Connection.Models.DataBaseEnum.Sql)),
                _ => throw new NullReferenceException(),
            };

        }
    }
}
