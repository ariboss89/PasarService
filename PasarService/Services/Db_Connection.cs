using System;
namespace PasarService.Services
{
    public class Db_Connection
    {
        //string connStr = "server = localhost; User Id = root;" +
        //            "Persist Security Info = True; database = db_pasar; Password = ariboss89";

        string connStr = "server = host.docker.internal; User Id = root;" +
                    "Persist Security Info = True; database = db_pasar; Password = ariboss89";

        public string ConnectionString()
        {
            return connStr;
        }
    }
}
