using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace Utilities
{
    //Written By Mr Mohsen
    public static class UtilitiesClass
    {
        public static class Conncection
        {
            //Connection
            public static string ConnectionString_MrGhasempour_EFarsnetDB = "Data Source=programmer18,1433;Initial Catalog=efarsnet;    User id=sa;Password=1234;";
            public static string ConnectionString_MrGhasempour_ConseptDB = "Data Source=programmer18,1433;Initial Catalog=Concepts;    User id=sa;Password=1234;";
            public static string ConnectionString_MrGhasempour_ConseptMediaDB = "Data Source=programmer18,1433;Initial Catalog=ConceptMedia;User id=sa;Password=1234;";
            public static string ConnectionString_Telegram_30_98 = "Data Source = 192.168.30.98;  Initial Catalog = TelegramCRW; Persist Security Info=True;User ID = site; Password=123@123";
            public static string ConnectionString_Telegram_Server13 = "Data Source = server13; Initial Catalog = TelegramCRW; Persist Security Info=True;User ID = site; Password=123@123";


            //Fecth from DB Font
            public static DataTable FetchAllFromTable(string connectionString, string table_name)
            {
                DataTable result = new DataTable();
                if (UtilitiesClass.StringChecker.isEmpty(connectionString) || UtilitiesClass.StringChecker.isEmpty(table_name))
                {
                    return result;
                }

                string select_query = @"SELECT * FROM {0}";
                select_query = string.Format(select_query, table_name);
                try
                {
                    SqlConnection select_Conn = new SqlConnection(connectionString);
                    SqlCommand cmd2 = new SqlCommand(select_query, select_Conn);
                    select_Conn.Open();
                    SqlDataReader dr = cmd2.ExecuteReader();
                    result.Load(dr);
                    select_Conn.Close();
                }
                catch (TransactionAbortedException ex)
                {
                    return result;
                }
                return result;
            }
            public static DataTable FetchAllFromTableWhere(string connectionString, string table_name, string Where)
            {
                DataTable result = new DataTable();
                if (UtilitiesClass.StringChecker.isEmpty(connectionString) || UtilitiesClass.StringChecker.isEmpty(table_name))
                {
                    return result;
                }

                string select_query = @"SELECT * FROM {0} " + Where;
                select_query = string.Format(select_query, table_name);
                try
                {
                    SqlConnection select_Conn = new SqlConnection(connectionString);
                    SqlCommand cmd2 = new SqlCommand(select_query, select_Conn);
                    select_Conn.Open();
                    SqlDataReader dr = cmd2.ExecuteReader();
                    result.Load(dr);
                    select_Conn.Close();
                }
                catch (TransactionAbortedException ex)
                {
                    return result;
                }
                return result;
            }
            public static DataTable FetchRecordsFromTable(string connectionString, string table_name, int TopCount)
            {
                DataTable result = new DataTable();
                if (UtilitiesClass.StringChecker.isEmpty(connectionString) || UtilitiesClass.StringChecker.isEmpty(table_name))
                {
                    return result;
                }
                if (TopCount < 0)
                {
                    return result;
                }

                string select_query = @"SELECT top({0}) * FROM {1}";
                select_query = string.Format(select_query, TopCount, table_name);
                try
                {
                    SqlConnection select_Conn = new SqlConnection(connectionString);
                    SqlCommand cmd2 = new SqlCommand(select_query, select_Conn);
                    select_Conn.Open();
                    SqlDataReader dr = cmd2.ExecuteReader();
                    result.Load(dr);
                    select_Conn.Close();
                }
                catch (TransactionAbortedException ex)
                {
                    return result;
                }
                return result;
            }
            public static DataTable FetchRecordsFromTable(string connectionString, string table_name, string IDName, int startID, int endID)
            {
                DataTable result = new DataTable();
                if (UtilitiesClass.StringChecker.isEmpty(connectionString)
                    || UtilitiesClass.StringChecker.isEmpty(table_name)
                    || UtilitiesClass.StringChecker.isEmpty(IDName)
                    )
                {
                    return result;
                }

                string select_query = @"SELECT * FROM {0} where {1}>= {2} and {1}<={3}";
                select_query = string.Format(select_query, table_name, IDName, startID, endID);
                try
                {
                    SqlConnection select_Conn = new SqlConnection(connectionString);
                    SqlCommand cmd2 = new SqlCommand(select_query, select_Conn);
                    select_Conn.Open();
                    SqlDataReader dr = cmd2.ExecuteReader();
                    result.Load(dr);
                    select_Conn.Close();
                }
                catch (TransactionAbortedException ex)
                {
                    return result;
                }
                return result;
            }

        }
        public static class TextFileIO
        {
            //
            // Summary:
            //     Writes a list of strings into file
            //
            // Parameters:
            //   objA:
            //     File Name.
            //
            //   objB:
            //     A list of strings which to be written
            //
            //   objC:
            //     A flag that determines if it should append the content to the end of file or not.
            //
            // Returns:
            //     
            public static void writeToFile(string FileName, List<string> lines, bool Append)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(FileName, Append))
                    {
                        foreach (string item in lines)
                        {
                            sw.WriteLine(item);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("ذخیره سازی انجام نشد، دوباره امتحان کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            //Write list of strings to file
            //Setting Append to true causes it to append the content to the end of file.
            public static void writeToFile(string FileName, string content, bool Append)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(FileName, Append))
                    {
                        sw.WriteLine(content);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("ذخیره سازی انجام نشد، دوباره امتحان کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            //reads file content line by line and returns a list of string
            public static List<string> readFromFileLineByLine(string FileName, bool showMessages)
            {
                List<string> lines = new List<string>();
                try
                {
                    using (StreamReader sw = new StreamReader(FileName, false))
                    {
                        string item = "";
                        while ((item = sw.ReadLine()) != null)
                        {
                            if (string.IsNullOrEmpty(item.Trim()))
                            {
                                continue;
                            }
                            if (!lines.Contains(item))
                            {
                                lines.Add(item);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    if (showMessages)
                    {
                        MessageBox.Show("بارگزاری فایل انجام نشد، دوباره امتحان کنید. نام فایل: \n" + FileName, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
                return lines;
            }

            //reads all file content at once and returns a string as output
            public static string readFromFileAtOnce(string FileName)
            {
                string content = "";
                try
                {
                    using (StreamReader sw = new StreamReader(FileName))
                    {
                        content = sw.ReadToEnd();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("بارگزاری انجام نشد، دوباره امتحان کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return content;
            }
        }
        public static class ExcelFileIO
        {
            public static DataTable exceldata(string filePath)
            {
                DataTable dtexcel = new DataTable();
                bool hasHeaders = false;
                string HDR = hasHeaders ? "Yes" : "No";
                string strConn;
                if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
                else
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
                System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(strConn);
                conn.Open();
                DataTable schemaTable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                DataRow schemaRow = schemaTable.Rows[0];
                string sheet = schemaRow["TABLE_NAME"].ToString();
                if (!sheet.EndsWith("_"))
                {
                    string query = "SELECT  * FROM [" + sheet + "]";
                    System.Data.OleDb.OleDbDataAdapter daexcel = new System.Data.OleDb.OleDbDataAdapter(query, conn);
                    dtexcel.Locale = CultureInfo.CurrentCulture;
                    daexcel.Fill(dtexcel);
                }

                conn.Close();
                return dtexcel;

            }
            private static List<string> readexcell(string filePath, int SheetNumber, int ColumnNumber, ref string message)
            {
                List<string> ListOfexcelRows = new List<string>();

                Microsoft.Office.Interop.Excel.Application xlApp;
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook = null;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;

                xlApp = new Microsoft.Office.Interop.Excel.Application();
                try
                {
                    xlWorkBook = xlApp.Workbooks.Open(filePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                }
                catch
                {
                    MessageBox.Show("چنین فایلی ورودی وجود ندارد", "خطا");
                    message = ("چنین فایلی ورودی وجود ندارد: " + filePath);
                    return ListOfexcelRows;
                }

                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                int current_sheet = 1;
                //int[] Cols = { message_column, tag_column }; //Columns to loop
                foreach (Microsoft.Office.Interop.Excel.Worksheet worksheet in xlWorkBook.Worksheets)
                {
                    if (current_sheet++ != SheetNumber)
                    {
                        continue;
                    }
                    try
                    {
                        foreach (Microsoft.Office.Interop.Excel.Range row in worksheet.UsedRange.Rows)
                        {
                            try
                            {
                                string data = worksheet.Cells[row.Row, ColumnNumber].Value2.ToString().Trim();
                                ListOfexcelRows.Add(data);
                            }
                            catch
                            {
                                //MessageBox.Show("شماره ستون مورد نظر وجود ندارد", "خطا");
                                break;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("شییت مورد نظر وجود ندارد", "خطا");
                        message = ("شییت مورد نظر وجود ندارد");
                    }
                    break;
                }
                if (ListOfexcelRows.Count == 0)
                {
                    message = ("اشکالی وجود دارد. یا شماره صفحه یا ستون اشتباه است");
                }
                return ListOfexcelRows;
            }
            private static bool writeToExcell(List<string> input, string filePath, int SheetNumber, int ColumnNumber)
            {
                if (!System.IO.File.Exists(filePath))
                {
                    if (!createExcellFile(filePath))
                    {
                        MessageBox.Show("فایل " + filePath + " وجود ندارد و ایجاد نشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                try
                {
                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

                    Microsoft.Office.Interop.Excel.Workbook workbook =
                        excel.Workbooks.Open(filePath,
                        ReadOnly: false,
                        Editable: true);

                    Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.Worksheets.Item[1] as Microsoft.Office.Interop.Excel.Worksheet;
                    if (worksheet == null)
                        return false;

                    int current_sheet = 1;

                    foreach (Microsoft.Office.Interop.Excel.Worksheet wrksht in workbook.Worksheets)
                    {

                        if (current_sheet++ != SheetNumber)
                        {
                            continue;
                        }
                        for (int i = 1; i <= input.Count; i++)
                        {
                            Microsoft.Office.Interop.Excel.Range row = wrksht.Rows.Cells[i, ColumnNumber];
                            row.Value = input[i - 1];
                        }
                        excel.Application.ActiveWorkbook.Save();
                        workbook.Close(1);
                        excel.Application.Quit();
                        excel.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                        break;
                    }
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            private static bool createExcellFile(string file_name)
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                if (xlApp == null)
                {
                    MessageBox.Show("Excel is not properly installed!!");
                    return false;
                }

                try
                {
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;

                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    xlWorkSheet.Cells[1, 1] = "filed";


                    xlWorkBook.SaveAs(file_name, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault);
                    xlWorkBook.Close(1);
                    xlApp.Application.Quit();
                    xlApp.Quit();

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }
        public static class CsvFileIO
        {
            /// <summary>
            /// Returns All Columns of a CSV File as a DataTable
            /// </summary>
            /// <param name="path">Absolute Path To The CSV File</param>
            /// <param name="delimiter">Delimiter of Columns</param>
            /// <param name="isFirstRowHeader">Is First Row Header?</param>
            /// <returns></returns>
            public static DataTable GetDataTableFromCsv(string path, string delimiter, bool isFirstRowHeader)
            {
                DataTable workTable = new DataTable("DT");

                List<string> lines = Utilities.UtilitiesClass.TextFileIO.readFromFileLineByLine(path, true);
                if ((lines.Count == 0) || (lines.Count == 1 && isFirstRowHeader))
                {
                    return workTable;
                }

                List<List<string>> all = new List<List<string>>();
                if (isFirstRowHeader)
                {
                    lines.RemoveAt(0);
                }


                int columnCount = Regex.Split(lines[0], delimiter).ToList().Count();
                for (int i = 1; i <= columnCount; i++)
                {
                    workTable.Columns.Add(i.ToString(), typeof(string));
                }


                for (int i = 0; i < lines.Count; i++)
                {
                    string[] list = Regex.Split(lines[i], delimiter).ToArray();
                    if (list.Length == 0)
                    {
                        continue;
                    }

                    DataRow dr = workTable.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = list[j];
                    }
                    workTable.Rows.Add(dr);
                }
                return workTable;
            }


            /// <summary>
            /// Returns All Desired Columns of a CSV File as a DataTable
            /// </summary>
            /// <param name="path">Absolute Path To The CSV File</param>
            /// <param name="delimiter">Delimiter of Columns</param>
            /// <param name="isFirstRowHeader">Is First Row Header?</param>
            /// <param name="columns">List All Desired Columns e.g. 1,2,4,6</param>
            /// <returns></returns>
            public static DataTable GetDataTableFromCsv(string path, string delimiter, bool isFirstRowHeader, params int[] columns)
            {
                DataTable workTable = new DataTable("DT");
                try
                {
                    if (Utilities.UtilitiesClass.StringChecker.isEmpty(delimiter))
                    {
                        delimiter = ",";
                    }
                    columns = columns.ToList().Distinct().ToArray();
                    

                    List<string> lines = Utilities.UtilitiesClass.TextFileIO.readFromFileLineByLine(path, true);
                    if ((lines.Count == 0) || (lines.Count == 1 && isFirstRowHeader))
                    {
                        return workTable;
                    }

                    List<List<string>> all = new List<List<string>>();
                    if (isFirstRowHeader)
                    {
                        //int columnCount = Regex.Split(lines[0], delimiter).ToList().Count();
                        string[] header_list = Regex.Split(lines[0], delimiter).ToArray();
                        for (int i = 0; i < columns.Count(); i++)
                        {
                            workTable.Columns.Add(header_list[columns[i] - 1].ToString(), typeof(string));
                        }
                        lines.RemoveAt(0);
                    }
                    else
                    {
                        int columnCount = Regex.Split(lines[0], delimiter).ToList().Count();
                        for (int i = 1; i <= columnCount; i++)
                        {
                            workTable.Columns.Add("Col " + i.ToString(), typeof(string));
                        }
                    }





                    for (int i = 0; i < lines.Count; i++)
                    {
                        string[] list = Regex.Split(lines[i], delimiter).ToArray();
                        if (list.Length == 0)
                        {
                            continue;
                        }
                        //List<string> desiredCols = new List<string>();
                        //foreach (var item in columns)
                        //{
                        //    desiredCols.Add(list[item]);
                        //}
                        DataRow dr = workTable.NewRow();
                        for (int j = 0; j < columns.Count(); j++)
                        {
                            dr[j] = list[columns[j] - 1];
                        }
                        workTable.Rows.Add(dr);
                    }
                    return workTable;
                }
                catch
                {
                    return workTable;
                }
            }
        }
        public static class ArabicConverter
        {
            // Added By Mr Mohsen
            //'۰', '؟', '+', '?', '!', '‍',       'Å', '{', '}', 'ږ', '【', '】', 'ﻵ', 'ﭗ', '〈', '〉', '﴾', '﴿', 'ۂ', 'ټ', 'ۺ', 'ݜ', 'ڕ', 'ګ', 'ݩ', 'ﺂ', 'ʍ', 'ﺙ', 'ﯠ', 'ڰ', 'ﮪ', '۾', 'ݐ', 'ﮁ', 'ۍ', 'ݕ','‼', 'ٹ', 'ݧ'
            //'.', '?', '+', '?', '!', '‍\u200c', 'A', '{', '}', 'ر', '[', ']',   'لا', 'پ', '<', '>',   '(', ')', 'ه', 'ت', 'ش', 'ش', 'ر', 'ک', 'ن', 'ا', 'M', 'ث', 'و', 'گ', 'ه', 'م', 'پ', 'چ', 'ی', 'پ','!', 'ت', 'ن'
            //chrFrom = new char[] { '،', '(', ')', '\t', '\u200c', ' ', '0', '1', '۱', '١', '2', '۲', '٢', '3', '۳', '٣', '4', '۴', '٤', '5', '۵', '٥', '6', '۶', '٦', '7', '۷', '٧', '8', '۸', '٨', '9', '۹', '٩', '.', ':', '%', '/', '@', '#', '_', 'a', 'A', 'b', 'B', 'c', 'C', 'D', 'd', 'e', 'E', 'f', 'F', 'g', 'G', 'h', 'H', 'i', 'I', 'j', 'J', 'k', 'K', 'l', 'L', 'm', 'M', 'n', 'N', 'o', 'O', 'p', 'P', 'Q', 'q', 'r', 'R', 's', 'S', 't', 'T', 'u', 'U', 'v', 'V', 'w', 'W', 'x', 'X', 'y', 'Y', 'z', 'Z', 'ء', 'ﺀ', 'ٱ', 'ا', 'ﺍ', 'ﺎ', 'إ', 'ﺇ', 'أ', 'ﺃ', 'ﺄ', 'آ', 'ﺁ', 'ب', 'ﺑ', 'ﺒ', 'ﺏ', 'ﺐ', 'پ', 'ﭘ', 'ﭙ', 'ة', 'ﺔ', 'ﺓ', 'ت', 'ﺘ', 'ﺖ', 'ﺗ', 'ﺕ', 'ٺ', 'ث', 'ﺛ', 'ﺜ', 'ﺚ', 'ج', 'ﺟ', 'ﺠ', 'ﺝ', 'ﺞ', 'چ', 'ﭼ', 'ﭽ', 'ﭻ', 'ح', 'ﺣ', 'ﺤ', 'ﺡ', 'ﺢ', 'خ', 'ﺧ', 'ﺨ', 'ﺦ', 'ﺥ', 'د', 'ﺩ', 'ﺪ', 'ذ', 'ﺬ', 'ﺫ', 'ر', 'ﺭ', 'ﺮ', 'ز', 'ﺯ', 'ﺰ', 'ژ', 'ﮋ', 'ﮊ', 'س', 'ﺳ', 'ﺴ', 'ﺲ', 'ﺱ', 'ش', 'ﺷ', 'ﺸ', 'ﺶ', 'ﺵ', 'ص', 'ﺻ', 'ﺼ', 'ﺺ', 'ﺹ', 'ض', 'ﻀ', 'ﺿ', 'ﺽ', 'ﺾ', 'ط', 'ﻃ', 'ﻄ', 'ﻂ', 'ﻁ', 'ظ', 'ﻈ', 'ﻇ', 'ﻆ', 'ع', 'ﻋ', 'ﻌ', 'ﻊ', 'ﻉ', 'غ', 'ﻏ', 'ﻐ', 'ﻍ', 'ﻎ', 'ف', 'ﻓ', 'ﻔ', 'ﻒ', 'ﻑ', 'ق', 'ﻗ', 'ﻘ', 'ﻖ', 'ﻕ', 'ك', 'ﻛ', 'ﻜ', 'ﻙ', 'ﻚ', 'ک', 'ﮐ', 'ﮑ', 'ﮏ', 'ﮎ', 'ڪ', 'گ', 'ﮔ', 'ﮕ', 'ﮓ', 'ﮒ', 'ل', 'ﻝ', 'ﻞ', 'ڵ', 'ﻼ', 'ﻻ', 'م', 'ﻣ', 'ﻤ', 'ﻢ', 'ﻡ', 'ن', 'ﻧ', 'ﻨ', 'ﻥ', 'ﻦ', 'ڹ', 'ه', 'ﻪ', 'ﻫ', 'ﻩ', 'ﻬ', 'ہ', 'ھ', 'و', 'ﻭ', 'ﻮ', 'ۊ', 'ؤ', 'ﺆ', 'ۆ', 'ۇ', 'ى', 'ﻰ', 'ﻯ', 'ي', 'ﻴ', 'ﻳ', 'ﻲ', 'ﻱ', 'ێ', 'ے', 'ی', 'ﯿ', 'ﯾ', 'ﯽ', 'ﯼ', 'ئ', 'ﺋ', 'ﺌ', 'ە', 'ۀ' , '۰', '؟', '+', '?', '!', '‍', 'Å', '{', '}', 'ږ', '【', '】', 'ﻵ', 'ﭗ', '〈', '〉', '﴾', '﴿', 'ۂ', 'ټ', 'ۺ', 'ݜ', 'ڕ', 'ګ', 'ݩ', 'ﺂ', 'ʍ', 'ﺙ', 'ﯠ', 'ڰ', 'ﮪ', '۾', 'ݐ', 'ﮁ', 'ۍ', 'ݕ', '‼', 'ٹ', 'ݧ' };
            //chrtoto = new char[] { '،', '(', ')', '\t', '\u200c', ' ', '0', '1', '1', '1', '2', '2', '2', '3', '3', '3', '4', '4', '4', '5', '5', '5', '6', '6', '6', '7', '7', '7', '8', '8', '8', '9', '9', '9', '.', ':', '%', '/', '@', '#', '_', 'a', 'a', 'b', 'b', 'c', 'c', 'd', 'd', 'e', 'e', 'f', 'f', 'g', 'g', 'h', 'h', 'i', 'i', 'j', 'j', 'k', 'k', 'l', 'l', 'm', 'm', 'n', 'n', 'o', 'o', 'p', 'p', 'q', 'q', 'r', 'r', 's', 's', 't', 't', 'u', 'u', 'v', 'v', 'w', 'w', 'x', 'x', 'y', 'y', 'z', 'z', 'ء', 'ء', 'ا', 'ا', 'ا', 'ا', 'ا', 'ا', 'ا', 'ا', 'ا', 'آ', 'آ', 'ب', 'ب', 'ب', 'ب', 'ب', 'پ', 'پ', 'پ', 'ت', 'ت', 'ت', 'ت', 'ت', 'ت', 'ت', 'ت', 'ث', 'ث', 'ث', 'ث', 'ث', 'ج', 'ج', 'ج', 'ج', 'ج', 'چ', 'چ', 'چ', 'چ', 'ح', 'ح', 'ح', 'ح', 'ح', 'خ', 'خ', 'خ', 'خ', 'خ', 'د', 'د', 'د', 'ذ', 'ذ', 'ذ', 'ر', 'ر', 'ر', 'ز', 'ز', 'ز', 'ژ', 'ژ', 'ژ', 'س', 'س', 'س', 'س', 'س', 'ش', 'ش', 'ش', 'ش', 'ش', 'ص', 'ص', 'ص', 'ص', 'ص', 'ض', 'ض', 'ض', 'ض', 'ض', 'ط', 'ط', 'ط', 'ط', 'ط', 'ظ', 'ظ', 'ظ', 'ظ', 'ع', 'ع', 'ع', 'ع', 'ع', 'غ', 'غ', 'غ', 'غ', 'غ', 'ف', 'ف', 'ف', 'ف', 'ف', 'ق', 'ق', 'ق', 'ق', 'ق', 'ک', 'ک', 'ک', 'ک', 'ک', 'ک', 'ک', 'ک', 'ک', 'ک', 'ک', 'گ', 'گ', 'گ', 'گ', 'گ', 'ل', 'ل', 'ل', 'ل', 'ﻻ', 'ﻻ', 'م', 'م', 'م', 'م', 'م', 'ن', 'ن', 'ن', 'ن', 'ن', 'ن', 'ه', 'ه', 'ه', 'ه', 'ه', 'ه', 'ه', 'و', 'و', 'و', 'و', 'و', 'و', 'و', 'و', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ئ', 'ئ', 'ئ', 'ه', 'ه' , '.', '?', '+', '?', '!', '‍\u200c', 'A', '{', '}', 'ر', '[', ']', 'لا', 'پ', '<', '>', '(', ')', 'ه', 'ت', 'ش', 'ش', 'ر', 'ک', 'ن', 'ا', 'M', 'ث', 'و', 'گ', 'ه', 'م', 'پ', 'چ', 'ی', 'پ', '!', 'ت', 'ن' };

            private static char[] chrFrom = new char[] { '،', '(', ')', '\t', '\u200c', ' ', '0', '1', '۱', '١', '2', '۲', '٢', '3', '۳', '٣', '4', '۴', '٤', '5', '۵', '٥', '6', '۶', '٦', '7', '۷', '٧', '8', '۸', '٨', '9', '۹', '٩', '.', ':', '%', '/', '@', '#', '_', 'a', 'A', 'b', 'B', 'c', 'C', 'D', 'd', 'e', 'E', 'f', 'F', 'g', 'G', 'h', 'H', 'i', 'I', 'j', 'J', 'k', 'K', 'l', 'L', 'm', 'M', 'n', 'N', 'o', 'O', 'p', 'P', 'Q', 'q', 'r', 'R', 's', 'S', 't', 'T', 'u', 'U', 'v', 'V', 'w', 'W', 'x', 'X', 'y', 'Y', 'z', 'Z', 'ء', 'ﺀ', 'ٱ', 'ا', 'ﺍ', 'ﺎ', 'إ', 'ﺇ', 'أ', 'ﺃ', 'ﺄ', 'آ', 'ﺁ', 'ب', 'ﺑ', 'ﺒ', 'ﺏ', 'ﺐ', 'پ', 'ﭘ', 'ﭙ', 'ة', 'ﺔ', 'ﺓ', 'ت', 'ﺘ', 'ﺖ', 'ﺗ', 'ﺕ', 'ٺ', 'ث', 'ﺛ', 'ﺜ', 'ﺚ', 'ج', 'ﺟ', 'ﺠ', 'ﺝ', 'ﺞ', 'چ', 'ﭼ', 'ﭽ', 'ﭻ', 'ح', 'ﺣ', 'ﺤ', 'ﺡ', 'ﺢ', 'خ', 'ﺧ', 'ﺨ', 'ﺦ', 'ﺥ', 'د', 'ﺩ', 'ﺪ', 'ذ', 'ﺬ', 'ﺫ', 'ر', 'ﺭ', 'ﺮ', 'ز', 'ﺯ', 'ﺰ', 'ژ', 'ﮋ', 'ﮊ', 'س', 'ﺳ', 'ﺴ', 'ﺲ', 'ﺱ', 'ش', 'ﺷ', 'ﺸ', 'ﺶ', 'ﺵ', 'ص', 'ﺻ', 'ﺼ', 'ﺺ', 'ﺹ', 'ض', 'ﻀ', 'ﺿ', 'ﺽ', 'ﺾ', 'ط', 'ﻃ', 'ﻄ', 'ﻂ', 'ﻁ', 'ظ', 'ﻈ', 'ﻇ', 'ﻆ', 'ع', 'ﻋ', 'ﻌ', 'ﻊ', 'ﻉ', 'غ', 'ﻏ', 'ﻐ', 'ﻍ', 'ﻎ', 'ف', 'ﻓ', 'ﻔ', 'ﻒ', 'ﻑ', 'ق', 'ﻗ', 'ﻘ', 'ﻖ', 'ﻕ', 'ك', 'ﻛ', 'ﻜ', 'ﻙ', 'ﻚ', 'ک', 'ﮐ', 'ﮑ', 'ﮏ', 'ﮎ', 'ڪ', 'گ', 'ﮔ', 'ﮕ', 'ﮓ', 'ﮒ', 'ل', 'ﻝ', 'ﻞ', 'ڵ', 'ﻼ', 'ﻻ', 'م', 'ﻣ', 'ﻤ', 'ﻢ', 'ﻡ', 'ن', 'ﻧ', 'ﻨ', 'ﻥ', 'ﻦ', 'ڹ', 'ه', 'ﻪ', 'ﻫ', 'ﻩ', 'ﻬ', 'ہ', 'ھ', 'و', 'ﻭ', 'ﻮ', 'ۊ', 'ؤ', 'ﺆ', 'ۆ', 'ۇ', 'ى', 'ﻰ', 'ﻯ', 'ي', 'ﻴ', 'ﻳ', 'ﻲ', 'ﻱ', 'ێ', 'ے', 'ی', 'ﯿ', 'ﯾ', 'ﯽ', 'ﯼ', 'ئ', 'ﺋ', 'ﺌ', 'ە', 'ۀ', '۰', '؟', '+', '?', '!', '‍', 'Å', '{', '}', 'ږ', '【', '】', 'ﭗ', '〈', '〉', '﴾', '﴿', 'ۂ', 'ټ', 'ۺ', 'ݜ', 'ڕ', 'ګ', 'ݩ', 'ﺂ', 'ʍ', 'ﺙ', 'ﯠ', 'ڰ', 'ﮪ', '۾', 'ݐ', 'ﮁ', 'ۍ', 'ݕ', '‼', 'ٹ', 'ݧ', '"' };
            private static char[] chrtoto = new char[] { '،', '(', ')', '\t', '\u200c', ' ', '0', '1', '1', '1', '2', '2', '2', '3', '3', '3', '4', '4', '4', '5', '5', '5', '6', '6', '6', '7', '7', '7', '8', '8', '8', '9', '9', '9', '.', ':', '%', '/', '@', '#', '_', 'a', 'a', 'b', 'b', 'c', 'c', 'd', 'd', 'e', 'e', 'f', 'f', 'g', 'g', 'h', 'h', 'i', 'i', 'j', 'j', 'k', 'k', 'l', 'l', 'm', 'm', 'n', 'n', 'o', 'o', 'p', 'p', 'q', 'q', 'r', 'r', 's', 's', 't', 't', 'u', 'u', 'v', 'v', 'w', 'w', 'x', 'x', 'y', 'y', 'z', 'z', 'ء', 'ء', 'ا', 'ا', 'ا', 'ا', 'ا', 'ا', 'ا', 'ا', 'ا', 'آ', 'آ', 'ب', 'ب', 'ب', 'ب', 'ب', 'پ', 'پ', 'پ', 'ت', 'ت', 'ت', 'ت', 'ت', 'ت', 'ت', 'ت', 'ث', 'ث', 'ث', 'ث', 'ث', 'ج', 'ج', 'ج', 'ج', 'ج', 'چ', 'چ', 'چ', 'چ', 'ح', 'ح', 'ح', 'ح', 'ح', 'خ', 'خ', 'خ', 'خ', 'خ', 'د', 'د', 'د', 'ذ', 'ذ', 'ذ', 'ر', 'ر', 'ر', 'ز', 'ز', 'ز', 'ژ', 'ژ', 'ژ', 'س', 'س', 'س', 'س', 'س', 'ش', 'ش', 'ش', 'ش', 'ش', 'ص', 'ص', 'ص', 'ص', 'ص', 'ض', 'ض', 'ض', 'ض', 'ض', 'ط', 'ط', 'ط', 'ط', 'ط', 'ظ', 'ظ', 'ظ', 'ظ', 'ع', 'ع', 'ع', 'ع', 'ع', 'غ', 'غ', 'غ', 'غ', 'غ', 'ف', 'ف', 'ف', 'ف', 'ف', 'ق', 'ق', 'ق', 'ق', 'ق', 'ک', 'ک', 'ک', 'ک', 'ک', 'ک', 'ک', 'ک', 'ک', 'ک', 'ک', 'گ', 'گ', 'گ', 'گ', 'گ', 'ل', 'ل', 'ل', 'ل', 'ﻻ', 'ﻻ', 'م', 'م', 'م', 'م', 'م', 'ن', 'ن', 'ن', 'ن', 'ن', 'ن', 'ه', 'ه', 'ه', 'ه', 'ه', 'ه', 'ه', 'و', 'و', 'و', 'و', 'و', 'و', 'و', 'و', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ی', 'ئ', 'ئ', 'ئ', 'ه', 'ه', '.', '?', '+', '?', '!', '\u200c', 'A', '{', '}', 'ر', '[', ']', 'پ', '<', '>', '(', ')', 'ه', 'ت', 'ش', 'ش', 'ر', 'ک', 'ن', 'ا', 'M', 'ث', 'و', 'گ', 'ه', 'م', 'پ', 'چ', 'ی', 'پ', '!', 'ت', 'ن', '"' };
            private static List<string> from;
            private static List<string> to;
            private static bool initialized = false;

            private static void Initialize()
            {
                if (initialized)
                {
                    return;
                }
                int i = 0;
                from = new List<string>();
                for (i = 0; i < chrFrom.Length; i++)
                {
                    from.Add(chrFrom[i].ToString());
                }

                to = new List<string>();
                for (i = 0; i < chrtoto.Length; i++)
                {
                    to.Add(chrtoto[i].ToString());
                }
                initialized = true;
            }
            public static string EditArabicLetters(string input, bool removeUnusualChars, bool RmConsSpaces)
            {
                Initialize();
                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                {
                    return "";
                }
                string str = input.Trim();
                int index = -1;
                if (removeUnusualChars)
                {
                    char[] temp = new char[str.Length];
                    int j = 0;
                    foreach (char ch in str.ToCharArray())
                    {
                        index = from.IndexOf(str.ElementAt(j).ToString());
                        if (index >= 0)
                        {
                            temp[j] = chrtoto[index];
                        }
                        else
                        {
                            temp[j] = (char)30;
                        }
                        j++;
                    }
                    string str2 = new string(temp);
                    str = Regex.Replace(str, ((char)30).ToString(), "");

                    if (RmConsSpaces)
                    {
                        RegexOptions options = RegexOptions.None;
                        Regex regex = new Regex("[ ]{2,}", options);
                        str = regex.Replace(str.ToString(), " ");
                    }
                    str = str.ToString().Trim();
                }
                else
                {
                    char[] temp = new char[str.Length];
                    int j = 0;
                    foreach (char ch in str.ToCharArray())
                    {
                        index = from.IndexOf(str.ElementAt(j).ToString());
                        if (index >= 0)
                        {
                            temp[j] = chrtoto[index];
                        }
                        else
                        {
                            temp[j] = ch;
                        }
                        j++;
                    }
                    str = new string(temp);
                    if (RmConsSpaces)
                    {
                        RegexOptions options = RegexOptions.None;
                        Regex regex = new Regex("[ ]{2,}", options);
                        str = regex.Replace(str, " ");
                    }
                    str = str.Trim();
                }
                return str;
            }

            public static void EditArabicLetters(ref List<string> words, bool removeUnusualChars, bool RmConsSpaces)
            {
                Initialize();
                int index = -1;
                if (removeUnusualChars)
                {
                    for (int k = 0; k < words.Count; k++)
                    {
                        string str = words[k];
                        char[] temp = new char[str.Length];
                        int j = 0;
                        foreach (char ch in str.ToCharArray())
                        {
                            index = from.IndexOf(str.ElementAt(j).ToString());
                            if (index >= 0)
                            {
                                temp[j] = chrtoto[index];
                            }
                            else
                            {
                                temp[j] = (char)30;
                            }
                            j++;
                        }
                        string str2 = new string(temp);
                        words[k] = Regex.Replace(str2, ((char)30).ToString(), "");

                        if (RmConsSpaces)
                        {
                            RegexOptions options = RegexOptions.None;
                            Regex regex = new Regex("[ ]{2,}", options);
                            words[k] = regex.Replace(words[k], " ");
                        }
                        words[k] = words[k].Trim();
                    }
                }
                else
                {
                    for (int k = 0; k < words.Count; k++)
                    {
                        string str = words[k];
                        char[] temp = new char[str.Length];
                        int j = 0;
                        foreach (char ch in str.ToCharArray())
                        {
                            index = from.IndexOf(str.ElementAt(j).ToString());
                            if (index >= 0)
                            {
                                temp[j] = chrtoto[index];
                            }
                            else
                            {
                                temp[j] = ch;
                            }
                            j++;
                        }
                        words[k] = new string(temp);
                        if (RmConsSpaces)
                        {
                            RegexOptions options = RegexOptions.None;
                            Regex regex = new Regex("[ ]{2,}", options);
                            words[k] = regex.Replace(words[k], " ");
                        }
                        words[k] = words[k].Trim();
                    }
                }

            }
            public static void EditArabicLetters(ref DataTable words, string filedname, bool removeUnusualChars, bool RmConsSpaces)
            {

                int index = -1;
                if (removeUnusualChars)
                {
                    for (int k = 0; k < words.Rows.Count; k++)
                    {
                        string str = words.Rows[k][filedname].ToString();
                        char[] temp = new char[str.Length];
                        int j = 0;
                        foreach (char ch in str.ToCharArray())
                        {
                            index = from.IndexOf(str.ElementAt(j).ToString());
                            if (index >= 0)
                            {
                                temp[j] = chrtoto[index];
                            }
                            else
                            {
                                temp[j] = (char)30;
                            }
                            j++;
                        }
                        string str2 = new string(temp);
                        words.Rows[k][filedname] = Regex.Replace(str2, ((char)30).ToString(), "");

                        if (RmConsSpaces)
                        {
                            RegexOptions options = RegexOptions.None;
                            Regex regex = new Regex("[ ]{2,}", options);
                            words.Rows[k][filedname] = regex.Replace(words.Rows[k][filedname].ToString(), " ");
                        }
                        words.Rows[k][filedname] = words.Rows[k][filedname].ToString().Trim();
                    }

                }
                else
                {

                    for (int k = 0; k < words.Rows.Count; k++)
                    {
                        string str = words.Rows[k][filedname].ToString();
                        char[] temp = new char[str.Length];
                        int j = 0;
                        foreach (char ch in str.ToCharArray())
                        {
                            index = from.IndexOf(str.ElementAt(j).ToString());
                            if (index >= 0)
                            {
                                temp[j] = chrtoto[index];
                            }
                            else
                            {
                                temp[j] = ch;
                            }
                            j++;
                        }
                        words.Rows[k][filedname] = new string(temp);
                        if (RmConsSpaces)
                        {
                            RegexOptions options = RegexOptions.None;
                            Regex regex = new Regex("[ ]{2,}", options);
                            words.Rows[k][filedname] = regex.Replace(words.Rows[k][filedname].ToString(), " ");
                        }
                        words.Rows[k][filedname] = words.Rows[k][filedname].ToString().Trim();
                    }

                }

            }


        }
        public static class StringChecker
        {
            public static bool isEmpty(string input)
            {
                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                {
                    return true;
                }
                return false;
            }
        }
        public static class StopWordsPr
        {
            public static string[] stopWordsList = new string[]
            {
                "0","1","2","3","4","5","6","7","8","9","-","﻿!",//""","#","(",")","*",",",".","/",":","[","]","،",
	            "؛","؟","٪","«","»","…","﻿و","ّه","ا","اتفاقا","اثرِ","اجراست","احتراما","احتمالا","احیاناً","اخیر","اخیراً",
                "اری","از","از آن پس","از جمله","ازاین رو","ازجمله","ازش","اساسا","اساساً","است","استفاد","استفاده","اسلامی اند",
                "اش","اشتباها","اشکارا","اصلا","اصلاً","اصولا","اصولاً","اعلام","اغلب","افزود","افسوس","اقل","اقلیت","اکثر","اکثرا","اکثراً",
                "اکثریت","اکنون","اگر","اگر چه","اگرچه","اگه","الا","الان","البتّه","البته","الهی","الی","ام","اما","امروز","امروزه",
                "امسال","امشب","امور","امیدوارم","امیدوارند","امیدواریم","ان","ان شاأالله","انتها","انجام","انچنان","اند","اندکی","انشاالله",
                "انصافا","انطور","انقدر","انکه","انگار","انها","او","اوست","اول","اولا","اولاً","اولین","اون","ای","ایا","اید","ایشان",
                "ایم","این","این جوری","این قدر","این گونه","اینان","اینجا","اینجاست","اینچنین","ایند","اینطور","اینقدر","اینک","اینکه",
                "اینگونه","اینو","اینها","اینهاست","آباد","آخ","آخر","آخرها","آخه","آدمهاست","آرام","آرام آرام","آره","آری","آزادانه",
                "آسان","آسیب پذیرند","آشکارا","آشنایند","آقا","آقای","آقایان","آمد","آمدن","آمده","آمرانه","آن","آن گاه","آنان","آنانی",
                "آنجا","آنچنان","آنچنان که","آنچه","آنرا","آنطور","آنقدر","آنکه","آنگاه","آنها","آن‌ها","آنهاست","آور","آورد","آوردن","آورده",
                "آوه","آهان","آهای","آی","آیا","آید","آیند","ب","با","بااین حال","بااین وجود","باد","بار","بارة","باره","بارها","باز","باز هم",
                "بازهم","بازی کنان","بازیگوشانه","باش","باشد","باشم","باشند","باشی","باشید","باشیم","بالا","بالاخره","بالاخص","بالاست","بالای","بالایِ",
                "بالطبع","بالعکس","باوجودی که","باورند","باید","بپا","بتدریج","بتوان","بتواند","بتوانی","بتوانیم","بجز","بخش","بخشه","بخشی",
                "بخصوص","بخواه","بخواهد","بخواهم","بخواهند","بخواهی","بخواهید","بخواهیم","بخوبی","بد","بدان","بدانجا","بدانها","بدون","بدهید",
                "بدین","بدین ترتیب","بدینجا","بر","برا","برابر","برابرِ","براحتی","براساس","براستی","برای","برایِ","برایت","برایش","برایشان","برایم",
                "برایمان","برآنند","برخوردار","برخوردارند","برخی","برداری","برعکس","برنامه سازهاست","بروز","بروشنی","بزرگ","بزودی","بس","بسا","بسادگی",
                "بسختی","بسوی","بسی","بسیار","بسیاری","بشدت","بطور","بطوری که","بعد","بعد از این که","بعدا","بعداً","بعدازظهر","بعدها","بعری","بعضا","بعضی",
                "بعضی شان","بعضی‌ها","بعضیهایشان","بعلاوه","بعید","بفهمی نفهمی","بکار","بکن","بکند","بکنم","بکنند","بکنی","بکنید","بکنیم","بگذاریم","بگو",
                "بگوید","بگویم","بگویند","بگویی","بگویید","بگوییم","بگیر","بگیرد","بگیرم","بگیرند","بگیری","بگیرید","بگیریم","بلافاصله","بلکه","بله","بلی",
                "بماند","بنابراین","بندی","بود","بودم","بودن","بودند","بوده","بودی","بودید","بودیم","بویژه","به","به آسانی","به تازگی","به تدریج",
                "به تمامی","به جای","به جز","به خوبی","به درشتی","به دلخواه","به راستی","به رغم","به روشنی","به زودی","به سادگی","به سرعت","به شان",
                "به شدت","به طور کلی","به طوری که","به علاوه","به قدری","به کرات","به گرمی","به مراتب","به ناچار","به وضوح","به ویژه","به هرحال",
                "به هیچ وجه","بهت","بهتر","بهترین","بهش","بی","بی اطلاعند","بی آنکه","بی تردید","بی تفاوتند","بی نیازمندانه","بی هدف","بیا","بیاب",
                "بیابد","بیابم","بیابند","بیابی","بیابید","بیابیم","بیاور","بیاورد","بیاورم","بیاورند","بیاوری","بیاورید","بیاوریم","بیاید","بیایم","بیایند",
                "بیایی","بیایید","بیاییم","بیرون","بیرونِ","بیست","بیش","بیشتر","بیشتری","بیگمان","بین","پ","پارسال","پارسایانه","پاره‌ای","پاعینِ",
                "پایین ترند","پدرانه","پرسان","پروردگارا","پریروز","پس","پس از","پس فردا","پشت","پشتوانه اند","پشیمونی","پنج","پهن شده","پی","پی درپی",
                "پیدا","پیداست","پیرامون","پیش","پیشِ","پیشاپیش","پیشتر","پیوسته","ت","تا","تازه","تاکنون","تان","تحت","تحریم هاست","تر","تر براساس",
                "تریلیارد","تریلیون","ترین","تصریحاً","تعدادی","تعمدا","تقریبا","تقریباً","تک تک","تلویحا","تلویحاً","تمام","تمام قد","تماما","تمامشان",
                "تمامی","تند تند","تنها","تو","توان","تواند","توانست","توانستم","توانستن","توانستند","توانسته","توانستی","توانستیم","توانم","توانند",
                "توانی","توانید","توانیم","توسط","تولِ","توؤماً","توی","تویِ","ث","ثالثاً","ثانیا","ثانیاً","ج","جا","جای","جایی","جدا","جداً","جداگانه",
                "جدید","جدیدا","جرمزاست","جریان","جز","جلو","جلوگیری","جلوی","جلویِ","جمع اند","جمعا","جمعی","جنابعالی","جناح","جنس اند","جور","جهت",
                "چ","چاپلوسانه","چت","چته","چرا","چرا که","چشم بسته","چطور","چقدر","چکار","چگونه","چنان","چنانچه","چنانکه","چند","چند روزه","چندان",
                "چنده","چندین","چنین","چو","چون","چه","چه بسا","چه طور","چهار","چی","چیز","چیزی","چیزیست","چیست","چیه","ح","حاشیه‌ای","حاضر","حاضرم",
                "حاکیست","حال","حالا","حتما","حتماً","حتی","حداقل","حداکثر","حدود","حدودِ","حدودا","حسابگرانه","حضرتعالی","حق","حقیرانه","حقیقتا","حکماً",
                "حول","خ","خارجِ","خالصانه","خب","خداحافظ","خداست","خدمات","خسته‌ای","خصوصا","خصوصاً","خلاصه","خواست","خواستم","خواستن","خواستند","خواسته",
                "خواستی","خواستید","خواستیم","خواه","خواهد","خواهم","خواهند","خواهی","خواهید","خواهیم","خوب","خود","خود به خود","خودبه خودی","خودت",
                "خودتان","خودتو","خودش","خودشان","خودم","خودمان","خودمو","خوش","خوشبختانه","خویش","خویشتن","خویشتنم","خیاه","خیر","خیره","خیلی","د",
                "دا","داام","دااما","داخل","داد","دادم","دادن","دادند","داده","دادی","دادید","دادیم","دار","داراست","دارد","دارم","دارند","داری","دارید",
                "داریم","داشت","داشتم","داشتن","داشتند","داشته","داشتی","داشتید","داشتیم","دامم","دانست","دانند","دایم","دایما","در","در باره","در بارهٌ",
                "در ثانی","در کل","در کنار","در مجموع","در نهایت","در واقع","دراین میان","درباره","درحالی که","درحالیکه","درست","درست و حسابی","درسته",
                "درصورتی که","درعین حال","درمجموع","درواقع","درون","دریغ","دریغا","درین","دسته دسته","دشمنیم","دقیقا","دم","دنبالِ","دو","دو روزه","دوباره",
                "دوم","ده","دهد","دهم","دهند","دهی","دهید","دهیم","دیده","دیر","دیرت","دیرم","دیروز","دیشب","دیگر","دیگران","دیگری","دیگه","دیوانه‌ای","دیوی",
                "ذ","ذاتاً","ر","را","راجع به","راحت","راسا","راست","راستی","راه","رسما","رسید","رسیده","رشته","رفت","رفتارهاست","رفته","رنجند","رو","رواست",
                "روب","روبروست","روز","روز به روز","روزانه","روزه ایم","روزه ست","روزه م","روزهای","روزه‌ای","روش","روی","رویِ","رویش","رهگشاست","ریزی","ز",
                "زشتکارانند","زمان","زمانی","زمینه","زنند","زود","زودتر","زهی","زیاد","زیاده","زیر","زیرِ","زیرا","زیرچشمی","ژ","س","سابق","ساخته","ساده اند",
                "سازی","ساکنند","سالانه","سالته","سالم‌تر","سالهاست","سالیانه","سایر","سپس","سخت","سخته","سر","سراپا","سراسر","سرانجام","سری","سریِ","سریع",
                "سریعا","سریعاً","سعی","سمتِ","سوم","سوی","سویِ","سه باره","سهواً","سیاه چاله هاست","سیخ","ش","شان","شاهدند","شاهدیم","شاید","شبهاست","شخصا","شخصاً",
                "شد","شدم","شدن","شدند","شده","شدی","شدید","شدیدا","شدیداً","شدیم","شش","شش نداشته","شما","شماری","شماست","شمایند","شناسی","شو","شود","شوراست",
                "شوقم","شوم","شوند","شونده","شوی","شوید","شویم","شیرین","شیرینه","شیک","ص","صد","صددرصد","صرفا","صرفاً","صریحاً","صندوق هاست","صورت","ض","ضدِّ",
                "ضدِّ","ضمن","ضمناً","ط","طبعا","طبعاً","طبقِ","طبیعتا","طرف","طریق","طلبکارانه","طور","طی","ظ","ظاهرا","ظاهراً","ع","عاجزانه","عاقبت","عبارتند","عجب",
                "عجولانه","عدم","عرفانی","عقب","عقبِ","علّتِ","علاوه بر","علاوه بر آن","علاوه برآن","علناً","علی الظاهر","علی رغم","علیرغم","علیه","عمدا","عمداً","عمدتا",
                "عمدتاً","عمده","عمل","عملا","عملاً","عملی اند","عموم","عموما","عموماً","عنقریب","عنوان","عنوانِ","عیناً","غ","غالبا","غزالان","غیر","غیرقانونی","ف",
                "فاقد","فبها","فر","فردا","فعلا","فعلاً","فقط","فکر","فلان","فلذا","فوق","ق","قاالند","قابل","قاطبه","قاطعانه","قاعدتاً","قانوناً","قبل","قبلا","قبلاً",
                "قبلند","قدر","قدری","قصدِ","قضایاست","قطعا","قطعاً","ک","کَی","کارند","کاش","کاشکی","کامل","کاملا","کاملاً","کتبا","کجا","کجاست","کدام","کرد","کردم",
                "کردن","کردند","کرده","کردی","کردید","کردیم","کس","کسانی","کسی","کل","کلا","کلی","کلیه","کم","کم کم","کمااینکه","کماکان","کمتر","کمتره","کمتری",
                "کمی","کن","کنار","کنارِ","کنارش","کنایه‌ای","کند","کنم","کنند","کننده","کنون","کنونی","کنی","کنید","کنیم","کو","که","کی","گ","گاه","گاهی","گذاری",
                "گذاشته","گذشته","گرچه","گردد","گردند","گرفت","گرفتارند","گرفتم","گرفتن","گرفتند","گرفته","گرفتی","گرفتید","گرفتیم","گروهی","گفت","گفتم","گفتن",
                "گفتند","گفته","گفتی","گفتید","گفتیم","گو","گونه","گوی","گویا","گوید","گویم","گویند","گویی","گویید","گوییم","گه","گهگاه","گیر","گیرد","گیرم",
                "گیرند","گیری","گیرید","گیریم","ل","لااقل","لاجرم","لب","لذا","لزوماً","لطفا","لطفاً","لیکن","م","ما","مادامی","ماست","مامان مامان گویان","مان",
                "مانند","مانندِ","مبادا","متاسفانه","متعاقبا","متفاوتند","متؤسفانه","مثل","مثلِ","مثلا","مجانی","مجبورند","مجددا","مجدداً","مجموعا","مجموعاً","محتاجند",
                "محکم","محکم‌تر","مخالفند","مختلف","مخصوصاً","مدّتی","مدام","مدت","مدتهاست","مذهبی اند","مرا","مرتب","مردانه","مردم","مردم اند","مرسی","مستحضرید",
                "مستقیما","مستند","مسلما","مشت","مشترکاً","مشغولند","مطمانا","مطمانم","مطمینا","مع الاسف","مع ذلک","معتقدم","معتقدند","معتقدیم","معدود","معذوریم",
                "معلومه","معمولا","معمولاً","معمولی","مغرضانه","مفیدند","مقابل","مقدار","مقصرند","مقصری","مکرر","مکرراً","مگر","مگر این که","مگر آن که","مگو","ملیارد",
                "ملیون","ممکن","ممیزیهاست","من","منتهی","منطقی","منی","مواجهند","موارد","موجودند","مورد","موقتا","می","میان","می‌تواند","می‌خواهیم","می‌داند","می‌رسد",
                "می‌رود","میزان","می‌شود","میکند","میکنم","می‌کنم","میکنند","می‌کنند","میکنی","میکنید","میکنیم","می‌کنیم","میلیارد","میلیون","ن","ناامید","ناخواسته",
                "ناراضی اند","ناشی","ناگاه","ناگزیر","ناگهان","ناگهانی","نام","نباید","نبش","نبود","نخست","نخستین","نخواهد","نخواهم","نخواهند","نخواهی","نخواهید",
                "نخواهیم","نخودی","ندارد","ندارم","ندارند","نداری","ندارید","نداریم","نداشت","نداشتم","نداشتند","نداشته","نداشتی","نداشتید","نداشتیم","نزد","نزدِ",
                "نزدیک","نزدیکِ","نسبتا","نشان","نشده","نظیر","نفرند","نکرده","نکن","نکند","نکنم","نکنند","نکنی","نکنید","نکنیم","نگاه","نگو","نماید","نموده","نمی",
                "نمی‌شود","نمی‌کند","نوع","نوعاً","نوعی","نه","نه تنها","نهایتا","نهایتاً","نیازمندند","نیز","نیست","نیستم","نیستند","نیستیم","نیمی","و","و لا غیر",
                "وابسته اند","واقعا","واقعاً","واقعی","واقفند","واما","وای","وجود","وحشت زده","وسطِ","وضع","وقتی","وقتی که","وقتیکه","وگرنه","وگو","ولی","وی",
                "ویا","ویژه","ه","ها","های","هایی","هبچ","هر","هر از گاهی","هر چند","هر چند که","هر چه","هرچند","هرچه","هرکس","هرگاه","هرگز","هزار","هست","هستم",
                "هستند","هستی","هستید","هستیم","هفت","هق هق کنان","هم","هم اکنون","هم اینک","همان","همان طور که","همان گونه که","همانا","همانند","همانها",
                "همچنان","همچنان که","همچنین","همچون","همچین","همدیگر","همزمان","همگان","همگی","همواره","همه","همهٌ","همه روزه","همه ساله","همه شان","همه‌اش",
                "همیشه","همین","همین که","هنگام","هنگامِ","هنگامی","هنگامی که","هنوز","هوی","هی","هیچ","هیچ گاه","هیچکدام","هیچکس","هیچگاه","هیچگونه","هیچی",
                "ی","یا","یاب","یابد","یابم","یابند","یابی","یابید","یابیم","یارب","یافت","یافتم","یافتن","یافته","یافتی","یافتید","یافتیم","یعنی","یقینا",
                "یقیناً","یک","یک جوری","یک کم","یک کمی","یکدیگر","یکریز","یکسال","یکهزار","یکی","یواش یواش","یه","تهران","مشهد","اصفهان","اهواز","قم","دزفول",
                "تبریز","شیراز","شمال","جنوب","غرب","شرق","اطراف","مرگ","استخدام","download","mb","رفتیم","ترکی","کیف","ننه","عن","httpaddr","username",
                "telegram","joinchat","me","number"
            };
            public static Hashtable getStopWordsHashtable()
            {
                Hashtable swords = new Hashtable();
                foreach (string item in stopWordsList)
                {
                    if (!swords.ContainsKey(item))
                    {
                        swords.Add(item, item);
                    }
                }
                return swords;
            }
            public static List<string> getStopWordsList()
            {
                List<string> swords = new List<string>();
                foreach (string item in stopWordsList)
                {
                    if (!swords.Contains(item))
                    {
                        swords.Add(item);
                    }
                }
                return swords;
            }
        }
        public static class FormDesign
        {
            //Controls Font
            public static System.Drawing.Font Nazanin14 = new System.Drawing.Font("B Nazanin", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));

            public static System.Drawing.Font getFont(string FontName, int size, bool Bold)
            {
                if (Bold)
                {
                    return new System.Drawing.Font(FontName, (float)size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
                }
                else
                {
                    return new System.Drawing.Font(FontName, (float)size, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
                }                              
            }
            private static void TxtBx_OnlyInteger_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            private static void TxtBx_OnlyFloat_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                    (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }

            public static int intitalizeDataGridview(ref DataGridView gridview, params string[] list)
            {
                int output = -1;
                if (list.Count() == 0)
                {
                    return output;
                }
                try
                {
                    foreach (var item in list)
                    {
                        gridview.Columns.Add(item, item);
                        gridview.Columns[item].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    }
                    output = 0;
                }
                catch
                {
                    output = -1;
                }

                return output;
            }
        }
    }
}
