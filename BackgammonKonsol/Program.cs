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
			BackGammonConsoleView view = new BackGammonConsoleView();
			Triangel [] spelplan = new Triangel[26];
			spelplan[0].antal = 2;
			spelplan[0].color = Colors.White;
			spelplan[5].antal = 5;
			spelplan[5].color = Colors.Black;
			spelplan[8].antal = 3;
			spelplan[8].color = Colors.Black;
			spelplan[12].antal = 5;
			spelplan[12].color = Colors.White;
			spelplan[13].antal = 5;
			spelplan[13].color = Colors.Black;
			spelplan[17].antal = 3;
			spelplan[17].color = Colors.White;
			spelplan[20].antal = 5;
			spelplan[20].color = Colors.White;
			spelplan[25].antal = 2;
			spelplan[25].color = Colors.Black;
			view.drawBoard(spelplan);
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
