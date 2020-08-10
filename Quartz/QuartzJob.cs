
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
    public class TestJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string username = context.JobDetail.JobDataMap.GetString("username");

            await Console.Out.WriteLineAsync("HelloJob is executing "+ username);

            string connectionString = "Server=localhost,10800;Database=AuctionHouseDB;User Id=sa;Password=Iep12345;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string sql_query = "Update AspNetUsers set firstName='" + "agbdlcid" + "' where UserName='"+username+"'";
            SqlCommand command = new SqlCommand(sql_query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand =  new SqlCommand(sql_query, connection);
            await adapter.UpdateCommand.ExecuteNonQueryAsync();

            command.Dispose();
            connection.Close();
        }
    }
}