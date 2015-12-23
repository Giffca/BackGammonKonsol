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


		// kollar om någon vunnit genom att se om något element är lika med 0, controllern kollar detta.
		// 100%
		public int[] checkersInGame(Triangel[] spelplan)
		{
			int[] brickor = {0,0};
            brickor[0] = 18-spelplan[0].antal;
            brickor[1] = 18-spelplan[spelplan.Length - 1].antal;

			return brickor;
		}


		// 75% gå i mål och kod för spelare 2 saknas.
<<<<<<< HEAD
        public int canMove(Triangel[] spelplan, Colors spelare, int[] dices)
        {
            bool hittadBricka = false;
            if (spelare == Colors.White)
            {
                if (spelplan[6].antal > 0)
                {
                    foreach (int n in dices)
                    {
                        if (n != 0)
                        {
                            if (spelplan[-1 + n].antal <= 1 || spelplan[-1 + n].color == spelare) return -1;
                        }
                    }
                    return 0;
                }


                for (int i = 0; i < 24; i++)
                {
                    int pos = correctPos(i);
                    if (i < 19)
                      {
                        if (spelplan[pos].color == spelare && spelplan[pos].antal > 0)
                        {
                            hittadBricka = true;
                            foreach (int n in dices)
                            {
                                if (i + n <= 24)
                                {
                                    if (legitMove(spelplan, pos, pos + n, dices, spelare) != -1) return 1;
                                }
                            }
                        }
                    }
                    else if (!hittadBricka)
                        {
                            foreach (int n in dices)
                            {
                                if (i + n > 24)
                                {
                                    return 2;
                                }
                                else
                                {
                                    if (legitMove(spelplan, pos, pos + n, dices, spelare) != -1) return 1;
                                }
                            }
                        }
                    

                }
            }
            //else
            //	{
            //		for (int i = 25; i > 0; i--)
            //		{	
            //			int pos = correctPos(i);

            //			if(spelplan[pos].color == spelare && spelplan[pos].antal > 0)
            //			{

            //			}
            //		}
            //	}
            return 0;
        }
           
        
          
=======
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

					for (int i = 1; i < 24; i++)
					{
						int pos = correctPos(i);
						if(spelplan[pos].color == spelare && spelplan[pos].antal > 0)
						{
							if(i < 19) canGoal = false;

							foreach (int n in dices) 
							{
								if(i+n <= 24)
									{ 
									if(legitMove(spelplan,pos,pos+n,dices,spelare) != -1) return 1;
									}
							}
						}
					}
				}
			//else
			//	{
			//		for (int i = 25; i > 0; i--)
			//		{	
			//			int pos = correctPos(i);
			//			if(spelplan[pos].color == spelare && spelplan[pos].antal > 0)
			//			{
							
			//			}
			//		}
			//	}
			return 0;
		}
>>>>>>> origin/master


		//flyttar en bricka.
		// 75%  gå i mål
		public bool move(Triangel[] spelplan, int first, int second, int[] dices,Colors spelare)
		{
			int index = legitMove(spelplan,first,second, dices, spelare);
			if(index != -1)
			{
               
                    if (first != -1)
                    {
                        first = correctPos(first); 
                    }
                    else if (spelare == Colors.White)
                    {
                        first = 6;
                    }
                    else
                    {
                        first = 19;
                    }

                    second = correctPos(second);
                    dices[index] = 0;

                    spelplan[first].antal--;
                    if (spelplan[second].antal == 1 && spelplan[second].color != spelare)
                    {
                        if (spelplan[second].color == Colors.White) spelplan[6].antal++;
                        else spelplan[19].antal++;

                        spelplan[second].color = spelare;
                        return true;
                    }

                    spelplan[second].color = spelare;
                    spelplan[second].antal++;
                    return true;
                
			}

			return false;
		}


		//privat funktion som kollar om man kan flytta brickan.
		// 75%  gå i mål
		private int legitMove(Triangel[] spelplan, int first, int second, int[] dices, Colors spelare)
		{
			int langd;
			int indextarning = -1;

			if(spelare==Colors.Black) 
			{
                if (first == -1) 
                {
                    langd = 25 - second;
                }
                else
                {
                    if (second >= first) return -1;
                    langd = first - second;
                }
			}
			else 
			{
                if (first == -10) 
                {
                    langd = second; 
                }
                else
                {
                    if (first >= second) return -1;
                    langd = second - first;
                }
				
			}


				for(int i=0;i<dices.Length;i++) if (dices[i]==langd) indextarning = i;
                if (first != -1)
                {
                    first = correctPos(first);
                }
                else if (spelare == Colors.White)
                {
                    first = 6;
                }
                else
                {
                    first = 19;
                }

			
                
				second = correctPos(second);

				if (spelplan[first].color != spelare || spelplan[first].antal == 0) return -1;
				if (spelplan[second].antal <= 1 || spelplan[second].color == spelare) 
				{
					return indextarning;
				}
			return -1;

		}



		//privat funktion som rättar vald plats till elementets plats i arrayen.
		// 75% gå i mål saknas.
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


            Triangel[] test2 = new Triangel[28];
            test2[0].antal = 3;
            test2[0].color = Colors.Black;
            test2[1].antal = 1;
            test2[1].color = Colors.Black;
            test2[2].antal = 1;
            test2[2].color = Colors.White;
            test2[3].antal = 1;
            test2[3].color = Colors.Black;

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
                if (dices[0] == dices[1]) if (dices[2] != dices[1] || dices[3] != dices[1]) ok = false;
                if (dices.Length != 4) ok = false;
			}
           
			System.Diagnostics.Debug.WriteLine("LetsRollTheDice " + ok);


			// Test för correctPos()
			ok = ok && test.correctPos(0) == 0;
			ok = ok && test.correctPos(5) == 5;
			ok = ok && test.correctPos(7) == 8;
			ok = ok && test.correctPos(18) == 19;
			ok = ok && test.correctPos(19) == 21;
			ok = ok && test.correctPos(24) == 26;

			System.Diagnostics.Debug.WriteLine("correctPos " + ok);
			//return ok;
             int[] dices2 = {3, 2, 0, 0};
            //Test för legitMove
          //   ok = ok && test.legitMove(test2, 1, 3, dices2, Colors.White) == 1;
           //  ok = ok && test.legitMove(test2, 3, 1, dices2, Colors.Black) == 1;
             System.Diagnostics.Debug.WriteLine("legitMove " + ok);

            //Test för move
           
           // ok = ok && test.move(test2, 1, 3, dices2, Colors.White);
             ok = ok && test.move(test2, 3, 1, dices2, Colors.Black);
            System.Diagnostics.Debug.WriteLine("move " + ok);
            return ok;
		}


	}
}
