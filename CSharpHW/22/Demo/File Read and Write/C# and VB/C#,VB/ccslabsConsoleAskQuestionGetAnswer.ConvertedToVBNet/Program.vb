Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Collections
Imports System.IO

Class Program
	Private Shared alResults As New ArrayList()
	' Global Array
	Friend Shared Sub Main(args As String())
		Dim Response As String = "y"


		While Response.ToLower() = "y"
			Ask("Enter Student Name : ")
			Ask("Enter Telugu Marks : ")
			Ask("Enter English Marks: ")
			Ask("Enter Maths Marks  : ")
				' Defaults to Y
			Response = Ask("Do you want to enter more results? Y/n", True)
		End While

		' Ok save the results
		SaveResults()
		WaitKey()
		ShowResults()
		WaitKey()

	End Sub

	Private Shared Function Ask(s As String, Optional YesNo As Boolean = False) As String
		If YesNo = True Then
			' We have asked if they want to do more questions
			Console.Write(s)
			Dim res As String = Console.ReadLine()
			If res.ToLower() = "y" OrElse res.Length < 1 Then
				' They said yes - or nothing - defaults to Yes
				Console.Clear()
				' Clear the screen for the next set of questions
				Return "y"
			Else
					' No more enteries
				Return "n"
			End If
		Else
			' Ok we are asking a normal Question
			Console.Write(s)
			alResults.Add(Console.ReadLine())
			'   Console.Write(Environment.NewLine); // Move to next line for next question
				' no need to return a value for this
			Return ""
		End If
	End Function

	Private Shared Sub SaveResults()

		Dim fs As New FileStream("c:\csharppgms\stdinfo.txt", FileMode.Append, FileAccess.Write, FileShare.Inheritable)
		Dim sw As New StreamWriter(fs)

		For Each val As String In alResults
			sw.WriteLine(val)
		Next

		sw.Close()
		fs.Close()
	End Sub

	Private Shared Sub ShowResults()
		Dim Question As Integer = 0
		Dim result As String = ""
		Console.Clear()
		' Clears the screen
		Dim fs As New FileStream("c:\csharppgms\stdinfo.txt", FileMode.Open, FileAccess.Read, FileShare.Inheritable)
		Dim sr As New StreamReader(fs)

		' Get results for the 4 questions and show them
		While Not sr.EndOfStream
			If Question < 4 Then
				result += sr.ReadLine() & "|"
				Question += 1
			Else
				Question = 0
				Console.WriteLine(result.Trim("|"C))
					' result the results
				result = ""
			End If
		End While

		sr.Close()
		fs.Close()
	End Sub

	Private Shared Sub WaitKey()
		Console.WriteLine("Press any key to continue...")
		Console.ReadKey(False)
	End Sub
End Class
