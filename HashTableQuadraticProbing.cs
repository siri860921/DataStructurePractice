using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures {
    public class HashTableQuadraticProbing<TKey, TValue>: HashTableOpenAdressing<TKey, TValue> {
        public HashTableQuadraticProbing() : base(DEFAULT_CAPACITY) { }
        public HashTableQuadraticProbing(int capacity): base(capacity) { }
        public HashTableQuadraticProbing(int capacity, double loadingFactor) : base(capacity, loadingFactor) { }

        protected override void adjustCapacity() {
            int log = (int)Math.Ceiling(Math.Log(capacity, 2));
            capacity = 2 ^ log;
        }

        protected override int probe(int x) {
            return (x * x + x) / 2;
        }

        protected override void setupProbing(TKey key) {
            throw new NotImplementedException();
        }
    }
}
