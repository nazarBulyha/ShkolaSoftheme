Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.IO
Imports System.Runtime.InteropServices

Public Partial Class Form1
	Inherits Form
	#Region "API Stuff"
	<DllImport("kernel32.dll", CharSet := CharSet.Unicode, SetLastError := True)> _
	Public Shared Function FindFirstFileW(lpFileName As String, ByRef lpFindFileData As WIN32_FIND_DATAW) As IntPtr
	End Function

	<DllImport("kernel32.dll", CharSet := CharSet.Unicode)> _
	Public Shared Function FindNextFile(hFindFile As IntPtr, ByRef lpFindFileData As WIN32_FIND_DATAW) As Boolean
	End Function

	<DllImport("kernel32.dll")> _
	Public Shared Function FindClose(hFindFile As IntPtr) As Boolean
	End Function

	<StructLayout(LayoutKind.Sequential, CharSet := CharSet.Unicode)> _
	Public Structure WIN32_FIND_DATAW
		Public dwFileAttributes As FileAttributes
		Friend ftCreationTime As System.Runtime.InteropServices.ComTypes.FILETIME
		Friend ftLastAccessTime As System.Runtime.InteropServices.ComTypes.FILETIME
		Friend ftLastWriteTime As System.Runtime.InteropServices.ComTypes.FILETIME
		Public nFileSizeHigh As Integer
		Public nFileSizeLow As Integer
		Public dwReserved0 As Integer
		Public dwReserved1 As Integer
		<MarshalAs(UnmanagedType.ByValTStr, SizeConst := 260)> _
		Public cFileName As String
		<MarshalAs(UnmanagedType.ByValTStr, SizeConst := 14)> _
		Public cAlternateFileName As String
	End Structure
	#End Region

	Private KeepRunning As Boolean = True
	Private HDSpaceUsed As Double = 0
	Private FileSizeTotal As Double = 0

	Public Sub New()
		InitializeComponent()
		Dim txt As String() = {"Remaining", "Done"}
		pccDiskDriveControl.Texts = txt
		pccDiskDriveControl.ShadowStyle = System.Drawing.PieChart.ShadowStyle.GradualShadow
		Dim cols As Color() = {Color.Red, Color.Green}
		pccDiskDriveControl.Colors = cols
		pccDiskDriveControl.InitialAngle = 45

		pccDiskDriveControl.SliceRelativeHeight = 0.2F
	End Sub



	Private Sub btnStartStop_Click(sender As Object, e As EventArgs)
		If btnStartStop.Text = "Start" Then
			KeepRunning = True
			FileSizeTotal = 0
			btnStartStop.Text = "Stop"
			Start()
		Else
			' must be stop then :P
			btnStartStop.Text = "Start"
			KeepRunning = False
		End If
	End Sub

	' Run the scan and update the progress
	Private Sub Start()
		' We can either collect all the files and then process which would be easier as we would know in advance how many files we had to process.
		' However, for this we will show the progress before knowing how many files we are going to process.
		' We will of course still need to know the total amount to do so we can calculate 100%. The easiest and quickest total we can get is the amount of
		' Hard drive space that has been used by the files. This is our 100%, and as each file is discovered we will get its size and that will
		' be the percentage done. Which will be shown in the graphical progress.

		Dim allDrives As String() = Environment.GetLogicalDrives()
		Dim dinfo As New DriveInfo(allDrives(1))
		' 0 = A, 1 = C
		HDSpaceUsed = (dinfo.TotalSize - dinfo.TotalFreeSpace)
		Dim values As Decimal() = {CDec(HDSpaceUsed), 0}
		' HDSpaceUSed = 100%

		' Setup the Graphics
		DoChart(values)

		' Run the Scan
		Scan(allDrives(1).ToString())


	End Sub

	Private Sub Scan(directory As String)
		If KeepRunning Then
			Dim INVALID_HANDLE_VALUE As New IntPtr(-1)
			Dim findData As WIN32_FIND_DATAW
			Dim findHandle As IntPtr = INVALID_HANDLE_VALUE


			Try
				findHandle = FindFirstFileW(directory & "\*", findData)
				If findHandle <> INVALID_HANDLE_VALUE Then

					Do
						Application.DoEvents()
						Try
							If findData.cFileName = "." OrElse findData.cFileName = ".." Then
							'	Continue Try
							End If

							Dim fullpath As String = directory & (If(directory.EndsWith("\"), "", "\")) & findData.cFileName
							If (findData.dwFileAttributes And FileAttributes.Directory) <> 0 Then
								Scan(fullpath)
							End If

							Dim finfo As New FileInfo(fullpath)
							FileSizeTotal += finfo.Length

							Dim values As Decimal() = {CDec(HDSpaceUsed), CDec(FileSizeTotal)}
							DoChart(values)
							lblFilename.Text = finfo.Name
								' ignore errors for this demo
						Catch generatedExceptionName As Exception




						End Try

					Loop While FindNextFile(findHandle, findData)
				End If
			Finally
				If findHandle <> INVALID_HANDLE_VALUE Then
					FindClose(findHandle)
				End If

			End Try
				' Do not do anything
		Else
		End If
	End Sub

	' Show and update the pie chart
	Private Sub DoChart(values As Decimal())
		pccDiskDriveControl.Values = values
	End Sub



End Class
