using System;
using System.Collections.Generic;

namespace DataStructurePractice{
        // min binary heap
    public class BinaryHeap<T> where T: IComparable{
        private List<T> treeList;
        // default binary heap constructor
        public BinaryHeap(){
            treeList = new List<T>(1);
        }

        // construct the binary heap with given size
        public BinaryHeap(int size){
            treeList = new List<T>(size);
        }

        // construct the binary heap from a given collection
        public BinaryHeap(T[] collection){
            int heapSize = collection.Length;
            treeList = new List<T>(heapSize);
            for(int i = 0; i < heapSize; ++i) Add(collection[i]);
        }

        // construct the binary tree from a given collection
        // use heapify method
        public BinaryHeap(IEnumerable<T> collection){
            treeList = new List<T>(collection);
            int heapSize = treeList.Count;
            for(int i = Math.Max(0, (heapSize - 1) / 2); i >= 0; --i){
                sink(i);
            }
        } 


        public bool IsEmpty(){
            return treeList.Count == 0;
        }

        public int Size(){
            return treeList.Count;
        }

        public bool Contains(T data){
            return treeList.Contains(data);
        }

        public void Clear(){
            treeList.Clear();
        }

        // return the smallest value in the binary heap
        public T Peek(){
            if(IsEmpty()) return default(T);
            return treeList[0];
        }

        // add a new node to the binary heap
        public void Add(T data){
            treeList.Add(data);
            swim(Size() - 1);
        }

        // removes the root of the binary heap
        public void Poll(){
            removeAt(0);
        }

        // remove a given data in the binary heap
        public bool Remove(T data){
            // search for data
            int heapSize = treeList.Count;
            int dataIdx = -1;
            for(int i = 0; i < heapSize; ++i){
                if(treeList[i].Equals(data)){
                    dataIdx = i;
                    break;
                }
            }
            if(dataIdx == -1) return false;
            
            removeAt(dataIdx);
            return true;
        }

        // swap two nodes
        private void swap(int idx1, int idx2){
            T temp = treeList[idx1];
            treeList[idx1] = treeList[idx2];
            treeList[idx2] = temp;
        }

        // perform bottom up node swim
        private void swim(int idx){
            int currentIdx = idx;
            int parentIdx = (currentIdx - 1) / 2;
            while(currentIdx > 0 && isLess(currentIdx, parentIdx)){
                swap(currentIdx, parentIdx);
                currentIdx = parentIdx;
                parentIdx = (currentIdx - 1) / 2;
            }
        }

        // perform up down node sink
        private void sink(int idx){
            int heapSize = Size();
            int currentIdx = idx;
            int leftIdx = 2 * idx + 1;
            int rightIdx = 2 * idx + 2;
            while(true){
                int swapIdx = leftIdx;
                if(rightIdx < heapSize && isLess(rightIdx, leftIdx)) swapIdx = rightIdx;
                if(currentIdx >= heapSize || isLess(currentIdx, swapIdx)) break;

                swap(currentIdx, swapIdx);
                currentIdx = swapIdx;
                leftIdx = 2 * currentIdx + 1;
                rightIdx = 2 * currentIdx + 2;
            }
        }

        // return true if node1 is smaller than node2
        private bool isLess(int idx1, int idx2){
            return treeList[idx1].CompareTo(treeList[idx2]) < 0;
        }

        // remove certain node at given index
        private void removeAt(int idx){
            if(idx < 0 || idx >= Size()) throw new ArgumentException("Index out of bound");
            swap(idx, Size() - 1);
            treeList.RemoveAt(Size() - 1);
        
            // select swimming or sinking
            int leftIdx = 2 * idx + 1;
            int rightIdx = 2 * idx + 2;
            if (treeList[idx].CompareTo(treeList[leftIdx]) < 0 && treeList[idx].CompareTo(treeList[rightIdx]) < 0)
                swim(idx);
            else   
                sink(idx);
        }
    }
}