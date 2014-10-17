using System;
using System.Collections.Generic;

namespace EditDistance
{
	/*
	 * Edit distance (metric) is a way of quantifying how dissimilar two strings (e.g., words)
	 * are to one another by counting the minimum number of operations required to
	 * transform one string into the other.
	 * 
	 * One of the most common variants is called Levenshtein distance,
	 * named after the Soviet Russian computer scientist Vladimir Levenshtein.
	 * 
	 * In this version, the allowed operations are the removal or insertion of a single character,
	 * or the substitution of one character for another.
	 * Levenshtein distance may also simply be called "edit distance",
	 * although several variants exist.
	 * 
	 * More: http://en.wikipedia.org/wiki/Edit_distance
	 * 
	 * Wagner–Fischer algorithm is a dynamic programming algorithm
	 * that computes the edit distance between two strings of characters.
	 * Time: O(mn)
	 * Space: O(mn) - with some tradeoffs, it can be improved to  O(min(m,n))
	 * 
	 * More: http://en.wikipedia.org/wiki/Wagner%E2%80%93Fischer_algorithm
	 */

	class MyEditDistance
	{
		private static string String1 = "";
		private static string String2 = "";

		public static void Main(string[] args)
		{
			// We expect two parameters (2 strings)
			if (args.Length != 2)
			{
				Console.WriteLine("Unexpected number of parameters ({0} instead of {1})", args.Length, 2);
				System.Environment.Exit(1);
			}

			String1 = args[0];
			String2 = args[1];

			Console.WriteLine("Selected strings:\n1: {0}\n2: {1}\n", String1, String2);

			EditDistance(String1, String2);
		}

		private static void EditDistance(string s, string t)
		{
			Array d = Array.CreateInstance(typeof(int), s.Length + 1, t.Length + 1);

            Console.WriteLine(d.GetUpperBound(0) - d.GetLowerBound(0));
            Console.WriteLine(d.GetUpperBound(1) - d.GetLowerBound(1));
			// Set all array members to 0.
			for (int i = d.GetLowerBound(0); i <= d.GetUpperBound(0); i++)
			{
				for (int j = d.GetLowerBound(1); j <= d.GetUpperBound(1); j++)
				{
					d.SetValue(0, i, j);
				}
			}
			Console.WriteLine("Set all array members to 0:");
			PrintValues(d, s, t);

			// Set starting conditions.
			for (int i = d.GetLowerBound(0); i <= d.GetUpperBound(0); i++)
			{
				d.SetValue(i, i, 0);
			}
			for (int j = d.GetLowerBound(1); j <= d.GetUpperBound(1); j++)
			{
				d.SetValue(j, 0, j);
			}
			Console.WriteLine("Set starting conditions:");
            PrintValues(d, s, t);

			// Begin calculating.
			for (int j = d.GetLowerBound(1) + 1; j <= d.GetUpperBound(1); j++)
			{
				for (int i = d.GetLowerBound(0) + 1; i <= d.GetUpperBound(0); i++)
				{
					if (s[i-1] == t[j-1])
					{
						int upLeft = Convert.ToInt32(d.GetValue(i - 1, j - 1));
						d.SetValue(upLeft, i, j);
					}
					else
					{
						int deletion = Convert.ToInt32(d.GetValue(i - 1, j)) + 1;
						int insertion = Convert.ToInt32(d.GetValue(i, j - 1)) + 1;
						int substitution = Convert.ToInt32(d.GetValue(i - 1, j - 1)) + 1;
						int min = 0;

						// Priority of operations if both operations are same cost:
						// 1. Substitution, 2. Insertion, 3. Deletion
						if (substitution <= insertion && substitution <= deletion)
						{
							// Substitute.
							min = substitution;
						}
						else if (insertion <= substitution && insertion <= deletion)
						{
							// Insert.
							min = insertion;
						}
						else if (deletion <= substitution && deletion <= insertion)
						{
							// Delete.
							min = deletion;
						}

						d.SetValue(min, i, j);
					}
				}
			}
			Console.WriteLine("Resulting prefix distances table:");
            PrintValues(d, s, t);


		}

		public static void PrintValues(Array d, string s, string t)  {
            Console.Write("\t\t");
            foreach (char c in t)
            {
                Console.Write(c + "\t");
            }
            Console.WriteLine();

            for (int i = d.GetLowerBound(0); i <= d.GetUpperBound(0); i++)
            {
                if (i > 0)
                    Console.Write(s[i - 1] + "\t");
                else
                    Console.Write("\t");
                for (int j = d.GetLowerBound(1); j <= d.GetUpperBound(1); j++)
                {
                    Console.Write(Convert.ToInt32(d.GetValue(i, j)).ToString() + "\t");
                }
                Console.WriteLine();
            }
            /*
			System.Collections.IEnumerator myEnumerator = myArr.GetEnumerator();
			int i = 0;
			int cols = myArr.GetLength( myArr.Rank - 1 );
			while ( myEnumerator.MoveNext() )  {
				if ( i < cols )  {
					i++;
				} else  {
					Console.WriteLine();
					i = 1;
				}
				Console.Write( "\t{0}", myEnumerator.Current );
			}
			Console.WriteLine("\n");
            */         
		}
	}
}
