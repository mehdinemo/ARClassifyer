using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECLAT
{
    class Vertical_transaction
    {
        public List<string> itemset = new List<string>();
        public List<int> tids = new List<int>();
        public List<int> onetids = new List<int>();
        public double support = new double();
        public double confidence = new double();

        public Vertical_transaction (List<string> set, List<int> ids, List<int> onids, double sup, double conf)
        {
            this.itemset = set;
            this.tids = ids;
            this.onetids = onids;
            this.support = sup;
            this.confidence = conf;
        }

        public Vertical_transaction(List<string> set, List<int> ids, double sup)
        {
            this.itemset = set;
            this.tids = ids;           
            this.support = sup;            
        }
    }
}
