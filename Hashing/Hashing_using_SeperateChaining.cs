using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashing
{
    //GFG Link https://www.geeksforgeeks.org/hashing-set-2-separate-chaining/

    /// <summary>
    /// Hashing is improvement for Direct access table, below implementation utilizes Seperate Chaining method for collision handling
    /// Time Complexity O( 1 + LoadFactor) for Search & Delete and O(1) for Insert
    /// LoadFactor = N/M, where N = no of key to be inserteed in HashTable & M = total no of slots in HashTable
    /// </summary>
    class Hashing_using_SeperateChaining
    {
        private List<int>[] _ArrayOfGenericList;
        public int Capacity { get; set; }
        //Constructor
        public Hashing_using_SeperateChaining(int size)
        {
            Capacity = size;
            _ArrayOfGenericList = new List<int>[Capacity];
        }

        public void Insert(int key)                                             //Time Complexcity O(1)
        {
            int index = CommonUtility.HashFunc1(key, Capacity);
            if (_ArrayOfGenericList[index] == null)                             //Initialize GenericList at each slot in HashTable
                _ArrayOfGenericList[index] = new List<int>();
            _ArrayOfGenericList[index].Add(key);
            Console.WriteLine($"Insert SUCCESS\t|| Key : {key} inserted at index : {index}");
        }

        public void SearchKey(int key)                                          //Time Complexcity O(1+loadFactor)
        {
            int index = CommonUtility.HashFunc1(key, Capacity);
            if (CheckValidIndex(index) && _ArrayOfGenericList[index]!=null)
            {
                foreach (int value in _ArrayOfGenericList[index])
                    if (value == key)
                    {
                        Console.WriteLine($"Key : {key} found at index : {index}");
                        break;
                    }
            }
            else
                Console.WriteLine($"Search FAIL\t|| Key : {key} not present in HashTable");
        }

        public void Delete(int key)                                             //Time Complexcity O(1+loadFactor)
        {
            int index = CommonUtility.HashFunc1(key, Capacity);
            if (CheckValidIndex(index) && _ArrayOfGenericList[index] != null)
            {
                foreach (int value in _ArrayOfGenericList[index])
                    if (value == key)
                    {
                        Console.WriteLine($"Key : {key} deleted from associated list at index : {index}");
                        _ArrayOfGenericList[index].Remove(key);
                        break;
                    }
            }
            else
                Console.WriteLine($"Delete FAIL\t|| Key : {key} not present in HashTable");
        }

        public void DisplayAll()
        {
            Console.WriteLine("\tDisplaying all keys present in HashTable");
            for (int index = 0; index < Capacity; index++)
            {
                Console.Write($"Index : {index} ");
                if (_ArrayOfGenericList[index] != null)
                    foreach (int value in _ArrayOfGenericList[index])
                        Console.Write($"-->{value}");
                Console.WriteLine();
            }
        }
        
        private bool CheckValidIndex(int index) => (index >= 0 && index < Capacity)? true : false;
    }
}
