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
		private int [] dice = new int[4];
	
		public int[] letsRollTheDice()
		{
			dice[2]=dice[3]=0;
			dice[0] = rnd.Next(1, 7);
			dice[1] = rnd.Next(1, 7);
			if(dice[0]==dice[1]) dice[3]=dice[2]=dice[1];

			return dice;
		}
			


	}
}
