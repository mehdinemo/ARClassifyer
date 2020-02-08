using Accord.IO;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math;
using Accord.Statistics.Models.Regression;
using Accord.Statistics.Models.Regression.Fitting;
using ECLAT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCore
{
    public class AR
    {
        public List<Itemset> eclatlitems = new List<Itemset>();
        public int[] labels;
        public int[] testlabels;
        List<string> tr_messages;       
        private List<string> AllKeyWords;
        public double[,] docRule;
        public double[,] testdocRule;

        public AR()
        {
        }

        public AR(List<string> tr_messages, List<string> AllKeyWords)
        {
            this.tr_messages = tr_messages;
            this.AllKeyWords = AllKeyWords;
        }

        public AR(List<Itemset> eclatlitems, List<string> AllKeyWords)
        {
            this.eclatlitems = eclatlitems;
            this.AllKeyWords = AllKeyWords;
        }

        public void EX_Rules(double support, double confidence, double max_confidence, double negsupport, double negconfidence, double negmax_confidence)
        {
            List<Transaction> data = new List<Transaction>();
            List<Rule> eclat_rules = new List<Rule>();
            List<Itemset> eclat_Litemsets = new List<Itemset>();
            List<Rule> negeclat_rules = new List<Rule>();
            List<Itemset> negeclat_Litemsets = new List<Itemset>();

            data = list2mat(tr_messages, "train");
            Eclat eclat = new Eclat(data, labels, support, confidence);
            eclat = eclat.Run(support, confidence);
            eclat_rules = eclat.eclat_rules;
            eclat_Litemsets = eclat.L_itemsets;            

            //Pruning Rules
            for (int i = 0; i < eclat_Litemsets.Count; i++)
            {
                var item = eclat_Litemsets[i];
                if (item.confidence >= max_confidence)
                {
                    List<string> temp2 = item.items;
                    for (int jj = i + 1; jj < eclat_Litemsets.Count; jj++)
                    {
                        List<string> temp1 = eclat_Litemsets[jj].items;
                        bool flag = true;
                        foreach (string s in temp2)
                        {
                            if (!temp1.Contains(s))
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                            eclat_Litemsets.RemoveAt(jj);
                    }
                }
            }

            //Negative Rules            
            int[] neglabels = new int[labels.Length];
            for (int i = 0; i < labels.Length; i++)
            {
                if (labels[i] == 0)
                    neglabels[i] = 1;
            }     
            
            Eclat negeclat = new Eclat(data, neglabels, negsupport, negconfidence);
            negeclat = negeclat.Run(negsupport, negconfidence);
            negeclat_rules = negeclat.eclat_rules;
            negeclat_Litemsets = negeclat.L_itemsets;
            
            //Pruning Rules
            for (int i = 0; i < negeclat_Litemsets.Count; i++)
            {
                var item = negeclat_Litemsets[i];
                if (item.confidence >= negmax_confidence)
                {
                    List<string> temp2 = item.items;
                    for (int jj = i + 1; jj < negeclat_Litemsets.Count; jj++)
                    {
                        List<string> temp1 = negeclat_Litemsets[jj].items;
                        bool flag = true;
                        foreach (string s in temp2)
                        {
                            if (!temp1.Contains(s))
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                            negeclat_Litemsets.RemoveAt(jj);
                    }
                }
            }

            //write model
            System.IO.File.Delete("AR.model");          
            string[] row = new string[negeclat_Litemsets.Count + eclat_Litemsets.Count];
            for (int i = 0; i < eclat_Litemsets.Count; i++)
            {
                var item = eclat_Litemsets[i];
                row[i] = "Pos&" + "{" + string.Join(",", item.items) + "}&" + item.support.ToString() + "&" + item.confidence.ToString("0.0000");
            }
            for (int i = 0; i < negeclat_Litemsets.Count; i++)
            {
                var item = negeclat_Litemsets[i];
                row[eclat_Litemsets.Count + i] = "Neg&" + "{" + string.Join(",", item.items) + "}&" + item.support.ToString() + "&" + item.confidence.ToString("0.0000");
            }
            System.IO.File.WriteAllLines("AR.model", row);

            eclatlitems = eclat_Litemsets;           
            foreach (var rule in negeclat_Litemsets)
            {
                eclatlitems.Add(new Itemset(rule.items, rule.support, -rule.confidence));
            }
            //return eclatlitems;
            docRule = calculate_docrule(data);

            //classification_train(docRule, labels, algoritm);            
        }              

        private double[,] calculate_docrule(List<Transaction> trans)
        {            
            double[,] docrule = new double[trans.Count, eclatlitems.Count];
            
            for (int i = 0; i < trans.Count; i++)
            {
                int col = 0;
                foreach (var rule in eclatlitems)
                {
                    bool flag = true;
                    for (int j = 0; j < rule.items.Count; j++)
                    {
                        if (!trans[i].items.Contains(rule.items[j]))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag == true && rule.confidence < 0)
                        docrule[i, col] = -1;
                    else if (flag == true && rule.confidence > 0)
                        docrule[i, col] = 1;
                    else
                        docrule[i, col] = 0;
                    col++;
                }
            }            
            //write_mat(docrule, "DocRule_train.txt");
            return docrule;
        }        

        public void Classification_Train(double[,] train_docrule, int[] label, string algorithm)
        {            
            string classmodelpath;
            int attrSize = eclatlitems.Count;
            int attrSizeTest = eclatlitems.Count;            
            
            // Specify the input variables
            DecisionVariable[] variables = new DecisionVariable[attrSize];
            for (int i = 0; i < attrSize; i++)
            {
                variables[i] = new DecisionVariable((i + 1).ToString(), DecisionVariableKind.Discrete);
            }

            if (algorithm == "Tree")
            {
                classmodelpath = algorithm + ".model";
                //RandomForest tree2 = new RandomForest(2, variables);
                DecisionTree tree = new DecisionTree(variables, 2);
                C45Learning teacher = new C45Learning(tree);
                var model = teacher.Learn(train_docrule.ToJagged(), label);
                //save model
                teacher.Save(Path.Combine("", classmodelpath));
            }
            if (algorithm == "SVM")
            {
                classmodelpath = algorithm + ".model";
                var learn = new SequentialMinimalOptimization()
                {
                    UseComplexityHeuristic = true,
                    UseKernelEstimation = false
                };
                SupportVectorMachine teacher = learn.Learn(train_docrule.ToJagged(), label);
                //save model
                teacher.Save(Path.Combine("", classmodelpath));
            }

            if (algorithm == "Logistic")
            {               
                    classmodelpath = algorithm + ".model";
                    var learner = new IterativeReweightedLeastSquares<LogisticRegression>()
                    {
                        Tolerance = 1e-4,  // Let's set some convergence parameters
                        Iterations = 1,  // maximum number of iterations to perform
                        Regularization = 0
                    };
                    LogisticRegression teacher = learner.Learn(train_docrule.ToJagged(), label);
                    teacher.Save(Path.Combine("", classmodelpath));                
            }

            if (algorithm == "GA")
                weights_ga_matlab();
        }

        public void Classification_Test(List<string> tests_messages/*double[,]mat*/, string algorithm)
        {            
            System.IO.File.Delete("RESULTES_" + algorithm);

            List<Transaction> ts_data = new List<Transaction>();
            ts_data = list2mat(tests_messages, "test");
            testdocRule = calculate_docrule(ts_data);
            //testdocRule = mat;

            string classmodelpath;
            int attrSize = eclatlitems.Count;
            int attrSizeTest = eclatlitems.Count;
            //int attrSize = testdocRule.GetLength(1);//eclatlitems.Count;
            //int attrSizeTest = testdocRule.GetLength(1);//eclatlitems.Count;

            string[] results = new string[testdocRule.GetLength(0)];
            // Specify the input variables
            DecisionVariable[] variables = new DecisionVariable[attrSize];
            for (int i = 0; i < attrSize; i++)
            {
                variables[i] = new DecisionVariable((i + 1).ToString(), DecisionVariableKind.Discrete);
            }

            if (algorithm == "Tree")
            {               
                classmodelpath = algorithm + ".model";
                
                // To load it back from the disk, we might need to use the Serializer class directly: 
                var new_model = Serializer.Load<C45Learning>(Path.Combine("", classmodelpath));                    
                for (int i = 0; i < testdocRule.GetLength(0); i++)
                {
                    double[] tt1 = new double[attrSizeTest];
                    for (int j = 0; j < attrSizeTest; j++)
                        tt1[j] = (int)(testdocRule[i, j]);                    
                    results[i] = new_model.Model.Decide(tt1).ToString();
                }
                File.WriteAllLines("RESULTES_" + algorithm, results);
            }
            if (algorithm == "SVM")
            {                
                classmodelpath = algorithm + ".model";                
                var new_model = Serializer.Load<SupportVectorMachine>(Path.Combine("", classmodelpath));
                for (int i = 0; i < testdocRule.GetLength(0); i++)
                {
                    double[] tt1 = new double[attrSizeTest];
                    for (int j = 0; j < attrSizeTest; j++)
                        tt1[j] = (int)(testdocRule[i, j]);
                    results[i] = new_model.Decide(tt1).ToString();
                }
                File.WriteAllLines("RESULTES_" + algorithm, results);
            }

            if (algorithm == "Logistic")
            {                
                classmodelpath = algorithm + ".model";                
                var new_model = Serializer.Load<SupportVectorMachine>(Path.Combine("", classmodelpath));
                for (int i = 0; i < testdocRule.GetLength(0); i++)
                {
                    double[] tt1 = new double[attrSizeTest];
                    for (int j = 0; j < attrSizeTest; j++)
                        tt1[j] = (int)(testdocRule[i, j]);
                    results[i] = new_model.Decide(tt1).ToString();
                }
                File.WriteAllLines("RESULTES_" + algorithm, results);
            }

            if (algorithm == "GA")
            {
                List<string> weights = readFromFile("weights.txt");
                string[] Weights = weights.ToArray<string>();
                double[] W = new double[Weights.Length - 1];
                for (int i = 0; i < Weights.Length - 1; i++)
                {
                    W[i] = Convert.ToDouble(Weights[i]);
                }
                double threshold = Convert.ToDouble(Weights[Weights.Length - 1]);

                double[][] DocRule = new double[testdocRule.GetLength(0)][];
                for (int i = 0; i < testdocRule.GetLength(0); i++)
                {                    
                    List<double> temp2 = new List<double>();
                    for (int j = 0; j < testdocRule.GetLength(1); j++)
                    {
                        temp2.Add(testdocRule[i, j]);
                    }
                    DocRule[i] = temp2.ToArray<double>();
                }

                double[] outputlabel = DocRule.Dot(W);
                
                for (int i = 0; i < outputlabel.Length; i++)
                {
                    double l = outputlabel[i];
                    if (l > threshold)
                        results[i] = "True";
                    else
                        results[i] = "False";                    
                }
                System.IO.File.WriteAllLines("RESULTES_GA", results);
            }            
        }

        private void weights_ga_matlab()
        {
            string path_MatlabFunctions = Environment.CurrentDirectory;
            // داده ورودی باید حتما در مسیر توابع متلب باشد            

            // توابع هزینه هم باید با عنوان
            //cost_func
            //در همان مسیر توابع متلب تعریف شوند.

            MLApp.MLApp matlab = new MLApp.MLApp();

            matlab.Execute("clear vars all;");

            matlab.Execute(@"cd '" + path_MatlabFunctions + "'");
            string temp = matlab.Execute("a=2");

            string x = matlab.Execute(@"[x]=GeneticForWighting()");
            //  string IDX = matlab.Execute("IDX");
            try
            {
                matlab.Execute("quit");
            }
            catch (Exception ex)
            {
            }            
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

        private List<Transaction> list2mat(List<string> messages,string type)
        {
            List<Transaction> trans = new List<Transaction>();
            if (type == "train")
                labels = new int[messages.Count];
            else
                testlabels = new int[messages.Count];

            if (AllKeyWords == null)
            {
                AllKeyWords = new List<string>();
                int j = 0;
                foreach (var message in messages)
                {                                        
                    List<string> items = new List<string>();
                    if (message.Contains("\t"))
                    {
                        char[] sep = { ' ', '\t' };
                        string[] mess = message.Trim().Split(sep);
                        for (int i = 0; i < mess.Length - 1; i++)
                        {
                            if (!items.Contains(mess[i]))
                                items.Add(mess[i]);
                            if (!AllKeyWords.Contains(mess[i]))
                                AllKeyWords.Add(mess[i]);
                        }
                        if (mess[mess.Length - 1] == "0")
                            if (type == "train")
                                labels[j] = 0;
                            else
                                testlabels[j] = 0;
                        else if (mess[mess.Length - 1] == "1")
                            if (type == "train")
                                labels[j] = 1;
                            else
                                testlabels[j] = 1;
                        else if (mess[mess.Length - 1] == "-1")
                            if (type == "train")
                                labels[j] = -1;
                            else
                                testlabels[j] = -1;
                    }
                    else
                    {
                        string[] mess = message.Trim().Split(' ');
                        for (int i = 0; i < mess.Length; i++)
                        {
                            if (!items.Contains(mess[i]))
                                items.Add(mess[i]);
                            if (!AllKeyWords.Contains(mess[i]))
                                AllKeyWords.Add(mess[i]);
                        }
                        if (type == "train")
                            labels[j] = -1;
                        else
                            testlabels[j] = -1;
                    }
                    j++;                    
                    items.Sort();
                    trans.Add(new Transaction(j, items.ToArray<string>()));
                }
                AllKeyWords.Sort();                
            }

            else
            {
                for (int i = 0; i < messages.Count; i++)
                {
                    List<string> items = new List<string>();
                    string[] mess = messages[i].Split('\t');
                    for (int j = 0; j < AllKeyWords.Count; j++)
                    {
                        if (messages[i].Contains(AllKeyWords[j]))
                            items.Add(AllKeyWords[j]);                        
                    }
                    items.Sort();
                    trans.Add(new Transaction(i + 1, items.ToArray<string>()));

                    if (mess.Length > 1)
                    {
                        if (type == "train")
                        {
                            if (mess[1] == "1")
                                labels[i] = 1;
                            else if (mess[1] == "0")
                                labels[i] = 0;
                            else
                                labels[i] = -1;
                        }
                        else
                        {
                            if (mess[1] == "1")
                                testlabels[i] = 1;
                            else if (mess[1] == "0")
                                testlabels[i] = 0;
                            else
                                testlabels[i] = -1;
                        }
                    }
                    else
                    {
                        if (type == "train")
                            labels[i] = -1;
                        else
                            testlabels[i] = -1;
                    }
                }               
            }
            return trans;
        }

        public void removekeys(int keynum)
        {
            if (keynum > AllKeyWords.Count)
                return;
            List<string> tempkey = new List<string>();
            int[] ind = new int[keynum];
            Random r = new Random();

            for (int i = 0; i < keynum; i++)
            {
                int temp = r.Next(AllKeyWords.Count);
                while (ind.Contains(temp))
                {
                    temp = r.Next(AllKeyWords.Count);
                }
                ind[i] = temp;
            }
            ind.Sort();

            for (int i = 0; i < keynum; i++)
            {
                tempkey.Add(AllKeyWords[ind[i]]);
            }
            AllKeyWords.Clear();
            foreach (string item in tempkey)
            {
                AllKeyWords.Add(item);
            }            
        }

        private void write_mat(double[,] mat, string path)
        {
            System.IO.File.Delete(path);
            string[] lines = new string[mat.GetLength(0)];
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                string row = "";
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    row += mat[i, j].ToString() + " ";
                }
                row += labels[i];
                lines[i] = row;
            }
            System.IO.File.WriteAllLines(path, lines);
        }
    }
}
