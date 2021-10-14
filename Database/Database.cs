using System;

namespace Database
{
    public class Database
    {
        private int[] data;

        private int count = 0;

        public Database(params int[] numbers)
        {
            this.data = new int[16];

            if (numbers.Length > 16)
            {
                throw new InvalidOperationException("Database capacity is no bigger than 16 integers!");
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                this.Add(numbers[i]);
            }

            this.count = numbers.Length;
        }

        public int Count
        {
            get { return count; }
        }

        public void Add(int element)
        {
            if (this.count >= 16)
            {
                throw new InvalidOperationException("Database capacity is exactly 16 integers!");
            }

            this.data[this.count] = element;
            this.count++;
        }

        public void Remove()
        {
            if (this.count == 0)
            {
                throw new InvalidOperationException("The collection is empty!");
            }

            this.count--;
            this.data[this.count] = 0;
        }

        /// <summary>
        /// Returns the database copied to an array.
        /// </summary>
        /// <returns>int[]</returns>
        public int[] Fetch()
        {
            int[] coppyArray = new int[this.count];

            for (int i = 0; i < this.count; i++)
            {
                coppyArray[i] = this.data[i];
            }

            return coppyArray;
        }

        public override string ToString()
        {
            return String.Join(", ", this.data);
        }
    }
}
