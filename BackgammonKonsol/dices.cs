using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonKonsol
{
	class dices
	{
		private Random rnd = new Random();
		private int dice1,dice2,dice3,dice4 = 0;
	
		public String letsRollTheDice()
		{
			dice3=dice4=0;
			dice1 = rnd.Next(1, 7);
			dice2 = rnd.Next(1, 7);
			if(dice1==dice2) dice4=dice3=dice2;

			return (dice1+" "+dice2 +" "+dice3+" "+dice4);
		}
			

	}
}
