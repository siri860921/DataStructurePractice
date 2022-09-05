using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures {
    public class AVLTree<T> where T : IComparable {
        private class Node {
            public int balanceFactor;
            public int height;
            public T data;
            public Node left;
            public Node right;

            public Node(T nodeData, Node leftNode, Node rightNode) {
                data = nodeData;
                left = leftNode;
                right = rightNode;
            }
        }

        private int nodeCount;
        private Node root;

        public AVLTree() {
            nodeCount = 0;
            root = null;
        }

        public int Height() {
            if(root == null) return 0;
            else return root.height;
        }

        public int Size() {
            return nodeCount;
        }

        public bool IsEmpty() {
            return nodeCount == 0;
        }

        public bool Contain(T value) {
            return contain(value, root);
        }

        public void Add(T value) {
            if(root == null) {
                root = new Node(value, null, null);
                nodeCount++;
                return;
            }
            root = add(root, value);
            
        }

        private bool contain(T value, Node node) {
            if(node == null) return false;
            int cmp = value.CompareTo(node.data);
            if(cmp < 0) return contain(value, node.left);
            if(cmp > 0) return contain(value, node.right);
            return true;
        }

        private Node add(Node node, T value) {
            if(node == null) return new Node(value, null, null);
            int cmp = value.CompareTo(node.data);
            if(cmp <= 0) node.left = add(node.left, value);
            else node.right = add(node.right, value);

            update(node);
            return balance(node);
        }

        // update the height and the balance factor of a node
        private void update(Node node) {
            int leftBranchSize = (node.left == null) ? 0 : node.left.height;
            int rightBranchSize = (node.right == null) ? 0 : node.right.height;
            node.height =  1 + Math.Max(leftBranchSize, rightBranchSize);
            node.balanceFactor = rightBranchSize - leftBranchSize;
        }

        // rebalance a node if its balance factor is 2 or -2
        private Node balance(Node node) {            
            // left heavy tree
            if(node.balanceFactor == -2) {
                // left-left case
                if(node.left.balanceFactor <= 0) return leftLeftCase(node);
                // left-right case
                else return leftRightCase(node);
            }
            // right heavy tree
            else if(node.balanceFactor == 2){
                // right-right case
                if(node.right.balanceFactor > 0) return rightRightCase(node);
                // right-left case
                else return rightLeftCase(node);
            }
            return node;
        }

        private Node leftLeftCase(Node node) {
            return rightRotation(node);
        }


        private Node leftRightCase(Node node) {
            leftRotation(node.left);
            return rightRotation(node);
        }

        private Node rightLeftCase(Node node) {
            leftRotation(node.right);
            return leftRotation(node);
        }

        private Node rightRightCase(Node node) {
            return leftRotation(node);
        }

        private Node leftRotation(Node node) {
            Node newBalancedNode = node.right;
            node.right = newBalancedNode.left;
            newBalancedNode.left = node;

            update(newBalancedNode);
            update(node);
            return newBalancedNode;
        }

        private Node rightRotation(Node node) {
            Node newBalancedNode = node.left;
            node.left = newBalancedNode.right;
            newBalancedNode.right = node;

            update(newBalancedNode);
            update(node);
            return newBalancedNode;
        }
    }
}
