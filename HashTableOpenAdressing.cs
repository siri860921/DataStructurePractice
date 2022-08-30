using System;
using System.Collections.Generic;

namespace DataStructures {
    public abstract class HashTableOpenAdressing<TKey, TValue>{
        protected double loadFactor;
        protected int capacity;
        protected int threshold;
        protected int modificationCount;

        protected int keyCount;
        protected int usedBuckets;
        protected TKey TUMB_STONE = (TKey)new object();

        protected TKey[] keys;
        protected TValue[] values;

        protected static int DEFAULT_CAPACITY = 7;
        protected static double DEFAULT_LOAD_FACTOR = 0.65;

        protected HashTableOpenAdressing() : this(DEFAULT_CAPACITY) { }

        protected HashTableOpenAdressing(int capacity) {
            if(capacity < 0) throw new Exception("Capacity must be larger than zero");
            this.capacity = capacity >= DEFAULT_CAPACITY ? capacity : DEFAULT_CAPACITY;
            this.loadFactor = DEFAULT_LOAD_FACTOR;
            this.threshold = (int)(capacity * DEFAULT_LOAD_FACTOR);

            keys = new TKey[capacity];
            values = new TValue[capacity];
            keyCount = 0;
            usedBuckets = 0;
            modificationCount = 0;
        }

        protected HashTableOpenAdressing(int capacity, double loadingFactor) {
            if(capacity < 0) throw new Exception("Capacity must be larger than zero.");
            if(loadFactor <= 0 || loadFactor >= 1) throw new Exception("Load factor must be in range (0, 1).");
            this.capacity = capacity >= DEFAULT_CAPACITY ? capacity : DEFAULT_CAPACITY;
            this.loadFactor = loadingFactor;
            this.threshold = (int)(capacity * loadingFactor);

            keys = new TKey[capacity];
            values = new TValue[capacity];
            keyCount = 0;
            usedBuckets = 0;
            modificationCount = 0;
        }

        public TValue this[TKey key] {
            get {
                if(key == null) throw new Exception("Key cannot be null.");
                int keyHash = key.GetHashCode();
                int bucketIdxH1 = normalizeIndex(keyHash);
                int firstTumbIdx = -1;
                setupProbing(key);
                for(int i = 0; true; ++i) {
                    int probingIdx = normalizeIndex(bucketIdxH1 + probe(i));
                    if(keys[probingIdx].Equals(TUMB_STONE)) {
                        if(firstTumbIdx == -1) firstTumbIdx = probingIdx;
                        continue;
                    }
                    if(keys[probingIdx] != null) {
                        if(keys[probingIdx].Equals(key)) {
                            TValue value2Return = values[probingIdx];
                            if(firstTumbIdx != -1) {
                                keys[firstTumbIdx] = keys[probingIdx];
                                values[firstTumbIdx] = values[probingIdx];
                                keys[probingIdx] = default(TKey);
                                values[probingIdx] = default(TValue);
                                usedBuckets--;
                            }
                            return value2Return;
                        }
                        else continue;
                    }
                    else return default(TValue);
                }
            }
        }


        public void Clear() {
            for(int i = 0; i < capacity; ++i) {
                keys[i] = default(TKey);
                values[i] = default(TValue);
            }
            keyCount = 0;
            usedBuckets = 0;
            modificationCount++;
        }

        public int Size() {
            return keyCount;
        }
        
        public int Capacity() {
            return capacity;
        }

        public bool IsEmpty() {
            return keyCount == 0;
        }

        public void Add(TKey key, TValue val) {
            if(key == null) throw new Exception("Key cannot be null.");
            if(usedBuckets >= threshold) resizeTable();

            int keyHash = key.GetHashCode();
            int bucketIdxH1 = normalizeIndex(keyHash);
            int firstTumbIdx = -1;
            setupProbing(key);
            for(int i = 0; true; ++i) {
                int probingIdx = normalizeIndex(bucketIdxH1 + probe(i));
                if(keys[probingIdx].Equals(TUMB_STONE)) {
                    if(firstTumbIdx == -1) firstTumbIdx = probingIdx;
                    continue;
                }
                if(keys[probingIdx] != null) {
                    // their the key has already exists
                    // update its value
                    if(keys[probingIdx].Equals(key)) {
                        values[probingIdx] = val;
                        return;
                    }
                }
                // probe to a null space
                else {
                    // if tumb is passed while probing
                    if(firstTumbIdx != -1) {
                        keys[firstTumbIdx] = key;
                        values[firstTumbIdx] = val;
                        keyCount++;
                        return;
                    }
                    else {
                        keys[probingIdx] = key;
                        values[probingIdx] = val;
                        keyCount++;
                        usedBuckets++;
                        return;
                    }
                }
            }
        }

        public bool ContainsKey(TKey key) {
            if(key == null) throw new Exception("Key cannot be null.");
            int keyHash = key.GetHashCode();
            int bucketIdxH1 = normalizeIndex(keyHash);
            int firstTumbIdx = -1;
            setupProbing(key);
            for(int i = 0; true; ++i) {
                int probingIdx = bucketIdxH1 + probe(i);
                if(keys[probingIdx].Equals(TUMB_STONE)) {
                    if(firstTumbIdx == -1) firstTumbIdx = probingIdx;
                    continue;
                }
                if(keys[probingIdx] != null) {
                    if(keys[probingIdx].Equals(key)) {
                        if(firstTumbIdx != -1) {
                            keys[firstTumbIdx] = keys[probingIdx];
                            values[firstTumbIdx] = values[probingIdx];
                            keys[probingIdx] = default(TKey);
                            values[probingIdx] = default(TValue);
                            usedBuckets--;
                        }
                        return true;
                    }
                    else continue;
                }
                else return false;
            }
        }

        public bool Remove(TKey key) {
            int keyHash = key.GetHashCode();
            int bucketIdxH1 = normalizeIndex(keyHash);
            setupProbing(key);
            for(int i = 0; true; ++i) {
                int probingIdx = normalizeIndex(bucketIdxH1 + probe(i));
                if(keys[probingIdx] == null) return false;
                if(keys[probingIdx].Equals(key)) {
                    keys[probingIdx] = TUMB_STONE;
                    values[probingIdx] = default(TValue);
                    keyCount--;
                    return true;
                }
            }
        }

        protected void resizeTable() {
            capacity = capacity * 2 + 1;
            adjustCapacity();
            TKey[] newKeys = new TKey[capacity];
            TValue[] newValues = new TValue[capacity];
            usedBuckets = 0;
            keyCount = 0;

            int oldKeyCount = keys.Length;
            for(int i = 0; i < oldKeyCount; ++i) {
                if(keys[i] == null || keys[i].Equals(TUMB_STONE)) continue;
                int keyHash = keys[i].GetHashCode();
                int bucketIdxH1 = normalizeIndex(keyHash);
                for(int j = 0; true; ++j) {
                    int probingIdx = normalizeIndex(bucketIdxH1 + probe(j));
                    if(newKeys[probingIdx] == null) {
                        newKeys[probingIdx] = keys[i];
                        newValues[probingIdx] = values[i];
                        keys[i] = default(TKey);
                        values[i] = default(TValue);
                        usedBuckets++;
                        keyCount++;
                        break;
                    }
                }
            }
            keys = newKeys;
            values = newValues;
        }

        protected int normalizeIndex(int keyHash) {
            return (keyHash & 0x7FFFFFFF) % capacity;
        }

        // find  the greatest common denominator of a and b
        protected int gcd(int a, int b) {
            if(b == 0) return a;
            return gcd(a, a % b);
        }

        protected abstract void setupProbing(TKey key);

        protected abstract int probe(int x);

        protected abstract void adjustCapacity();


    }
}
