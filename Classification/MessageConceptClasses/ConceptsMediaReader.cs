using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification
{

    class ConceptsMediaStructure
    {
        public int Id;
        public string Concept;
        public int ConceptId;
    }
    class Structure
    {
        public int Id=-1;
        public string overall="";
        public int tag=-1;
        
        //delimited by $
        public List<string> spc_list;

        public int ConceptID=-1;

        public Structure()
        {
            spc_list = new List<string>();
        }
    }
    class ConceptsMediaReader
    {
        List<ConceptsMediaStructure> ConceptsDataTable;

        public ConceptsMediaReader()
        {
            ConceptsDataTable = new List<ConceptsMediaStructure>();
        }
        public List<ConceptsMediaStructure> loadAllConceptsIntoMemory()
        {
            ConceptsDataTable = loadConceptsIntoMemory();
            if (ConceptsDataTable.Count <0)
            {
                Console.WriteLine("Could Not Load Concepts");
                return new List<ConceptsMediaStructure>();
            }
            return ConceptsDataTable;
        }

        //Count
        private int getConceptsTableWordsCount()
        {
            int words_count = 0;
            string query2 = "select count(*) from Concepts order by id";
            query2 = string.Format(query2);

            SqlConnection sqlConn2 = new SqlConnection(Utilities.UtilitiesClass.Conncection.ConnectionString_MrGhasempour_ConseptMediaDB);
            try
            {
                SqlCommand cmd2 = new SqlCommand(query2, sqlConn2);
                sqlConn2.Open();
                SqlDataReader dr = cmd2.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                words_count = int.Parse(dt.Rows[0][0].ToString());
                sqlConn2.Close();
            }
            catch (Exception)
            {
                sqlConn2.Close();
                return -1;
            }
            return words_count;
        }

        //Load
        private List<ConceptsMediaStructure> loadConceptsIntoMemory()
        {
            List<ConceptsMediaStructure> TempTable = new List<ConceptsMediaStructure>();
                string select_query = @"SELECT Id,Concept,ConceptId FROM Concepts ORDER BY ConceptId";

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
                        ConceptsMediaStructure ct = new ConceptsMediaStructure();
                        ct.Id = int.Parse(result.Rows[k]["Id"].ToString());
                        ct.Concept = result.Rows[k]["Concept"].ToString().Trim();
                        ct.ConceptId = int.Parse(result.Rows[k]["ConceptId"].ToString());
                        TempTable.Add(ct);
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
