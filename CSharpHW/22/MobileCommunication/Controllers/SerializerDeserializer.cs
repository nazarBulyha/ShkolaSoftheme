namespace MobileCommunication.Controllers
{
	using System;
	using System.Diagnostics;
	using System.IO;
	using System.Xml.Serialization;

	public class SerializerDeserializer
	{
		public readonly string FolderPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\CallLogs\";
		public readonly string FileName = @"Operator.xml";

		private Stopwatch stopWatch;
		private TimeSpan ts;

		public void SerializeXmlTime<TItem>(TItem myItem)
		{
			stopWatch = new Stopwatch();
			stopWatch.Start();

			Directory.CreateDirectory(Path.GetDirectoryName(FolderPath + FileName) ?? throw new InvalidOperationException());

			var serializer = new XmlSerializer(typeof(TItem));

			using (var fileStream = new FileStream(FolderPath + FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				serializer.Serialize(fileStream, myItem);
			}

			stopWatch.Stop();
			ts = stopWatch.Elapsed;

			var elapsedTime = $"{ts.Seconds:00}.{ts.Milliseconds / 10:00}.{ts.Milliseconds / 100:00}.{ts.Milliseconds / 1000:00}.{ts.Milliseconds / 10000:00}";

			Console.WriteLine($"RunTime serialize: {elapsedTime}");
			Console.WriteLine();
		}

		public TItem DeserializeXmlTime<TItem>()
		{
			if (!File.Exists(FolderPath + FileName))
			{
				var item = (TItem)Activator.CreateInstance(typeof(TItem));

				Console.WriteLine("Deserialize = 0. Empty instance of operator was created.");

				return item;
			}

			stopWatch = new Stopwatch();
			stopWatch.Start();

			using (var fileStream = new FileStream(FolderPath + FileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
			{
				var serializer = new XmlSerializer(typeof(TItem));

				var item = (TItem)serializer.Deserialize(fileStream);

				stopWatch.Stop();
				ts = stopWatch.Elapsed;

				var elapsedTime = $"{ts.Seconds:00}.{ts.Milliseconds / 10:00}.{ts.Milliseconds / 100:00}.{ts.Milliseconds / 1000:00}.{ts.Milliseconds / 10000:00}";

				Console.WriteLine($"RunTime deserialize: {elapsedTime}");

				return item;
			}
		}

		public void SerializeXml<TItem>(TItem myItem)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(FolderPath + FileName) ?? throw new InvalidOperationException());

			var serializer = new XmlSerializer(typeof(TItem));

			using (var fileStream = new FileStream(FolderPath + FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				serializer.Serialize(fileStream, myItem);
			}
		}

		public TItem DeserializeXml<TItem>()
		{
			if (!File.Exists(FolderPath + FileName))
			{
				Console.WriteLine("Deserialize = 0. Empty instance of operator was created.");

				return (TItem)Activator.CreateInstance(typeof(TItem));
			}

			using (var fileStream = new FileStream(FolderPath + FileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
			{
				var serializer = new XmlSerializer(typeof(TItem));

				return (TItem)serializer.Deserialize(fileStream);
			}
		}

	}
}
