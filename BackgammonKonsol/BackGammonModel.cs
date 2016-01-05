﻿//
//	Programmerare:  Timmy & Victoria
//	Datum:	2015-2016
//	Beskrivning: Model fil till Backgammon spelet
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{

	enum COLOR
	{
		WHITE,
		BLACK 
	};

	struct triangel  
	{
		public int antal;
		public COLOR color;
	}


	class BackgammonModel
	{
		private Random rnd = new Random();


		// Slå tärning
		// returnar int array [4]
		public int[] letsRollTheDice()
		{
			int[] dices = new int[4];
			dices[0] = rnd.Next(1, 7);
			dices[1] = rnd.Next(1, 7);
			if (dices[0] == dices[1]) dices[3] = dices[2] = dices[1];

			return dices;
		}

		// Lägger ut startbrickor för ett nytt spel
		// returnar triangel array [26]
		public triangel[] newGame()
		{
            triangel[] spelplan = new triangel[26];

 			spelplan[0].antal = 2;
			spelplan[0].color = COLOR.WHITE;
			spelplan[5].antal = 5;
			spelplan[5].color = COLOR.BLACK;
			spelplan[6].color = COLOR.WHITE;
			spelplan[8].antal = 3;
			spelplan[8].color = COLOR.BLACK;
			spelplan[12].antal = 5;
			spelplan[12].color = COLOR.WHITE;
			spelplan[13].antal = 5;
			spelplan[13].color = COLOR.BLACK;
			spelplan[17].antal = 3;
			spelplan[17].color = COLOR.WHITE;
			spelplan[19].color = COLOR.BLACK;
			spelplan[20].antal = 5;
			spelplan[20].color = COLOR.WHITE;
			spelplan[25].antal = 2;
			spelplan[25].color = COLOR.BLACK;

           return spelplan;
		}

		//kollar om man kan flytta en bricka
		// returnar -1 om man kan gå från baren, 1 om man kan flytta bland trianglarna, 2 om man kan gå i mål, -1 om man inte kan flytta något.
        public int canMove(triangel[] spelplan, COLOR spelare, int[] dices)
		{
			if (spelare == COLOR.WHITE)
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

		//Funktion som tar reda på alla möjliga moves för alla olika trianglar.
		// inte gjord
		public int [,] allAvailableMoves(triangel[] spelplan, int[] dices, COLOR spelare)
		{
			int [,] moves = new int [26,2];

			moves[0,0] = 1;

			return moves;
		}

		// Flyttar en bricka.
		// returnar true om det gick, annars false.
		public bool move(triangel[] spelplan, int first, int second, int[] dices,COLOR spelare)
		{
			if(first == 25 || first == 26) first = -1;
			int index = legitMove(spelplan,first,second, dices, spelare);
			if(index != -1)
			{
				if (first != -1) first = correctPos(first);
				else if (spelare == COLOR.WHITE) first = 6;
				else first = 19;

				second = correctPos(second);
				dices[index] = 0;

				spelplan[first].antal--;
				if(spelplan[second].antal == 1 && spelplan[second].color != spelare)
				{
					if(spelplan[second].color == COLOR.WHITE) spelplan[6].antal++;
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

		// Tar bort bricka från spelplanen (går i mål)
		// returnar true om det gick, annars false.
		public bool moveGoal(triangel[] spelplan, int first,int[] dices,COLOR spelare)
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



		// privat funktion som kollar om man kan gå i mål.
		// returnar int, index till tärning om det går annars -1.
		private int legitMoveGoal(triangel[] spelplan, int first, int[] dices, COLOR spelare)
		{
			int[] dice = new int [4];
			for(int i = 0; i<4;i++) dice[i] = dices[i];
			int index = -1;
			if(spelare==COLOR.WHITE)
				{
				for(int i=20; i<=25; i++)
					{
					if(spelplan[i].antal == 0 || spelplan[i].color == COLOR.BLACK)
						{
						for(int j = 0; j<4;j++)
							{
								if(dice[j]==26-i) dice[j]--;
							}
						}
					else break;
					}
				for(int i=0;i<dice.Length;i++) if (dice[i]+first == 25) index = i;
				}

			if(spelare==COLOR.BLACK)
				{
				for(int i=5; i>=0; i--)
					{
					if(spelplan[i].antal == 0 || spelplan[i].color == COLOR.WHITE)
						{
						for(int j = 0; j<4;j++)
							{
								if(dice[j]==1+i) dice[j]--;
							}
						}
					else break;
					}
				for(int i=0;i<dice.Length;i++) if (first-dice[i] == 0) index = i;
				}


			

			return index;
		}


		// privat funktion som kollar om man kan flytta brickan.
		// returnar int, index till tärning om det går annars -1.
		private int legitMove(triangel[] spelplan, int first, int second, int[] dices, COLOR spelare)
		{
			int langd;
			int indextarning = -1;

			if(spelare==COLOR.BLACK) 
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
				else if(spelare == COLOR.WHITE) first = 6;
				else first = 19;
				second = correctPos(second);

				if (spelplan[first].color != spelare || spelplan[first].antal == 0) return -1;
				if (spelplan[second].antal <= 1 || spelplan[second].color == spelare) 
				{
					return indextarning;
				}
				



			return -1;

		}



		// Funktion som rättar vald plats till elementets plats i arrayen.
		// returnar int, arrayets index för inskickad triangel position.
		public int correctPos(int spelplanPos)
		{
			if (spelplanPos > 0 && spelplanPos <= 6) return spelplanPos-1;
			if (spelplanPos > 6 && spelplanPos <= 18) return spelplanPos;
			if (spelplanPos > 18 && spelplanPos <= 24) return spelplanPos+1;
			if(spelplanPos == 25) return 6;
			if(spelplanPos == 26) return 19;
			else return spelplanPos;
		}


		//SelfTest
		public static bool SelfTest()
		{
			bool ok = true;
			BackgammonModel test = new BackgammonModel();


			// Test för Triangel Struct
			//
			triangel [] test1 = new triangel [4];
			test1[0].antal = 3;
			test1[0].color = COLOR.BLACK;
			test1[1].antal = 1;
			test1[1].color = COLOR.WHITE;

			ok = ok && test1[0].antal == 3 && test1[0].color == COLOR.BLACK;
			ok = ok && test1[1].antal == 1 && test1[1].color == COLOR.WHITE;
			ok = ok && test1[2].antal == 0 && test1[2].color == COLOR.WHITE;
			ok = ok && test1[3].antal == 0 && test1[3].color == COLOR.WHITE;

			ok = ok && (int)COLOR.BLACK == 1;
			ok = ok && (int)COLOR.WHITE == 0;

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
			triangel[] testspelplan = new triangel[5];

			for(int i = 0; i<5;i++) testspelplan[i].antal = 1;

			testspelplan = test.newGame();

			for(int i = 0; i<5;i++) ok = ok && testspelplan[i].antal != 1;
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
            ok = ok && test.legitMove(testspelplan, 13, 11, dices1, COLOR.BLACK) == 0; 
            ok = ok && test.legitMove(testspelplan, 20, 19, dices1, COLOR.BLACK) == -1; 
            ok = ok && test.legitMove(testspelplan, 21, 19, dices1, COLOR.BLACK) == -1; 
            ok = ok && test.legitMove(testspelplan, 19, 21, dices1, COLOR.BLACK) == -1; 
            ok = ok && test.legitMove(testspelplan, 13, 12, dices1, COLOR.BLACK) == -1; 
			dices1[0] = 4;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, COLOR.BLACK) == 0;
 			dices1[1] = 4;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, COLOR.BLACK) == 1; 
			dices1[2] = 4;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, COLOR.BLACK) == 2; 
			dices1[3] = 4;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, COLOR.BLACK) == 3;
			dices1[0] = 0;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, COLOR.BLACK) == 3;
			dices1[1] = 0;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, COLOR.BLACK) == 3; 
			dices1[2] = 0;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, COLOR.BLACK) == 3; 
			dices1[3] = 0;
			ok = ok && test.legitMove(testspelplan, 24, 20, dices1, COLOR.BLACK) == -1;
			//spela från bar
			testspelplan[19].antal = 1;

			for(int i = 0; i<4;i++) dices1[i] = i+1;

			ok = ok && test.legitMove(testspelplan, -1, 1, dices1, COLOR.BLACK) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 24, dices1, COLOR.BLACK) == 0;
			ok = ok && test.legitMove(testspelplan, -1, 23, dices1, COLOR.BLACK) == 1;
			ok = ok && test.legitMove(testspelplan, -1, 22, dices1, COLOR.BLACK) == 2;
			ok = ok && test.legitMove(testspelplan, -1, 21, dices1, COLOR.BLACK) == 3;
			testspelplan[19].antal = 0;
			ok = ok && test.legitMove(testspelplan, -1, 24, dices1, COLOR.BLACK) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 23, dices1, COLOR.BLACK) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 22, dices1, COLOR.BLACK) == -1;
			ok = ok && test.legitMove(testspelplan, -1, 21, dices1, COLOR.BLACK) == -1;
			testspelplan[19].antal = 1;

			for(int i = 25; i>21;i--) 
			{
				testspelplan[i].antal = 2;
				testspelplan[i].color = COLOR.WHITE;
				ok = ok && test.legitMove(testspelplan, -1, i-1, dices1, COLOR.BLACK) == -1;
			}

	


			//Spelare White
			dices1[0] = 2;
			dices1[1] = 1;
			dices1[2] = 0;
			dices1[3] = 0;
            ok = ok && test.legitMove(testspelplan, 1, 3, dices1, COLOR.WHITE) == 0;  
            ok = ok && test.legitMove(testspelplan, 8, 10, dices1, COLOR.WHITE) == -1; 
            ok = ok && test.legitMove(testspelplan, 22, 20, dices1, COLOR.WHITE) == -1;
            ok = ok && test.legitMove(testspelplan, 12, 10, dices1, COLOR.WHITE) == -1;
            ok = ok && test.legitMove(testspelplan, 17, 20, dices1, COLOR.WHITE) == -1;
			dices1[0] = 4;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, COLOR.WHITE) == 0; 
			dices1[1] = 4;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, COLOR.WHITE) == 1;
			dices1[2] = 4;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, COLOR.WHITE) == 2;
			dices1[3] = 4;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, COLOR.WHITE) == 3;
			dices1[0] = 0;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, COLOR.WHITE) == 3; 
			dices1[1] = 0;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, COLOR.WHITE) == 3;
			dices1[2] = 0;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, COLOR.WHITE) == 3;
			dices1[3] = 0;
			ok = ok && test.legitMove(testspelplan, 1, 5, dices1, COLOR.WHITE) == -1;


            //spela från bar
			testspelplan[6].antal = 1;

			for(int i = 0; i<4;i++) dices1[i] = i+1;

			ok = ok && test.legitMove(testspelplan, -1, 24, dices1, COLOR.WHITE) == -1;

			for (int i = 0; i < 4; i++)
			{
				ok = ok && test.legitMove(testspelplan, -1, i + 1, dices1, COLOR.WHITE) == i;
			}

			testspelplan[6].antal = 0;

			for (int i = 0; i < 4; i++)
			{
				ok = ok && test.legitMove(testspelplan, -1, i + 1, dices1, COLOR.WHITE) == -1;
			}

			testspelplan[6].antal = 1;

			for (int i = 0; i < 4; i++)
			{
				testspelplan[i].antal = 2;
				testspelplan[i].color = COLOR.BLACK;
				ok = ok && test.legitMove(testspelplan, -1, i + 1, dices1, COLOR.WHITE) == -1;
			}



            System.Diagnostics.Debug.WriteLine("legitMove " + ok);

            //Test för legitMoveGoal()
			
			testspelplan = new triangel[26];
			testspelplan[25].antal = 1;
			testspelplan[25].color = COLOR.WHITE;
			testspelplan[0].antal = 1;
			testspelplan[0].color = COLOR.BLACK;
			for(int i = 0; i<4;i++) dices1[i] = 6;
			ok = ok && test.legitMoveGoal(testspelplan,24,dices1,COLOR.WHITE) == 3;
			ok = ok && test.legitMoveGoal(testspelplan,1,dices1,COLOR.BLACK) == 3;
			ok = ok && test.legitMoveGoal(testspelplan,24,dices1,COLOR.WHITE) == 3;
			ok = ok && test.legitMoveGoal(testspelplan,1,dices1,COLOR.BLACK) == 3;


            System.Diagnostics.Debug.WriteLine("legitMoveGoal " + ok);


            //Test för moveGoal()

			testspelplan = new triangel[26];
			testspelplan[25].antal = 1;
			testspelplan[25].color = COLOR.WHITE;
			testspelplan[0].antal = 1;
			testspelplan[0].color = COLOR.BLACK;
			for(int i = 0; i<4;i++) dices1[i] = 6;
			ok = ok && test.moveGoal(testspelplan,24,dices1,COLOR.WHITE) == true;
			ok = ok && test.moveGoal(testspelplan,1,dices1,COLOR.BLACK) == true;
			ok = ok && test.moveGoal(testspelplan,24,dices1,COLOR.WHITE) == false;
			ok = ok && test.moveGoal(testspelplan,1,dices1,COLOR.BLACK) == false;
            System.Diagnostics.Debug.WriteLine("moveGoal " + ok);

            //Test för move()
			testspelplan = test.newGame();

            //Spelare Black
            dices1[0] = 2;
            dices1[1] = 1;
            dices1[2] = 0;
            dices1[3] = 0;
            ok = ok && test.move(testspelplan, 13, 11, dices1, COLOR.BLACK) == true;
            ok = ok && test.move(testspelplan, 20, 19, dices1, COLOR.BLACK) == false;
            ok = ok && test.move(testspelplan, 21, 19, dices1, COLOR.BLACK) == false;
            ok = ok && test.move(testspelplan, 19, 21, dices1, COLOR.BLACK) == false;
            ok = ok && test.move(testspelplan, 13, 12, dices1, COLOR.BLACK) == false;

            dices1[0] = 4;
            ok = ok && test.move(testspelplan, 24, 20, dices1, COLOR.BLACK) == true;
            dices1[1] = 4;
            ok = ok && test.move(testspelplan, 24, 20, dices1, COLOR.BLACK) == true;
         
            //spela från bar
            testspelplan[19].antal = 1;
            ok = ok && test.move(testspelplan, -1, 1, dices1, COLOR.BLACK) == false;


            //Spelare White
            dices1[0] = 2;
            dices1[1] = 1;
            dices1[2] = 0;
            dices1[3] = 0;
            ok = ok && test.move(testspelplan, 1, 3, dices1, COLOR.WHITE) == true;
            ok = ok && test.move(testspelplan, 8, 10, dices1, COLOR.WHITE) == false;
            ok = ok && test.move(testspelplan, 22, 20, dices1, COLOR.WHITE) == false;
            ok = ok && test.move(testspelplan, 12, 10, dices1, COLOR.WHITE) == false;
            ok = ok && test.move(testspelplan, 17, 20, dices1, COLOR.WHITE) == false;
            dices1[0] = 4;
            ok = ok && test.move(testspelplan, 1, 5, dices1, COLOR.WHITE) == true;
            dices1[1] = 4;
   
            //spela från bar
            testspelplan[6].antal = 1;
            ok = ok && test.move(testspelplan, -1, 21, dices1, COLOR.WHITE) == false;

            System.Diagnostics.Debug.WriteLine("move " + ok);

            //Test för canMove()
            //
            testspelplan = test.newGame();

            //spelare Black
            ok = ok && test.canMove(testspelplan, COLOR.BLACK, dices1) == 1;
            testspelplan[19].antal = 1;
            for (int i = 0; i < 4; i++) dices1[i] = i + 1;
            ok = ok && test.canMove(testspelplan, COLOR.BLACK, dices1) == -1;
        

            //spelare White
            ok = ok && test.canMove(testspelplan, COLOR.WHITE, dices1) == 1;
            testspelplan[6].antal = 1;
            for (int i = 0; i < 4; i++) dices1[i] = i + 1;
            ok = ok && test.canMove(testspelplan, COLOR.BLACK, dices1) == -1;

            System.Diagnostics.Debug.WriteLine("canMove " + ok);
            return ok;
		}


	}
}