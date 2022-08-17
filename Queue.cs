using System;
using System.Collections.Generic;

namespace DataStructurePractice{
        public class CustomQueue<T>{
        System.Collections.Generic.LinkedList<T> list;

        public CustomQueue(){
            list = new System.Collections.Generic.LinkedList<T>();
        }

        public int Size(){
            return list.Count;
        }

        public bool IsEmpty(){
            return IsEmpty() == 0;
        }

        public void Clear(){
            list.Clear();
        }

        public void Enqueue(T data){
            list.AddLast(data);
        }

        public void Dequeue(){
            if(IsEmpty()) throw new Exception("Empty queue");
            list.RemoveFirst();
        }
    }
}