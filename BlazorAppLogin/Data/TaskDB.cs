using Microsoft.VisualBasic;
using System;
using System.Data.Common;

using System.Data;
using System.Data.SqlClient;
using BlazorAppLogin.Authentication;

using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Abstractions;
using static System.Collections.Specialized.BitVector32;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Windows.Input;

namespace BlazorAppLogin.Data
{

    public class TaskRecord
    {
        public int Task_ID { get; set; }
        public string Task_Category { get; set; } = "";
        public string Update_Name { get; set; } = "";
        public string In_Charge { get; set; } = "";
        public DateTime Created_Date { get; set; }
        public DateTime Completed_Date { get; set; } 


        public TaskRecord() { }
        // Copy constructor
        public TaskRecord(TaskRecord other)
        {
            this.Task_ID = other.Task_ID;
            this.Task_Category = other.Task_Category;
            this.Update_Name = other.Update_Name;
            this.In_Charge = other.In_Charge;
            this.Created_Date = other.Created_Date;
            this.Completed_Date = other.Completed_Date;
        }
    }

    public class TaskDB
    {
        //private readonly IConfiguration _configuration;

        public TaskRecord MyTaskRecord { get; set; } = new TaskRecord();
        public string ErrorMessage { get; set; } = "";

        private SqlConnection? MConnection { get; set; }
        public TaskDB(IConfiguration configuration)
        {



            string sqlconnectionstring = configuration.GetConnectionString("myDatabase");
            MConnection = new SqlConnection(sqlconnectionstring);

        }

        public void CloseConnection()
        {
            try
            {
                MConnection.Close();
            }
            catch
            {

            }

        }

        public bool pTaskFind(int xTkId, string xInCharge)
        {

            // Create Instance of Connection and Command Object

            SqlCommand myCommand = new SqlCommand("P_C_Task_Fd", MConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterTkId = new SqlParameter("@Task_ID", SqlDbType.Int);
            parameterTkId.Value = xTkId;
            myCommand.Parameters.Add(parameterTkId);

            SqlParameter parameterInChg = new SqlParameter("@In_Charge", SqlDbType.NVarChar, 30);
            parameterInChg.Value = xInCharge;
            myCommand.Parameters.Add(parameterInChg);

            // Execute the command
            MConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            MyTaskRecord.Task_ID = 0;

            if (result.Read())
            {
                {
                    var withBlock = MyTaskRecord;
                    // On Error Resume Next
                    withBlock.Task_ID = (int)result["Task_ID"];
                    withBlock.Task_Category = (string)result["Task_Category"];
                    withBlock.Update_Name = (string)result["Update_Name"];
                    withBlock.In_Charge = (string)result["In_Charge"];

                    withBlock.Created_Date = (DateTime)result["Created_Date"];
                    withBlock.Completed_Date = (DateTime)result["Completed_Date"];
                }
            }
            // Return the datareader result
            result.Close();
            MConnection.Close();
            return MyTaskRecord.Task_ID > 0;
        }

        public SqlDataReader pTaskSearch(string xSearch, int xPageRow, int xpageindex)
        {

            // Create Instance of Connection and Command Object

            SqlCommand myCommand = new SqlCommand("P_C_Task_Src", MConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterSearch = new SqlParameter("@search_text", SqlDbType.NVarChar, 100);
            parameterSearch.Value = xSearch;
            myCommand.Parameters.Add(parameterSearch);


            SqlParameter parameterPageRow = new SqlParameter("@PageRow", SqlDbType.Int);
            parameterPageRow.Value = xPageRow;
            myCommand.Parameters.Add(parameterPageRow);

            SqlParameter parameterpageindex = new SqlParameter("@pageindex", SqlDbType.Int);
            parameterpageindex.Value = xpageindex;
            myCommand.Parameters.Add(parameterpageindex);

            // Execute the command
            MConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader();

 
            // Return the datareader result
            return result;
        }

        public bool pTaskUpdate(ref int xID, string xSearch, string xAction)
        {

            SqlCommand myCommand = new SqlCommand("P_c_Task_Updt", MConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterReturn_v = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4);
            parameterReturn_v.Direction = ParameterDirection.ReturnValue;
            myCommand.Parameters.Add(parameterReturn_v);


            SqlParameter parameterID = new SqlParameter("@ID", SqlDbType.Int, 4);
            parameterID.Value = xID;
            parameterID.Direction = ParameterDirection.InputOutput;
            myCommand.Parameters.Add(parameterID);

            SqlParameter parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 100);
            parameterDescription.Value = xSearch;
            myCommand.Parameters.Add(parameterDescription);


            SqlParameter parameterAction = new SqlParameter("@Action", SqlDbType.NVarChar, 10);
            parameterAction.Value = xAction;
            myCommand.Parameters.Add(parameterAction);



            try
                {
                MConnection.Open();
                myCommand.ExecuteNonQuery();
                MConnection.Close();

                xID = (int)parameterID.Value;
                    }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                MConnection.Close();
                 return false;
            }

            return ((int)parameterReturn_v.Value == 0);

    }

    }
}
