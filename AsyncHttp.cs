using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Test
{
	public class AsyncHttp
	{
		private Thread ThreadClient;
		private WebClient Client;
		private NameValueCollection Args;
		private string Adress;

		public delegate void KoniecDelegate(string result);
		public event KoniecDelegate Finish;

		public delegate void BladDelegate(string error_msg);
		public event BladDelegate Error;

		private void ThreadClient_Main()
		{
			try
			{
				string res = Encoding.UTF8.GetString(Client.UploadValues(Adress, Args));
				Finish(res);
			}
			catch (WebException e)
			{
				Error(e.Message);
			}
		}

		public void Start(string adress,NameValueCollection args)
		{
			Args = args;
			Adress = adress;

			Client = new WebClient();

			ThreadClient = new Thread(new ThreadStart(ThreadClient_Main));
			ThreadClient.IsBackground = true;
			ThreadClient.Start();
		}
	}
}
