using System;
using System.Collections;
using System.Text;

namespace MyTest2
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			Hashtable openWith = new Hashtable();

			openWith.Add("txt", "notepad.exe");
			openWith.Add("bmp", "paint.exe");
			openWith.Add("dib", "paint.exe");
			openWith.Add("rtf", "wordpad.exe");



			try
			{
				openWith.Add("txt", "winword.exe");
			}
			catch
			{
				Console.WriteLine("An element with Key = \\\"txt\\\" already exists.");
			}

			Console.WriteLine("for key =rtf,value = {0}", openWith["rtf"]);
			openWith["rtf"] = "winword.exe";

			if (!openWith.ContainsKey("ht"))
			{
				openWith.Add("ht","hypertrm.exe");
				Console.WriteLine("Value added for key=ht:{0}",openWith["ht"]);
			}


			foreach (DictionaryEntry de in openWith)
			{
				Console.WriteLine("Key={0},Value={1}", de.Key, de.Value);
			}

			ICollection valueColl = openWith.Values;
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();

			foreach (string s in valueColl)
			{
				Console.WriteLine("Value={0}", s);
			}

			ICollection keyColl = openWith.Keys;
			Console.WriteLine();
			Console.WriteLine();

			foreach (string s in keyColl)
			{
				Console.WriteLine("key={0}",s);
			}

			string strTest = "ILovec#";
			Console.WriteLine((Md5Sum(strTest)));

			MainClass_new new1 = new MainClass_new();
			Console.WriteLine(new1.Add(1,6));

			new1.Name = "abc";





		}

		public static string Md5Sum(string strToEncrypt)
		{
			byte[] bs = UTF8Encoding.UTF8.GetBytes(strToEncrypt);
			System.Security.Cryptography.MD5 md5;
			md5 = System.Security.Cryptography.MD5CryptoServiceProvider.Create();
			byte[] hashBytes = md5.ComputeHash(bs);
			string hashString = "";
			for (int i = 0; i < hashBytes.Length; i++)
			{
				hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
			}
			return hashString.PadLeft(32, '0');
		}
	}
	}




