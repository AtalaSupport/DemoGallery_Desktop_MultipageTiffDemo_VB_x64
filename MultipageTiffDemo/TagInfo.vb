Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Atalasoft.Imaging.Metadata
Imports Atalasoft.Imaging
Imports Atalasoft.Imaging.Codec.Tiff

Namespace MultipageTiffDemo
	''' <summary>
	''' Summary description for TagInfo.
	''' </summary>
	Public Class TagInfo
		Inherits System.Windows.Forms.Form
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing
		Private lvTags As System.Windows.Forms.ListView
		Private btnClose As System.Windows.Forms.Button
		Private lvId As System.Windows.Forms.ColumnHeader
		Private lvTagData As System.Windows.Forms.ColumnHeader
		Private lvType As System.Windows.Forms.ColumnHeader


		'private MetadataContainer metadata = null;
		Private tags As TiffTagCollection = Nothing
		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()
		End Sub

		Public Sub New(ByVal fileName As String, ByVal frameIndex As Integer)
			InitializeComponent()

			' Read the metadata.
			'metadata = new MetadataContainer(fileName, frameIndex);
			Dim theFile As TiffFile
			Dim tiffImg As TiffDirectory
'INSTANT VB NOTE: The following 'using' block is replaced by its pre-VB.NET 2005 equivalent:
'			using (FileStream fs = New FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
			Dim fs As FileStream = New FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)
			Try
				theFile = New TiffFile()
				theFile.Read(fs)
				tiffImg = theFile.Images(frameIndex)

				' this may return some pointers instead of data.
				tags = tiffImg.Tags

				If tags Is Nothing Then
					MessageBox.Show("This image doesn't contain any TIFF tags or has not been updated to a file.")
					Me.Close()
				End If

				DisplayTiffTags()
			Finally
				CType(fs, IDisposable).Dispose()
			End Try
'INSTANT VB NOTE: End of the original C# 'using' block
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If Not components Is Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.lvTags = New System.Windows.Forms.ListView()
			Me.btnClose = New System.Windows.Forms.Button()
			Me.lvId = New System.Windows.Forms.ColumnHeader()
			Me.lvTagData = New System.Windows.Forms.ColumnHeader()
			Me.lvType = New System.Windows.Forms.ColumnHeader()
			Me.SuspendLayout()
			' 
			' lvTags
			' 
			Me.lvTags.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.lvId, Me.lvType, Me.lvTagData})
			Me.lvTags.Dock = System.Windows.Forms.DockStyle.Top
			Me.lvTags.FullRowSelect = True
			Me.lvTags.MultiSelect = False
			Me.lvTags.Name = "lvTags"
			Me.lvTags.Size = New System.Drawing.Size(359, 212)
			Me.lvTags.Sorting = System.Windows.Forms.SortOrder.Ascending
			Me.lvTags.TabIndex = 0
			Me.lvTags.View = System.Windows.Forms.View.Details
			' 
			' btnClose
			' 
			Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.btnClose.Location = New System.Drawing.Point(138, 226)
			Me.btnClose.Name = "btnClose"
			Me.btnClose.Size = New System.Drawing.Size(82, 25)
			Me.btnClose.TabIndex = 1
			Me.btnClose.Text = "&Close"
			' 
			' lvId
			' 
			Me.lvId.Text = "Tag ID"
			Me.lvId.Width = 120
			' 
			' lvTagData
			' 
			Me.lvTagData.Text = "Tag Data"
			Me.lvTagData.Width = 150
			' 
			' lvType
			' 
			Me.lvType.Text = "Type"
			Me.lvType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
			Me.lvType.Width = 50
			' 
			' TagInfo
			' 
			Me.AcceptButton = Me.btnClose
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.CancelButton = Me.btnClose
			Me.ClientSize = New System.Drawing.Size(359, 266)
			Me.Controls.AddRange(New System.Windows.Forms.Control() { Me.btnClose, Me.lvTags})
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
			Me.Name = "TagInfo"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
			Me.Text = "TIFF Tag Information"
			Me.ResumeLayout(False)

		End Sub
		#End Region


		' Load the tag info into the listview.
		Private Sub DisplayTiffTags()
			If tags Is Nothing Then
				Return
			End If

			Dim item As ListViewItem = Nothing
			For Each tag As TiffTag In tags
				Dim tagName As String
				If System.Enum.IsDefined(GetType(TiffTagID), tag.ID) Then
					tagName = System.Enum.GetName(GetType(TiffTagID), tag.ID)
				Else
					tagName = tag.ID.ToString()
				End If
				item = lvTags.Items.Add(tagName)
				item.SubItems.Add(tag.Data.GetType().Name)
				item.SubItems.Add(tag.Data.ToString())
			Next tag


		End Sub
	End Class
End Namespace
