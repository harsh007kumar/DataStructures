using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XOR_LinkedList
{
    // Step 1: Go to : ProjectProperties>>Build>>AllowUnsafeCode (check this) than only you can use unsafe code blocks, the project has to be compiled with the /unsafe switch on
    // C-Sharp Corner https://www.c-sharpcorner.com/UploadFile/f0b2ed/understanding-unsafe-code-in-C-Sharp/
    // Bitwise operations https://www.programiz.com/csharp-programming/bitwise-operators
    // Might Be useful  https://www.josephkirwin.com/2013/06/14/reversing-xor-linked-lists-in-c/
    unsafe class XOR_LinkedList
    {
        static void Main(string[] args)
        {
            unsafe
            {
                XORLinkedList XOR_List = new XORLinkedList();
                XOR_List.PrintContent();
                XOR_List.Add(10); 
                XOR_List.Add(15);
                XOR_List.Add(55);
                XOR_List.Add(100);
                XOR_List.Add(555);
                XOR_List.PrintContent();
            }

            Console.ReadKey();
        }
    }

    public unsafe struct Node
    {
        public int Data { get; set; }
        public Node* PointerDiff;
        //Default Constructor
        public Node(int no)
        {
            Data = no;
            PointerDiff = (Node*) 0;
        }
    }

    // Reference 3.9 A : Karumanchi, Narasimha.Data Structures and Algorithms Made Easy: Data Structures and Algorithmic Puzzles(p. 111). Kindle Edition.
    public unsafe class XORLinkedList
    {
        public Node* Head;
        public Node* LastNode;
        protected int count = 0;
        public int Count { get { return count; } }
        //Default Constructor
        public XORLinkedList() => Head = LastNode = (Node*)0;

        public void PrintContent()
        {
            //if (Head == null) return;
            Console.WriteLine("Printing Elements From Start/Head");

            Node* temp = Head, prvNode = (Node*)0;
            while (temp != (Node*)0)                                               // Break Loop on reaching last Node or if temp is empty
            {
                Console.Write($"--> {(*temp).Data} ");
                //if (temp != LastNode) break;                                        // Takes care of scenario when we just have single Node in list
                Node* next = NextNode(temp->PointerDiff, prvNode);
                prvNode = temp;                                                     // Assign Current Node address to prvNode
                temp = next;                                                        // Update Next Node address to temp
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Calculate and returns address of next node in List
        /// </summary>
        /// <param name="currentNodePointerDiff">pointer difference stored in current node</param>
        /// <param name="prvNode">address of previous node</param>
        /// <returns></returns>
        public Node* NextNode(Node* currentNodePointerDiff, Node* prvNode) => XOR(currentNodePointerDiff,prvNode);

        public void Add(int data)
        {
            Node newNode = new Node(data);
            if (Head == (Node*)0)
                Head = &newNode;                                                    // Set Head to point to new Node in case of Empty XORList
            else
            {
                Node* prvNode = XOR(LastNode->PointerDiff);                         // XOR to get address of previous Node
                // Perform Bitwise Exclusive OR (XOR) on previous to last node and new node address after casting each into int & casting result back to address in memory of type Node
                LastNode->PointerDiff = XOR(prvNode, &newNode);                     // same as LastNode->PointerDiff = (Node*)((int)LastNode->PointerDiff ^ (int)&newNode);
            }

            newNode.PointerDiff = XOR(LastNode);                                    // same as newNode.PointerDiff = LastNode
            LastNode = &newNode;
            count++;
            Console.WriteLine($"Inserting : {data} at the Start, Address : {PrintAddress(LastNode)}");

        }

        public void Delete()
        {
            if (Head == (Node*)0) return;
            Node* prvNode = (Node*)((int)LastNode->PointerDiff ^ (int)0);
            prvNode->PointerDiff = (Node*)((int)prvNode->PointerDiff ^ (int)LastNode->PointerDiff);
            LastNode = prvNode;
            count--;
            Console.WriteLine($"Deleting First Node : {(*LastNode).Data} from the list");
        }















        // Calculate and return XOR of 2 inputs and returns result of type Node*
        public Node* XOR(Node* first, Node* second) => (Node*) XOR((int) first, (int) second);
        // Calculate and return XOR of given input with 0 and returns result of type Node*
        public Node* XOR(Node* first) => (Node*) XOR((int) first, 0);
        // Returns XOR of inputs of type Integer
        public int XOR(int first, int second = 0) => first ^ second;

        /// <summary>
        /// Takes Pointer of type Node* and typecast address first into int type and than converts to Hex value
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        protected string PrintAddress(Node* ptr) => "Ox00" + ((int) ptr).ToString("X");

    }
}
