using System;
using System.Collections.Generic;

namespace DataStructures {
    public abstract class SuffixArray {
        protected int size;
        protected int[] T; // T is the text
        protected int[] sa; // sorted suffix array values
        protected int[] lcps; // longest common prefix array

        public SuffixArray(int[] text) {
            if(text == null) throw new Exception("Text cannot be null.");
            T = text;
            size = text.Length;
        }

        public int TextLengh() {
            return T.Length;
        }

        public int[] GetSuffixArray() {
            if(sa == null) return null;
            return sa;
        }

        protected void buildSuffixArray() {
            if(sa == null || sa.Length < 1) return;
            construct();
        }

        protected void buildLCPArray() {
            if(lcps == null || lcps.Length < 1) return;
            if(sa == null || sa.Length < 1) throw new Exception("Suffix array has not been constructed.");
            kasai();
        }

        protected static int[] toIntArray(string str) {
            if(str == null) throw new Exception("String cannot be null.");
            int[] t = new int[str.Length];
            for(int i = 0; i < str.Length; ++i) t[i] = str[i];
            return t;
        }

        // use Kasai algorithm to build LCP array
        // http://www.mi.fu-berlin.de/wiki/pub/ABI/RnaSeqP4/suffix-array.pdf
        private void kasai() {
            lcps = new int[size];
            int[] invSuffixArray = new int[size];
            for(int i = 0; i < size; ++i) invSuffixArray[sa[i]] = i;

            int len = 0;
            for(int i = 0; i < size; ++i) {
                if(invSuffixArray[i] == 0) {
                    lcps[invSuffixArray[i]] = 0;
                    continue;
                }
                int saIdx = invSuffixArray[i];
                int prevSuffixValue = sa[saIdx - 1];
                while((i + len < size) && (prevSuffixValue + len < size) && (T[i + len] == T[prevSuffixValue + len])) len++;
                lcps[saIdx] = len;
                if(len > 0) len--;
            }
        }

        protected abstract void construct();
    }
}
