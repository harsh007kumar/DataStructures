using System;

namespace LinkedList
{
    public class Node
    {
        public int Data { get; set; }
        public Node next, prv;

        public Node(int no) //Parametrized Constructor
        {
            Data = no;
            next = prv = null;
        }
        public Node()       //Default Constructor
        { next = prv = null; }
    }

    public abstract class BaseLinkedList
    {
        public Node Head;
        protected int count = 0;
        public int Count { get { return count; } }
        protected BaseLinkedList() => Head = null;
    }

    public class DoublyLinkedList : BaseLinkedList
    {
        public DoublyLinkedList() : base() {}

        public void AddAtStart(int data)
        {
            Node newNode = new Node(data);   // Create New Node
            if (Head != null)
                Head.prv = newNode;          // Point the previously First Node->Previous to newly added one
            newNode.next = Head;             // Point the newly added Node->Next to previously First Node
            Head = newNode;                  // Pointing the Head of LinkedList to newly added node
            count++;
            Console.WriteLine($"Inserting : {data} at the Start");
        }

        public void AddAtEnd(int data)
        {
            Node newNode = new Node(data);   // Create New Node
            if (Head == null)
                Head = newNode;              // Point Head to first element in List
            else
            {
                Node Temp = Head;
                while (Temp.next != null)   // Iterate to the Last Node in List
                    Temp = Temp.next;
                Temp.next = newNode;         // Point Last Node->Next to new element
                newNode.prv = Temp;          // Point newly added Node->Previous to previously last element
            }
            count++;
            Console.WriteLine($"Inserting : {data} at the End");
        }

        public void AddAtPosition(int data, int pos = 1)
        {
            Node newNode = new Node(data);
            Node temp = Head;
            int currentPos = 1;
            if (pos <= count)               // Node can only be inserted at position which exists
            {
                Console.WriteLine($"Inserting : {data} at Position : {pos}");
                while (pos != currentPos)
                {
                    temp = temp.next;
                    currentPos++;
                }

                (temp.prv).next = newNode;   // Previous Node next should point to newly added Node
                newNode.prv = temp.prv;      // Newly added Node prv should point to previous of last Node which was there
                temp.prv = newNode;          // Original node's prv should now point to new Node
                newNode.next = temp;         // Node next should point to node which was originally present at that position

                count++;                    // Increment size of linkedList
            }
        }

        public void DeleteByKey(int data)
        {
            Node current = Head;
            while (current != null)
            {
                if (current.Data == data)
                {
                    if (current.prv != null)
                        (current.prv).next = current.next;
                    if (current.next != null)
                        (current.next).prv = current.prv;
                    count--;
                    Console.WriteLine($"Deleting : {data} from the list");
                    break;
                }
                current = current.next;     //Traverse to next Node
            }
        }

        public void DeleteAtStart()
        {
            if (Head == null) return;

            Console.WriteLine($"Deleting First Node : {Head.Data} from the list");
            (Head.next).prv = Head.prv;     //Set prv of 2nd element to prv Node value present in 1st Node/Head
            Head = Head.next;               //Set Head to point to 2nd element

            count--;
        }

        public void DeleteAtPosition(int pos = 1)
        {
            // Deletion can only be performed at position which exists
            if (Head == null || pos > count) return;

            Node temp = Head;
            int currentPos = 1;
            while (pos != currentPos)
            {
                temp = temp.next;
                currentPos++;
            }
            Console.WriteLine($"Deleting : {temp.Data} present at Position : {pos}");
            (temp.prv).next = temp.next;
            (temp.next).prv = temp.prv;
            count--;
        }

        public void DeleteAtEnd()
        {
            if (Head == null) return;
            Node temp = Head;
            while (temp.next != null)
                temp = temp.next;

            (temp.prv).next = null;         // Set 2nd last element's next pointer to null

            Console.WriteLine($"Deleting Last Node : {temp.Data} from the list");
            count--;
        }

        // To Print Elements From Start
        public void PrintFromStart()
        {
            if (Head == null) return;

            Node current = Head;
            Console.WriteLine("Printing Elements From Start/Head");
            while (current != null)
            {
                Console.Write($"--> {current.Data}");
                current = current.next;
            }
            Console.WriteLine();
        }

        // To Print Elements From Last
        public void PrintFromEnd()
        {
            if (Head == null) return;

            Node current = Head;
            Console.WriteLine("Printing Elements From End");
            while (current.next != null)
                current = current.next;
            while (current != null)
            {
                Console.Write($"--> {current.Data}");
                current = current.prv;
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Circular Linked List implementation where each Node has data element and single pointer 'next' pointing to next node in list
    /// </summary>
    public class CircularLinkedList : BaseLinkedList
    {
        public CircularLinkedList() : base() { }

        /// <summary>
        /// Calculates no of nodes present currently in the list
        /// </summary>
        /// <returns></returns>
        public int CalculateLength()
        {
            if (Head == null) return 0;
            int len = 0;
            Node temp = Head;
            do
            {
                len++;
                temp = temp.next;
            } while (temp != Head);

            count = len;                    // Setting the count variable
            return len;
        }

        public void PrintContent()
        {
            if (Head == null) return;
            Console.WriteLine("Printing Elements From Start/Head");
            Node temp = Head;
            do
            {
                Console.Write($"--> {temp.Data} ");
                temp = temp.next;
            } while (temp != Head);

            Console.WriteLine();
        }

        public void AddAtStart(int data)
        {
            Node newNode = new Node(data);
            if (Head == null)
                newNode.next = newNode;
            else
            {
                Node temp = Head;
                while (temp.next != Head)
                    temp = temp.next;

                temp.next = newNode;        // Set last node next pointing to newly added node
                newNode.next = Head;        // Set new node next to 1st element in list
            }
            Head = newNode;                 // Set Head to point to new Node

            count++;
            Console.WriteLine($"Inserting : {data} at the Start");
        }

        public void AddAtEnd(int data)
        {
            Node newNode = new Node(data);

            if (Head == null)
            {
                newNode.next = newNode;
                Head = newNode;
            }
            else
            {
                Node temp = Head;
                while (temp.next != Head)
                    temp = temp.next;

                newNode.next = Head;
                temp.next = newNode;
            }

            count++;
            Console.WriteLine($"Inserting : {data} at the End");
        }

        public void DeleteFirst()
        {
            if (Head == null) return;

            Node temp = Head;
            while (temp.next != Head)
                temp = temp.next;

            Console.WriteLine($"Deleting First Node : {Head.Data} from the list");
            temp.next = Head = Head.next;   // Assumption list had atleast 2 nodes
            
            count--;
            
        }

        public void DeleteLast()
        {
            if(Head==null) return;
            Node last = Head, oneBeforeLast = null ;
            while (last.next != Head)
            {
                oneBeforeLast = last;
                last = last.next;
            }

            if (oneBeforeLast != null)
                oneBeforeLast.next = Head;
            else
                Head = null;                // Only 1 node was present in list now list is empty

            count--;
            Console.WriteLine($"Deleting Last Node : {last.Data} from the list");
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("\n\n ===================== Doubly LinkedList ==========================");
            DoublyLinkedList doublyList = new DoublyLinkedList();
            doublyList.AddAtStart(5);   // 5
            doublyList.AddAtStart(10);  // 10 5
            doublyList.AddAtEnd(15);    // 10 5 15
            doublyList.AddAtEnd(20);    // 10 5 15 20
            doublyList.AddAtStart(25);  // 25 10 5 15 20
            doublyList.AddAtEnd(30);    // 25 10 5 15 20 30
            doublyList.PrintFromStart();    // Print from Start
            Console.WriteLine($"No of Node currently in Doubly LinkedList : {doublyList.Count}");
            doublyList.AddAtPosition(8, 4);
            doublyList.PrintFromStart();    // Print from Start
            Console.WriteLine($"No of Node currently in Doubly LinkedList : {doublyList.Count}");

            doublyList.DeleteByKey(15); // Delete no in between
            doublyList.DeleteAtPosition(2); // Delete no at position
            doublyList.DeleteAtEnd();       // Delete Last
            doublyList.PrintFromStart();    // Print from Start
            Console.WriteLine($"No of Node currently in Doubly LinkedList : {doublyList.Count}");

            doublyList.DeleteAtStart();     // Delete First
            doublyList.PrintFromEnd();      // Print from End

            Console.WriteLine("\n\n ===================== Circular LinkedList =======================");
            CircularLinkedList circularList = new CircularLinkedList();
            circularList.AddAtEnd(55);
            circularList.AddAtStart(90);
            circularList.AddAtEnd(75);
            circularList.AddAtStart(100);
            circularList.PrintContent();
            circularList.DeleteFirst();
            circularList.DeleteLast();
            var noOfNodes = circularList.CalculateLength();
            circularList.PrintContent();
            Console.ReadKey();
        }
    }
}
