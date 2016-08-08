using System;
using System.Security.Cryptography.X509Certificates;
namespace MyTest2
{
	public  class MainClass_new
	{

		private string m_name="";
		public string Name
		{
			set { m_name = value; }
			get { return m_name; }
		}
		private int m_life = 100;
		public int Life
		{
			get { return m_life; }
		}

		public int Add(int a, int b)
		{
			return a + b;
		}

		private void CallIt()
		{
			Console.WriteLine("#######");
		}
	}
}

