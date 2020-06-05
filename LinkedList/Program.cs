using System;

namespace LinkedList
{
    class Node
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
    class DoublyLinkedList
    {
        public Node Head;
        private int count = 0;
        public int Count
        {
            get { return count; }
        }

        public DoublyLinkedList() { Head = null; }

        public void AddAtStart(int data)
        {
            Node addOne = new Node(data);   // Create New Node
            if(Head != null)
                Head.prv = addOne;          // Point the previously First Node->Previous to newly added one
            addOne.next = Head;             // Point the newly added Node->Next to previously First Node
            Head = addOne;                  // Pointing the Head of LinkedList to newly added node
            count++;
        }

        public void AddAtEnd(int data)
        {
            Node addOne = new Node(data);   // Create New Node
            if (Head == null)
                Head = addOne;              // Point Head to first element in List
            else
            {
                Node Temp = Head;
                while (Temp.next != null)   // Iterate to the Last Node in List
                    Temp = Temp.next;
                Temp.next = addOne;         // Point Last Node->Next to new element
                addOne.prv = Temp;          // Point newly added Node->Previous to previously last element
            }
            count++;
        }

        public void DeleteByKey(int no)
        {
            Node current = Head;
            while(current!=null)
            {
                if(current.Data==no)
                {
                    if(current.prv!=null)
                        (current.prv).next = current.next;
                    if (current.next != null)
                        (current.next).prv = current.prv;
                    count--;
                    break;
                }
                current = current.next;     //Traverse to next Node
            }
        }
    }
    class MainClass
    {
        public static void Main(string[] args)
        {
            DoublyLinkedList myList = new DoublyLinkedList();
            myList.AddAtStart(5);   // 5
            myList.AddAtStart(10);  // 10 5
            myList.AddAtEnd(15);    // 10 5 15
            myList.AddAtEnd(20);    // 10 5 15 20
            myList.AddAtStart(25);  // 25 10 5 15 20
            myList.AddAtEnd(30);    // 25 10 5 15 20 30
            PrintFromStart(myList); // Print from Start
            Console.WriteLine($"No of Node currently in Doubly LinkedList : {myList.Count}");

            myList.DeleteByKey(15); // Delete no in between
            myList.DeleteByKey(30); // Delete Last
            PrintFromStart(myList); // Print from Start
            Console.WriteLine($"No of Node currently in Doubly LinkedList : {myList.Count}");

            myList.DeleteByKey(25); // Delete First
            PrintFromEnd(myList);   // Print from End
            Console.ReadKey();
        }

        // To Print Elements From Start
        static void PrintFromStart(DoublyLinkedList myList)
        {
            Node current = null;
            if(myList !=null)
                current = myList.Head;

            Console.WriteLine("\nPrinting Elements From Start/Head");
            while(current!=null)
            {
                Console.Write($"--> {current.Data}");
                current= current.next;
            }
            Console.WriteLine();
        }

        // To Print Elements From Last
        static void PrintFromEnd(DoublyLinkedList myList)
        {
            Node current = null;
            if (myList != null)
                current = myList.Head;

            Console.WriteLine("\nPrinting Elements From End");
            while (current.next != null)
                current = current.next;
            while (current!= null)
            {
                Console.Write($"--> {current.Data}");
                current = current.prv;
            }
            Console.WriteLine();
        }
    }
}
