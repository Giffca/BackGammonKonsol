using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonKonsol
{
	class BackGammonController
	{
		private BackGammonModel _model;
		private BackGammonConsoleView _vy;


		//spelar spelet
		//30%  inte så viktigt, ska ändå inte exporteras. men bra för att testa.
		public void PlayGame()
		{
		Console.ForegroundColor = ConsoleColor.White;
		_model = new BackGammonModel();
		_vy = new BackGammonConsoleView();

		Triangel [] spelplan = new Triangel[26];  // 26 för att två platser används för utslagna brickor.
			int player1checkers = 15;
			int player2checkers = 15;
			
			_model.newGame(spelplan);
			spelplan[19].antal = 3;

			Colors spelare = Colors.Black;

			while(true)
			{
				_vy.drawBoard(spelplan);
				Console.WriteLine();
				int [] dices = _model.letsRollTheDice();
				int status = _model.canMove(spelplan,spelare,dices);

				while(status != 0 && player1checkers != 0)
					{
					Console.Write("Spelare ");
					if((int)spelare == 0) Console.Write("O slog ");
					else Console.Write("@ slog ");
					for(int i=0; i<dices.Length;i++) if(dices[i] != 0) Console.Write(dices[i] + " ");
					Console.WriteLine();
					Console.WriteLine();
					int first;
					int second;
						if (status == -1)
							{
							first = -1;
							Console.Write("Spela in utslagen bricka till: ");
							second = Convert.ToInt32(Console.ReadLine());
							}
						else
							{
							Console.Write("Från: ");
							first = Convert.ToInt32(Console.ReadLine());
							Console.Write("Till: ");
							second = Convert.ToInt32(Console.ReadLine());
							}


						if((second == 0 || second == 25) && status == 2)
							{
							if(_model.moveGoal(spelplan,first,dices,spelare))
								{
 								if(spelare == Colors.White) player1checkers--;
								else player2checkers--;
								}

							}

						else if(!_model.move(spelplan, first, second, dices, spelare)) 
							{
							Console.Write("Felakigt move");
							Console.ReadLine();
							}

					_vy.drawBoard(spelplan);
					status = _model.canMove(spelplan,spelare,dices);
					}

				if(player1checkers == 0)
						{
						Console.WriteLine("Spelare 1 vinner.");
						break;
						}
				Console.Write("Nästa spelares turn");
				Console.ReadLine();
				if(spelare == Colors.White) spelare = Colors.Black;
				else spelare = Colors.White;
			}
			Console.ReadLine();
			
		}
	}
}
