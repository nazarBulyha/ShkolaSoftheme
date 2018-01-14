namespace MobileCommunication.Controllers
{
	using System;
	using System.Diagnostics;
	using System.IO;
	using System.Runtime.Serialization.Formatters.Binary;
	using System.Xml.Serialization;

	using Newtonsoft.Json;

	using ProtoBuf;

	public class SerializerDeserializer
	{
		public readonly string FolderPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\CallLogs\";

		public readonly string XmlFileName = @"Operator.xml";

		public readonly string JsonFileName = @"Operator.json";

		public readonly string BinaryFileName = @"Operator.txt";

		public readonly string ProtobufFileName = @"Operator.proto";

		private Stopwatch stopWatch;

		private TimeSpan ts;

		#region XMLSerializer

		public void SerializeXmlTime<TItem>(TItem myItem)
		{
			if (myItem == null)
			{
				return;
			}

			Directory.CreateDirectory(Path.GetDirectoryName(FolderPath + XmlFileName) ?? throw new InvalidOperationException());

			stopWatch = new Stopwatch();
			stopWatch.Start();

			using (var fileStream = new FileStream(FolderPath + XmlFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				var serializer = new XmlSerializer(typeof(TItem));

				serializer.Serialize(fileStream, myItem);
			}

			stopWatch.Stop();
			ts = stopWatch.Elapsed;

			var elapsedTime = $"{ts.Seconds:00}.{ts.Milliseconds / 10:00}.{ts.Milliseconds / 100:00}.{ts.Milliseconds / 1000:00}.{ts.Milliseconds / 10000:00}";

			Console.WriteLine($"ml serialize: {elapsedTime}");
			Console.WriteLine();
		}

		public TItem DeserializeXmlTime<TItem>()
		{
			if (!File.Exists(FolderPath + XmlFileName))
			{
				var item = (TItem)Activator.CreateInstance(typeof(TItem));

				Console.WriteLine("Deserialize xml = 0. Empty instance of operator was created.");

				return item;
			}

			TItem deserializeResult;

			stopWatch = new Stopwatch();
			stopWatch.Start();

			using (var fileStream = new FileStream(FolderPath + XmlFileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
			{
				var serializer = new XmlSerializer(typeof(TItem));

				deserializeResult = (TItem)serializer.Deserialize(fileStream);
			}

			stopWatch.Stop();
			ts = stopWatch.Elapsed;

			var elapsedTime = $"{ts.Seconds:00}.{ts.Milliseconds / 10:00}.{ts.Milliseconds / 100:00}.{ts.Milliseconds / 1000:00}.{ts.Milliseconds / 10000:00}";

			Console.WriteLine($"Xml deserialize: {elapsedTime}");

			return deserializeResult;
		}

		public void SerializeXml<TItem>(TItem myItem)
		{
			if (myItem == null)
			{
				return;
			}

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
			if (myItem == null)
			{
				return;
			}

			Directory.CreateDirectory(Path.GetDirectoryName(FolderPath + JsonFileName) ?? throw new InvalidOperationException());

			stopWatch = new Stopwatch();
			stopWatch.Start();

			using (var file = File.CreateText(FolderPath + JsonFileName))
			{
				var serializer = new JsonSerializer { Formatting = Formatting.Indented };

				serializer.Serialize(file, myItem);
			}

			stopWatch.Stop();
			ts = stopWatch.Elapsed;

			var elapsedTime = $"{ts.Seconds:00}.{ts.Milliseconds / 10:00}.{ts.Milliseconds / 100:00}.{ts.Milliseconds / 1000:00}.{ts.Milliseconds / 10000:00}";

			Console.WriteLine($"Json serialize: {elapsedTime}");
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

			TItem deserializeResult;

			stopWatch = new Stopwatch();
			stopWatch.Start();

			using (var file = File.OpenText(FolderPath + JsonFileName))
			{
				var serializer = new JsonSerializer();

				deserializeResult = (TItem)serializer.Deserialize(file, typeof(TItem));
			}

			stopWatch.Stop();
			ts = stopWatch.Elapsed;

			var elapsedTime = $"{ts.Seconds:00}.{ts.Milliseconds / 10:00}.{ts.Milliseconds / 100:00}.{ts.Milliseconds / 1000:00}.{ts.Milliseconds / 10000:00}";

			Console.WriteLine($"Json deserialize: {elapsedTime}");

			return deserializeResult;
		}

		public void SerializeJson<TItem>(TItem myItem)
		{
			if (myItem == null)
			{
				return;
			}

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

		#region BinarySerializer

		public void SerializeBinaryTime<TItem>(TItem myItem)
		{
			if (myItem == null)
			{
				return;
			}

			Directory.CreateDirectory(Path.GetDirectoryName(FolderPath + BinaryFileName) ?? throw new InvalidOperationException());

			stopWatch = new Stopwatch();
			stopWatch.Start();

			using (var stream = new FileStream(FolderPath + BinaryFileName, FileMode.OpenOrCreate))
			{
				var formatter = new BinaryFormatter();

				formatter.Serialize(stream, myItem);
			}

			stopWatch.Stop();
			ts = stopWatch.Elapsed;

			var elapsedTime = $"{ts.Seconds:00}.{ts.Milliseconds / 10:00}.{ts.Milliseconds / 100:00}.{ts.Milliseconds / 1000:00}.{ts.Milliseconds / 10000:00}";

			Console.WriteLine($"Binary serialize: {elapsedTime}");
			Console.WriteLine();
		}

		public TItem DeserializeBinaryTime<TItem>()
		{
			if (!File.Exists(FolderPath + BinaryFileName))
			{
				Console.WriteLine("Deserialize binary = 0. Empty instance of operator was created.");

				return (TItem)Activator.CreateInstance(typeof(TItem));
			}

			TItem deserializeResult;

			stopWatch = new Stopwatch();
			stopWatch.Start();

			using (var stream = new FileStream(FolderPath + BinaryFileName, FileMode.Open))
			{
				var formatter = new BinaryFormatter();

				deserializeResult = (TItem)formatter.Deserialize(stream);
			}

			stopWatch.Stop();
			ts = stopWatch.Elapsed;

			var elapsedTime = $"{ts.Seconds:00}.{ts.Milliseconds / 10:00}.{ts.Milliseconds / 100:00}.{ts.Milliseconds / 1000:00}.{ts.Milliseconds / 10000:00}";

			Console.WriteLine($"Binary deserialize: {elapsedTime}");

			return deserializeResult;
		}

		public void SerializeBinary<TItem>(TItem myItem)
		{
			if (myItem == null)
			{
				return;
			}

			Directory.CreateDirectory(Path.GetDirectoryName(FolderPath + BinaryFileName) ?? throw new InvalidOperationException());

			using (var stream = new FileStream(FolderPath + BinaryFileName, FileMode.OpenOrCreate))
			{
				var formatter = new BinaryFormatter();

				formatter.Serialize(stream, myItem);
			}
		}

		public TItem DeserializeBinary<TItem>()
		{
			if (!File.Exists(FolderPath + BinaryFileName))
			{
				Console.WriteLine("Deserialize binary = 0. Empty instance of operator was created.");

				return (TItem)Activator.CreateInstance(typeof(TItem));
			}

			using (var stream = new FileStream(FolderPath + BinaryFileName, FileMode.OpenOrCreate))
			{
				var formatter = new BinaryFormatter();

				return (TItem)formatter.Deserialize(stream);
			}
		}

		#endregion

		#region Protobuf

		public void SerializeGpbTime<TItem>(TItem myItem)
		{
			if (myItem == null)
			{
				return;
			}

			Directory.CreateDirectory(Path.GetDirectoryName(FolderPath + ProtobufFileName) ?? throw new InvalidOperationException());

			stopWatch = new Stopwatch();
			stopWatch.Start();

			using (var stream = new FileStream(FolderPath + ProtobufFileName, FileMode.OpenOrCreate))
			{
				Serializer.Serialize(stream, myItem);
			}

			stopWatch.Stop();
			ts = stopWatch.Elapsed;

			var elapsedTime = $"{ts.Seconds:00}.{ts.Milliseconds / 10:00}.{ts.Milliseconds / 100:00}.{ts.Milliseconds / 1000:00}.{ts.Milliseconds / 10000:00}";

			Console.WriteLine($"Protobuf serialize: {elapsedTime}");
			Console.WriteLine();
		}

		public TItem DeserializeGpbTime<TItem>()
		{
			if (!File.Exists(FolderPath + ProtobufFileName))
			{
				Console.WriteLine("Deserialize protobuf = 0. Empty instance of operator was created.");

				return (TItem)Activator.CreateInstance(typeof(TItem));
			}

			TItem deserializeResult;

			stopWatch = new Stopwatch();
			stopWatch.Start();

			using (var stream = new FileStream(FolderPath + ProtobufFileName, FileMode.OpenOrCreate))
			{
				deserializeResult = Serializer.Deserialize<TItem>(stream);
			}

			stopWatch.Stop();
			ts = stopWatch.Elapsed;

			var elapsedTime = $"{ts.Seconds:00}.{ts.Milliseconds / 10:00}.{ts.Milliseconds / 100:00}.{ts.Milliseconds / 1000:00}.{ts.Milliseconds / 10000:00}";

			Console.WriteLine($"Protobuf deserialize: {elapsedTime}");

			return deserializeResult;
		}

		public void SerializeGpb<TItem>(TItem myItem)
		{
			if (myItem == null)
			{
				return;
			}

			Directory.CreateDirectory(Path.GetDirectoryName(FolderPath + ProtobufFileName) ?? throw new InvalidOperationException());

			using (var stream = new FileStream(FolderPath + ProtobufFileName, FileMode.OpenOrCreate))
			{
				Serializer.Serialize(stream, myItem);
			}
		}

		public TItem DeserializeGpb<TItem>()
		{
			if (!File.Exists(FolderPath + ProtobufFileName))
			{
				Console.WriteLine("Deserialize protobuf = 0. Empty instance of operator was created.");

				return (TItem)Activator.CreateInstance(typeof(TItem));
			}

			using (var stream = new FileStream(FolderPath + ProtobufFileName, FileMode.OpenOrCreate))
			{
				return Serializer.Deserialize<TItem>(stream);
			}
		}

		#endregion
	}
}