using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures {
    public class HashTableDoubleHashing<TKey, TValue>: HashTableOpenAdressing<TKey, TValue> where TKey: ISecondaryHash<TKey> {
        int hash2;

        public HashTableDoubleHashing() : base(DEFAULT_CAPACITY) { }

        public HashTableDoubleHashing(int capacity) : base(capacity) { }

        public HashTableDoubleHashing(int capacity, int loadFactor): base(capacity, loadFactor) { }

        // adjust the capacity
        // the capacity should satisfy GCD(capacity, hash2)
        protected override void adjustCapacity() {
            while(gcd(capacity, hash2) != 1) capacity++;
        }

        protected override int probe(int x) {
            return x * hash2;
        }

        protected override void setupSecondaryHash(TKey key) {
            hash2 = normalizeIndex(key.GetSecondaryHash());
            if(hash2 == 0) hash2 = 1;
        }
    }

    public interface ISecondaryHash<TKey> {
        int GetSecondaryHash();
    }

}
