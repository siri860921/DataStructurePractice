using System;

public class LinkedList<T>{
    private class Node{
        public Node next;
        public T data;

        public Node(T nodeData, Node nextNode){
            next = nextNode;
            data = nodeData;
        }
    }
    
    Node head;
    Node tail;
    int size = 0;

    public LinkedList(){
        head = null;
        tail = null;
        size = 0;
    }

    public bool IsEmpty(){
        return size == 0;
    }

    public int Size(){
        return size;
    }

    public bool Contains(T data){
        Node trav = head;
        while(trav != null){
            if(trav.data.Equals(data)) return true;
        }
        return false;
    }

    public void Add(T data){
        Node newNode = new Node(data, null);
        if(size == 0){
            head = newNode;
            tail = newNode;
            size++;
            return;
        }
        tail.next = newNode;
        tail = newNode;
        size++;
    }
    public void AddFront(T data){
        Node newNode = new Node(data, null);
        if(size == 0){
            head = newNode;
            tail = newNode;
            size++;
            return;
        }
        newNode.next = head;
        head = newNode;
        size++;
    }
    public void Insert(int idx, T data){
        if(idx < 0 || idx >= size) throw new Exception("Index out of bound");
        if(idx == 0){
            AddFront(data);
            return;
        }
        if(idx == size - 1){
            Add(data);
            return;
        }
        Node trav = head;
        int counter = 0;
        while(counter < idx){
            trav = trav.next;
            counter++;
        }
        Node newNode = new Node(data, trav.next);
        trav.next = newNode;
        size++;
    }

    public T PeekLast(){
        if(size == 0) throw new Exception("Empty List.");
        return tail.data;
    }

    public T PeekFront(){
        if(size == 0) throw new Exception("Empty List.");
        return head.data;
    }

    public void RemoveLast(){
        if(size == 0) throw new Exception("Empty List");
        Node trav = head;
        int counter = 0;
        while(counter < size - 1){
            counter++;
            trav = trav.next;
        }
        trav.next = null;
        tail.data = null;
        tail = trav;
        size--;
    }

    public void RemoveFront(){
        if(size == 0) throw new Exception("Empty List");
        Node temp = head.next;
        head.data = null;
        head.next = null;
        head = temp;
        size--;
    }

    public bool Remove(T data){
        Node trav = head;
        if(trav.data.Equals(data)){
            head.data = null;
            head = null;
            tail = null;
            size = 0;
            return true;
        }
        int counter = 0;
        while(trav != null){
            if(trav.next.data.Equals(data)){
                Node temp = trav.next;
                trav.next = temp.next;
                temp.data = null;
                temp.next = null;
                temp = null;
                if(counter == size - 1 - 1) tail = trav;
                size --;
                return true;
            }
            counter++;
            trav = trav.next;
        }
        return false;
    }

    public int IndexOf(T data){
        Node trav = head;
        int counter = 0;
        while(trav != null){
            if(trav.data.Equals(data)) return counter;
            counter++;
            trav = trav.next;
        }
        return -1;
    }

    public bool RemoveAt(int idx){
        if(idx < 0 || idx >= size) throw new Exception("Index out of bound");
        if(size == 0) return false;
        if(idx == 0){
            RemoveFront();
            return true;
        }
        if(idx == size - 1){
            RemoveLast();
            return true;
        }
        Node trav = head;
        counter = 0;
        while(counter < idx){
            trav = trav.next;
            counter++;
        }
        size--;
        Node temp = trav.next;
        trav.next = temp.next;
        temp.next = null;
        temp.data = null;
        return true;
    }
}