using System;
using System.Collections.Generic;

namespace DataStructurePractice{

    public class HashTableSeparateChaining<TKey, TValue> {
        class Entry {
            private int hashValue;
            private TKey key;
            private TValue val;

            public Entry(TKey key, TValue val) {
                this.key = key;
                this.val = val;
                this.hashValue = key.GetHashCode();
            }

            public int HashValue { get => hashValue; }
            public TKey Key { get => key; }
            public TValue Value { get => val; set => val = value; }
        }

        private static int DEFAULT_CAPACITY = 3;
        private static double DEFAULT_LOAD_FACTOR = 0.75;

        private double maxLoadFactor;
        private int capacity;
        private int size;
        private int threshold;
        private List<Entry>[] table;

        public HashTableSeparateChaining() : this(DEFAULT_CAPACITY, DEFAULT_LOAD_FACTOR) { }

        public HashTableSeparateChaining(int capacity) {
            if(capacity < 0) throw new Exception("Capacity must be larger than 0.");
            this.capacity = capacity;
            this.maxLoadFactor = DEFAULT_LOAD_FACTOR;
            this.size = 0;
            this.threshold = (int)(this.capacity * DEFAULT_LOAD_FACTOR);
            table = new List<Entry>[this.capacity];
        }

        public HashTableSeparateChaining(int capacity, double maxLoadFactor) {
            if(capacity < 0) throw new Exception("Capacity must be larger than 0.");
            if(maxLoadFactor <= 0) throw new Exception("Max load factor must be larger than 0.");
            this.maxLoadFactor = maxLoadFactor;
            this.capacity = capacity;
            this.size = 0;
            this.threshold = (int)(capacity * maxLoadFactor);
            table = new List<Entry>[this.capacity];
        }

        public int Size() {
            return size;
        }

        public bool IsEmpty() {
            return size == 0;
        }

        public void Clear() {
            Array.Clear(table, 0, capacity);
            size = 0;
        }

        public bool ContainsKey(TKey key) {
            int bucketIdx = normalizedIndex(key.GetHashCode());
            Entry existEntry = bucketSeekEntry(bucketIdx, key);
            return existEntry != null;
        }

        public void Add(TKey key, TValue value) {
            if(key == null) throw new Exception("Key cannot be null");
            Entry newEntry = new Entry(key, value);
            int bucketIdx = normalizedIndex(newEntry.HashValue);
            bucketInsertEntry(bucketIdx, newEntry);
        }

        // converts a hash value to an index
        // the index is ensured to be in the domain of [0, capacity)
        private int normalizedIndex(int hashValue) {
            return (hashValue & 0x7FFFFFFF) % capacity;
        }

        // insert an entry if the entry does not exist in the hash table
        // if the entry has already existed
        // update the entry value
        private void bucketInsertEntry(int bucketIdx, Entry entry) {
            if(entry == null) throw new Exception("Entry cannot be null");
            if(table[bucketIdx] == null) table[bucketIdx] = new List<Entry>();
            List<Entry> bucket = table[bucketIdx];
            Entry existEntry = bucketSeekEntry(bucketIdx, entry.Key);
            if(existEntry == null) {
                bucket.Add(entry);
                // enlarge the hash table if the size exceed the threshold
                size++;
                if(size > threshold) resizeTable();
            }
            else {
                // update the entry value
                existEntry.Value = entry.Value;
            }
        }

        // find and return a particular entry in a given bicket if it exist
        // returns null otherwise
        private Entry bucketSeekEntry(int bucketIdx, TKey key) {
            if(key == null) return null;
            List<Entry> bucket = table[bucketIdx];
            if(bucket == null) return null;
            foreach(Entry entry in bucket) {
                if(entry.Key.Equals(key)) return entry;
            }
            return null;
        }

        private void resizeTable() {

        }
    }   
}