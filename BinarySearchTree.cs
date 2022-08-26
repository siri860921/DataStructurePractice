using System;
using System.Collections.Generic;

namespace DataStructurePractice{
    public enum TreeTraversalOrder
    {
        PREORDER,
        INORDER,
        POSTORDER,
        LEVELORDER
    }

    public class BinarySearchTree<T> where T: IComparable{
        private class Node{
            public T data;
            public Node left;
            public Node right;

            public Node(T nodeData, Node leftNode, Node rightNode){
                data = nodeData;
                left = leftNode;
                right = rightNode;
            }
        }

        int nodeCount; // number of nodes in the binary tree
        Node root; // the root node of the binary tree

        public BinarySearchTree(){
            int nodeCount = 0;
            root = null;
        }

        public bool IsEmpty(){
            return nodeCount == 0;
        }

        public int Size(){
            return nodeCount;
        }
      
        public bool Contain(T data){
            return contain(data, root);
        }

        // add a new element to the binary tree
        // cancel add operation if that element has been already in the binary tree
        public bool Add(T data){
            if(root == null){
                root = new Node(data, null, null);
                return true;
            }
            if(Contain(data)) return false;
            Node trav = root;
            while(true){
                if(data.CompareTo(trav.data) < 0 && trav.left == null){
                    trav.left = new Node(data, null, null);
                    nodeCount++;
                    return true;
                }
                if(data.CompareTo(trav.data) > 0 && trav.right == null){
                    trav.right = new Node(data, null, null);
                    nodeCount++;
                    return true;
                }
                if(data.CompareTo(trav.data) < 0) trav = trav.left;
                else if(data.CompareTo(trav.data) > 0) trav = trav.right;
            }
        }

        public bool Remove(T data){
            if(!Contain(data)) return false;
            
            // find the node that stores the data
            Node trav = root;
            Node travParent = root;
            while(true){
                if(data.CompareTo(trav.data) == 0) break;
                travParent = trav;
                if(data.CompareTo(trav) < 0) trav = trav.left;
                else if(data.CompareTo(trav) > 0) trav = trav.right;
            }
            remove(trav, travParent);
            nodeCount--;
            return true;
        }

        private void remove(Node trav, Node travParent){
            // case 0: if the removed node has no branches
            if(trav.left == null && trav.right == null){
                trav.data = default(T);
            }
            // case 1: if the removed node only has left branch
            else if(trav.left != null && trav.right == null){
                travParent.left = trav.left;
                trav.data = default(T);
            }
            // case 2: if the removed node only has right branch
            else if(trav.left == null && trav.right != null){
                travParent.right = trav.right;
                trav.data = default(T);
                trav = null;
            }
            // case 3: if the removed node ha both branches
            else{
                // dig on the left branch to find the maximun value
                Node travMax = trav.left;
                Node travMaxParent = trav.left;
                while(travMax.right != null){
                    travMaxParent = travMax;
                    travMax = travMax.right;
                }
                trav.data = travMax.data;
                remove(travMax, travMaxParent);
            }
        }

        private bool contain(T data, Node startNode){
            if(startNode == null) return false;
            if(data.CompareTo(startNode.data) > 0) return contain(data, startNode.right);
            else if(data.CompareTo(startNode.data) < 0) return contain(data, startNode.left);
            else return true;           
        }

        // traverse the binary tree with given traversal method
        public T[] Traversal(TreeTraversalOrder order){
            switch(order){
                case TreeTraversalOrder.PREORDER:
                    return preOrderTraverse();
                case TreeTraversalOrder.INORDER:
                    return inOrderTraverse();
                case TreeTraversalOrder.POSTORDER:
                    return postOrderTraverse();
                case TreeTraversalOrder.LEVELORDER:
                    return levelOrderTraverse();
                default:
                    return preOrderTraverse();
            }
        }

        private T[] preOrderTraverse(){
            Stack<Node> nodeStack = new Stack<Node>();
            List<T> preOrderList = new List<T>();

            nodeStack.Push(root);
            while(nodeStack.Count != 0){
                Node trav = nodeStack.Pop();
                if (trav.right != null) nodeStack.Push(trav.right);
                if(trav.left != null) nodeStack.Push(trav.left);
                preOrderList.Add(trav.data);
            }

            return preOrderList.ToArray();
        }

        private T[] inOrderTraverse(){
            Stack<Node> nodeStack = new Stack<Node>();
            List<T> inOrderList = new List<T>();
            
            nodeStack.Push(root);
            Node trav = root;
            while(nodeStack.Count != 0 && trav != null){
                if(trav.left != null){
                    nodeStack.Push(trav.left);
                    trav = trav.left;
                }
                else{
                    trav = nodeStack.Pop();
                    inOrderList.Add(trav.data);
                    if(trav.right != null) nodeStack.Push(trav.right);
                    trav = trav.right;
                }
            }

            return inOrderList.ToArray();
        }

        private T[] postOrderTraverse(){
            Stack<Node> nodeStack = new Stack<Node>();
            Stack<Node> subNodeStack = new Stack<Node>();
            List<T> postOrderList = new List<T>();

            nodeStack.Push(root);
            Node trav = root;
            while(nodeStack.Count != 0){
                trav = nodeStack.Peek();
                if(trav.left == null && trav.right == null){
                    nodeStack.Pop();
                    postOrderList.Add(trav.data);
                }
                if(trav.Equals(subNodeStack.Peek())){
                    subNodeStack.Pop();
                    nodeStack.Pop();
                    postOrderList.Add(trav.data);
                }
                if(trav.left != null && trav.right != null) subNodeStack.Push(trav);
                if(trav.right != null) nodeStack.Push(trav.right);
                if(trav.left != null) nodeStack.Push(trav.left);
            }

            return postOrderList.ToArray();
        }

        private T[] levelOrderTraverse(){
            Queue<Node> nodeQueue = new Queue<Node>();
            List<T> levelOrderList =  new List<T>();
            nodeQueue.Enqueue(root);
            Node trav = root;
            while(nodeQueue.Count != 0){
                trav = nodeQueue.Dequeue();
                levelOrderList.Add(trav.data);
                if(trav.left != null) nodeQueue.Enqueue(trav.left);
                if(trav.right != null) nodeQueue.Enqueue(trav.right);
            }

            return levelOrderList.ToArray();
        }
    }
}