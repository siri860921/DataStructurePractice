using System;

namespace DataStructures {
    public class FenwickTree {
        private int arrSize;
        private long[] originalData;
        private long[] tree;

        public FenwickTree(long[] arr) {
            if(arr == null) throw new Exception("Array argument cannot be null.");
            arrSize = arr.Length;
            originalData = new long[arr.Length];
            arr.CopyTo(originalData, 0);
            tree = new long[arr.Length + 1];

            // fill initial values in the tree
            tree[0] = 0L;
            for(int i = 0; i < arrSize; ++i) tree[i + 1] = arr[i];

            // construct fenwick tree
            for(int i = 1; i <= arrSize; ++i) {
                int writeInIdx = i + LSB(i);
                if(writeInIdx > arrSize) continue;
                tree[writeInIdx] += tree[i];
            }
        }

        // returns the element prefix sum between [0, idx]
        public long PrefixSum(int arrIdx) {
            if(arrIdx < 0 || arrIdx >= arrSize) throw new Exception("Index out of bound.");
            long prefixSum = 0;
            int downSwimIdx = arrIdx + 1;
            while(downSwimIdx > 0) {
                prefixSum += tree[downSwimIdx];
                downSwimIdx -= LSB(downSwimIdx);
            }
            return prefixSum;
        }

        // return the element sum between [lowBoundIdx, upBoundIdx]
        public long Sum(int arrLowBoundIdx, int arrUpBoundIdx) {
            if(arrUpBoundIdx < 0 || arrUpBoundIdx >= arrSize) throw new Exception("Index out of bound.");
            if(arrLowBoundIdx < 0 || arrLowBoundIdx >= arrSize) throw new Exception("Index out of bound");
            if(arrLowBoundIdx == 0) return PrefixSum(arrUpBoundIdx);
            return PrefixSum(arrUpBoundIdx) - PrefixSum(arrLowBoundIdx - 1);
        }

        // update the values in the fenwick tree as an element at the given index is updated
        public void Set(int arrIdx, long val) {
            if(arrIdx < 0 || arrIdx >= arrSize) throw new Exception("Index out bound.");
            long delta = val - originalData[arrIdx];
            int travIdx = arrIdx + 1;
            while(travIdx <= arrSize) {
                tree[travIdx] += delta;
                travIdx += LSB(travIdx); 
            }
        }

        // returns the least significant bit (LSB) of the index
        // lsb(108) = lsb(0b1101100) =     0b100 = 4
        // lsb(104) = lsb(0b1101000) =    0b1000 = 8
        private static int LSB(int treeIdx) {
            int e = 0;
            while((treeIdx & (1 << e)) != 1) e++;
            return 1 << e;
        }
    }
}
