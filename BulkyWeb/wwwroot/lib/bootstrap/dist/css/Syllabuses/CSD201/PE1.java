/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pe1;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.Map;
import java.util.Queue;
import java.util.concurrent.atomic.AtomicInteger;
import sun.security.krb5.internal.APRep;

/**
 *
 * @author lenovo
 */
class Lecturer {

    int lecturer_code;
    double teaching_hours;

    public Lecturer() {
    }

    public Lecturer(int lecturer_code, double teaching_hours) {
        this.lecturer_code = lecturer_code;
        this.teaching_hours = teaching_hours;
    }

    @Override
    public String toString() {
        return "Lecturer{" + "lecturer_code=" + lecturer_code + ", teaching_hours=" + teaching_hours + '}';
    }

}

class Node {

    Lecturer lecturer;
    Node left, right;
    Node next; //For stack
    public Node(Lecturer lecturer) {
        this.lecturer = lecturer;
        left = right = null;
    }

    public Node() {
    }

}
class MyStack {

    Node head;

    public MyStack() {
        head = null;
    }

    boolean isEmpty() {
        return head == null;
    }

    void push(Node node) {
        node.next = head;
        head = node;
    }

    //Remove the top element
    Node pop() {
        Node node = head;
        head = head.next;
        return node;
    }

    //Return the top element, not delete
    Node top() {
        return head;
    }

}
class BST {

    Node root;
    ArrayList<Lecturer> Ain = new ArrayList<Lecturer>();
    ArrayList<Lecturer> Apre = new ArrayList<Lecturer>();
    static int preIndex = 0;
    private Lecturer max_teaching = new Lecturer();

    public BST() {

    }

    public BST(Node root) {
        this.root = root;
    }

    public BST(ArrayList<Lecturer> lecturers) {
        for (Lecturer lecturer : lecturers) {
            insert(lecturer);
        }
    }

    public boolean isEmpty() {
        return root == null;
    }

    public void clear() {
        root = null;
    }

    // Insert a new node
    private void insert(Lecturer lecturer) {
        root = insert(root, lecturer);
    }

    private Node insert(Node root, Lecturer lecturer) {
        if (root == null) {
            root = new Node(lecturer);
            return root;
        }

        if (lecturer.lecturer_code < root.lecturer.lecturer_code) {
            root.left = insert(root.left, lecturer);
        }

        if (lecturer.lecturer_code > root.lecturer.lecturer_code) {
            root.right = insert(root.right, lecturer);
        }
        return root; // root is always the first element (50)
    }

    public int num_level() {
        if (isEmpty()) {
            return 0;
        }

        Queue<Node> queue = new LinkedList<>();
        queue.add(root);
        int height = 0;
        while (!queue.isEmpty()) {
            int levelSize = queue.size();
            while (levelSize > 0) {
                Node currentNode = queue.poll();
                //System.out.println(currentNode.lecturer.toString() + " ");
                if (currentNode.left != null) {
                    queue.add(currentNode.left);
                }
                if (currentNode.right != null) {
                    queue.add(currentNode.right);
                }
                levelSize--;
            }
            height++;

        }
        return height;
    }

    public void in_order_recur() {
        in_order_recur(root, this.Ain);
    }

    private void in_order_recur(Node root, ArrayList<Lecturer> lecturers) {
        if (root == null) {
            return;
        }
        in_order_recur(root.left, lecturers);
        System.out.println(root.lecturer.toString());
        lecturers.add(root.lecturer);
        in_order_recur(root.right, lecturers);
    }

    public Lecturer peek_max_mark() {
        //return peek_max_mark(root);
        peek_max_mark(root);
        return max_teaching;
    }

    private void peek_max_mark(Node root) {
        if (root == null) {
            return;
        }
        peek_max_mark(root.left);
        if (root.lecturer.teaching_hours > this.max_teaching.teaching_hours) {
            max_teaching = root.lecturer;
        }
        peek_max_mark(root.right);
    }

    public void pre_order_list_stack() {
        pre_order_list_stack(root);
    }

    private void pre_order_list_stack(Node node) {
        MyStack stack = new MyStack();
        stack.push(root);

        while (!stack.isEmpty()) {

            node = stack.pop();

            while (node != null) {

                System.out.print(node.lecturer.toString() + "\n");
                Apre.add(node.lecturer);
                if (node.right != null) {
                    stack.push(node.right);
                }

                node = node.left;
            }

        }
    }


    public void insert_equal(ArrayList<Lecturer> lecturers) {
        int i, j;
        Lecturer key = new Lecturer();
        for (i = 1; i < lecturers.size(); ++i) {
            key = lecturers.get(i);
            j = i - 1;
            while (j >= 0 && key.lecturer_code < lecturers.get(j).lecturer_code) {
                lecturers.set(j+1, lecturers.get(j));
                j--;
            }
            lecturers.set(j+1, key);
        }
        //Print tree
        for (Lecturer lecturer : lecturers) {
            System.out.println(lecturer.toString());
        }
        
        System.out.println("Ain equals A?");
        boolean flag = true;
        for(i = 0 ; i< Ain.size();++i){
            if(Ain.get(i).lecturer_code != lecturers.get(i).lecturer_code){
                flag = false;
                break;
            }
        }
        System.out.println(flag);
    }
    public void comprise_in_pre(ArrayList<Lecturer> Ain, ArrayList<Lecturer> Apre) {
        root = compriseInPre(Ain, Apre, 0, Ain.size() - 1);
    }

    private Node compriseInPre(ArrayList<Lecturer> inOrder, ArrayList<Lecturer> preOrder, int inStart, int inEnd) {
        if (inStart > inEnd) {
            return null;
        }

        Node newNode = new Node(preOrder.get(preIndex++));

        if (inStart == inEnd) {
            return newNode;
        }

        int inIndex = search(inOrder, inStart, inEnd, newNode.lecturer.lecturer_code);

        newNode.left = compriseInPre(inOrder, preOrder, inStart, inIndex - 1);
        newNode.right = compriseInPre(inOrder, preOrder, inIndex + 1, inEnd);

        return newNode;
    }

    private int search(ArrayList<Lecturer> arr, int start, int end, int value) {
        for (int i = start; i <= end; i++) {
            if (arr.get(i).lecturer_code == value) {
                return i;
            }
        }
        return -1;
    }
    
}

public class PE1 {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
        ArrayList<Lecturer> lecturers = new ArrayList<>();
        lecturers.add(new Lecturer(5, 55));
        lecturers.add(new Lecturer(3, 33));
        lecturers.add(new Lecturer(2, 22));
        lecturers.add(new Lecturer(4, 44));
        lecturers.add(new Lecturer(7, 77));
        lecturers.add(new Lecturer(6, 66));
        lecturers.add(new Lecturer(8, 88));
        lecturers.add(new Lecturer(1, 11));
        lecturers.add(new Lecturer(9, 99));
        BST bst = new BST(lecturers);
        System.out.println("Height of tree: " + bst.num_level());

        System.out.println("Max teaching hours");
        System.out.println(bst.peek_max_mark().toString());

        System.out.println("Inorder recursion(Store in Ain)");
        bst.in_order_recur(); // Result to Ain below
        
        System.out.println("Preorder_list_stack: (Store in Apre)");
        bst.pre_order_list_stack(); //Result to Apre below

        System.out.println("Insertion sort for A");
        bst.insert_equal(lecturers);
        
        System.out.println("Comprise Ain and Apre");
        BST bst1 = new BST();
        bst1.comprise_in_pre(bst.Ain,bst.Apre);
        bst1.in_order_recur();
    }

}
