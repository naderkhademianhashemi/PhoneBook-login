using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entity;

namespace Business
{
    public class PhonesBusiness
    {
        private const string SP_PhoneById = "SP_PhoneById";
        private const string SP_UpdatePhone = "SP_UpdatePhone";
        private const string SP_AddPhone = "SP_AddPhone";
        private const string SP_DeletePhone = "SP_DeletePhone";
        private const string SP_Phone = "SP_Phone";

        public string GetConnectionString()
        {
            return Business.Properties.Settings.Default.ConStr;

        }
        public SqlConnection GetSqlConnection()
        {
            SqlConnection _connection = new SqlConnection(GetConnectionString());
            try
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }

            }
            catch (Exception ex)
            {

            }

            return _connection;
        }

        public Result<List<Phone>> GetPhoneList()
        {
            var res = new Result<List<Phone>>();

            var PhoneBook = new List<Phone>();
            
            SqlConnection Cnn = GetSqlConnection();
            var rdcmd = new SqlCommand(SP_Phone, Cnn);
            rdcmd.CommandType = CommandType.StoredProcedure;

            try
            {
                Cnn.Open();
                SqlDataReader dr = rdcmd.ExecuteReader();
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        Phone Ph = new Phone();
                        Ph.Id = (int)dr["Id"];
                        Ph.Name = (dr["Name"] == DBNull.Value) ? "" : (string)dr["Name"];
                        Ph.Tell = (dr["Tell"] == DBNull.Value) ? "" : (string)dr["Tell"];
                        Ph.Email = (dr["Email"] == DBNull.Value) ? "" : (string)dr["Email"];
                        Ph.Family = (dr["Family"] == DBNull.Value) ? "" : (string)dr["Family"];
                        //Ph.Tell = (string)dr[10];
                        PhoneBook.Add(Ph);
                    }
                Cnn.Close();
                Cnn.Dispose();
                res.Data = PhoneBook;
                res.IsSuccess = true;
                return res;
            }
            catch (Exception ex)
            {
                res.MSG = ex.Message;
                return res;
            }
            finally
            {
                rdcmd.Dispose();
            }

        }
        public Result<Phone> AddPhone(Phone Ph)
        {
            var res = new Result<Phone>();
            SqlConnection Cnn = GetSqlConnection();

            try
            {
                Cnn.Open();
                var InsertCmd = new SqlCommand(SP_AddPhone,Cnn);
                InsertCmd.CommandType = CommandType.StoredProcedure;
                InsertCmd.Parameters.AddWithValue("@Name", Ph.Name);
                InsertCmd.Parameters.AddWithValue("@Tell", Ph.Tell);
                InsertCmd.Parameters.AddWithValue("@Family", Ph.Family);
                InsertCmd.Parameters.AddWithValue("@Email", Ph.Email);
                InsertCmd.ExecuteNonQuery();
                InsertCmd.Dispose();
                Cnn.Close();
                Cnn.Dispose();
                res.IsSuccess = true;
                return res;
            }
            catch (Exception ex)
            {
                res.MSG = ex.Message;
                return res;
            }
            finally
            {

            }
        }


        public Result<Phone> UpdatePhone(Phone Ph)
        {
            var res = new Result<Phone>();

            SqlConnection Cnn = GetSqlConnection();
            var rdcmd = new SqlCommand(SP_PhoneById, Cnn);
            rdcmd.CommandType = CommandType.StoredProcedure;
            rdcmd.Parameters.AddWithValue("@Id", Ph.Id);

            try
            {
                Cnn.Open();
                SqlDataReader rd = rdcmd.ExecuteReader();
                if (rd.HasRows)
                {
                    var UpdCmd = new SqlCommand(SP_UpdatePhone, Cnn);
                    UpdCmd.CommandType = CommandType.StoredProcedure;

                    UpdCmd.Parameters.AddWithValue("@Id", Ph.Id);
                    UpdCmd.Parameters.AddWithValue("@Name", Ph.Name);
                    UpdCmd.Parameters.AddWithValue("@Tell", Ph.Tell);
                    UpdCmd.Parameters.AddWithValue("@Family", Ph.Family);
                    UpdCmd.Parameters.AddWithValue("@Email", Ph.Email);
                    rd.Close();
                    UpdCmd.ExecuteNonQuery();
                    UpdCmd.Dispose();
                }

                rd.Dispose();
                res.IsSuccess = true;
                return res;

            }
            catch (Exception ex)
            {
                res.MSG = ex.Message;
                return res;
            }
            finally
            {
                rdcmd.Dispose();
                Cnn.Close();
                Cnn.Dispose();
            }
        }


        public Result<Phone> DeletePhone(int id)
        {
            var res = new Result<Phone>();
            SqlConnection Cnn = GetSqlConnection();
            SqlCommand delcmd = new SqlCommand(SP_DeletePhone, Cnn);
            delcmd.Parameters.AddWithValue("@Id", id);
            delcmd.CommandType = CommandType.StoredProcedure;

            try
            {
                Cnn.Open();
                delcmd.ExecuteNonQuery();
                Cnn.Close();
                Cnn.Dispose();
                res.IsSuccess = true;
                return res;
            }
            catch (Exception ex)
            {
                res.MSG = ex.Message;
                return res;
            }
            finally
            {
                delcmd.Dispose();
            }

        }


    }
}
