using System;
using System.Linq;
using Database;

namespace StartUp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            int[] nums = Enumerable.Range(1, 5).ToArray();
            Console.WriteLine(string.Join(", ", nums));

            Database.Database db = new Database.Database();
            Database.Database db2 = new Database.Database(nums);
            Database.Database db3 = new Database.Database(Enumerable.Range(1, 16).ToArray());
            //Database.Database db4 = new Database.Database(Enumerable.Range(1, 20).ToArray());

            Console.WriteLine(db.Count);
            Console.WriteLine(db2.Count);
            Console.WriteLine(nums.Length);
            Console.WriteLine(db3.Count);
            //Console.WriteLine(db4.Count);

            Console.WriteLine();

            Console.WriteLine(db2.ToString());


            

        }
    }
}
