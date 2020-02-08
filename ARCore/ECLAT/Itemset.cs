using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECLAT
{
    public class Itemset
    {
        public List<String> items=new List<string>();
        public double support=new double();
        public double confidence = new double();

        public Itemset()
        {
            items=new List<string>();
            support=new double();        
        }

        public Itemset(List<String> elements, double sup, double conf)
        {
            this.items = elements;
            this.support = sup;
            this.confidence = conf;
        }

        public String ToString()
        {
            string s="";
            foreach(string item in items)
            {
                s+=item+" ";
            }
            s+="\t"+support.ToString();
            return s;
        }
    }
}
