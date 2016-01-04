using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonKonsol
{
	class BackGammonConsoleView
	{


		// Skriver ut spelplanen
		// 99%  inte så viktigt, ska ändå inte exporteras.
		public void drawBoard(triangel[] spelplan)
		{
			if (spelplan.Length != 26) return;
			Console.Clear();
			Console.WriteLine("_________________________________________________________");
			Console.WriteLine("|\t\t\t |     |\t\t\t |");
			Console.WriteLine("| 13  14  15  16  17  18 |     |  19  20  21  22  23  24 |");
			for (int i = 0; i < 11; i++)
			{
				if(i != 5) Console.Write("|");
				else 
				{
					Console.WriteLine("|\t\t\t |     |\t\t\t |");
					Console.Write("|");
				}
				

				if (i < 5)
				{

					for (int j = 13; j < 26; j++)
					{

						if (j == 19)
						{
							
							if (spelplan[j].antal > i)
							{
								Console.Write("|  @  | ");
							}
							else Console.Write("|     | ");


						}
						else if (j == 25)
						{
							if (spelplan[j].antal > i)
							{
								if (spelplan[j].color == COLOR.BLACK) Console.Write(" @  |");
								else Console.Write("  O |");
							}
							else Console.Write("    |");
						}
						else
						{
							if (spelplan[j].antal > i)
							{
								if (spelplan[j].color == COLOR.BLACK) Console.Write(" @  ");
								else Console.Write(" O  ");
							}
							else Console.Write("    ");
						}


					}

				}
				

				if (i > 4 && i < 11)
				{

					for (int j = 0; j < 13; j++)
					{

						int l = 12 - j;
						if (j == 6)
						{
							if (spelplan[l].antal > 10 - i)
							{
								Console.Write("|  O  | ");
							}
							else Console.Write("|     | ");

							
						}
						else if (j == 12)
						{

							if (spelplan[l].antal > 10 - i)
							{
								if (spelplan[l].color == COLOR.BLACK) Console.Write(" @  |");
								else Console.Write(" O  |");
							}
							else Console.Write("    |");
						}
						else
						{
							if (spelplan[l].antal > 10 - i)
							{
								if (spelplan[l].color == COLOR.BLACK) Console.Write(" @  ");
								else Console.Write(" O  ");
							}
							else Console.Write("    ");
						}


					}


				}
				Console.WriteLine();

			}
			Console.WriteLine("| 12  11  10  9   8   7  |     |  6   5   4   3   2   1  |");
			Console.WriteLine("|________________________________________________________|");
		}
	}
}
