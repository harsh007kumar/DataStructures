using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashing
{
    // GFG referrence https://www.geeksforgeeks.org/hashing-set-3-open-addressing/
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = { 19, 27, 36, 10, 64 };
            // Create HashTable of size, lets Say 13
            Hashing_using_OpenAddressing hashTable = new Hashing_using_OpenAddressing(13);
            Hashing_using_SeperateChaining hashTable_2 = new Hashing_using_SeperateChaining(13);
            for (int i = 0; i < input.Length; i++)
            {
                hashTable.Insert(input[i]);
                hashTable_2.Insert(input[i]);
            }
            Console.WriteLine("=============== Hashing_using_OpenAddressing\t=====================");
            hashTable.SearchKey(36);    
            hashTable.SearchKey(100);   // This key does not exist in hash table 
            hashTable.DisplayAll();     
            hashTable.Delete(15);       // This key does not exist in hash table 
            hashTable.Delete(27);
            hashTable.DisplayAll();
            hashTable.Insert(15);       
            hashTable.DisplayAll();
            Console.WriteLine("=============== Hashing_using_SeperateChaining\t=====================");
            hashTable_2.SearchKey(36);
            hashTable_2.SearchKey(100);   // This key does not exist in hash table 
            hashTable_2.DisplayAll();
            hashTable_2.Delete(15);       // This key does not exist in hash table 
            hashTable_2.Delete(27);
            hashTable_2.DisplayAll();
            hashTable_2.Insert(15);
            hashTable_2.DisplayAll();

            Console.ReadKey();
        }
    }

    public class HashNode
    {
        public int Value { get; set; }
        public bool SlotIsAvaliable { get; set; }

        public HashNode(int key)
        {
            Value = key;
            SlotIsAvaliable = false;
        }
    }

    /// <summary>
    /// Hashing is improvement for Direct access table, below implementation utilizes Open Addressing method for collision handling
    /// Time Complexity O( 1 / (1-LoadFactor)) for Search, Insert & Delete
    /// LoadFactor = N/M, where N = no of key to be inserteed in HashTable & M = total no of slots in HashTable
    /// </summary>
    public class Hashing_using_OpenAddressing
    {
        private HashNode[] _arr;
        public int CapacityLeft { get; set; }
        public int Len { get; set; }
        //Constructor
        public Hashing_using_OpenAddressing(int size)
        {
            _arr = new HashNode[size];
            CapacityLeft = size;
            Len = size;
        }

        public void Insert(int key)
        {
            if(CheckSpaceAvaliable())
            {
                // Keeping running the loop till we find empty space
                for(int i = 0;i<Len;i++)
                {
                    int index = OpenAddressing.LinearProbling(key, i, Len);
                    if(_arr[index]==null || _arr[index].SlotIsAvaliable)
                    {
                        _arr[index] = new HashNode(key);
                        Console.WriteLine($"Insert SUCCESS\t|| Key : {key} inserted at index : {index}");
                        //Decrease Space left on HashTable
                        CapacityLeft--;
                        break;
                    }
                }
            }
        }

        public void SearchKey(int key)
        {
            if(CapacityLeft<1)
                Console.WriteLine("Search COMPLETE\t|| Cannot search on empty HashTable");
            else
            {
                // Keeping running the loop till we find the element or empty slot
                for (int i = 0; i < Len; i++)
                {
                    int index = OpenAddressing.LinearProbling(key, i, Len);
                    if (_arr[index] == null)
                    {
                        Console.WriteLine($"Search FAIL\t|| Key : {key} not present in HashTable");
                        break;
                    }
                    else if (_arr[index].Value == key && _arr[index].SlotIsAvaliable == false)
                    {
                        Console.WriteLine($"Search SUCCESS\t|| Key : {key} found at index : {index}");
                        break;
                    }
                }
            }
        }

        public void Delete(int key)
        {
            if (CapacityLeft < 1)
                Console.WriteLine("Delete FAIL\t|| Cannot delete from an empty HashTable");
            else
            {
                // Keeping running the loop till we find the element or empty slot
                for (int i = 0; i < Len; i++)
                {
                    int index = OpenAddressing.LinearProbling(key, i, Len);
                    if (_arr[index] == null)
                    {
                        Console.WriteLine($"Delete FAIL\t|| {key} not present in HashTable");
                        break;
                    }
                    else if (_arr[index].Value == key && _arr[index].SlotIsAvaliable==false)
                    {
                        _arr[index].SlotIsAvaliable = true;
                        Console.WriteLine($"Delete SUCCESS\t|| {key} deleted from index : {index}");
                        CapacityLeft++;
                        break;
                    }
                }
            }
        }

        public void DisplayAll()
        {
            if (CapacityLeft < 1)
                Console.WriteLine("No Keys present, HashTable is empty");
            else
            {
                Console.WriteLine("\tDisplaying all keys present in HashTable");
                // Keeping running the loop till we find the element or empty slot
                for (int i = 0; i < Len; i++)
                {
                    if (_arr[i] != null && _arr[i].SlotIsAvaliable==false)
                        Console.WriteLine($"Index : {i} Key : {_arr[i].Value}");
                }
            }
        }

        bool CheckSpaceAvaliable()
        {
            bool hasSpace = true;
            if (CapacityLeft < 1)
            {
                Console.WriteLine("HashTable is full cannot insert more keys, delete exisitng ones before adding new");
                hasSpace = false;
            }
            return hasSpace;
        }
    }
}
