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
			
			Triangel [] test1 = new Triangel [2];
			test1[0].antal = 3;
			test1[0].color = Colors.Black;
			test1[1].antal = 1;
			test1[1].color = Colors.White;

			Console.WriteLine(test1[0].antal + " " + test1[0].color);
			Console.WriteLine(test1[1].antal + " " + test1[1].color);


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
