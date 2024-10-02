/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package csd201_pe;

public class Main {

    public static void main(String[] args) {
        double[][] A = {
                {5, 5},
                {3, 3},
                {2, 2},
                {4, 4},
                {7, 7},
                {6, 6},
                {8, 8},
                {1, 1},
                {9, 9}
        };

        TeacherBST bst = new TeacherBST();
        
        // I - 1
        for (double[] info : A) {
            bst.addTeacher((int)info[0], info[1]);
        }
        
        // I - 2
        
        
        // I - 3
        System.out.println("\nPrint all nodes in the form of <data, level>:");
        bst.printAllNode();
        
        // I - 4
        System.out.println("Set bal for all TeacherNode...");
        bst.setBalForAllNode(bst.root);
        
        // I - 5
        System.out.println("\nOutput all teachers in pre-order traversal (recursion):");
        bst.traversePreOrder();
        
        // II
        System.out.println("\nPrint sorted array with bubble sort:");
        TeacherNode[] originalArray = new TeacherNode[A.length];
        for (int i = 0; i < A.length; i++) {
            originalArray[i] = new TeacherNode((int) A[i][0], A[i][1]);
        }
        bst.tree_sort(originalArray);
        
        double[][] sortedArray = new double[A.length][2];
        for (int i = 0; i < originalArray.length; i++) {
            sortedArray[i][0] = originalArray[i].code;
            sortedArray[i][1] = originalArray[i].coeff;
        }
        for (double[] sortedArray1 : sortedArray) {
            System.out.print("(" + sortedArray1[0] + ", " + sortedArray1[1] + ") ");
        }
    }
    
}
