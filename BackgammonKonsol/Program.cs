using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonKonsol
{
	class Program
	{
		static void Main(string[] args)
		{
	
			dice test = new dice();
			String a = "";
			while(a == "") 
				{
				int [] tarn = test.letsRollTheDice();
				foreach(int j in tarn)
				{
					Console.Write(j + " ");
				}
				Console.WriteLine();
				a = Console.ReadLine();
				}
		}
	}
}
