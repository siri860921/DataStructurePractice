using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures {
    public class SuffixArray: SuffixArrayBase {
        public SuffixArray(string text): base(text) {
            this.buildSuffixArray();
            this.buildLCPArray();
        }      

        // construct the suffix array
        protected override void construct() {
            SortedDictionary<string, int> sortedDic = new SortedDictionary<string, int>();
            for(int i = 0; i < size; ++i) {
                sortedDic.Add(T.Substring(i), i);
            }
        }
    }   
}
