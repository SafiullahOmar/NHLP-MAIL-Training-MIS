using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TMIS
{
    public class DatabaseCommunicator
    {
        SqlConnection connector;
        SqlCommand command;
        SqlDataAdapter adapter;
        SqlDataReader reader;
        DataTable data;
        string[] parameters;
        public DatabaseCommunicator()
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            connector = new SqlConnection(constr);
        }
        public DataTable SelectRecords(string query)
        {
            adapter = new SqlDataAdapter(query, connector);
            data = new DataTable();
            adapter.Fill(data);
            return data;
        }
        public void ExecuteQuery(string query)
        {
            command = new SqlCommand(query, connector);
            if (connector.State == ConnectionState.Closed)
                connector.Open();
            command.ExecuteNonQuery();
            connector.Close();
            SqlConnection.ClearPool(this.connector);
        }
        public DataTable selectProcedureExecuter(string procedureName)
        {
            command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;
            command.Connection = connector;

            if (connector.State == ConnectionState.Closed)
            {
                connector.Open();
            }
            SqlDataReader reader = command.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(reader);
            reader.Close();
            connector.Close();
            SqlConnection.ClearPool(this.connector);
            return data;
        }
        public DataTable ReturnDataTable(string[] parameters, string[] values, string procedureName)
        {

            SqlDataAdapter da = new SqlDataAdapter(procedureName, connector);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < parameters.Length; i++)
            {
                SqlParameter p = new SqlParameter(parameters[i], values[i]);
                da.SelectCommand.Parameters.Add(p);
            }
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlConnection.ClearPool(this.connector);
            return dt;
        }
        public DataTable selectProcedureExecuter(string[] parameters, string[] values, string procedureName)
        {
            command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;
            command.Connection = connector;

            for (int i = 0; i < parameters.Length; i++)
            {
                SqlParameter p = new SqlParameter(parameters[i], values[i]);
                command.Parameters.Add(p);
            }

            if (connector.State == ConnectionState.Closed)
            {
                connector.Open();
            }
            SqlDataReader reader = command.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(reader);
            reader.Close();
            connector.Close();
            SqlConnection.ClearPool(this.connector);
            return data;
        }
        public bool DML_SP_Executer(string[] p, string[] v, string procedure)
        {
            command = new SqlCommand(procedure, connector);
            command.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < p.Length; i++)
            {
                if (i == p.Length - 1)
                {
                    command.Parameters.Add(p[p.Length - 1].ToString(), SqlDbType.Bit);
                    command.Parameters[p[p.Length - 1]].Direction = ParameterDirection.Output;
                    //cmd.Parameters[p[p.Length-1]].Direction = ParameterDirection.Output;
                }
                else
                {
                    SqlParameter sp = new SqlParameter();
                    sp.ParameterName = p[i];
                    if (!String.IsNullOrWhiteSpace(v[i]))
                        sp.Value = v[i];
                    else
                        sp.Value = DBNull.Value;
                    command.Parameters.Add(sp);
                }
            }
            if (connector.State == ConnectionState.Closed)
                connector.Open();
            command.ExecuteNonQuery();
            bool r = (bool)command.Parameters[p.Length - 1].Value;
            connector.Close();
            SqlConnection.ClearPool(this.connector);
            return r;
        }
        public string DML_SP_Executer_string(string[] p, string[] v, string procedure)
        {
            command = new SqlCommand(procedure, connector);
            command.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < p.Length; i++)
            {
                if (i == p.Length - 1)
                {
                    command.Parameters.Add(p[p.Length - 1].ToString(), SqlDbType.NVarChar, 400);
                    command.Parameters[p[p.Length - 1]].Direction = ParameterDirection.Output;
                    //cmd.Parameters[p[p.Length-1]].Direction = ParameterDirection.Output;
                }
                else
                {
                    SqlParameter sp = new SqlParameter();
                    sp.ParameterName = p[i];
                    if (!String.IsNullOrWhiteSpace(v[i]))
                        sp.Value = v[i];
                    else
                        sp.Value = DBNull.Value;
                    command.Parameters.Add(sp);
                }
            }
            if (connector.State == ConnectionState.Closed)
                connector.Open();
            command.ExecuteNonQuery();
            string r = (string)command.Parameters[p.Length - 1].Value;
            connector.Close();
            SqlConnection.ClearPool(this.connector);
            return r;
        }
        public bool DML_SP_Executer(string[] p, string[] v, string procedure, DataTable dt)
        {
            command = new SqlCommand(procedure, connector);
            command.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < p.Length; i++)
            {
                if (i == p.Length - 1)
                {
                    SqlParameter spNew = command.Parameters.Add(p[p.Length - 1].ToString(), SqlDbType.Structured);
                    spNew.Value = dt;

                }
                else if (i == p.Length - 2)
                {
                    command.Parameters.Add(p[p.Length - 2].ToString(), SqlDbType.Bit);
                    command.Parameters[p[p.Length - 2]].Direction = ParameterDirection.Output;
                    //cmd.Parameters[p[p.Length-1]].Direction = ParameterDirection.Output;
                }
                else
                {
                    SqlParameter sp = new SqlParameter();
                    sp.ParameterName = p[i];
                    sp.Value = v[i];
                    command.Parameters.Add(sp);
                }
            }
            if (connector.State == ConnectionState.Closed)
                connector.Open();
            command.ExecuteNonQuery();
            bool r = (bool)command.Parameters[p.Length - 2].Value;
            connector.Close();
            SqlConnection.ClearPool(this.connector);
            return r;
        }

        

    }
}