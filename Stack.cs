using System;
using System.Collections.Generic;

public class Stack<T>{
    private LinkedList<T> list;
    
    public Stack(){
        list = new LinkedList<T>();
    }

    public int Size(){
        return list.Count();
    }

    public bool IsEmpty(){
        return Size() == 0;
    }

    public void Clear(){
        list.Clear();
    }

    public T Peek(){
        return list.Last();
    }

    public void Push(T data){
        list.AddLast(data);
    }

    public void Pop(){
        if(IsEmpty()) throw new Exception("Empty stack.");
        list.RemoveLast();
    }
}