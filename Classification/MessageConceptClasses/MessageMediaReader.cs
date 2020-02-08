using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification
{
    class MessageMediaStructure
    {
        public int Id;
        public string message;
        public int tagged;
        public int ConceptID;
    }
    class MessageMediaReader
    {
        List<MessageMediaStructure> MessagesList;

        public MessageMediaReader()
        {
            MessagesList = new List<MessageMediaStructure>();
        }

        public List<MessageMediaStructure> loadMessagesFromDataBase(int ConceptID, int type)
        {
            List<MessageMediaStructure> TempTable = new List<MessageMediaStructure>();
            if (ConceptID <= 0)
            {
                return TempTable;
            }

            //string select_query = @"select id,message,tagged " +
            //        "from(select * from Messages) a " +
            //        "inner join " +
            //        "(select MessageId,tagged  from MessageConcept where ConceptId = {0}) b " +
            //        "on a.Id= b.messageid";

            string select_query= "select ID, Message,[InConcept-00] " +
                    "from Relations b where ConceptID = {0} " +
                    "and (b.[InConcept-00] = {1} or b.[InConcept-00] = {2})";

            if(type!= -1)
            {
                select_query = string.Format(select_query, ConceptID, type, type);
            }            
            else
            {
                select_query = string.Format(select_query, ConceptID, 2, 1);
            }
                


            SqlConnection select_Conn = new SqlConnection(Utilities.UtilitiesClass.Conncection.ConnectionString_MrGhasempour_ConseptMediaDB);
            try
            {
                SqlCommand cmd2 = new SqlCommand(select_query, select_Conn);
                select_Conn.Open();
                SqlDataReader dr = cmd2.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(dr);
                for (int k = 0; k < result.Rows.Count; k++)
                {
                    MessageMediaStructure ms = new MessageMediaStructure();
                    ms.Id = int.Parse(result.Rows[k]["ID"].ToString());
                    ms.message = result.Rows[k]["Message"].ToString().Trim();
                    if (result.Rows[k]["InConcept-00"] != null)
                    {
                        string t = result.Rows[k]["InConcept-00"].ToString();
                        if (t.ToLower() == "1")
                        {
                            ms.tagged = 1;
                        }
                        else
                        {
                            ms.tagged = 0;
                        }
                        ms.ConceptID = ConceptID;
                        TempTable.Add(ms);
                    }
                    else
                    {
                        ms.tagged = -1;
                    }

                }
                select_Conn.Close();
            }
            catch (Exception)
            {
                select_Conn.Close();
            }
            return TempTable;
        }
        public List<MessageMediaStructure> loadMessagesFromDataBase(int ConceptID, int type, string where)
        {
            List<MessageMediaStructure> TempTable = new List<MessageMediaStructure>();
            if (ConceptID <= 0)
            {
                return TempTable;
            }
            where = " and (" + where + ") ";
            string select_query = "select ID, Message,[InConcept-00] " +
                    "from Relations b where ConceptID = {0} " +
                    "and (b.[InConcept-00] = {1} or b.[InConcept-00] = {2})";

            if (type != -1)
            {
                select_query = string.Format(select_query, ConceptID, type, type);
            }
            else
            {
                select_query = string.Format(select_query, ConceptID, 2, 1);
            }



            SqlConnection select_Conn = new SqlConnection(Utilities.UtilitiesClass.Conncection.ConnectionString_MrGhasempour_ConseptMediaDB);
            try
            {
                SqlCommand cmd2 = new SqlCommand(select_query, select_Conn);
                select_Conn.Open();
                SqlDataReader dr = cmd2.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(dr);
                for (int k = 0; k < result.Rows.Count; k++)
                {
                    MessageMediaStructure ms = new MessageMediaStructure();
                    ms.Id = int.Parse(result.Rows[k]["ID"].ToString());
                    ms.message = result.Rows[k]["Message"].ToString().Trim();
                    if (result.Rows[k]["InConcept-00"] != null)
                    {
                        string t = result.Rows[k]["InConcept-00"].ToString();
                        if (t.ToLower() == "1")
                        {
                            ms.tagged = 1;
                        }
                        else
                        {
                            ms.tagged = 0;
                        }
                        ms.ConceptID = ConceptID;
                        TempTable.Add(ms);
                    }
                    else
                    {
                        ms.tagged = -1;
                    }

                }
                select_Conn.Close();
            }
            catch (Exception)
            {
                select_Conn.Close();
            }
            return TempTable;
        }

        public List<MessageMediaStructure> loadMessagesFromDataBase(int ConceptID, int count, int type)
        {
            List<MessageMediaStructure> TempTable = new List<MessageMediaStructure>();
            if (ConceptID <= 0)
            {
                return TempTable;
            }
            //string select_query = @"select top({0}) id,message,tagged " +
            //    "from(select * from Messages ) a " +
            //    "inner join " +
            //    "(select MessageId,tagged  from MessageConcept where ConceptId = {1}) b " +
            //    "on a.Id= b.messageid";

            string select_query = "select top({0}) ID, Message,[InConcept-00] " +
                "from Relations b where ConceptID = {1} " +
                "and (b.[InConcept-00] = {2} or b.[InConcept-00] = {3})";

            if (type != -1)
            {
                select_query = string.Format(select_query, count, ConceptID, type, type);
            }
            else
            {
                select_query = string.Format(select_query, count, ConceptID,  2, 1);
            }

            SqlConnection select_Conn = new SqlConnection(Utilities.UtilitiesClass.Conncection.ConnectionString_MrGhasempour_ConseptMediaDB);
            try
            {
                SqlCommand cmd2 = new SqlCommand(select_query, select_Conn);
                select_Conn.Open();
                SqlDataReader dr = cmd2.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(dr);
                for (int k = 0; k < result.Rows.Count; k++)
                {
                    MessageMediaStructure ms = new MessageMediaStructure();
                    ms.Id = int.Parse(result.Rows[k]["ID"].ToString());
                    ms.message = result.Rows[k]["Message"].ToString().Trim();
                    if (result.Rows[k]["InConcept-00"] != null)
                    {
                        string t = result.Rows[k]["InConcept-00"].ToString();
                        if (t.ToLower() == "1")
                        {
                            ms.tagged = 1;
                        }
                        else
                        {
                            ms.tagged = 0;
                        }
                        ms.ConceptID = ConceptID;
                        TempTable.Add(ms);
                    }
                }
                select_Conn.Close();
            }
            catch (Exception)
            {
                select_Conn.Close();
            }
            return TempTable;
        }
        public List<MessageMediaStructure> loadMessagesFromDataBase(int ConceptID, int count, int type, string where)
        {
            List<MessageMediaStructure> TempTable = new List<MessageMediaStructure>();
            if (ConceptID <= 0)
            {
                return TempTable;
            }
            //string select_query = @"select top({0}) id,message,tagged " +
            //    "from(select * from Messages ) a " +
            //    "inner join " +
            //    "(select MessageId,tagged  from MessageConcept where ConceptId = {1}) b " +
            //    "on a.Id= b.messageid";
            where = " and (" + where + ") ";
            string select_query = "select top({0}) ID, Message,[InConcept-00] " +
                "from Relations b where ConceptID = {1} " +
                "and (b.[InConcept-00] = {2} or b.[InConcept-00] = {3})"+
                where;

            if (type != -1)
            {
                select_query = string.Format(select_query, count, ConceptID, type, type);
            }
            else
            {
                select_query = string.Format(select_query, count, ConceptID, 2, 1);
            }

            SqlConnection select_Conn = new SqlConnection(Utilities.UtilitiesClass.Conncection.ConnectionString_MrGhasempour_ConseptMediaDB);
            try
            {
                SqlCommand cmd2 = new SqlCommand(select_query, select_Conn);
                select_Conn.Open();
                SqlDataReader dr = cmd2.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(dr);
                for (int k = 0; k < result.Rows.Count; k++)
                {
                    MessageMediaStructure ms = new MessageMediaStructure();
                    ms.Id = int.Parse(result.Rows[k]["ID"].ToString());
                    ms.message = result.Rows[k]["Message"].ToString().Trim();
                    if (result.Rows[k]["InConcept-00"] != null)
                    {
                        string t = result.Rows[k]["InConcept-00"].ToString();
                        if (t.ToLower() == "1")
                        {
                            ms.tagged = 1;
                        }
                        else
                        {
                            ms.tagged = 0;
                        }
                        ms.ConceptID = ConceptID;
                        TempTable.Add(ms);
                    }
                }
                select_Conn.Close();
            }
            catch (Exception)
            {
                select_Conn.Close();
            }
            return TempTable;
        }

        public List<MessageMediaStructure> loadMessagesFromDataBase(int ConceptID, int Base, int count, int type)
        {
            List<MessageMediaStructure> TempTable = new List<MessageMediaStructure>();
            if (ConceptID <= 0)
            {
                return TempTable;
            }

            ////order by Id OFFSET {0}  ROWS  FETCH NEXT {1} ROWS ONLY
            //string select_query = @"select id,message,tagged " +
            //    "from(select * from Messages ) a " +
            //    "inner join " +
            //    "(select MessageId,tagged  from MessageConcept where ConceptId = {2} ) b " +
            //    "on a.Id= b.messageid";

            string select_query = "select ID, Message,[InConcept-00] " +
                "from Relations b where ConceptID = {0} " +
                "and (b.[InConcept-00] = {3} or b.[InConcept-00] = {4})" +
                " order by Id OFFSET {1}  ROWS  FETCH NEXT {2} ROWS ONLY";


            if (type != -1)
            {
                select_query = string.Format(select_query, Base, count, ConceptID, type, type);
            }
            else
            {
                select_query = string.Format(select_query, ConceptID, Base, count, 2    ,1);
            }

            SqlConnection select_Conn = new SqlConnection(Utilities.UtilitiesClass.Conncection.ConnectionString_MrGhasempour_ConseptMediaDB);
            try
            {
                SqlCommand cmd2 = new SqlCommand(select_query, select_Conn);
                select_Conn.Open();
                SqlDataReader dr = cmd2.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(dr);

                for (int k = 0; k < result.Rows.Count; k++)
                {
                    MessageMediaStructure ms = new MessageMediaStructure();
                    ms.Id = int.Parse(result.Rows[k]["ID"].ToString());
                    ms.message = result.Rows[k]["Message"].ToString().Trim();
                    if (result.Rows[k]["InConcept-00"] != null)
                    {
                        string t = result.Rows[k]["InConcept-00"].ToString();
                        if (t.ToLower() == "1")
                        {
                            ms.tagged = 1;
                        }
                        else
                        {
                            ms.tagged = 0;
                        }
                        ms.ConceptID = ConceptID;
                        TempTable.Add(ms);
                    }
                }
                select_Conn.Close();
            }
            catch (Exception)
            {
                select_Conn.Close();
            }
            return TempTable;
        }
        public List<MessageMediaStructure> loadMessagesFromDataBase(int ConceptID, int Base, int count, int type, string where)
        {
            List<MessageMediaStructure> TempTable = new List<MessageMediaStructure>();
            if (ConceptID <= 0)
            {
                return TempTable;
            }
            where = " and (" + where + ") ";
            ////order by Id OFFSET {0}  ROWS  FETCH NEXT {1} ROWS ONLY
            //string select_query = @"select id,message,tagged " +
            //    "from(select * from Messages ) a " +
            //    "inner join " +
            //    "(select MessageId,tagged  from MessageConcept where ConceptId = {2} ) b " +
            //    "on a.Id= b.messageid";

            string select_query = "select ID, Message,[InConcept-00] " +
                "from Relations b where ConceptID = {0} " +
                "and (b.[InConcept-00] = {3} or b.[InConcept-00] = {4})" +
                where +
                " order by Id OFFSET {1}  ROWS  FETCH NEXT {2} ROWS ONLY";


            if (type != -1)
            {
                select_query = string.Format(select_query, Base, count, ConceptID, type, type);
            }
            else
            {
                select_query = string.Format(select_query, ConceptID, Base, count, 2, 1);
            }

            SqlConnection select_Conn = new SqlConnection(Utilities.UtilitiesClass.Conncection.ConnectionString_MrGhasempour_ConseptMediaDB);
            try
            {
                SqlCommand cmd2 = new SqlCommand(select_query, select_Conn);
                select_Conn.Open();
                SqlDataReader dr = cmd2.ExecuteReader();
                DataTable result = new DataTable();
                result.Load(dr);

                for (int k = 0; k < result.Rows.Count; k++)
                {
                    MessageMediaStructure ms = new MessageMediaStructure();
                    ms.Id = int.Parse(result.Rows[k]["ID"].ToString());
                    ms.message = result.Rows[k]["Message"].ToString().Trim();
                    if (result.Rows[k]["InConcept-00"] != null)
                    {
                        string t = result.Rows[k]["InConcept-00"].ToString();
                        if (t.ToLower() == "1")
                        {
                            ms.tagged = 1;
                        }
                        else
                        {
                            ms.tagged = 0;
                        }
                        ms.ConceptID = ConceptID;
                        TempTable.Add(ms);
                    }
                }
                select_Conn.Close();
            }
            catch (Exception)
            {
                select_Conn.Close();
            }
            return TempTable;
        }


    }
}
