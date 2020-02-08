using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Excel = Microsoft.Office.Interop.Excel;

namespace ECLAT
{
    public class Eclat
    {        
        List<Transaction> data = new List<Transaction>();
        int[] labels;
        public List<Itemset> L_itemsets = new List<Itemset>();
        public List<Rule> eclat_rules = new List<Rule>();
        public double min_sup;
        public double min_conf;
        int nr_of_transactions = new int();

        List<Itemset> eclat_Litemsets = new List<Itemset>();
        double support = new double();
        double confidence = new double();
        Eclat eclat;

        public Eclat() { }

        public Eclat(List<Transaction> data, int[] labels,double min_sup, double min_conf)
        {
            this.data = data;
            this.labels = labels;
            this.min_sup = min_sup;
            this.min_conf = min_conf;
        }

        public Eclat(List<Transaction> data, double min_sup, double min_conf)
        {
            this.data = data;           
            this.min_sup = min_sup;
            this.min_conf = min_conf;
        }

        private void start_eclat()
        {
            nr_of_transactions = data.Count;
            //find all unique items
            List<string> one_itemsets = get_1_itemsets(data);
            List<Vertical_transaction> vertical_data;
            //convert database and get support for 1-itemsets
            if (labels != null)
                vertical_data = convert_to_vertical(one_itemsets, data, labels);
            else
                vertical_data = convert_to_vertical(one_itemsets, data);
            
            foreach (Vertical_transaction t in vertical_data)
            {
                L_itemsets.Add(new Itemset(t.itemset, t.support, t.confidence));
            }
            //loop
            while (vertical_data.Count > 1)
            {
                vertical_data = generate_candidates(vertical_data);
                foreach (Vertical_transaction t in vertical_data)
                    L_itemsets.Add(new Itemset(t.itemset, t.support, t.confidence));
            }
        }

        // generate candidate itemset
        private List<Vertical_transaction> generate_candidates(List<Vertical_transaction> vertical_data)
        {
            List<Vertical_transaction> new_data = new List<Vertical_transaction>();
            if (vertical_data.Count == 0)
                return new_data;            
            if (vertical_data[0].itemset.Count == 1)
            {
                foreach (Vertical_transaction t1 in vertical_data)
                {
                    foreach (Vertical_transaction t2 in vertical_data)
                    {
                        if (t1.itemset[0] != t2.itemset[0])
                        {
                            List<string> items_list = new List<string>();
                            items_list.Add(t1.itemset[0]);
                            items_list.Add(t2.itemset[0]);
                            //deal with duplicates
                            items_list.Sort();
                            List<int> tids = new List<int>();
                            List<int> onetids = new List<int>();
                            tids = t1.tids.Intersect(t2.tids).ToList<int>();
                            onetids = t1.onetids.Intersect(t2.onetids).ToList<int>();
                            double support = get_support(tids);
                            double confidence = get_confidence(tids, onetids);

                            Vertical_transaction new_transaction = new Vertical_transaction(items_list, tids, onetids, support, confidence);
                            if (is_new(new_transaction, new_data) && support >= min_sup && confidence >= min_conf)
                            {
                                new_data.Add(new_transaction);
                                string log = "";
                                foreach (string itemset in new_transaction.itemset)                                
                                    log += itemset.ToString() + ", ";                                
                            }
                        }
                    }
                }
                return new_data;
            }

            else
            {
                string log = "";
                foreach (Vertical_transaction v in vertical_data)
                {
                    foreach (string s in v.itemset) { log += s + ", "; }
                    log += "\n";
                }

                int k_count = vertical_data[0].itemset.Count;
                foreach (Vertical_transaction t1 in vertical_data)
                {
                    t1.itemset.Sort();
                    foreach (Vertical_transaction t2 in vertical_data)
                    {
                        t2.itemset.Sort();                        
                        bool merge = true;
                        List<string> new_set = new List<string>();
                        for (int iterator = 0; iterator < k_count; iterator++)
                        {
                            if (t1.itemset[iterator].Equals(t2.itemset[iterator]) && iterator < k_count - 1)                            
                                new_set.Add(t1.itemset[iterator]);                            
                            else if (!t1.itemset[iterator].Equals(t2.itemset[iterator]) && iterator < k_count - 1)                            
                                merge = false;                            
                        }
                        if (t1.itemset[k_count - 1].Equals(t2.itemset[k_count - 1]))                        
                            merge = false;                        
                        if (merge == true)
                        {
                            new_set.Add(t1.itemset[k_count - 1]);
                            new_set.Add(t2.itemset[k_count - 1]);

                            List<int> tids = t1.tids.Intersect(t2.tids).ToList<int>();
                            List<int>  onetids= t1.onetids.Intersect(t2.onetids).ToList<int>();
                            double support = get_support(tids);
                            double confidence = get_confidence(tids, onetids);
                            Vertical_transaction new_transaction = new Vertical_transaction(new_set, tids, onetids, support, confidence);
                            if (is_new(new_transaction, new_data) && support >= min_sup && confidence >= min_conf)                            
                                new_data.Add(new_transaction);
                        }                        
                    }
                }
                return new_data;
            }
        }

        //check if vertical transaction is in list
        private bool is_new(Vertical_transaction new_transaction, List<Vertical_transaction> new_data)
        {
            foreach (Vertical_transaction v in new_data)
            {
                var diff = v.itemset.Except(new_transaction.itemset);
                if (diff.ToList().Count == 0)
                {
                    return false;
                }
            }
            return true;
        }

        //convert database to vertical format
        private List<Vertical_transaction> convert_to_vertical(List<string> one_itermsets, List<Transaction> data, int[] labels)
        {
            List<Vertical_transaction> vertical_database = new List<Vertical_transaction>();

            foreach (string item in one_itermsets)
            {
                List<int> tids = new List<int>();
                List<int> onestids = new List<int>();
                List<string> itemset = new List<string>();
                itemset.Add(item);
                int i = 0;
                foreach (Transaction t in data)
                {                    
                    if (t.items.Contains(item))
                    {
                        tids.Add(t.tid);
                        if (labels[i] == 1)
                            onestids.Add(t.tid);
                    }
                    i++;
                }
                double support = get_support(tids);
                double confidence = get_confidence(tids, onestids);
                if (support >= min_sup && confidence >= min_conf)
                    vertical_database.Add(new Vertical_transaction(itemset, tids, onestids, support, confidence));
            }
            return vertical_database;
        }

        //convert database to vertical format
        private List<Vertical_transaction> convert_to_vertical(List<string> one_itermsets, List<Transaction> data)
        {
            List<Vertical_transaction> vertical_database = new List<Vertical_transaction>();

            foreach (string item in one_itermsets)
            {
                List<int> tids = new List<int>();                
                List<string> itemset = new List<string>();
                itemset.Add(item);                
                foreach (Transaction t in data)
                {
                    if (t.items.Contains(item))                    
                        tids.Add(t.tid);
                }
                double support = get_support(tids);
                if (support >= min_sup)
                    vertical_database.Add(new Vertical_transaction(itemset, tids, support));
            }
            return vertical_database;
        }

        //find itemset with 1 element
        private List<string> get_1_itemsets(List<Transaction> data)
        {
            List<string> list = new List<string>();
            foreach (Transaction t in data)
            {
                for (int i = 0; i < t.items.Count; i++)
                {
                    if (!list.Contains(t.items[i]))
                    {
                        list.Add(t.items[i]);
                    }
                }
            }
            return list;
        }

        //calculate support
        private double get_support(List<int> t)
        {
            double support = t.Count;
            return support;
        }

        private double get_confidence(List<int> t, List<int> ones)
        {
            double confidence = ones.Count / (double)t.Count;
            return confidence;
        }

        //remove infrequent candidates from list
        private List<Vertical_transaction> remove_infrequent(double support, List<Vertical_transaction> candidates)
        {
            foreach (Vertical_transaction t in candidates)
            {
                if (t.support < support)
                {
                    candidates.Remove(t);
                }
            }
            return candidates;
        }

        public Eclat Run(double supp, double conf)
        {
            confidence = conf;
            support = supp;
            
            if (labels != null)
                eclat = new Eclat(data, labels, support, confidence);
            else
                eclat = new Eclat(data, support, confidence);
            eclat.start_eclat();
            eclat_Litemsets = eclat.L_itemsets;

            //generate
            Rules_generator generator = new Rules_generator(support, confidence, eclat_Litemsets);
            generator.generate_rules();
            eclat_rules = generator.rules;
            eclat.eclat_rules = eclat_rules;
            //save                       
            //Data_saver saver = new Data_saver(eclat_rules, eclat_Litemsets);
            //saver.save();
            return eclat;
        }        
    }    
}
