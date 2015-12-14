using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonKonsol
{
	class BackGammonConsoleView
	{
		public void drawBoard(Triangel[] spelplan)
		{
			if (spelplan.Length != 26) return;
			Console.Clear();
			Console.WriteLine("__________________________________________________________");
			Console.WriteLine("|\t\t\t |\t|\t\t\t |");
			Console.WriteLine("| 13  14  15  16  17  18 |\t|  19  20  21  22  23  24|");
			for (int i = 0; i < 11; i++)
			{
				Console.Write("|");

				if (i < 5)
				{

					for (int j = 13; j < 26; j++)
					{

						if (j == 19)
						{
							Console.Write("|\t| ");
						}
						else if (j == 25)
						{
							if (spelplan[j].antal > i)
							{
								if (spelplan[j].color == Colors.Black) Console.Write(" @ |");
								else Console.Write("  O |");
							}
							else Console.Write("   |");
						}
						else
						{
							if (spelplan[j].antal > i)
							{
								if (spelplan[j].color == Colors.Black) Console.Write(" @  ");
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
							Console.Write("|\t| ");
						}
						else if (j == 12)
						{

							if (spelplan[l].antal > 10 - i)
							{
								if (spelplan[l].color == Colors.Black) Console.Write(" @ |");
								else Console.Write(" O |");
							}
							else Console.Write("   |");
						}
						else
						{
							if (spelplan[l].antal > 10 - i)
							{
								if (spelplan[l].color == Colors.Black) Console.Write(" @  ");
								else Console.Write(" O  ");
							}
							else Console.Write("    ");
						}


					}


				}
				Console.WriteLine("");

			}
			Console.WriteLine("| 12  11  10  9   8   7  |\t|  6   5   4   3   2   1 |");
			Console.WriteLine("|________________________________________________________|");
		}
	}
}
