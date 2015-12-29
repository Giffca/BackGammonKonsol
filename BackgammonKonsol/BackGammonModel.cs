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


		// 100%
		public int[] letsRollTheDice()
		{
			int[] dices = new int[4];
			dices[0] = rnd.Next(1, 7);
			dices[1] = rnd.Next(1, 7);
			if (dices[0] == dices[1]) dices[3] = dices[2] = dices[1];

			return dices;
		}

		// 100% 
		public Triangel[] newGame()
		{
            Triangel[] spelplan = new Triangel[26];

 			spelplan[0].antal = 2;
			spelplan[0].color = Colors.White;
			spelplan[5].antal = 5;
			spelplan[5].color = Colors.Black;
			spelplan[6].color = Colors.White;
			spelplan[8].antal = 3;
			spelplan[8].color = Colors.Black;
			spelplan[12].antal = 5;
			spelplan[12].color = Colors.White;
			spelplan[13].antal = 5;
			spelplan[13].color = Colors.Black;
			spelplan[17].antal = 3;
			spelplan[17].color = Colors.White;
			spelplan[19].color = Colors.Black;
			spelplan[20].antal = 5;
			spelplan[20].color = Colors.White;
			spelplan[25].antal = 2;
			spelplan[25].color = Colors.Black;

           return spelplan;
		}

		// 90% inte testat efter bugs.
        public int canMove(Triangel[] spelplan, Colors spelare, int[] dices)
		{
			if (spelare == Colors.White)
				{
					if(spelplan[6].antal > 0)
					{
						foreach (int n in dices) 
							{
							if(n != 0)
								{ 
								if(spelplan[-1+n].antal <= 1 || spelplan[-1+n].color == spelare) return -1;
								}
							}
						return 0;
					}


					bool canGoal = true;
					for (int i = 1; i <= 24; i++)
					{
						int pos = correctPos(i);
						if(spelplan[pos].color == spelare && spelplan[pos].antal > 0)
						{
							if(i < 19) canGoal = false; 
							else if (canGoal && legitMoveGoal(spelplan,i,dices,spelare) != -1) return 2;


							foreach (int n in dices) 
							{
								if (n != 0)
								{ 
									if(i+n <= 24 && legitMove(spelplan,i,i+n,dices,spelare) != -1) 
									{
										if (i>18 && canGoal) return 2;
										else return 1;
									}
								}
							}

							
							
						}
					}
				}


			else
				{

					if(spelplan[19].antal > 0)
						{
							foreach (int n in dices) 
								{
								if(n != 0)
									{ 
									if(spelplan[26-n].antal <= 1 || spelplan[26-n].color == spelare) return -1;
									}
								}
							return 0;
						}

					bool canGoal = true;
					for (int i = 24; i >= 1; i--)
					{
						int pos = correctPos(i);
						if(spelplan[pos].color == spelare && spelplan[pos].antal > 0)
						{
							if(i > 6) canGoal = false; 
							else if (canGoal && legitMoveGoal(spelplan,i,dices,spelare) != -1) return 2;


							foreach (int n in dices) 
							{
								if (n != 0)
								{ 
									if(i-n >= 1 && legitMove(spelplan,i,i-n,dices,spelare) != -1) 
									{
										if (i<=6 && canGoal) return 2;
										else return 1;
									}
								}
							}

							
							
						}
					}
				}
			return 0;
		}



		//flyttar en bricka.
		// 100%
		public bool move(Triangel[] spelplan, int first, int second, int[] dices,Colors spelare)
		{
			int index = legitMove(spelplan,first,second, dices, spelare);
			if(index != -1)
			{
				if (first != -1) first = correctPos(first);
				else if (spelare == Colors.White) first = 6;
				else first = 19;

				second = correctPos(second);
				dices[index] = 0;

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

		//funktion som används när man försöker gå i mål.
		// 90% inte testat bugs
		public bool moveGoal(Triangel[] spelplan, int first,int[] dices,Colors spelare)
		{
			int index = legitMoveGoal(spelplan,first,dices,spelare);
			if(index != -1)
				{
				first = correctPos(first);
				dices[index] = 0;
				spelplan[first].antal--;
				return true;
				}
	
			return false;
		}



		//funktion som kollar om man kan gå i mål.
		// 90% inte testat bugs
		private int legitMoveGoal(Triangel[] spelplan, int first, int[] dices, Colors spelare)
		{
			int[] dice = new int [4];
			for(int i = 0; i<4;i++) dice[i] = dices[i];

			if(spelare==Colors.White)
				{
				for(int i=20; i<=25; i++)
					{
					if(spelplan[i].antal == 0 || spelplan[i].color == Colors.Black)
						{
						for(int j = 0; j<4;j++)
							{
								if(dice[j]==26-i) dice[j]--;
							}
						}
					else break;
					}
				for(int i=0;i<dice.Length;i++) if (dice[i]+first == 25) return i;
				}

			if(spelare==Colors.Black)
				{
				for(int i=5; i>=0; i--)
					{
					if(spelplan[i].antal == 0 || spelplan[i].color == Colors.White)
						{
						for(int j = 0; j<4;j++)
							{
								if(dice[j]==1+i) dice[j]--;
							}
						}
					else break;
					}
				for(int i=0;i<dice.Length;i++) if (first-dice[i] == 0) return i;
				}


			

			return -1;
		}


		//privat funktion som kollar om man kan flytta brickan.
		// 100%
		private int legitMove(Triangel[] spelplan, int first, int second, int[] dices, Colors spelare)
		{
			int langd;
			int indextarning = -1;

			if(spelare==Colors.Black) 
			{
				if(first == -1) langd = 25-second;
				else
					{
						if(second >= first) return -1;
						langd = first-second;
					}
			}

			else 
			{
				if(first == -1) langd = second;
				else
					{ 
						if(first >= second) return -1;
						langd = second-first;
					}
			}



				for(int i=0;i<dices.Length;i++) if (dices[i]==langd) indextarning = i;
				if(indextarning == -1) return -1;

				if (first != -1) first = correctPos(first);
				else if(spelare == Colors.White) first = 6;
				else first = 19;
				second = correctPos(second);

				if (spelplan[first].color != spelare || spelplan[first].antal == 0) return -1;
				if (spelplan[second].antal <= 1 || spelplan[second].color == spelare) 
				{
					return indextarning;
				}
				



			return -1;

		}



		//privat funktion som rättar vald plats till elementets plats i arrayen.
		// 100%
		private int correctPos(int spelplanPos)
		{
			if (spelplanPos > 0 && spelplanPos <= 6) return spelplanPos-1;
			if (spelplanPos > 6 && spelplanPos <= 18) return spelplanPos;
			else return spelplanPos+1;
		}


		//vet inte hur mycket Ragnar vill ha selftests, men många saker behöver testas iaf.
		// 33%
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
				if (dices[0] == dices[1]) if (dices[1] != dices[2] && dices[2] != dices[3]) ok = false;
			}
			System.Diagnostics.Debug.WriteLine("LetsRollTheDice " + ok);

			// Test för newGame()
			//

			Triangel[] testspelplan = new Triangel[5];

 			testspelplan[0].antal = 1;
			testspelplan[1].antal = 1;
			testspelplan[2].antal = 1;
			testspelplan[3].antal = 1;
			testspelplan[4].antal = 1;

			testspelplan = test.newGame();

			ok = ok && testspelplan[0].antal != 1;
			ok = ok && testspelplan[1].antal != 1;
			ok = ok && testspelplan[2].antal != 1;
			ok = ok && testspelplan[3].antal != 1;
			ok = ok && testspelplan[4].antal != 1;
			ok = ok && testspelplan.Length==26;

			System.Diagnostics.Debug.WriteLine("newGame " + ok);

			// Test för correctPos()
			//
			ok = ok && test.correctPos(1) == 0;
			ok = ok && test.correctPos(6) == 5;
			ok = ok && test.correctPos(7) == 7;
			ok = ok && test.correctPos(18) == 18;
			ok = ok && test.correctPos(19) == 20;
			ok = ok && test.correctPos(24) == 25;

			System.Diagnostics.Debug.WriteLine("correctPos " + ok);
			

            //Test för legitMove()
			//
            int[] dices1 = { 2, 1, 0, 0 };
			testspelplan = test.newGame();

            // Spelare Black
            ok = ok && test.legitMove(testspelplan, 13, 11, dices1, Colors.Black) == 0; 
            ok = ok && test.legitMove(testspelplan, 20, 19, dices1, Colors.Black) == -1; 
            ok = ok && test.legitMove(testspelplan, 21, 19, dices1, Colors.Black) == -1; 
            ok = ok && test.legitMove(testspelplan, 19, 21, dices1, Colors.Black) == -1; 
            ok = ok && test.legitMove(testspelplan, 13, 12, dices1, Colors.Black) == -1; 
			dices1[0] = 4;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, Colors.Black) == 0;
 			dices1[1] = 4;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, Colors.Black) == 1; 
			dices1[2] = 4;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, Colors.Black) == 2; 
			dices1[3] = 4;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, Colors.Black) == 3;
			dices1[0] = 0;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, Colors.Black) == 3;
			dices1[1] = 0;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, Colors.Black) == 3; 
			dices1[2] = 0;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, Colors.Black) == 3; 
			dices1[3] = 0;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, Colors.Black) == -1;
			//spela från bar
			testspelplan[19].antal = 1;

			dices1[0] = 1;
			dices1[1] = 2;
			dices1[2] = 3;
			dices1[3] = 4;

			ok = ok && test.legitMove(testspelplan, -1, 1, dices1, Colors.Black) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 24, dices1, Colors.Black) == 0;
			ok = ok && test.legitMove(testspelplan, -1, 23, dices1, Colors.Black) == 1;
			ok = ok && test.legitMove(testspelplan, -1, 22, dices1, Colors.Black) == 2;
			ok = ok && test.legitMove(testspelplan, -1, 21, dices1, Colors.Black) == 3;
			testspelplan[19].antal = 0;
			ok = ok && test.legitMove(testspelplan, -1, 24, dices1, Colors.Black) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 23, dices1, Colors.Black) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 22, dices1, Colors.Black) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 21, dices1, Colors.Black) == -1;
			testspelplan[19].antal = 1;
			testspelplan[25].antal = 2;
			testspelplan[24].antal = 2;
			testspelplan[23].antal = 2;
			testspelplan[22].antal = 2;
			testspelplan[25].color = Colors.White;
			testspelplan[24].color = Colors.White;
			testspelplan[23].color = Colors.White;
			testspelplan[22].color = Colors.White;
			ok = ok && test.legitMove(testspelplan, -1, 24, dices1, Colors.Black) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 23, dices1, Colors.Black) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 22, dices1, Colors.Black) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 21, dices1, Colors.Black) == -1;


			//Spelare White
			dices1[0] = 2;
			dices1[1] = 1;
			dices1[2] = 0;
			dices1[3] = 0;
            ok = ok && test.legitMove(testspelplan, 1, 3, dices1, Colors.White) == 0;  
            ok = ok && test.legitMove(testspelplan, 8, 10, dices1, Colors.White) == -1; 
            ok = ok && test.legitMove(testspelplan, 22, 20, dices1, Colors.White) == -1;
            ok = ok && test.legitMove(testspelplan, 12, 10, dices1, Colors.White) == -1;
            ok = ok && test.legitMove(testspelplan, 17, 20, dices1, Colors.White) == -1;
			dices1[0] = 4;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, Colors.White) == 0; 
			dices1[1] = 4;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, Colors.White) == 1;
			dices1[2] = 4;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, Colors.White) == 2;
			dices1[3] = 4;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, Colors.White) == 3;
			dices1[0] = 0;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, Colors.White) == 3; 
			dices1[1] = 0;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, Colors.White) == 3;
			dices1[2] = 0;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, Colors.White) == 3;
			dices1[3] = 0;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, Colors.White) == -1;


            //spela från bar
			testspelplan[6].antal = 1;
			dices1[0] = 1;
			dices1[1] = 2;
			dices1[2] = 3;
			dices1[3] = 4;

			ok = ok && test.legitMove(testspelplan, -1, 24, dices1, Colors.White) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 1, dices1, Colors.White) == 0;
			ok = ok && test.legitMove(testspelplan, -1, 2, dices1, Colors.White) == 1;
			ok = ok && test.legitMove(testspelplan, -1, 3, dices1, Colors.White) == 2;
			ok = ok && test.legitMove(testspelplan, -1, 4, dices1, Colors.White) == 3;
			testspelplan[6].antal = 0;
			ok = ok && test.legitMove(testspelplan, -1, 1, dices1, Colors.White) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 2, dices1, Colors.White) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 3, dices1, Colors.White) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 4, dices1, Colors.White) == -1;
			testspelplan[6].antal = 1;
			testspelplan[0].antal = 2;
			testspelplan[1].antal = 2;
			testspelplan[2].antal = 2;
			testspelplan[3].antal = 2;
			testspelplan[0].color = Colors.Black;
			testspelplan[1].color = Colors.Black;
			testspelplan[2].color = Colors.Black;
			testspelplan[3].color = Colors.Black;
			ok = ok && test.legitMove(testspelplan, -1, 1, dices1, Colors.White) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 2, dices1, Colors.White) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 3, dices1, Colors.White) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 4, dices1, Colors.White) == -1;


            System.Diagnostics.Debug.WriteLine("legitMove " + ok);

            //Test för legitMoveGoal()
			testspelplan = test.newGame();
            //Spelare Black
			dices1[0] = 1;
			dices1[1] = 1;
			dices1[2] = 1;
			dices1[3] = 1;
            ok = ok && test.legitMoveGoal(testspelplan, 5, dices1, Colors.Black) == -1;

            //Spelare White
            ok = ok && test.legitMoveGoal(testspelplan, 21, dices1, Colors.White) == -1;

            System.Diagnostics.Debug.WriteLine("legitMoveGoal " + ok);


            //Test för moveGoal()

                 //  System.Diagnostics.Debug.WriteLine("moveGoal " + ok);

            //Test för move()
			testspelplan = test.newGame();

            //Spelare Black
            dices1[0] = 2;
            dices1[1] = 1;
            dices1[2] = 0;
            dices1[3] = 0;
            ok = ok && test.move(testspelplan, 13, 11, dices1, Colors.Black) == true;
            ok = ok && test.move(testspelplan, 20, 19, dices1, Colors.Black) == false;
            ok = ok && test.move(testspelplan, 21, 19, dices1, Colors.Black) == false;
            ok = ok && test.move(testspelplan, 19, 21, dices1, Colors.Black) == false;
            ok = ok && test.move(testspelplan, 13, 12, dices1, Colors.Black) == false;

            dices1[0] = 4;
            ok = ok && test.move(testspelplan, 24, 20, dices1, Colors.Black) == true;
            dices1[1] = 4;
            ok = ok && test.move(testspelplan, 24, 20, dices1, Colors.Black) == true;
         
            //spela från bar
            testspelplan[19].antal = 1;
            ok = ok && test.move(testspelplan, -1, 1, dices1, Colors.Black) == false;


            //Spelare White
            dices1[0] = 2;
            dices1[1] = 1;
            dices1[2] = 0;
            dices1[3] = 0;
            ok = ok && test.move(testspelplan, 1, 3, dices1, Colors.White) == true;
            ok = ok && test.move(testspelplan, 8, 10, dices1, Colors.White) == false;
            ok = ok && test.move(testspelplan, 22, 20, dices1, Colors.White) == false;
            ok = ok && test.move(testspelplan, 12, 10, dices1, Colors.White) == false;
            ok = ok && test.move(testspelplan, 17, 20, dices1, Colors.White) == false;
            dices1[0] = 4;
            ok = ok && test.move(testspelplan, 1, 5, dices1, Colors.White) == true;
            dices1[1] = 4;
   
            //spela från bar
            testspelplan[6].antal = 1;
            ok = ok && test.move(testspelplan, -1, 21, dices1, Colors.White) == false;

            System.Diagnostics.Debug.WriteLine("move " + ok);

            //Test för canMove()
            return ok;
		}


	}
}
