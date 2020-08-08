using System;
using System.Security.AccessControl;

namespace RangeMinimumQueries_RMQ
{
    // https://www.youtube.com/watch?v=uUatD9AudXo
    /// <summary>
    /// Sparse Table allow performing efficient Range queries on static array,
    /// Range queries are of various types but most common ones are Min, Max, Sum, GCD range queries.
    /// The idea behind Sparse Table is to pre-compute the answer for all intervals of size 2 to power x to efficietly answer range queries b/w index [l..r]
    /// Downside is m/r space required for an array of size 'N' &  let 2 to the power 'P' we the largest value that fits inside N, i.e, P = floor(Log base 2 (N))
    /// Total table space required : N columns * P rows i.e. Space Required O(nLogn)
    /// Query Time for associative function which are overlap friendly O(1) like (Min,Max) & O(logn) for func which are not overlap friendly (Addition,Substraction,Multiplication)
    /// Build Time for Sparse table is O(nlogn)
    /// </summary>
    public class SparseTable
    {
        private readonly int[,] Spt;
        public int Rows { get; set; }
        public int Columns { get; set; }

        // this index table is useful only when we want to query the index of the min(or max) element in range [l..r],
        // not useful for most other range queries like Sum, G.C.D/H.C.F
        private readonly int[,] IndexTable;

        public SparseTable(int[] inputArr)
        {
            Columns = inputArr.Length;                              // Length of input array
            Rows = (int)Math.Floor(Math.Log(Columns, 2)) + 1;       // P + 1, here P = floor value of Log(base 2 (inputArr Length))
            Spt = new int[Rows, Columns];
            IndexTable = new int[Rows, Columns];

            // fill 1st row with input array
            for (int i = 0; i < Columns; i++)
            {
                Spt[0, i] = inputArr[i];
                IndexTable[0, i] = i;
            }

            // build the entire table
            BuildTable();
        }

        /// <summary>
        /// We build table by reusing already computed range value of previous cells (Dynamic Programming)
        /// each Cell[row,col] represent the computed value for range from 'col' to 'col + 2 to power (row-1)' in original input array
        /// </summary>
        void BuildTable()
        {
            for (int row = 1; row < Rows; row++)
                for (int col = 0; col <= Columns - (1 << row); col++)   // 1 << row is equivalent to Math.Pow(2,row)
                {
                    var left = Spt[row - 1, col];
                    var rt = Spt[row - 1, col + (1 << (row - 1))];
                    //Console.Write($"\nLeft {left} Right {rt} \tResult {Spt[row, col]}");
                    Spt[row, col] = RangeCombinationFunc(left, rt);
                    IndexTable[row, col] = left <= rt ? IndexTable[row - 1, col] : IndexTable[row - 1, col + (1 << (row - 1))];
                }
            PrintSparseTable();
        }

        /// <summary>
        /// This function decides what kind of Sparse Table we are going to build,
        /// we can overload this function in derieved classes to construct a different Sparse Table
        /// In this example however we our constructing Min Sparse Table hence func would return the min of two passed values
        /// </summary>
        /// <param name="left"></param>
        /// <param name="rt"></param>
        /// <returns></returns>
        protected int RangeCombinationFunc(int left, int rt) => Math.Min(left, rt);

        /// <summary>
        /// Returns the correct ans for the range b/w given input indexes || Time O(1) since finding Min is Overlap friendly func || Space O(1)
        /// Suitable for Sparse Table created for Min, Max, GCD
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        public int RangeQuery(int index1, int index2)
        {
            var left = Math.Min(index1, index2);
            var rt = Math.Max(index1, index2);
            var intervalLength = rt - left + 1;
            var pow = (int)Math.Floor(Math.Log(intervalLength, 2));     // finding power to get to the desired row
            var k = 1 << pow;                                           // k = largest (2^pow) which fits in given intervalLength
            return RangeCombinationFunc(Spt[pow, left], Spt[pow, rt - k + 1]);
        }

        /// <summary>
        /// Returns the correct ans for the range b/w given input indexes || Time O(log(base2)n) used for Non Overlap friendly func like Multiply,Add,etc || Space O(1)
        /// Cascading Query is Alternate to Range Query when func such as *, +, -, / are used to create Sparse Table
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        public int AssociativeFuncQuery(int index1, int index2)
        {
            var left = Math.Min(index1, index2);
            var rt = Math.Max(index1, index2);
            var intervalLength = rt - left + 1;
            int ans = -1;
            // since multiplication is Not OverLap friendly will hence we have to divide the interval into sub-intervals which dont overlap
            while (intervalLength > 0 && left <= rt)
            {
                var pow = (int)Math.Floor(Math.Log(intervalLength, 2)); // finding power to get to the desired row
                var k = 1 << pow;                                       // k = largest (2^pow) which fits in current intervalLength
                ans = (ans == -1) ? Spt[pow, left] : ans * Spt[pow, left];
                intervalLength -= k;
                left += k;
            }
            return ans;
        }

        public void PrintSparseTable()
        {
            Console.WriteLine("\nPrinting the entire Sparse Table below");
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                    Console.Write($" {Spt[row, col]}");
                Console.WriteLine();
            }
        }

        // used in GraphUtility.LowestCommonAnsectorInDirectedAcyclicGraph() to query LCA's Index || Time O(1)
        public int IndexQuery(int index1, int index2)
        {
            var leftIndex = Math.Min(index1, index2);
            var rtIndex = Math.Max(index1, index2);
            var intervalLength = rtIndex - leftIndex + 1;
            var pow = (int)Math.Floor(Math.Log(intervalLength, 2));     // finding power to get to the desired row
            var k = 1 << pow;                                           // k = largest (2^pow) which fits in given intervalLength
            var leftValue = Spt[pow, leftIndex];
            var rtValue = Spt[pow, rtIndex - k + 1];
            return leftValue <= rtValue ? IndexTable[pow, leftIndex] : IndexTable[pow, rtIndex - k + 1];
        }
    }
}
