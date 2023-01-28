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



    public class UserRecord
    {
        public int ID { get; set; }
        public string Code { get; set; } = "";
        public string Description { get; set; } = "";

        public string UserType { get; set; } = "";
        public DateTime CreatedAt { get; set; }


        public UserRecord() { }
        // Copy constructor
        public UserRecord(UserRecord other)
        {
            this.ID = other.ID;
            this.Code = other.Code;
            this.Description = other.Description;
            this.UserType = other.UserType;
            this.CreatedAt = other.CreatedAt;
        }


    }


    public class UserDB
    {
        //private readonly IConfiguration _configuration;

        public UserRecord MyUserRecord { get; set; } = new UserRecord();
        public string ErrorMessage { get; set; } = "";

        private SqlConnection? MConnection { get; set; }
        public UserDB(IConfiguration configuration)
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
        public bool pLogin(string Code, string UserPwd)
        {
            if (Strings.Trim(Code) == "" | Strings.Trim(UserPwd) == "")
            {
                ErrorMessage = "Invalid UserID";
                return false;
            }
            // Create Instance of Connection and Command Object
            SqlCommand myCommand = new SqlCommand("P_C_Login", MConnection);


            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterReturn_v = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4);
            parameterReturn_v.Direction = ParameterDirection.ReturnValue;
            myCommand.Parameters.Add(parameterReturn_v);


            SqlParameter parameterCode = new SqlParameter("@Code", SqlDbType.NVarChar, 30);
            parameterCode.Value = Code;
            myCommand.Parameters.Add(parameterCode);

            SqlParameter parameterPwd = new SqlParameter("@Pwd", SqlDbType.NVarChar, 100);
            parameterPwd.Value = UserPwd;
            myCommand.Parameters.Add(parameterPwd);

            // Open the connection and execute the Command
            int r = 1;
            try
            {
                MConnection.Open();
                myCommand.ExecuteNonQuery();
                MConnection.Close();
                r = System.Convert.ToInt32(parameterReturn_v.Value);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                MConnection.Close();
                return false;
            }
            return r == 100;
        }

        public bool pRecordFind(int xId, string xCode)
        {

            // Create Instance of Connection and Command Object

            SqlCommand myCommand = new SqlCommand("P_C_User_Fd", MConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.Int);
            parameterId.Value = xId;
            myCommand.Parameters.Add(parameterId);

            SqlParameter parameterCode = new SqlParameter("@Code", SqlDbType.NVarChar, 30);
            parameterCode.Value = xCode;
            myCommand.Parameters.Add(parameterCode);



            // Execute the command
            MConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);



            MyUserRecord.ID = 0;

            if (result.Read())
            {
                {
                    var withBlock = MyUserRecord;
                    // On Error Resume Next
                    withBlock.ID = (int)result["ID"];
                    withBlock.Code = (string)result["Code"];
                    withBlock.Description = (string)result["Description"];
                    withBlock.UserType = (string)result["UserType"];

                    withBlock.CreatedAt = (DateTime)result["createdAt"];

                }
            }
            // Return the datareader result
            result.Close();
            MConnection.Close();
            return MyUserRecord.ID > 0;
        }

        public SqlDataReader pRecordSearch(string xDesc, int xPageRow, int xpageindex)
        {


            // Create Instance of Connection and Command Object

            SqlCommand myCommand = new SqlCommand("P_C_User_Src", MConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterDesc = new SqlParameter("@Desc", SqlDbType.NVarChar, 100);
            parameterDesc.Value = xDesc;
            myCommand.Parameters.Add(parameterDesc);


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

        public bool pRecordUpdate(ref int xID, string xDescription, string xUserType, string xPwd, string xAction)
        {

            SqlCommand myCommand = new SqlCommand("P_c_user_Updt", MConnection);

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
            parameterDescription.Value = xDescription;
            myCommand.Parameters.Add(parameterDescription);

            SqlParameter parameterUserType = new SqlParameter("@UserType", SqlDbType.NVarChar, 100);
            parameterUserType.Value = xUserType;
            myCommand.Parameters.Add(parameterUserType);

            SqlParameter parameterPwd = new SqlParameter("@Pwd", SqlDbType.NVarChar, 100);
            parameterPwd.Value = xPwd;
            myCommand.Parameters.Add(parameterPwd);

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

        public bool pRecordAdd(string xCode,string xDescription, string xUserType, string xPwd)
        {

            SqlCommand myCommand = new SqlCommand("P_c_Login_Add", MConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterReturn_v = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4);
            parameterReturn_v.Direction = ParameterDirection.ReturnValue;
            myCommand.Parameters.Add(parameterReturn_v);

            SqlParameter parameterCode = new SqlParameter("@Code", SqlDbType.NVarChar, 100);
            parameterCode.Value = xCode;
            myCommand.Parameters.Add(parameterCode);

            SqlParameter parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 100);
            parameterDescription.Value = xDescription;
            myCommand.Parameters.Add(parameterDescription);

            SqlParameter parameterUserType = new SqlParameter("@UserType", SqlDbType.NVarChar, 100);
            parameterUserType.Value = xUserType;
            myCommand.Parameters.Add(parameterUserType);

            SqlParameter parameterPwd = new SqlParameter("@Pwd", SqlDbType.NVarChar, 100);
            parameterPwd.Value = xPwd;
            myCommand.Parameters.Add(parameterPwd);

            try
            {
                MConnection.Open();
                myCommand.ExecuteNonQuery();
                MConnection.Close();

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
