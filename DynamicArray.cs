using System;

public class DynamicArray<T>{
    int size = 0;
    int capacity = 0;
    T[] data;

    static public T this[int idx]{
    get {
        if(idx >= size) throw new Exception("Index out of bound.");
        return data[idx];
    }
    set {
        if(idx >= size) throw new Exception("Index out of bound.");
        data[idx] = value;
    }
}

    public DynamicArray(){
        this.capacity = 16;
        this.data = new T[this.capacity];
    }

    public DynamicArray(int capacity){
        if(capacity <= 0) throw new Exception("Input capacity must be larger than 0.");
        this.capacity = capacity;
        this.data = new T[capacity];
    }

    public int Length(){
        return size;
    }

    public bool IsEmpty(){
        return size = 0 ? true : false;
    }

    public void Clear(){
        data = null;
        capacity = 16;
        data = new T[capacity];
        size = 0;
    }

    public void Add(T elem){
        if(size + 1 > capacity) capacity *= 2;
        T[] newData = new T[capacity];
        for(int i = 0; i < size; ++i){
            newData[i] = data[i];
        }
        size++;
        newData[size - 1] = elem;
        data = null;
        data = newData;
    }

    public RemoveAt(int idx){
        if(idx < 0 || idx >= size) throw new Exception("Index out of bounds.");
        for(int i = 0; i < size; ++i){
            if(i > idx) data[i - 1] = data[i];
        }
        size--;
    }

    public bool Remove(T elem){
        for(int i = 0; i < size; ++i){
            if(data.Equals(elem)){
                 RemoveAt(i);
                 return true;
            }
        }
        return false;
    }

    public bool Contain(T elem){
        for(int i = 0; i < size; ++i){
            if(data[i].Equals(elem)) return true;
        }
        return false;
    }
    
    public int IndexOf(T elem){
        for(int i = 0; i < size; ++i){
            if(data[i].Equals(elem)) return i;
        }
        return -1;
    }
}