using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonKonsol
{

	enum Colors
	{
		Black,
		White 
	};


	struct Triangel  // datatyp för plats där brickor kan ligga.
	{
		public int antal;
		public Colors color;
	}


	class BackGammonModel
	{
		private Random rnd = new Random();


		// kanske ändra return typ till arraylist eller alltid [4].
		public int[] letsRollTheDice()
		{
			int[] dices = new int[4];
			dices[2] = dices[3] = 0;
			dices[0] = rnd.Next(1, 7);
			dices[1] = rnd.Next(1, 7);
			if (dices[0] == dices[1]) dices[3] = dices[2] = dices[1];

			if (dices[3] == 0) return (new int[2] { dices[0], dices[1] });
			return dices;
		}


		// Ska räkna alla brickor på spelplanen.
		public int[] checkersInGame(Triangel[] spelplan)
		{
			return (new int[2] {0,0});
		}

		//Ska kolla om man kan flytta brickan. tärningen ska även in här.
		public bool canMove(Triangel[] spelplan, int first, int second)
		{
			return true;
		}


		public static bool SelfTest()
		{
			bool ok = true;
			BackGammonModel test = new BackGammonModel();


			// Test för Triangel Struct
			Triangel [] test1 = new Triangel [4];
			test1[0].antal = 3;
			test1[0].color = Colors.Black;
			test1[1].antal = 1;
			test1[1].color = Colors.White;

			ok = ok && test1[0].antal == 3 && test1[0].color == Colors.Black;
			ok = ok && test1[1].antal == 1 && test1[1].color == Colors.White;
			ok = ok && test1[2].antal == 0 && test1[2].color == Colors.Black;
			ok = ok && test1[3].antal == 0 && test1[3].color == Colors.Black;

			ok = ok && (int)Colors.Black == 0;
			ok = ok && (int)Colors.White == 1;

			System.Diagnostics.Debug.WriteLine("Triangel " + ok);

			// Test för LetsRollTheDice()

			for (int i = 0; i < 100 && ok; i++)
			{
				int[] dices = test.letsRollTheDice();
				ok = ok && dices[0] >= 1 && dices[0] <= 6 && dices[1] >= 1 && dices[1] <= 6;
				if (dices[0] == dices[1]) if (dices.Length != 4) ok = false;
			}


			System.Diagnostics.Debug.WriteLine("LetsRollTheDice " + ok);
			return ok;
		}


	}
}
