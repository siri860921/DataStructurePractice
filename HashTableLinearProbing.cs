using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures {
    public class HashTableLinearProbing<TKey, TValue>: HashTableOpenAdressing<TKey, TValue> {
        public HashTableLinearProbing() : base(DEFAULT_CAPACITY) { }
        public HashTableLinearProbing(int capacity) : base(capacity) { }
        public HashTableLinearProbing(int capacity, double loadingFactor): base(capacity, loadingFactor) { }

        // linear constant in the linear probing
        // the linear constant should satisfy GCD(capacity, LINEAR_CONSTANT) = 1
        private static int LINEAR_CONSTANT = 17;

        protected override void adjustCapacity() {
            while(gcd(capacity, LINEAR_CONSTANT) != 1) {
                capacity++;
            } 
        }

        protected override int probe(int x) {
            return LINEAR_CONSTANT * x;
        }

        protected override void setupSecondaryHash(TKey key) {
            throw new NotImplementedException();
        }
    }
}
