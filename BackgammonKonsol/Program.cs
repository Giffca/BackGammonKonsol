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
			
			Triangel [] test1 = new Triangel [3];
			test1[0].antal = 3;
			test1[0].color = Colors.Black;
			test1[1].antal = 1;
			test1[1].color = Colors.White;

			Console.WriteLine(test1[0].antal + " " + test1[0].color);
			Console.WriteLine(test1[1].antal + " " + test1[1].color);
			Console.WriteLine(test1[2].antal + " " + test1[2].color + "\n\n");  // utan initiallesering.

			BackGammonModel.SelfTest();
			BackGammonModel test = new BackGammonModel();
			String a = "";
			while(a == "") 
				{

				foreach(int j in test.letsRollTheDice())
					{
					Console.Write(j + " ");
					}
				Console.WriteLine();
				a = Console.ReadLine();
				}

		}
	}
}
