using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetInfoOfDuplicateRecords
{
    internal class Class1
    {
        public CrmServiceClient connect()
        {

            var url = ConfigurationManager.AppSettings["url"];
            var userName = ConfigurationManager.AppSettings["username"];
            var password = ConfigurationManager.AppSettings["password"];

            string conn = $@"  Url = {url}; AuthType = OAuth;
            UserName = {userName};
            Password = {password};
            AppId = 51f81489-12ee-4a9e-aaae-a2591f45987d;
            RedirectUri = app://58145B91-0C36-4500-8554-080854F2AC97;
            LoginPrompt=Auto;
            RequireNewInstance = True";


            var svc = new CrmServiceClient(conn);
            return svc;
        }
        public string getCity(string city)
        {
            int pos = city.IndexOf("-");
            string C = city.Insert(pos, " ");
            return C;

        }
    }
}
