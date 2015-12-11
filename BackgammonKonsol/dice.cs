using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonKonsol
{
	class dice
	{
		private Random rnd = new Random();
		private int [] dices = new int[4];
	
		public int[] letsRollTheDice()
		{
			dices[2]=dices[3]=0;
			dices[0] = rnd.Next(1, 7);
			dices[1] = rnd.Next(1, 7);
			if(dices[0]==dices[1]) dices[3]=dices[2]=dices[1];

			if (dices[3] == 0) return (new int[2]{dices[0],dices[1]});
			return dices;
		}
			


	}
}
