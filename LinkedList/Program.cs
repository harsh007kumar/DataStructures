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
            if (Head != null)
                Head.prv = addOne;          // Point the previously First Node->Previous to newly added one
            addOne.next = Head;             // Point the newly added Node->Next to previously First Node
            Head = addOne;                  // Pointing the Head of LinkedList to newly added node
            count++;
            Console.WriteLine($"Inserting : {data} at the Start");
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
            Console.WriteLine($"Inserting : {data} at the End");
        }

        public void AddAtPosition(int data, int pos = 1)
        {
            Node addOne = new Node(data);
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

                (temp.prv).next = addOne;   // Previous Node next should point to newly added Node
                addOne.prv = temp.prv;      // Newly added Node prv should point to previous of last Node which was there
                temp.prv = addOne;          // Original node's prv should now point to new Node
                addOne.next = temp;         // Node next should point to node which was originally present at that position

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
            myList.AddAtPosition(8, 4);
            PrintFromStart(myList); // Print from Start
            Console.WriteLine($"No of Node currently in Doubly LinkedList : {myList.Count}");

            myList.DeleteByKey(15); // Delete no in between
            myList.DeleteAtPosition(2); // Delete no at position
            myList.DeleteAtEnd();       // Delete Last
            PrintFromStart(myList); // Print from Start
            Console.WriteLine($"No of Node currently in Doubly LinkedList : {myList.Count}");

            myList.DeleteAtStart();     // Delete First
            PrintFromEnd(myList);   // Print from End
            Console.ReadKey();
        }

        // To Print Elements From Start
        static void PrintFromStart(DoublyLinkedList myList)
        {
            Node current = null;
            if(myList !=null)
                current = myList.Head;

            Console.WriteLine("Printing Elements From Start/Head");
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

            Console.WriteLine("Printing Elements From End");
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
