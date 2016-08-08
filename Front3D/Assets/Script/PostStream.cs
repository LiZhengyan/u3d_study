using UnityEngine;
using System.Collections;
using System.Text;

public class PostStream  {
	//保存域头
	public System.Collections.Hashtable Headers=new System.Collections.Hashtable();
	const int HASHSIZE=16;   //末尾16个字节保存md5签名
	const int BYTE_LEN=1;  
	const int SHORT16_LEN=2;
	const int INT32_LEN=4;
	const int FLOAT_LEN=4;

	private int m_index=0;
	public int Length {get {return m_index;}}

	private string m_secretKey="123456"; //数字签名

	//保存Post信息
	private string [,] m_field;
	private const int MAX_POST=128;
	private const int PAIR=2;
	private const int HEAD=0;
	private const int CONTENT=1;
	//收到的字节数组
	private byte[] m_bytes=null;
	public byte[] BYTES { get { return m_bytes;}}

	//发送的字符串
	private string m_content="";
	//读取是否出现错误
	private bool m_errorRead=false;
	//是否进行数子签名
	private bool m_sum=true;

	public PostStream()
	{
		Headers = new System.Collections.Hashtable();
		m_index=0;
		m_bytes=null;
		m_content="";
		m_errorRead=false;
	}

	public void BeginWrite(bool issum)
	{
		m_index =0;
		m_sum=issum;
		m_field=new string[MAX_POST,PAIR];
		Headers.Add("Content-type","application/x-www-form-urlencoded");
	}

	public void Write(string head ,string content)
	{
		if(m_index>=MAX_POST)
			return;
		m_field[m_index,HEAD]=head;
		m_field[m_index,CONTENT]=content;

		m_index++;
		if(m_content.Length==0)
		{
			m_content+= (head + "=" +content);
		}	
		else
		{
			m_content+= ("&" + head +  "=" + content);
		}
	}

	public void EndWrite()
	{
		if(m_sum)
		{
			string hasstring="";
			for(int i =0;i<MAX_POST;i++){
				hasstring+=m_field[i,CONTENT];
			}
			hasstring+=m_secretKey;
			m_content+= "&key="+ Md5Sum(hasstring);
		}
		m_bytes=UTF8Encoding.UTF8.GetBytes(m_content);
	}


	public static string Md5Sum(string strToEncrypt)
	{
		byte[] bs=UTF8Encoding.UTF8.GetBytes(strToEncrypt);
		System.Security.Cryptography.MD5  md5;
		md5 = System.Security.Cryptography.MD5CryptoServiceProvider.Create();
		byte[] hashBytes =md5.ComputeHash(bs);
		string hashString="";
		for(int i =0; i<hashBytes.Length;i++)
		{
			hashString+=System.Convert.ToString(hashBytes[i],16).PadLeft(2,'0');
		}
		return hashString.PadLeft(32,'0');
	}

	public bool BeginRead(WWW www ,bool issum)
	{
		m_bytes=www.bytes;
		m_content=www.text;
		m_sum=issum;

		if(m_bytes == null)
		{
			m_errorRead=true;
			return false;
		}

		short lenght=0;
		
	}

	// 忽律一个字节
	public void IgnoreByte()
	{
		if(m_errorRead) return;
		m_index+=BYTE_LEN;
	}

	// 读取一个字节

	public void ReadByte(ref byte bts)
	{
		if(m_errorRead)
			return;

		bts=m_bytes[m_index];
		m_index+=BYTE_LEN;
	}

	public void ReadShort(ref short number)
	{
		if(m_errorRead) return;
		number = System.BitConverter.ToInt16(m_bytes,m_index);
		m_index+=SHORT16_LEN;
	}

	public void ReadInt(ref int number)
	{
		if(m_errorRead) return;
		number=System.BitConverter.ToInt32(m_bytes,m_index);
		m_index+=INT32_LEN;
	}
	
	public void ReadFloat(ref float number)
	{
		if(m_errorRead) return;
		number=System.BitConverter.ToSingle(m_bytes,m_index);
		m_index+=FLOAT_LEN;
	}
	public void ReadString(ref string str)
	{
		if(m_errorRead) return;
		short num=0;
		ReadShort(ref num);
		str=Encoding.UTF8.GetString(m_bytes,m_index,(int)num);
		m_index+=num;
	}

	public void ReadBytes(ref byte[] bts)
	{
		if(m_errorRead)
			return;

		bts=m_bytes[m_index];
		m_index+=BYTE_LEN;
	}

}
