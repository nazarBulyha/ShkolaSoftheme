namespace MobileCommunication.Controllers
{
	using System;
	using System.Diagnostics;
	using System.IO;
	using System.Xml.Serialization;

	using Newtonsoft.Json;

	public class SerializerDeserializer
	{
		public readonly string FolderPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\CallLogs\";

		public readonly string XmlFileName = @"Operator.xml";

		public readonly string JsonFileName = @"Operator.json";

		private Stopwatch stopWatch;

		private TimeSpan ts;

		#region XMLSerializer

		public void SerializeXmlTime<TItem>(TItem myItem)
		{
			stopWatch = new Stopwatch();
			stopWatch.Start();

			Directory.CreateDirectory(Path.GetDirectoryName(FolderPath + XmlFileName) ?? throw new InvalidOperationException());

			using (var fileStream = new FileStream(FolderPath + XmlFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				var serializer = new XmlSerializer(typeof(TItem));

				serializer.Serialize(fileStream, myItem);
			}

			stopWatch.Stop();
			ts = stopWatch.Elapsed;

			var elapsedTime = $"{ts.Seconds:00}.{ts.Milliseconds / 10:00}.{ts.Milliseconds / 100:00}.{ts.Milliseconds / 1000:00}.{ts.Milliseconds / 10000:00}";

			Console.WriteLine($"RunTime xml serialize: {elapsedTime}");
			Console.WriteLine();
		}

		public TItem DeSerializeJsonTime<TItem>()
		{
			if (!File.Exists(FolderPath + XmlFileName))
			{
				var item = (TItem)Activator.CreateInstance(typeof(TItem));

				Console.WriteLine("Deserialize xml = 0. Empty instance of operator was created.");

				return item;
			}

			stopWatch = new Stopwatch();
			stopWatch.Start();

			using (var fileStream = new FileStream(FolderPath + XmlFileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
			{
				var serializer = new XmlSerializer(typeof(TItem));

				var item = (TItem)serializer.Deserialize(fileStream);

				stopWatch.Stop();
				ts = stopWatch.Elapsed;

				var elapsedTime = $"{ts.Seconds:00}.{ts.Milliseconds / 10:00}.{ts.Milliseconds / 100:00}.{ts.Milliseconds / 1000:00}.{ts.Milliseconds / 10000:00}";

				Console.WriteLine($"RunTime xml deserialize: {elapsedTime}");

				return item;
			}
		}

		public void SerializeXml<TItem>(TItem myItem)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(FolderPath + XmlFileName) ?? throw new InvalidOperationException());

			using (var fileStream = new FileStream(FolderPath + XmlFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				var serializer = new XmlSerializer(typeof(TItem));

				serializer.Serialize(fileStream, myItem);
			}
		}

		public TItem DeserializeXml<TItem>()
		{
			if (!File.Exists(FolderPath + XmlFileName))
			{
				Console.WriteLine("Deserialize xml = 0. Empty instance of operator was created.");

				return (TItem)Activator.CreateInstance(typeof(TItem));
			}

			using (var fileStream = new FileStream(FolderPath + XmlFileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
			{
				var serializer = new XmlSerializer(typeof(TItem));

				return (TItem)serializer.Deserialize(fileStream);
			}
		}

		#endregion

		#region JSONSerializer

		public void SerializeJsonTime<TItem>(TItem myItem)
		{
			stopWatch = new Stopwatch();
			stopWatch.Start();

			Directory.CreateDirectory(Path.GetDirectoryName(FolderPath + JsonFileName) ?? throw new InvalidOperationException());

			using (var file = File.CreateText(FolderPath + JsonFileName))
			{
				var serializer = new JsonSerializer { Formatting = Formatting.Indented };

				serializer.Serialize(file, myItem);
			}

			stopWatch.Stop();
			ts = stopWatch.Elapsed;

			var elapsedTime = $"{ts.Seconds:00}.{ts.Milliseconds / 10:00}.{ts.Milliseconds / 100:00}.{ts.Milliseconds / 1000:00}.{ts.Milliseconds / 10000:00}";

			Console.WriteLine($"RunTime json serialize: {elapsedTime}");
			Console.WriteLine();
		}

		public TItem DeserializeJsonTime<TItem>()
		{
			if (!File.Exists(FolderPath + JsonFileName))
			{
				var item = (TItem)Activator.CreateInstance(typeof(TItem));

				Console.WriteLine("Deserialize json = 0. Empty instance of operator was created.");

				return item;
			}

			stopWatch = new Stopwatch();
			stopWatch.Start();

			using (var file = File.OpenText(FolderPath + JsonFileName))
			{
				var serializer = new JsonSerializer();

				var item = (TItem)serializer.Deserialize(file, typeof(TItem));

				stopWatch.Stop();
				ts = stopWatch.Elapsed;

				var elapsedTime = $"{ts.Seconds:00}.{ts.Milliseconds / 10:00}.{ts.Milliseconds / 100:00}.{ts.Milliseconds / 1000:00}.{ts.Milliseconds / 10000:00}";

				Console.WriteLine($"RunTime json deserialize: {elapsedTime}");

				return item;
			}
		}

		public void SerializeJson<TItem>(TItem myItem)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(FolderPath + JsonFileName) ?? throw new InvalidOperationException());

			using (var file = File.CreateText(FolderPath + JsonFileName))
			{
				var serializer = new JsonSerializer();

				serializer.Serialize(file, myItem);
			}
		}

		public TItem DeserializeJson<TItem>()
		{
			if (!File.Exists(FolderPath + JsonFileName))
			{
				Console.WriteLine("Deserialize json = 0. Empty instance of operator was created.");

				return (TItem)Activator.CreateInstance(typeof(TItem));
			}

			using (var file = File.OpenText(FolderPath + JsonFileName))
			{
				var serializer = new JsonSerializer();

				return (TItem)serializer.Deserialize(file, typeof(TItem));
			}
		}

		#endregion
	}
}