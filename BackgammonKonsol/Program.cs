using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonKonsol
{
	class Program
	{
		static void Main(string[] args)
		{
			BackGammonController c = new BackGammonController();
			BackGammonModel.SelfTest();
			c.PlayGame();
		}
	}
}
