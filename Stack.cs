using System;
using System.Collections.Generic;

namespace DataStructurePractice
{
    public class CustomStack<T>
    {
        private System.Collections.Generic.LinkedList<T> list;

        public CustomStack()
        {
            list = new System.Collections.Generic.LinkedList<T>();
        }

        public int Size()
        {
            return list.Count;
        }

        public bool IsEmpty()
        {
            return Size() == 0;
        }

        public void Clear()
        {
            list.Clear();
        }

        public T Peek()
        {
            return list.Last.Value;
        }

        public void Push(T data)
        {
            list.AddLast(data);
        }

        public void Pop()
        {
            if (IsEmpty()) throw new Exception("Empty stack.");
            list.RemoveLast();
        }
    }
}