using System;

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

	public class EditDistance
	{
		private string string1 = "";
		private string string2 = "";

		/// <summary>Reference string.</summary>
		/// <value>Gets or sets reference string. Generates new table if set to a new value.</value>
		public string StringFrom { 
			get
			{
				return string2;
			}
			set
			{
				string2 = value;
				GenerateTable();
			}
		}
		/// <summary>String that we change to reference string.</summary>
		/// <value>Gets or sets  string. Generates new table if set to a new value.</value>
		public string StringTo { 
			get
			{
				return string1;
			}
			set
			{
				string1 = value;
				GenerateTable();
			}
		}

		private int distance = 0;
		/// <summary>Final result - Edit Distance between strings.</summary>
		/// <value>Gets Edit Distance.</value>
		public int Distance
		{
			get
			{
				return distance;
			}
		}

		private Array distanceTable;
		/// <summary>Distance Table (2-D Array) between two strings.</summary>
		/// <value>Gets Distance Table (2-D Array).</value>
		public Array DistanceTable
		{
			get
			{
				return distanceTable;
			}
		}

		/// <summary>Creates and initializes Edit Distance table for two given strings.</summary>
		/// <param name="from">Reference string we compare second string to.</param>
		/// <param name="to">String that we change to first string.</param>
		public EditDistance(string from, string to)
		{
			string1 = to;
			string2 = from;

			GenerateTable();
		}

		private void GenerateTable()
		{
			distanceTable = Array.CreateInstance(typeof(int), string1.Length + 1, string2.Length + 1);

			// Set all array members to 0.
			for (int i = distanceTable.GetLowerBound(0); i <= distanceTable.GetUpperBound(0); i++)
			{
				for (int j = distanceTable.GetLowerBound(1); j <= distanceTable.GetUpperBound(1); j++)
				{
					distanceTable.SetValue(0, i, j);
				}
			}

			// Set starting conditions.
			for (int i = distanceTable.GetLowerBound(0); i <= distanceTable.GetUpperBound(0); i++)
			{
				distanceTable.SetValue(i, i, 0);
			}
			for (int j = distanceTable.GetLowerBound(1); j <= distanceTable.GetUpperBound(1); j++)
			{
				distanceTable.SetValue(j, 0, j);
			}
		}

		public void CalculateTable()
		{
			for (int j = distanceTable.GetLowerBound(1) + 1; j <= distanceTable.GetUpperBound(1); j++)
			{
				for (int i = distanceTable.GetLowerBound(0) + 1; i <= distanceTable.GetUpperBound(0); i++)
				{
					if (string1[i-1] == string2[j-1])
					{
						int upLeft = Convert.ToInt32(distanceTable.GetValue(i - 1, j - 1));
						distanceTable.SetValue(upLeft, i, j);
					}
					else
					{
						int deletion = Convert.ToInt32(distanceTable.GetValue(i - 1, j)) + 1;
						int insertion = Convert.ToInt32(distanceTable.GetValue(i, j - 1)) + 1;
						int substitution = Convert.ToInt32(distanceTable.GetValue(i - 1, j - 1)) + 1;
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

						distanceTable.SetValue(min, i, j);
						distance = min;
					}
				}
			}
		}

		public void PrintValues()  {
			Console.Write("\t\t");
			foreach (char c in string2)
			{
				Console.Write(c + "\t");
			}
			Console.WriteLine();

			for (int i = distanceTable.GetLowerBound(0); i <= distanceTable.GetUpperBound(0); i++)
			{
				if (i > 0)
					Console.Write(string1[i - 1] + "\t");
				else
					Console.Write("\t");
				for (int j = distanceTable.GetLowerBound(1); j <= distanceTable.GetUpperBound(1); j++)
				{
					Console.Write(Convert.ToInt32(distanceTable.GetValue(i, j)).ToString() + "\t");
				}
				Console.WriteLine();
			}       
		}
	}
}

