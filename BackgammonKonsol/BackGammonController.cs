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

			_vy.drawBoard(spelplan);
			
		}
	}
}
