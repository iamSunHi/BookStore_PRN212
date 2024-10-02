/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package csd201_pe;

import java.util.LinkedList;
import java.util.Queue;

public class TeacherBST {

    TeacherNode root;

    public TeacherBST() {
        this.root = null;
    }

    // I - 1
    public void addTeacher(int code, double coeff) {
        root = addTeacherRecursive(root, code, coeff);
    }

    private TeacherNode addTeacherRecursive(TeacherNode current, int code, double coeff) {
        if (current == null) {
            return new TeacherNode(code, coeff);
        }

        if (code < current.code) {
            current.left = addTeacherRecursive(current.left, code, coeff);
        } else if (code > current.code) {
            current.right = addTeacherRecursive(current.right, code, coeff);
        }
        return current;
    }
    
    // I - 2
    

    // I - 3
    public int countLevels(TeacherNode node) {
        return countLevelsBFS(node);
    }

    private int countLevelsBFS(TeacherNode root) {
        if (root == null) {
            return 0;
        }

        Queue<TeacherNode> queue = new LinkedList<>();
        queue.add(root);
        int levels = 0;

        while (!queue.isEmpty()) {
            int size = queue.size();

            for (int i = 0; i < size; i++) {
                TeacherNode node = queue.poll();

                if (node.left != null) {
                    queue.add(node.left);
                }

                if (node.right != null) {
                    queue.add(node.right);
                }
            }

            levels++;
        }
        return levels;
    }

    public void printAllNode() {
        printNode(root);
        System.out.println("");
    }

    private void printNode(TeacherNode teacherNode) {
        if (teacherNode != null) {
            System.out.println("<Code: " + teacherNode.code + " - Coeff: " + teacherNode.coeff + ", Level: " + countLevels(teacherNode) + "> ");
            printNode(teacherNode.left);
            printNode(teacherNode.right);
        }
    }

    // I - 4
    private int height(TeacherNode t) {
        if (t == null) {
            return 0;
        } else {
            int lDepth = height(t.left);
            int rDepth = height(t.right);
            if (lDepth > rDepth) {
                return (lDepth + 1);
            } else {
                return (rDepth + 1);
            }
        }
    }
    public void setBalForAllNode(TeacherNode t) {
        if (t == null)
            return;
        else if (t.left == null && t.right == null)
            return;
        
        if (t.left == null && t.right != null) {
            t.bal = height(t.right);
        }
        else if (t.left != null && t.right == null) {
            t.bal = height(t.left);
        }
        else {
            t.bal = Math.abs(height(t.left) - height(t.right));
        }
        
        setBalForAllNode(t.left);
        setBalForAllNode(t.right);
    }

    // I - 5
    public void traversePreOrder() {
        traversePreOrderRecursive(root);
        System.out.println("");
    }

    private void traversePreOrderRecursive(TeacherNode teacherNode) {
        if (teacherNode != null) {
            System.out.print("<" + teacherNode.code + ", " + teacherNode.coeff + ", " + teacherNode.bal  + "> ");
            traversePreOrderRecursive(teacherNode.left);
            traversePreOrderRecursive(teacherNode.right);
        }
    }

    // II
    public void tree_sort(TeacherNode[] array) {
        for (int i = 0; i < array.length - 1; i++) {
            for (int j = 0; j < array.length - i - 1; j++) {
                if (array[j].code > array[j + 1].code) {
                    TeacherNode temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }

    }
}
