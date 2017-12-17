namespace MobileCommunication.Additional
{
	using System;

	public static class Global
	{
		public static string Path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\CallLogs\";
		public static string StandardLogName = $"CallLogFor {DateTime.Now:dd_MM_yyyy}.txt";
	}
}
