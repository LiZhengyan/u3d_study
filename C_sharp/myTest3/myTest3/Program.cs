using System;
using System.Collections.Generic;
using System.Text;

namespace myTest3
{
	//public class Enemy
	//{
	//	public Enemy()
	//	{
	//		Console.WriteLine("Enemy Contructor");
	//	}
	//	public void updateAI()
	//	{
	//		Console.WriteLine("update enemy Ai");
	//	}
	//}

	//public class Boss : Enemy
	//{
	//	public Boss()
	//	{
	//		Console.WriteLine("Boss Contructor");
	//	}
	//}



	//class Program
	//{
	//	static void Main(string[] args)
	//	{
	//		Boss boss = new Boss();
	//		boss.updateAI();

	//	}
	//}



	public class Enemy
	{
		public int num = 5;
		public Enemy()
		{
			Console.WriteLine("enemy contructor!");
		}

		public virtual void UpdateAI()
		{
			Console.WriteLine("update enemy ai");
		}
	}

	public class Boss : Enemy
	{
		public Boss()
		{
			Console.WriteLine("boss contructor");
		}
		public override void UpdateAI()
		{
			Console.WriteLine("update boss ai");
		}
	}



	class Program
	{
		static void Main(string[] args)
		{
			Enemy[] enemies = new Enemy[2];
			enemies[0] = new Enemy();
			enemies[1] = new Boss();

			enemies[0].UpdateAI();
			enemies[1].UpdateAI();

			enemies[0].num = 10;

		}
	}
}
