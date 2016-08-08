using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Mytest4
{
	public class Player
	{
		public static int count = 0;
		public Player()
		{
			count++;
		}
		private string p_Name = "";
		public string Pname
		{
			set { p_Name = value;}
			get { return p_Name; }
		}
	}


	class Program
	{
		static void Main(string[] args)
		{
			Player player1 = new Player();
			Console.WriteLine(Player.count);
			Player player2 = new Player();
			Console.WriteLine(Player.count);
			player1.Pname = "Luck";
			player2.Pname = "Li Lei";

			Console.WriteLine(player1.Pname);
			Console.WriteLine(player2.Pname);

			if (String.Compare(player1.Pname, player2.Pname) == 0)
			{
				Console.WriteLine("字符串一致");
			}
			else {
				Console.WriteLine("字符串不一致");
			}
		}
	}
}
