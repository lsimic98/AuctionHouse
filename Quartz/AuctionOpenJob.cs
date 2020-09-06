
using System;
using System.Linq;
using System.Threading.Tasks;
using AuctionHouse.Controllers;
using AuctionHouse.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Quartz;

namespace AuctionHouse.QuartzJobs
{
    public class AuctionOpenJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            int auctionId = context.JobDetail.JobDataMap.GetInt("AuctionId");

            await Console.Out.WriteLineAsync("HelloJob is executing "+ auctionId);

            string connectionString = "Server=localhost,10800;Database=AuctionHouseDB;User Id=sa;Password=Iep12345;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();


            //string sql_query = "Update AspNetUsers set firstName='" + "agbdlcid" + "' where UserName='"+username+"'";
            string sql_query = "UPDATE Auctions SET state=CASE WHEN state=@Ready then @Open else @Expired END WHERE Id=@Id";
            
            SqlCommand command = new SqlCommand(sql_query, connection);
            command.Parameters.AddWithValue("@Ready", "Ready");
            command.Parameters.AddWithValue("@Open", "Open");
            command.Parameters.AddWithValue("@Expired", "Expired");
            command.Parameters.AddWithValue("@Id", auctionId);
            
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand =  command;
            await adapter.UpdateCommand.ExecuteNonQueryAsync();

            command.Dispose();
            connection.Close();
        }
    }
}