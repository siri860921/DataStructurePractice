using System;

public class DoublyLinkedList<T>{
    private class Node{
        public Node prev;
        public Node next;
        public T data;

        public Node(T data, Node prevNode, Node nextNode){
            this.prev = prevNode;
            this.next = nextNode;
            this.data = data;
        }
    }

    private Node head;
    private Node tail;
    private int size;

    public DoublyLinkedList(){
        this.head = null;
        this.tail = null;
        this.size = 0;
    }
    
    public void Clear(){
        Node trav = head;
        while(trav != null){
            Node temp = trav.next;
            trav.prev = null;
            trav.next = null;
            trav.data = null;
            trav = temp;
            temp = null;
        }
        size = 0;
        head = null;
        tail = null;
    }

    public int Size(){
        return size;
    }

    public bool IsEmpty(){
        return size == 0;
    }

    public bool Contain(T data){
        Node trav = head;
        while(trav != null){
            if(trav.data.Equals(data)) return true;
            trav = trav.next;
        }
        return false;
    }

    public void Add(T data){
        Node lastNode = new Node(data, null, null);
        if(size == 0){
            head = lastNode;
            tail = lastNode;
            return;
        }
        tail.next = lastNode;
        lastNode.prev = tail;
        tail = lastNode;
        size++;
    }

    public void AddFront(T data){
        Node frontNode = new Node(data, null, null);
        if(size == 0){
            head = frontNode;
            tail = frontNode;
            return;
        }
        head.prev = frontNode;
        frontNode.next = head;
        head = frontNode;
        size++;        
    }

    public void Insert(int idx, T data){
        if(idx < 0 || idx >= size) throw new Exception("Index out of bound.");
        size++;
        if(idx = 0){
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
        Node insertNode = new Node(data, trav, trav.next);
        trav.next.prev = insertNode;
        trav.next = insertNode;
    }

    public T PeekFirst(){
        if(size == 0) throw new Exception("Empty list.");
        return head.data;
    }

    public T PeekLast(){
        if(size == 0) throw new Exception("Empty list.");
        return tail.data;
    }

    public void RemoveFirst(){
        if(size == 0) throw new Exception("Empty list.");
        Node temp = head.next;
        temp.prev = null;
        head.data = null;
        head.next = null;
        head = temp;
        temp = null;
        size--;
    }

    public void RemoveLast(){
        if(size == 0) throw new Exception("Empty list.");
        Node temp = tail.prev;
        temp.next = null;
        tail.data = null;
        tail.prev = null;
        tail = temp;
        temp = null;
        size--;
    }

    public bool Remove(T data){
        Node trav = head;
        while(trav != null){
            if(trav.data.Equals(data)){
                trav.data = null;
                if(size == 1){
                    head = null;
                    tail = null;
                    size = 0;
                    return true;
                }
                size--;
                trav.prev.next = trav.next;
                trav.next.prev = trav.prev;
                trav.prev = null;
                trav.next = null;
                return true;
            }
        }
        return false;
    }

    public int IndexOf(T data){
        Node trav = head;
        int counter = 0;
        while(trav != null){
        if(trav.data.Equals(data))  return counter;
            trav = trav.next;
            counter++;
        }
        return -1;
    }

    public bool RemoveAt(int idx){
        if(idx < 0 || idx >= size) throw new Exception("Index out of bound.");
        if(size == 0) return false;
        if(idx == 0){
            RemoveFirst();
            return true;
        }
        if(idx == size - 1){
            RemoveLast();
            return true;
        }
        int counter = 0;
        Node trav = head;
        while(counter != idx){
            trav = trav.next;
        }
        size--;
        trav.prev.next = trav.next;
        trav.next.prev = trav.prev;
        trav.prev = null;
        trav.next = null;
        trav = null;
        return true;
    }
}
