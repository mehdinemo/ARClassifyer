using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace Classification
{
    class G
    {
        public static string ApplicationName = "Messagees_Concepts";
        public static int MaxTryToConnectToDatabase = 10;
        //Connection
        public static string ConnectionStringMrGhasempour_ConseptDB = "Data Source=programmer18,1433;Initial Catalog=Concepts;User id=sa;Password=1234;";
        public static string ConnectionStringTelegram = "Data Source = 192.168.30.98; Initial Catalog = TelegramCRW; Persist Security Info=True;User ID = site; Password=123@123";
        //Controls Font
        public static System.Drawing.Font Global_Font = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));


        public static int Messages_Status1_FetchedIntoApplication = 1;

        //update
        public static int UpdateWord(int PK_WID, string new_wordvalue)
        {
            if (PK_WID <= 0 || string.IsNullOrWhiteSpace(new_wordvalue) == true)
            {
                MessageBox.Show("G." + MethodBase.GetCurrentMethod().Name + ". " + "شناسه یا مقدار کلمه اشتباه است");
                return -1;
            }
            string query2 = @"update Words  set WORDVALUE=N'{0}' where PK_WId = ({1})";
            query2 = string.Format(query2, new_wordvalue, PK_WID);
            MessageBox.Show("G." + MethodBase.GetCurrentMethod().Name + ". " + query2);

            SqlConnection sqlConn2 = new SqlConnection(ConnectionStringMrGhasempour_ConseptDB);
            try
            {
                SqlCommand cmd2 = new SqlCommand(query2, sqlConn2);
                sqlConn2.Open();
                cmd2.ExecuteNonQuery();
                sqlConn2.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("G." + MethodBase.GetCurrentMethod().Name + ".خطا: " + e.ToString());
                sqlConn2.Close();
                return -1;
            }
            return 0;
        }

        //gets
        public static DataTable FetchXRecordsFromMessagesConcept(int SkipYRecords, int ReadXRecords, int Status)
        {
            DataTable result = new DataTable();
            //LogCat.AppendText("\n" + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "().Waiting on transactions executed by other instances...");
            string select_query = @"SELECT * FROM MessageConcept where status={0} OFFSET {1} ROWS  FETCH NEXT {2} ROWS ONLY;";
            select_query = string.Format(select_query, Status, SkipYRecords, ReadXRecords);

            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    SqlConnection select_Conn = new SqlConnection(G.ConnectionStringMrGhasempour_ConseptDB);
                    SqlCommand cmd2 = new SqlCommand(select_query, select_Conn);
                    select_Conn.Open();
                    SqlDataReader dr = cmd2.ExecuteReader();
                    result.Load(dr);
                    select_Conn.Close();
                    if (result.Rows.Count > 0)
                    {
                        G.ChangeStatusOfMessages(result, G.Messages_Status1_FetchedIntoApplication);
                    }
                    tran.Complete();
                }
                catch (TransactionAbortedException ex)
                {
                    MessageBox.Show("G." + MethodBase.GetCurrentMethod().Name + "().TransactionAbortedException Message: " + ex.Message);
                    return result;
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show("G." + MethodBase.GetCurrentMethod().Name + "().ApplicationException Message: " + ex.Message);
                    return result;
                }
            }
            return result;
        }
        public static DataTable FetchXRecordsFromConcepts(int SkipYRecords, int ReadXRecords, int Status)
        {
            DataTable result = new DataTable();
            //LogCat.AppendText("\n" + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "().Waiting on transactions executed by other instances...");
            string select_query = @"SELECT * FROM Concepts where status={0} OFFSET {1} ROWS  FETCH NEXT {2} ROWS ONLY;";
            select_query = string.Format(select_query, Status, SkipYRecords, ReadXRecords);

            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    SqlConnection select_Conn = new SqlConnection(G.ConnectionStringMrGhasempour_ConseptDB);
                    SqlCommand cmd2 = new SqlCommand(select_query, select_Conn);
                    select_Conn.Open();
                    SqlDataReader dr = cmd2.ExecuteReader();
                    result.Load(dr);
                    select_Conn.Close();
                    if (result.Rows.Count > 0)
                    {
                        G.ChangeStatusOfMessages(result, G.Messages_Status1_FetchedIntoApplication);
                    }
                    tran.Complete();
                }
                catch (TransactionAbortedException ex)
                {
                    MessageBox.Show("G." + MethodBase.GetCurrentMethod().Name + "().TransactionAbortedException Message: " + ex.Message);
                    return result;
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show("G." + MethodBase.GetCurrentMethod().Name + "().ApplicationException Message: " + ex.Message);
                    return result;
                }
            }
            return result;
        }
        public static DataTable fetchDuplicatesFromConceptsDB(string message)
        {
            DataTable dt = new DataTable();
            message = message.Trim();
            if (string.IsNullOrWhiteSpace(message) == true)
            {
                return dt;
            }
            string query = @"select * from Messages where length= {0}";
            query = string.Format(query, message.Length);
            //MessageBox.Show(query);
            try
            {
                SqlConnection select_Conn = new SqlConnection(G.ConnectionStringMrGhasempour_ConseptDB);
                SqlCommand cmd2 = new SqlCommand(query, select_Conn);
                select_Conn.Open();
                SqlDataReader dr = cmd2.ExecuteReader();
                dt.Load(dr);
                select_Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("G." + MethodBase.GetCurrentMethod().Name + "().ApplicationException Message: " + ex.Message);
                return dt;
            }
            return dt;
        }

        //changes
        public static int ChangeStatusOfMessages(DataTable dt, int status)
        {

            if (status >= 0 && dt.Rows.Count > 0)
            {
                string IDs = "-1";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    IDs += "," + row["Id"].ToString();
                }
                string query2 = @"update Messages set STATUS = {0} where Id in ({1})";
                query2 = string.Format(query2, status, IDs);

                SqlConnection sqlConn2 = new SqlConnection(G.ConnectionStringMrGhasempour_ConseptDB);
                try
                {
                    SqlCommand cmd2 = new SqlCommand(query2, sqlConn2);
                    sqlConn2.Open();
                    SqlDataReader dr = cmd2.ExecuteReader();
                    sqlConn2.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("G." + MethodBase.GetCurrentMethod().Name + ". " + e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sqlConn2.Close();
                    return -1;
                }
                return 0;
            }
            else
            {
                MessageBox.Show("G." + MethodBase.GetCurrentMethod().Name + ". " + "شناسه کلمه اشتباه است");
                return -1;
            }
        }
        public static int ChangeStatusOfMessages(int WordId, int status)
        {

            if (status >= 0 && WordId > 0)
            {
                string query2 = @"update Messages set STATUS = {0} where Id ={1};";
                query2 = string.Format(query2, status, WordId);

                SqlConnection sqlConn = new SqlConnection(G.ConnectionStringMrGhasempour_ConseptDB);
                try
                {
                    SqlCommand cmd2 = new SqlCommand(query2, sqlConn);
                    sqlConn.Open();
                    SqlDataReader dr = cmd2.ExecuteReader();
                    sqlConn.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("G." + MethodBase.GetCurrentMethod().Name + ". " + e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sqlConn.Close();
                    return -1;
                }
                return 0;
            }
            else
            {
                MessageBox.Show("G." + MethodBase.GetCurrentMethod().Name + ". " + "شناسه کلمه اشتباه است");
                return -1;
            }
        }

        //Checks       
        public static int checkMessageDuplicationInConceptsDB(string message)
        {
            message = message.Trim();
            DataTable dt = G.fetchDuplicatesFromConceptsDB(message);
            int id = -1;
            foreach (DataRow item in dt.Rows)
            {
                if (item["Message"].ToString().Trim() == message)
                {
                    id = int.Parse(item["id"].ToString());
                    break;
                }
            }
            return id;
        }
    }
}
