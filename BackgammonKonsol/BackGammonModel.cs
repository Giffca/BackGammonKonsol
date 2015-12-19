using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonKonsol
{

	enum Colors
	{
		White,
		Black 
	};

	struct Triangel  
	{
		public int antal;
		public Colors color;
	}


	//Kommer definitivt behövas mer funktioner som sedan används i den grafiska miljön.
	class BackGammonModel
	{
		private Random rnd = new Random();


		// kanske ändra return typ till arraylist eller alltid [4].
		// 99%
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


		// kollar om någon vunnit genom att se om något element är lika med 0, controllern kollar detta.
		// 99%
		public int[] checkersInGame(Triangel[] spelplan)
		{
			int[] brickor = {0,0};
			for(int i = 0;i<26;i++)
			{
				if (spelplan[i].color == Colors.Black) brickor[0] += spelplan[i].antal;
				else brickor[1] += spelplan[i].antal;

				if(brickor[0]>0 && brickor[1]>0) return brickor;
			}

			return brickor;
		}

		//kollar om en spelare kan göra något move.  loopa genom spelplan, ifall triangel color == spelare så kolla om den kan göra något move därifrån, om det går så return true;
		// men den ska kolla utslagna först och om en utslagen kan spelas ut så måste man flytta den, tror jag.  så kanske ändra return typ på denna.
		// 0%
		public bool canMove(Colors spelare, int[] dices)
		{
			return false;
		}


		//flyttar en bricka.
		// 75%  gå i mål och gå från bar saknas.
		public bool move(Triangel[] spelplan, ref int first, ref int second, int[] dices,Colors spelare)
		{
			if(legitMove(spelplan,ref first,ref second, dices, spelare))
			{
				
				spelplan[first].antal--;
				if(spelplan[second].antal == 1 && spelplan[second].color != spelare)
				{
					if(spelplan[second].color == Colors.White) spelplan[6].antal++;
					else spelplan[19].antal++;

					spelplan[second].color= spelare;
					return true;
				}

				spelplan[second].color = spelare;
				spelplan[second].antal++;
				return true;
			}

			return false;
		}


		//privat funktion som kollar om man kan flytta brickan.
		// 75%  gå i mål och gå från bar saknas.
		private bool legitMove(Triangel[] spelplan, ref int first, ref int second, int[] dices, Colors spelare)
		{
			int langd;
			int indextarning = -1;

			if(spelare==Colors.Black) 
			{
				if(second >= first) return false;
				langd = first-second;
			}
			else 
			{
				if(first >= second) return false;
				langd = second-first;
			}



				for(int i=0;i<dices.Length;i++) if (dices[i]==langd) indextarning = i;
				if(indextarning == -1) return false;

				first = correctPos(first);
				second = correctPos(second);

				if (spelplan[first].color != spelare || spelplan[first].antal == 0) return false;
				if (spelplan[second].antal <= 1 || spelplan[second].color == spelare) 
				{
					dices[indextarning] = 0;
					return true;
				}
				



			return false;

		}



		//privat funktion som rättar vald plats till elementets plats i arrayen.
		// 75% gå i mål och från bar saknas.
		private int correctPos(int spelplanPos)
		{
			if (spelplanPos > 0 && spelplanPos <= 6) return spelplanPos-1;
			if (spelplanPos > 6 && spelplanPos <= 18) return spelplanPos;
			else return spelplanPos+1;
		}


		//vet inte hur mycket Ragnar vill ha selftests, men många saker behöver testas iaf.
		public static bool SelfTest()
		{
			bool ok = true;
			BackGammonModel test = new BackGammonModel();


			// Test för Triangel Struct
			//
			Triangel [] test1 = new Triangel [4];
			test1[0].antal = 3;
			test1[0].color = Colors.Black;
			test1[1].antal = 1;
			test1[1].color = Colors.White;

			ok = ok && test1[0].antal == 3 && test1[0].color == Colors.Black;
			ok = ok && test1[1].antal == 1 && test1[1].color == Colors.White;
			ok = ok && test1[2].antal == 0 && test1[2].color == Colors.White;
			ok = ok && test1[3].antal == 0 && test1[3].color == Colors.White;

			ok = ok && (int)Colors.Black == 1;
			ok = ok && (int)Colors.White == 0;

			System.Diagnostics.Debug.WriteLine("Triangel " + ok);

			// Test för LetsRollTheDice()
			//
			for (int i = 0; i < 100 && ok; i++)
			{
				int[] dices = test.letsRollTheDice();
				ok = ok && dices[0] >= 1 && dices[0] <= 6 && dices[1] >= 1 && dices[1] <= 6;
				if (dices[0] == dices[1]) if (dices.Length != 4) ok = false;
			}
			System.Diagnostics.Debug.WriteLine("LetsRollTheDice " + ok);


			// Test för correctPos()
			ok = ok && test.correctPos(1) == 0;
			ok = ok && test.correctPos(6) == 5;
			ok = ok && test.correctPos(7) == 7;
			ok = ok && test.correctPos(18) == 18;
			ok = ok && test.correctPos(19) == 20;
			ok = ok && test.correctPos(24) == 25;

			System.Diagnostics.Debug.WriteLine("correctPos " + ok);
			return ok;
		}


	}
}
