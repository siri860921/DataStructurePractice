using System;
using System.Collections.Generic;

public class CustomStack<T>{
    private LinkedList<T> list;
    
    public CustomStack(){
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