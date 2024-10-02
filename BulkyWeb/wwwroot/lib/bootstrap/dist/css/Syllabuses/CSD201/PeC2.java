package pe.c2;

import java.util.Arrays;
import java.util.LinkedList;
import java.util.Queue;
import java.util.Stack;
        

class worker implements Comparable<worker>{
    int key;
    int age;
    public worker(int key,int age) {this.key=key; this.age=age;}  
    @Override
    public String toString() {return "("+key+", "+age+")"; }
    @Override
    public int compareTo(worker o) {
        if(key < o.key) return -1;
        if(key > o.key) return +1;
        return 0;
    }
}
class node {
    worker data;
    node left,right;
    public node (worker x) {data=x; left=right=null;}
    @Override
    public String toString() { return data+""; }
}
class bst {
    node root;
    public bst() {root = null; }
// Find the node of T containing worker whose ket matches a given key ;and keep its parent node if possible
node[] find (int key) {
    node t = root ,par =null;
    while (t != null && t.data.key != key) {
        par =t ;
        if(t.data.key > key) t = t.left;
        else t = t.right;
    }    
    return new node[]{t,par};
}
// Insert a new worker to T if this worker has not stored on T yet
void  insert(worker x){
    node[] res = find(x.key);
    if(res [0] != null) return ;
    node new_node = new node(x);
    if (res [1] == null ) root = new_node;
    else if (res[1].data.key <x.key) res[1].right = new_node;
    else res[1].left = new_node;
}
//Output the workers on T in descending order.
// acs right thanh left
void des_output(){
    des_output(root);
}
private void des_output(node t) {
    if(t==null) return;
    des_output(t.right);
    System.err.println("->"+t);
    des_output(t.left);
}
//Delete the right-most node of T.
void delete(){
    if(root == null ) return ;
    node t = root ,par = null ;
    while (t.right != null) {
        par = t;
        t = t.right;
    }
    if( par == null ) root = t.left;
    else par.right = t. left;
}
//Determine the height of T using a level order traversal.
int height(){
    //Queus<node> Q = new LinkList();
    MyQueue Q = new MyQueue();
    
    Q.add(root); Q.add(null); int h = 0;
    for(;;) {
        node t = Q.remove();
        if ( t == null) {
            if(Q.isEmpty()) break;
            Q.add(null); h++;
        } else {
            if(t.left != null )Q.add(t.left);
            if(t.right != null)Q.add(t.right);
        }
    }
    return h+1;
}
//Create a binary search tree of the largest height from a given non-empty sequence of workers
public bst(worker[] a) {
    //root = smallest(a,0,a.length-1);
    root = null; Arrays.sort(a);
    for (worker object :a){
        insert (object);
    }
}
private node smallest(worker[] a ,int low ,int high) {
    if(low > high ) return null ;
    int mid = (low + high) / 2;
    node new_node = new node(a[mid]);
    new_node.left = smallest(a,low,mid-1);
    new_node.right = smallest(a,mid+1,high);
    return new_node;
}
// Count the number of workers stoed in T ,whose age less than 25.
int count (){
    //Stack<node> S = new Stack();
    MyStack S =new MyStack();
    int c = 0; S.push(root);
    while (!S.empty()) {
        node t = S.pop();
        while (t != null ){
            if(t.data.age < 25) c++;
            if(t.right != null ) S.push(t.right);
            t = t.left;
        }
    }
    return c;
    }
}
class MyStack {
    node[] arr; int top;
    public MyStack(){
        arr = new node[100] ; top = -1;
    }
    void push(node x){
        top++ ;arr[top] = x;
    }
    boolean empty(){
        return top == -1;
    }
    node pop() {
        node x =arr[top];top--; return x;
    }
}
class node_queue{
    node data;
    node_queue next;
    public node_queue(node data ) {
        this.data=data;
        this.next=null;
    }
}
class MyQueue {
    node_queue head,tail;
    public MyQueue (){head = null;}
    public boolean isEmpty() {return head == null;}
    void add(node x) {
        node_queue new_node = new node_queue(x);
        if(isEmpty()) head = new_node;
        else tail.next = new_node;
        tail = new_node;
    }
    node remove(){
        node x = head.data ; head = head.next;
        return x;
    }
}
public class PeC2 {
    public static void main(String[] args) {
        bst T = new bst(); T.insert(new worker(5,5));T.insert(new worker(2,2));T.insert(new worker(1,1));
        T.insert(new worker(3,3));T.insert(new worker(8,8));T.insert(new worker(7,7));T.insert(new worker(9,9));
        T.insert(new worker(4,4));T.insert(new worker(6,6));
        T.des_output();System.err.println();
        System.err.println("Count="+T.count());
        T.delete();System.out.println("After deleting:");T.des_output();
        System.err.println("Height = " + T.height());
        bst T2 = new bst(new worker[] {
            new worker(5,5),new worker(2,2),new worker(1,1),new worker(3,3),new worker(8,8),
            new worker(7,7),new worker(9,9),new worker(4,4),new worker(6,6)});
        System.out.println("Height max =" +T2.height());
        }
    }

// sum, avg, count ,in bst