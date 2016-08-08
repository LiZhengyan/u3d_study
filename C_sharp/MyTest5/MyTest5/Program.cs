using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MyTest5
{
	class Program
	{
		static void Main(string[] args)
		{
			string str1 = "apple orange banana";
			Console.WriteLine("str1"+str1);

			string str2 = str1 + " Peach";
			Console.WriteLine("str2:" + str2);



			if (String.Compare(str1, str2) == 0)
			{
				Console.WriteLine("str1 == str2");
			}
			else {
				Console.WriteLine("str1 != str2");
			}

			int n = str1.IndexOf(' ', 0);
			Console.WriteLine("str1 第一个空格在第{0}个字节",n);

			str2 = str1.Remove(n);
			Console.WriteLine("删除str1的第一个空格以后的文字："+str2);


			str2 = str1.Replace(' ', '_');
			Console.WriteLine("替换空格以后：" + str2);

		}
	}
}
