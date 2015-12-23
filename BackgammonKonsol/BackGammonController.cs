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
			spelplan[0].antal = 2;
			spelplan[0].color = Colors.White;
			spelplan[5].antal = 5;
			spelplan[5].color = Colors.Black;
			spelplan[6].antal = 3;
			spelplan[6].color = Colors.White;
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
			
			Colors spelare = Colors.White;

			while(_model.checkersInGame(spelplan)[0] != 0 || _model.checkersInGame(spelplan)[1] != 0 )
			{
				_vy.drawBoard(spelplan);
				Console.WriteLine();
				int [] dices = _model.letsRollTheDice();

				while(_model.canMove(spelplan,spelare,dices) != 0)
					{ 
					Console.Write("Spelare ");
					if((int)spelare == 0) Console.Write("O slog ");
					else Console.Write("@ slog ");
					for(int i=0; i<dices.Length;i++) if(dices[i] != 0) Console.Write(dices[i] + " ");
					Console.WriteLine();
					Console.WriteLine();
					int first;
					int second;
						if (_model.canMove(spelplan,spelare,dices) == -1)
							{
							Console.Write("Spela in utslagen bricka till: ");
							first = -10;
							second = Convert.ToInt32(Console.ReadLine());
							}
						else
							{
							Console.Write("Från: ");
							first = Convert.ToInt32(Console.ReadLine());
							Console.Write("Till: ");
							second = Convert.ToInt32(Console.ReadLine());
							}
						if(!_model.move(spelplan, first, second, dices, spelare)) 
							{
							Console.Write("Felakigt move");
							Console.ReadLine();
							}

					_vy.drawBoard(spelplan);
					}

				Console.Write("Nästa spelares turn");
				Console.ReadLine();
				if(spelare == Colors.White) spelare = Colors.Black;
				else spelare = Colors.White;
			}
			
		}
	}
}
