using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
	class Program
	{
		static void Main(string[] args)
		{
			BackgammonController c = new BackgammonController();
			BackgammonModel.SelfTest();
			c.PlayGame();
		}
	}
}
