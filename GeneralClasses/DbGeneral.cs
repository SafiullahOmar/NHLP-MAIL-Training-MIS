using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

namespace TMIS.GeneralClasses
{
    
    public class DbGeneral   
    {
        private SqlCommand tranCommand = new SqlCommand();
        protected SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        private SqlTransaction trans;
        private SqlDataAdapter da;
        private SqlCommand cmd;

        public DataTable SelectRecords(string query)
        {
          SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, this.con);
          DataTable dataTable = new DataTable();
          sqlDataAdapter.Fill(dataTable);
          return dataTable;
        }

        public void ExecuteQuery(string query)
        {
          SqlCommand sqlCommand = new SqlCommand(query, this.con);
          this.con.Open();
          sqlCommand.ExecuteNonQuery();
          this.con.Close();
        }

        public void ExecuteQuery(StringBuilder query)
        {
          SqlCommand sqlCommand = new SqlCommand(query.ToString(), this.con);
          if (this.con.State == ConnectionState.Closed)
            this.con.Open();
          sqlCommand.ExecuteNonQuery();
          this.con.Close();
        }

        public int SelectMaxID(string tblName, string colName)
        {
          SqlCommand sqlCommand = new SqlCommand("select isnull(max(" + colName + "),0)+1 from " + tblName, this.con);
          this.con.Open();
          int num = (int) sqlCommand.ExecuteScalar();
          this.con.Close();
          return num;
        }

        public SqlConnection Connection
        {
          get
          {
            return this.con;
          }
        }

        public void Reseed(string tablename, string colname)
        {
          SqlCommand sqlCommand = new SqlCommand("declare @ID int set @ID=(select isnull(max(" + colname + "),0) from " + tablename + ") DBCC CHECKIDENT (" + tablename + ", reseed, @ID)", this.con);
          this.con.Open();
          sqlCommand.ExecuteNonQuery();
          this.con.Close();
        }

        public void openConnection()
        {
          if (this.con.State != ConnectionState.Closed)
            return;
          this.con.Open();
        }

        public void BeginTransaction()
        {
          this.openConnection();
          this.tranCommand = new SqlCommand();
          this.tranCommand.Connection = this.con;
          this.trans = this.con.BeginTransaction();
          this.tranCommand.Transaction = this.trans;
        }

        public void EndTransaction()
        {
          this.trans.Commit();
          this.con.Close();
        }

        public void RollBackTransaction()
        {
          this.trans.Rollback();
        }

        public void ExecuteTransCommand(string query)
        {
          this.tranCommand.CommandText = query;
          this.tranCommand.ExecuteNonQuery();
        }

        public void tranReseed(string tablename, string colname)
        {
          this.tranCommand.CommandText = "declare @ID int set @ID=(select isnull(max(" + colname + "),0) from " + tablename + ") DBCC CHECKIDENT (" + tablename + ", reseed, @ID)";
          this.tranCommand.ExecuteNonQuery();
        }

        public DataTable ExecuteSelectTransCommand(string query)
        {
          DataTable dataTable = new DataTable();
          this.tranCommand.CommandText = query;
          new SqlDataAdapter(this.tranCommand).Fill(dataTable);
          return dataTable;
        }

        public string tranMaxID(string TableName, string ColumnName)
        {
          this.tranCommand.CommandText = "select isnull(Max(" + ColumnName + "),0)+1 from " + TableName;
          if (this.con.State == ConnectionState.Closed)
            this.openConnection();
          return this.tranCommand.ExecuteScalar().ToString();
        }

        public string ExecuteTranScaller(string query)
        {
          this.tranCommand.CommandText = query;
          this.openConnection();
          try
          {
            return this.tranCommand.ExecuteScalar().ToString();
          }
          catch (NullReferenceException ex)
          {
            return "0";
          }
        }

        public DataTable ExecuteSelectStoreProcedure(string procedureName, SqlParameter[] commandParameter, bool parm)
        {
          SqlCommand selectCommand = new SqlCommand();
          selectCommand.Connection = this.con;
          this.openConnection();
          selectCommand.CommandType = CommandType.StoredProcedure;
          selectCommand.CommandText = procedureName;
          if (parm)
          {
            foreach (SqlParameter sqlParameter in commandParameter)
              selectCommand.Parameters.Add(sqlParameter);
          }
          DataTable dataTable = new DataTable();
          new SqlDataAdapter(selectCommand).Fill(dataTable);
          return dataTable;
        }

        public void ExecuteTransStoreProcedure(string procedureName, SqlParameter[] commandParameter, bool parm)
        {
          this.tranCommand.CommandType = CommandType.StoredProcedure;
          this.tranCommand.CommandText = procedureName;
          this.tranCommand.Parameters.Clear();
          if (parm)
          {
            foreach (SqlParameter sqlParameter in commandParameter)
              this.tranCommand.Parameters.Add(sqlParameter);
          }
          this.tranCommand.ExecuteNonQuery();
        }

        public string ExecuteTransStoreProcedureReturn(string procedureName, SqlParameter[] commandParameter, bool parm, string ouputParm)
        {
          this.tranCommand.CommandType = CommandType.StoredProcedure;
          this.tranCommand.CommandText = procedureName;
          this.tranCommand.Parameters.Clear();
          if (parm)
          {
            foreach (SqlParameter sqlParameter in commandParameter)
              this.tranCommand.Parameters.Add(sqlParameter);
          }
          this.tranCommand.ExecuteNonQuery();
          return this.tranCommand.Parameters[ouputParm].Value.ToString();
        }

        public string[] ExecuteTransStoreProcedureReturnArray(string procedureName, SqlParameter[] commandParameter, bool parm, string[] ouputParm)
        {
          this.tranCommand.CommandType = CommandType.StoredProcedure;
          this.tranCommand.CommandText = procedureName;
          this.tranCommand.Parameters.Clear();
          if (parm)
          {
            foreach (SqlParameter sqlParameter in commandParameter)
              this.tranCommand.Parameters.Add(sqlParameter);
          }
          this.tranCommand.ExecuteNonQuery();
          string[] strArray = new string[ouputParm.Length];
          int index1 = 0;
          foreach (string index2 in ouputParm)
          {
            strArray[index1] = this.tranCommand.Parameters[index2].Value.ToString();
            ++index1;
          }
          return strArray;
        }

        public DataTable ExecuteTransStoreProcedureReturnTable(string procedureName, SqlParameter[] commandParameter, bool parm)
        {
          this.tranCommand.CommandType = CommandType.StoredProcedure;
          this.tranCommand.CommandText = procedureName;
          this.tranCommand.Parameters.Clear();
          if (parm)
          {
            foreach (SqlParameter sqlParameter in commandParameter)
              this.tranCommand.Parameters.Add(sqlParameter);
          }
          DataTable dataTable = new DataTable();
          new SqlDataAdapter(this.tranCommand).Fill(dataTable);
          return dataTable;
        }

        public string ExecuteScaller(string query)
        {
          SqlCommand sqlCommand = new SqlCommand(query, this.con);
          this.openConnection();
          try
          {
            return sqlCommand.ExecuteScalar().ToString();
          }
          catch (NullReferenceException ex)
          {
            return "0";
          }
        }

        public void FillListBox(ListBox lstbox, string sQuery, string VMember, string DMember)
        {
          this.da = new SqlDataAdapter(sQuery, this.con);
          DataTable dataTable = new DataTable();
          this.da.Fill(dataTable);
          lstbox.DataSource = (object) dataTable;
          lstbox.DataTextField = DMember;
          lstbox.DataValueField = VMember;
          lstbox.DataBind();
        }
  }
}