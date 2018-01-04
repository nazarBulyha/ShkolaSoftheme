using System;

namespace MobileCommunication.Extensions
{
	using System.IO;
	using System.Xml.Serialization;

	public static class SerializerDesserializer
	{
		public static readonly string FolderPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\CallLogs\";
		public static readonly string ItemName = @"Operator.txt";

		public static void Serialize<TItem>(TItem myItem)
		{
			var serializer = new XmlSerializer(typeof(TItem));

			using (var fileStream = new FileStream(FolderPath + ItemName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				serializer.Serialize(fileStream, myItem);
			}
		}

		public static object Deserialize<TItem>()
		{
			if (!File.Exists(FolderPath + ItemName))
			{
				return Activator.CreateInstance(typeof(TItem));
			}

			using (var fileStream = new FileStream(FolderPath + ItemName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
			{
				var serializer = new XmlSerializer(typeof(TItem));

				return (TItem)serializer.Deserialize(fileStream);
			}
		}
	}
}
