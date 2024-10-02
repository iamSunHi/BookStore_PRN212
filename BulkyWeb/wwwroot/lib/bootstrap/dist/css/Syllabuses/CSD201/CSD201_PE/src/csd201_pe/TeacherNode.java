/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package csd201_pe;

public class TeacherNode {
    int code;
    double coeff;
    TeacherNode left;
    TeacherNode right;
    int bal;

    public TeacherNode(int code, double coeff) {
        this.code = code;
        this.coeff = coeff;
        this.left = null;
        this.right = null;
        this.bal = 0;
    }
}
