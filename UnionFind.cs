using System;
using System.Collections;

namespace DataStructurePractice
{
    public class UnionFind
    {
        int[] sz; // stores the number of nodes in each group
        int[] ids; // store the root of each node
        int size;
        int numberOfGroups;

        public UnionFind(int collectionSize)
        {
            if (collectionSize < 0) throw new Exception("The size of the Union Find must greater than 0.");
            sz = new int[collectionSize];
            ids = new int[collectionSize];
            numberOfGroups = collectionSize;

            for (int i = 0; i < collectionSize; ++i)
            {
                sz[i] = 1;
                ids[i] = i;
            }
        }

        public int Size()
        {
            return size;
        }

        // returns true if p and q are in the same group
        public bool IsConnected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        // returns current number of groups
        public int NumberOfGroups()
        {
            return numberOfGroups;
        }

        // returns the group size that p belongs to
        public int groupSize(int p)
        {
            return sz[Find(p)];
        }

        // find the group that p belongs to
        public int Find(int p)
        {
            if (p < 0 || p >= ids.Length) throw new Exception("Index out of bound.");

            // find the belonging group
            int root = p;
            while (root != ids[root])
            {
                root = ids[root];
            }

            // route compression
            int next = p;
            while (next != root)
            {
                next = ids[next];
                ids[next] = root;
            }

            return root;
        }

        // unify the groups the groups that respectively belong to p and q
        public void Unify(int p, int q)
        {
            int rootP = Find(p);
            int rootQ = Find(q);
            if (rootP == rootQ) return;

            if (ids[rootP] > ids[rootQ])
            {
                ids[rootQ] = rootP;
                sz[rootP] += sz[rootQ];
                sz[rootQ] = 0;
            }
            else
            {
                ids[rootP] = rootQ;
                sz[rootQ] += sz[rootP];
                sz[rootP] = 0;
            }
            numberOfGroups--;
        }
    }
}