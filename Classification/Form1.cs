using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ARCore;
using ECLAT;

namespace Classification
{
    public partial class Form1 : Form
    {
        double[,] train_docword;
        double[,] test_docword;
        private List<string> AllKeyWords;
        List<string> trainmessages;
        List<string> testmessages;
        //List<string> matrix;
        double[,] documentMatrix;
        int[] label;
        double[,] testdocumentMatrix;
        int[] testlabel;
        AR ac;

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("SVM");
            comboBox1.Items.Add("Logistic");
            //comboBox1.Items.Add("Tree");
            comboBox1.Items.Add("GA");
        }

        private void load_train_button(object sender, EventArgs e)
        {            
            //calculate matrix using concept and vocab
            string type = "train";
            /*var result = LoadfromMessageFile(concept.Text, type)*/
            //documentMatrix = result.Item1;
            //label = result.Item2;
            trainmessages = readFromFile(concept.Text);//LoadfromMessageFile(concept.Text, type);

            //try
            //{
            //    AllKeyWords = readFromFile("vocab.txt");
            //}
            //catch (FileNotFoundException)
            //{
            //    AllKeyWords = null;
            //}

            write_docword(trainmessages, AllKeyWords, "train");
        }

        private void load_test_button(object sender, EventArgs e)
        {
            string type = "test";
            //var result = LoadfromMessageFile(testconcept.Text, type);
            //testdocumentMatrix = result.Item1;
            //testlabel = result.Item2;
            //testmessages = LoadfromMessageFile(testconcept.Text, type);
            testmessages = readFromFile(testconcept.Text);

            write_docword(testmessages, AllKeyWords, "test");
        }

        private void ex_rules_Click(object sender, EventArgs e)
        {
            double psup;
            double pcon;
            double ppco;
            double nsup;
            double ncon;
            double npco;

            ac = new AR(trainmessages, AllKeyWords);
            try
            {
                psup = Convert.ToDouble(psupp.Text);
                pcon = Convert.ToDouble(pconf.Text);
                ppco = Convert.ToDouble(ppconf.Text);
                nsup = Convert.ToDouble(nsupp.Text);
                ncon = Convert.ToDouble(nconf.Text);
                npco = Convert.ToDouble(npconf.Text);
            }
            catch
            {
                MessageBox.Show("incorect format");
                return;
            }

            ac = new AR(trainmessages, AllKeyWords);
            ac.EX_Rules(psup, pcon, ppco, nsup, ncon, npco);
        }

        private void train_button(object sender, EventArgs e)
        {
            string algorithm = comboBox1.Text;

            int[] labels = ac.labels;
            double[,] tr_docrule = ac.docRule;
            ac.Classification_Train(tr_docrule, labels, algorithm);
            write_mat(tr_docrule, labels, "train");
            ////ac.removekeys(100);

            //ac = new AR();
            //ac.Classification_Train(train_docword, label, algorithm);
        }
        
        private void test_button(object sender, EventArgs e)
        {
            string algorithm = comboBox1.Text;
            if (ac == null)
            {
                List<Itemset> eclatlitems = new List<Itemset>();
                try
                {
                    AllKeyWords = readFromFile(keyword_txt.Text);
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Keywords error!");
                }

                try
                {
                    eclatlitems = load_model("AR.model");
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("AR.model dont exist!");
                }

                ac = new AR(eclatlitems, AllKeyWords);
            }

            ac.Classification_Test(testmessages, algorithm);

            write_mat(ac.testdocRule, ac.testlabels, "test");

            ///*******************************************************************/
            //ac.Classification_Test(test_docword, algorithm);
            ///*******************************************************************/

            List<string> result = readFromFile("RESULTES_" + algorithm);
            double accuracy;
            int TPos = 0;
            int TNeg = 0;
            int FPos = 0;
            int FNeg = 0;

            for (int i = 0; i < result.Count; i++)
            {
                if (result[i] == "True")
                {
                    if (testlabel[i] == 1)
                        TPos++;
                    else
                        FPos++;
                }
                else
                {
                    if (testlabel[i] == 0)
                        TNeg++;
                    else
                        FNeg++;
                }
            }

            accuracy = (double)(TPos + TNeg) / result.Count;
            double percision = (double)TPos / (TPos + FPos);
            double recall = (double)TPos / (TPos + FNeg);

            string[] row = { accuracy.ToString("0.0000"), TPos.ToString(), FPos.ToString(), TNeg.ToString(), FNeg.ToString(), percision.ToString("0.0000"), recall.ToString("0.0000") };
            dataGridView1.Rows.Add(row);
        }

        public List<string> LoadfromMessageFile(string messagefile, string type)
        {
            List<string> newDataRows = new List<string>();

            List<MessageMediaStructure> DataRows;
            bool db = false;
            int conceptID = 0;
            try
            {
                conceptID = int.Parse(messagefile);
                db = true;
            }
            catch
            {

            }
            if (db == true)
            {
                DataRows = readFromDataBase(conceptID, -1);
            }
            else
            {
                DataRows = readExcel(messagefile);
            }

            foreach (var item in DataRows)
            {
                string temp = item.message;
                //if (type == "train")
                    temp = temp + "\t" + item.tagged.ToString();
                newDataRows.Add(temp);
            }
            
            return newDataRows;
        }

        public Dictionary<string, Words> WordCount(string input, string[] words)
        {
            var result = new Dictionary<string, Words>();
            var pattern = string.Join("|", words);
            var reg = new Regex(pattern);
            var matchs = reg.Matches(input);
            int i = 0;
            foreach (string word in words)
            {
                //var isabout = new Words();// Isaboutwords[word];
                if (result.ContainsKey(word))
                {
                    continue;
                }
                result.Add(word, new Words()
                {
                    Word = word,
                    index = i,                    
                    Count = matchs.OfType<Match>().Count(r => r.Value == word)
                });
                i++;
            }
            return result;
        }

        private List<string> readFromFile(string file_name)
        {
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader(file_name))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line.Trim()) != true)
                    {
                        lines.Add(line.Trim());
                    }
                }
            }
            return lines;
        }
        private List<MessageMediaStructure> readFromDataBase(int ConceptID, int type)
        {
            MessageMediaReader mr = new MessageMediaReader();
            List<MessageMediaStructure> messages = new List<MessageMediaStructure>();
            messages = mr.loadMessagesFromDataBase(ConceptID, type);
            if (messages.Count <= 0)
            {
                Console.WriteLine("Could Not Load Messages");
                return new List<MessageMediaStructure>();
            }
            return messages;
        }
        private List<MessageMediaStructure> readFromDataBase(int ConceptID, int count, int type)
        {
            MessageMediaReader mr = new MessageMediaReader();
            List<MessageMediaStructure> messages = new List<MessageMediaStructure>();
            messages = mr.loadMessagesFromDataBase(ConceptID, count, type);
            if (messages.Count <= 0)
            {
                Console.WriteLine("Could Not Load Messages");
                return new List<MessageMediaStructure>();
            }
            return messages;
        }

        private List<MessageMediaStructure> readExcel(string filePath)
        {
            List<MessageMediaStructure> ListOfexcelRows = new List<MessageMediaStructure>();

            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = null;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                //xlWorkBook = xlApp.Workbooks.Open(Environment.CurrentDirectory + "\\" + filePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkBook = xlApp.Workbooks.Open(filePath);//, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            }
            catch
            {
                MessageBox.Show("فایل ورودی وجود ندارد", "خطا");
                return ListOfexcelRows;
            }

            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            foreach (Microsoft.Office.Interop.Excel.Worksheet worksheet in xlWorkBook.Worksheets)
            {
                int i = 1;
                foreach (Microsoft.Office.Interop.Excel.Range row in worksheet.UsedRange.Rows)
                {
                    MessageMediaStructure ex = new MessageMediaStructure();

                    try
                    {
                        //ex.lable = int.Parse(worksheet.Cells[row.Row, 2].Value.ToString());
                        ex.tagged = int.Parse(worksheet.Cells[row.Row, 2].Value.ToString());
                    }
                    catch
                    {
                        //NuOfEmptyTags++;
                        //continue;
                    }

                    try
                    {
                        //ex.text = worksheet.Cells[row.Row, 1].Value.ToString().Trim();
                        ex.message = worksheet.Cells[row.Row, 1].Value.ToString().Trim();
                    }
                    catch
                    {
                        //NuOfWrongRows++;
                    }
                    if (ex.message != null)
                    {
                        ex.Id = i;
                        ListOfexcelRows.Add(ex);
                        i++;
                    }
                }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            xlWorkBook.Close();
            xlApp.Quit();

            return ListOfexcelRows;
        }

        private void sslCertField_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void sslCertField_DragEnter_concept(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                concept.Text = files[0];
            }
        }
        private void sslCertField_DragEnter_keywords(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                textBox1.Text = files[0];
            }
        }

        private void sslCertField_DragEnter_tconcept(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                testconcept.Text = files[0];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        public List<Itemset> load_model(string filepath)
        {
            List<double> conf = new List<double>();
            List<Itemset> eclatlitems = new List<Itemset>();
            StreamReader sr = new StreamReader(filepath);
            double confidence;

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                List<string> elements = new List<string>();
                List<string> negelements = new List<string>();
                string[] rule = line.Split('&');
                string items = rule[1].Trim('{', '}');
                string[] temp = items.Split(',');
                foreach (string ite in temp)
                {
                    elements.Add(ite);
                }
                confidence = Convert.ToDouble(rule[3]);
                eclatlitems.Add(new Itemset(elements, Convert.ToDouble(rule[2]), confidence));
            }
            return eclatlitems;
        }        

        private void write_mat(double[,]mat, int[]label,string type)
        {
            if(mat.GetLength(0)!=label.Length)
            {
                MessageBox.Show("Document and Labels most have same size!");
                return;
            }
            string[] lines = new string[mat.GetLength(0)];
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                lines[i] = "";
                for (int j = 0; j < mat.GetLength(1); j++)
                    lines[i] += mat[i, j].ToString() + ",";
                lines[i] += label[i].ToString();
            }
            File.WriteAllLines(type + ".txt", lines);
        }

        private void write_docword(List<string> messages,List<string> keys, string type)
        {
            double[,] doc_word = new double[messages.Count, keys.Count];
            string[] lines = new string[messages.Count];
            int[] labels = new int[messages.Count];

            int j = 0;
            foreach (var message in messages)
            {
                List<string> items = new List<string>();
                //char[] sep = { ' ', '\t' };
                string[] mess = message.Trim().Split('\t');

                lines[j] = "";
                for (int i = 0; i < keys.Count; i++)
                {
                    if (message.Contains(keys[i]))
                    {
                        doc_word[j, i] = 1;
                        lines[j] += "1,";
                    }                        
                    else
                    {
                        doc_word[j, i] = 0;
                        lines[j] += "0,";
                    }                        
                }

                if (mess[1] == "0")
                {
                    lines[j] += "0";
                    labels[j] = 0;
                }                    
                else //if (mess[mess.Length - 1] == "1")
                {
                    lines[j] += "1";
                    labels[j] = 1;
                }                    
                //else if (mess[mess.Length - 1] == "-1")
                //{
                //    lines[j] += "-1";
                //    if (type == "train")
                //        label[j] = -1;
                //    else
                //        testlabel[j] = -1;
                //}
                    
                j++;                
            }
            if (type == "train")
            {
                train_docword = doc_word;
                label = labels;
            }
            else
            {
                test_docword = doc_word;
                testlabel = labels;
            }

            File.WriteAllLines("docword_" + type + ".txt", lines);
        }

        private void keyword_txt_Click(object sender, EventArgs e)
        {
            AllKeyWords = readFromFile(textBox1.Text);
        }
    }
}
