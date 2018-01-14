namespace MobileCommunication
{
	using System;

	using MobileCommunication.Controllers;

	public class Program
	{
		private static void Main()
		{
			var myOperator = new SerializerDeserializer().DeserializeXml<Operator>();

			var operations = new ProgramOperations();
			var createOperator = false;
			var fillOperator = false;
			var addNewUser = false;

			// Get user answer what to do
			int activity;
			do
			{
				Console.WriteLine("Choose what do you want to execute:");
				Console.WriteLine("1 - Create Operator.");
				Console.WriteLine("2 - Make actions(call/sms).");
				Console.WriteLine("3 - Add new user.");
				Console.WriteLine("4 - Create Operator + Make actions + Add new user.");
				Console.WriteLine();

				int.TryParse(Console.ReadLine(), out activity);
			}
			while (activity != 1 && activity != 2 && activity != 3 && activity != 4);

			switch (activity)
			{
				case 1:
					{
						createOperator = myOperator.ListAccounts.Count <= 0;
						break;
					}
				case 2:
					{
						createOperator = myOperator.ListAccounts.Count <= 0;
						fillOperator = true;
						break;
					}
				case 3:
					{
						createOperator = myOperator.ListAccounts.Count <= 0;
						addNewUser = true;
						break;
					}
				case 4:
					{
						createOperator = myOperator.ListAccounts.Count <= 0;
						fillOperator = true;
						addNewUser = true;
						break;
					}

				// ReSharper disable once RedundantEmptySwitchSection
				default:
					break;
			}

			operations.CreateOperator(createOperator);
			operations.FillOperator(fillOperator);
			operations.AddNewUser(addNewUser, myOperator);

			if (myOperator.ListAccounts.Count != 0)
			{
				UserActivity.GetMostActiveUser(myOperator.Logger.FolderPath + myOperator.Logger.FileName, myOperator.ListAccounts);
			}

			Console.ReadLine();
		}
	}
}