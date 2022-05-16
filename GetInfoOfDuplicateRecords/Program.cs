using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using System.Net;
using System.Configuration;
using Microsoft.Xrm.Tooling.Connector;
using System.IO;

namespace GetInfoOfDuplicateRecords
{
    internal class Program
    {
        static void Main(string[] args)

        {

            Class1 getService = new Class1();
            CrmServiceClient service = getService.connect();

            if (service != null)
            {
                Console.WriteLine("Connection Established with crm");

            }
            Console.WriteLine("Connection Created Successfully...!");
            Console.WriteLine("Counting Records...!");

            QueryExpression QueryToSearchAccount = new QueryExpression()
            {
                EntityName = "account",
                ColumnSet = new ColumnSet(true)
            };
            EntityCollection entityCollection = service.RetrieveMultiple(QueryToSearchAccount);
            int NumberOfRecordsAvailableInEntity = entityCollection.Entities.Count;
            Console.WriteLine(NumberOfRecordsAvailableInEntity);
            foreach (Entity record in entityCollection.Entities)
            {
                var GUID = record.Id;
                if (record.Contains("zx_udaannumber"))
                {

                    //string GST_Number = !record.Contains("zx_gstno") ? "" : record.Attributes["zx_gstno"].ToString();
                    string Udaan_Number = record.Attributes["zx_udaannumber"].ToString();

                    QueryExpression QueryToSearchDuplicates = new QueryExpression()
                    {
                        EntityName = "account",
                        ColumnSet = new ColumnSet(true),
                        Criteria = new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                new ConditionExpression
                                {
                                    AttributeName = "zx_udaannumber",
                                    Operator = ConditionOperator.Equal,
                                    Values = { Udaan_Number }
                                }
                            }
                        }
                    };
                    
                    EntityCollection duplicateEntityCollection = service.RetrieveMultiple(QueryToSearchDuplicates);
                    int duplicateCounts = duplicateEntityCollection.Entities.Count;
                    if (duplicateCounts > 1)
                    {
                        Console.WriteLine("Duplicate counts of account record with Udaan Number" + " " + Udaan_Number + " " + "is" + " " + duplicateCounts);
                    }
                }
            }
            Console.WriteLine("Duplicate Count complete");
            Console.ReadLine();
        }
    }
}
