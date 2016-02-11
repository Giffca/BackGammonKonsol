using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
	class BackgammonController
	{
		private BackgammonModel _model;
		private BackgammonConsoleView _vy;


		//spelar spelet
		//30%  inte så viktigt, ska ändå inte exporteras. men bra för att testa.
		public void PlayGame()
		{
		Console.ForegroundColor = ConsoleColor.White;
		_model = new BackgammonModel();
		_vy = new BackgammonConsoleView();

		    triangel [] spelplan = _model.newGame();
			int player1checkers = 15;
			int player2checkers = 15;

			player spelare = player.one;

			while(true)
			{
                _vy.drawBoard(spelplan);           
				Console.WriteLine();
				int [] dices = _model.letsRollTheDice();
				int status = _model.canMove(spelplan,spelare,dices);

				while(status != 0 && player1checkers != 0 && player2checkers != 0)
					{
					Console.Write("Spelare ");
					if((int)spelare == 0) Console.Write("O slog ");
					else Console.Write("@ slog ");
					for(int i=0; i<dices.Length;i++) if(dices[i] != 0) Console.Write(dices[i] + " ");
					Console.WriteLine();
					Console.WriteLine();
					int first = 0;
					int second = -1;
						if (status == -1)
							{
							first = -1;
							Console.Write("Spela in utslagen bricka till: ");
							while(!(second > 0))try {second = Convert.ToInt32(Console.ReadLine());} catch{}
							}
						else
							{
							Console.Write("Från: ");
							while(!(first > 0))try {first = Convert.ToInt32(Console.ReadLine());} catch{}
							
							Console.Write("Till: ");
							while(!(second > -1))try {second = Convert.ToInt32(Console.ReadLine());} catch{}
							}


						if((second == 0 || second == 25) && status == 2)
							{
							if(_model.moveGoal(spelplan,first,dices,spelare))
								{
 								if(spelare == player.one) player1checkers--;
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
				if(player2checkers == 0)
						{
						Console.WriteLine("Spelare 2 vinner.");
						break;
						}

				Console.Write("Nästa spelares turn");
				Console.ReadLine();
				if(spelare == player.one) spelare = player.two;
				else spelare = player.one;
			}
			Console.ReadLine();
			
		}
	}
}
