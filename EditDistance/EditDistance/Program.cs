using System;

namespace EditDistance
{
	class Program
	{
		public static void Main(string[] args)
		{
			// We expect two parameters (2 strings)
			if (args.Length != 2)
			{
				Console.WriteLine("Unexpected number of parameters ({0} instead of {1})", args.Length, 2);
				System.Environment.Exit(1);
			}

			EditDistance editDistance = new EditDistance(args[0], args[1]);
			editDistance.CalculateTable();
			editDistance.PrintValues();

			editDistance.StringFrom = "cat";
			editDistance.CalculateTable();
			editDistance.PrintValues();
		}
	}
}
