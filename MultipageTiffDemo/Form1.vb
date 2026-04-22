Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports Atalasoft.Imaging.WinControls
Imports Atalasoft.Imaging
Imports Atalasoft.Imaging.Codec
Imports Atalasoft.Imaging.Codec.Tiff
Imports Atalasoft.Imaging.ImageProcessing
Imports Atalasoft.Imaging.ImageProcessing.Document
Imports Atalasoft.Imaging.ImageProcessing.Effects
Imports Atalasoft.Imaging.ImageProcessing.Filters
Imports Atalasoft.Imaging.ImageProcessing.Transforms
Imports Atalasoft.Imaging.ImageProcessing.Channels
Imports Atalasoft.Imaging.Metadata
Imports Atalasoft.Imaging.Drawing

Namespace MultipageTiffDemo
    ''' <summary>
    ''' Summary description for Form1.
    ''' </summary>
    Public Class Form1 : Inherits System.Windows.Forms.Form
        ' Local storage.
        Private _validLicense As Boolean
        Private pageCount As Integer = 0
        Private currentPage As Integer = 1 ' 1-based index
        Private currentFile As String = ""
        Private tiffCompression As TiffCompression = TiffCompression.Group4FaxEncoding
        Private maxPageMenus As Integer = 15
        Private dragDropIndex As Integer() = New Integer() {}
        Private imageChanged As Boolean = False

#Region "Private Designer Vars"
        Private mainMenu1 As System.Windows.Forms.MainMenu
        Private menuFile As System.Windows.Forms.MenuItem
        Private WithEvents menuOpen As System.Windows.Forms.MenuItem
        Private WithEvents menuSaveAs As System.Windows.Forms.MenuItem
        Private menuFileSep1 As System.Windows.Forms.MenuItem
        Private WithEvents menuExit As System.Windows.Forms.MenuItem
        Private menuPages As System.Windows.Forms.MenuItem
        Private WithEvents menuAddPage As System.Windows.Forms.MenuItem
        Private WithEvents menuRemovePage As System.Windows.Forms.MenuItem
        Private statusBar1 As System.Windows.Forms.StatusBar
        Private statusMessage As System.Windows.Forms.StatusBarPanel
        Private statusPage As System.Windows.Forms.StatusBarPanel
        Private menuPageSep2 As System.Windows.Forms.MenuItem
        Private WithEvents menuAppend As System.Windows.Forms.MenuItem
        Private menuModify As System.Windows.Forms.MenuItem
        Private menuModifySep1 As System.Windows.Forms.MenuItem
        Private WithEvents menuErosion As System.Windows.Forms.MenuItem
        Private menuAntialiasDisplay As System.Windows.Forms.MenuItem
        Private menuPageSep1 As System.Windows.Forms.MenuItem
        Private WithEvents menuDilation As System.Windows.Forms.MenuItem
        Private WithEvents menuBoundaryExtraction As System.Windows.Forms.MenuItem
        Private menuAutoDeskew As System.Windows.Forms.MenuItem
        Private menuMorpho As System.Windows.Forms.MenuItem
        Private menuFileSep2 As System.Windows.Forms.MenuItem
        Private WithEvents menuAntiNone As System.Windows.Forms.MenuItem
        Private WithEvents menuAntiGray As System.Windows.Forms.MenuItem
        Private WithEvents menuAntiReduction As System.Windows.Forms.MenuItem
        Private WithEvents menuAntiFull As System.Windows.Forms.MenuItem
        Private menuTiffCompression As System.Windows.Forms.MenuItem
        Private WithEvents menuCompressNone As System.Windows.Forms.MenuItem
        Private WithEvents menuCompressGroup3 As System.Windows.Forms.MenuItem
        Private WithEvents menuCompressGroup4 As System.Windows.Forms.MenuItem
        Private WithEvents menuCompressLzw As System.Windows.Forms.MenuItem
        Private WithEvents menuCompressDeflate As System.Windows.Forms.MenuItem
        Private WithEvents menuCompressJpeg As System.Windows.Forms.MenuItem
        Private WithEvents menuCompressHuffman As System.Windows.Forms.MenuItem
        Private WithEvents menuCompressMac As System.Windows.Forms.MenuItem
        Private WithEvents menuFlipHorizontal As System.Windows.Forms.MenuItem
        Private menuRotate As System.Windows.Forms.MenuItem
        Private WithEvents menuRotate90 As System.Windows.Forms.MenuItem
        Private WithEvents menuRotate180 As System.Windows.Forms.MenuItem
        Private WithEvents menuRotate270 As System.Windows.Forms.MenuItem
        Private WithEvents menuFlipVertical As System.Windows.Forms.MenuItem
        Private WithEvents menuMorphoOpen As System.Windows.Forms.MenuItem
        Private WithEvents menuMorphoClose As System.Windows.Forms.MenuItem
        Private WithEvents menuUndo As System.Windows.Forms.MenuItem
        Private menuModifySep2 As System.Windows.Forms.MenuItem
        Private onPageMenuClick_Renamed As System.EventHandler = Nothing
        Private WithEvents Viewer As Atalasoft.Imaging.WinControls.WorkspaceViewer
        Private components As System.ComponentModel.IContainer
        Private menuPageSep3 As System.Windows.Forms.MenuItem
        Private pageMenus As MenuItem = Nothing
        Private WithEvents menuInvert As System.Windows.Forms.MenuItem
        Private WithEvents menuInsertPage As System.Windows.Forms.MenuItem
        Private imageList1 As System.Windows.Forms.ImageList
        Private WithEvents toolBar1 As System.Windows.Forms.ToolBar
        Private tbOpen As System.Windows.Forms.ToolBarButton
        Private tbSave As System.Windows.Forms.ToolBarButton
        Private tbSep As System.Windows.Forms.ToolBarButton
        Private toolBarButton1 As System.Windows.Forms.ToolBarButton
        Private tbArrow As System.Windows.Forms.ToolBarButton
        Private tbSelectRectangle As System.Windows.Forms.ToolBarButton
        Private tbSelectEllipse As System.Windows.Forms.ToolBarButton
        Private tbPan As System.Windows.Forms.ToolBarButton
        Private tbMagnifier As System.Windows.Forms.ToolBarButton
        Private tbZoom As System.Windows.Forms.ToolBarButton
        Private tbZoomSelection As System.Windows.Forms.ToolBarButton
        Private contextMenu1 As System.Windows.Forms.ContextMenu
        Private tbPrevious As System.Windows.Forms.ToolBarButton
        Private tbNext As System.Windows.Forms.ToolBarButton
        Private ellipseSelect As Atalasoft.Imaging.WinControls.EllipseRubberband
        Private rectangleSelect As Atalasoft.Imaging.WinControls.RectangleSelection
        Private WithEvents menuRotateCustom As System.Windows.Forms.MenuItem
        Private progressBar1 As System.Windows.Forms.ProgressBar
        Private WithEvents mnuDeskew1 As System.Windows.Forms.MenuItem
        Private WithEvents mnuDeskew2 As System.Windows.Forms.MenuItem
        Private WithEvents mnuDeskew4 As System.Windows.Forms.MenuItem
        Private WithEvents mnuDeskew10 As System.Windows.Forms.MenuItem
        Private WithEvents menuTiffTags As System.Windows.Forms.MenuItem
        Private WithEvents menuAutoZoomNone As System.Windows.Forms.MenuItem
        Private WithEvents menuAutoZoomBestFit As System.Windows.Forms.MenuItem
        Private WithEvents menuAutoZoomBestFitShrink As System.Windows.Forms.MenuItem
        Private WithEvents menuAutoZoomFitWidth As System.Windows.Forms.MenuItem
        Private WithEvents menuAutoZoomFitHeight As System.Windows.Forms.MenuItem
        Private WithEvents menuMedian As System.Windows.Forms.MenuItem
        Private WithEvents cmbAutoZoom As System.Windows.Forms.ComboBox
        Private WithEvents menuDespeckle As System.Windows.Forms.MenuItem
        Private WithEvents thumbnailView1 As Atalasoft.Imaging.WinControls.ThumbnailView
        Private WithEvents imagePrintDocument1 As Atalasoft.Imaging.WinControls.ImagePrintDocument
        Private printDialog1 As System.Windows.Forms.PrintDialog
        Private WithEvents menuPrint As System.Windows.Forms.MenuItem
        Private menuThresholding As System.Windows.Forms.MenuItem
        Private WithEvents menuThresholdingAdaptive As System.Windows.Forms.MenuItem
        Private WithEvents menuThresholdingGlobal As System.Windows.Forms.MenuItem
        Private menuItem1 As System.Windows.Forms.MenuItem
        Private menuBorderRemoval As System.Windows.Forms.MenuItem
        Private WithEvents menuBorderRemovalH As System.Windows.Forms.MenuItem
        Private WithEvents menuBorderRemovalV As System.Windows.Forms.MenuItem
        Private WithEvents menuBorderRemovalAll As System.Windows.Forms.MenuItem
        Private menuItem2 As System.Windows.Forms.MenuItem
        Private WithEvents menuAbout As System.Windows.Forms.MenuItem
        Private WithEvents menuThresholdingDynamic As System.Windows.Forms.MenuItem
        Private WithEvents menuThresholdingSimple As System.Windows.Forms.MenuItem
        Private menuItem3 As System.Windows.Forms.MenuItem
#End Region

        Public Sub New()
            CheckLicenseFile()

            If Me._validLicense Then
                '
                ' Required for Windows Form Designer support
                '
                InitializeComponent()

                Me.onPageMenuClick_Renamed = New System.EventHandler(AddressOf Me.OnPageMenuClick)

                'display the correct checkmark in the antialias menu
                RemoveAntialiasCheckmarks()
                Me.cmbAutoZoom.SelectedIndex = 0
                menuAntialiasDisplay.MenuItems(CInt(Viewer.AntialiasDisplay)).Checked = True
                Viewer.ImageBorderPen = New Atalasoft.Imaging.Drawing.AtalaPen(Color.Black, 1)

                'this.thumbnailView1.BackgroundThumb = new AtalaImage(79,102,PixelFormat.Pixel24bppBgr,Color.Black);

                ' Create a temporary file for editing
                Me.currentFile = System.IO.Path.GetTempFileName()
            End If
        End Sub

#Region "Check for license code"

        Private Sub CheckLicenseFile()
            Try
                Dim img As AtalaImage = New AtalaImage
                img.Dispose()
            Catch ex As Atalasoft.Imaging.AtalasoftLicenseException
                LicenseCheckFailure("License validation failed:  " & ex.Message)
                Return
            End Try

            If AtalaImage.Edition <> LicenseEdition.Document Then
                LicenseCheckFailure("This demo requires a Document Imaging License." & Constants.vbCrLf & "Your current license is for '" & AtalaImage.Edition.ToString() & "'.")
                Return
            End If

            Me._validLicense = True
        End Sub

        Private Sub LicenseCheckFailure(ByVal message As String)
            AddHandler Load, AddressOf Form1_Load
            If MessageBox.Show(Me, message & Constants.vbCrLf & Constants.vbCrLf & "Would you like to request an evaluation license?", "License Required", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                ' Locate the activation utility.
                Dim path As String = ""
                Dim key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\Atalasoft\dotImage\4.0")
                If Not key Is Nothing Then
                    path = Convert.ToString(key.GetValue("AssemblyBasePath"))
                    If Not path Is Nothing AndAlso path.Length > 5 Then
                        path = path.Substring(0, path.Length - 3) & "AtalasoftToolkitActivation.exe"
                    Else
                        path = System.IO.Path.GetFullPath("..\..\..\..\..\AtalasoftToolkitActivation.exe")
                    End If

                    key.Close()
                End If

                If File.Exists(path) Then
                    System.Diagnostics.Process.Start(path)
                Else
                    MessageBox.Show(Me, "We were unable to location the DotImage activation utility." & Constants.vbCrLf & "Please run it from the Start menu shortcut.", "File Not Found")
                End If
            End If
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If (Not Me._validLicense) Then
                Application.Exit()
            End If
        End Sub

#End Region

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub


        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
            Me.mainMenu1 = New System.Windows.Forms.MainMenu
            Me.menuFile = New System.Windows.Forms.MenuItem
            Me.menuOpen = New System.Windows.Forms.MenuItem
            Me.menuSaveAs = New System.Windows.Forms.MenuItem
            Me.menuFileSep1 = New System.Windows.Forms.MenuItem
            Me.menuTiffCompression = New System.Windows.Forms.MenuItem
            Me.menuCompressNone = New System.Windows.Forms.MenuItem
            Me.menuCompressDeflate = New System.Windows.Forms.MenuItem
            Me.menuCompressGroup3 = New System.Windows.Forms.MenuItem
            Me.menuCompressGroup4 = New System.Windows.Forms.MenuItem
            Me.menuCompressJpeg = New System.Windows.Forms.MenuItem
            Me.menuCompressLzw = New System.Windows.Forms.MenuItem
            Me.menuCompressMac = New System.Windows.Forms.MenuItem
            Me.menuCompressHuffman = New System.Windows.Forms.MenuItem
            Me.menuPrint = New System.Windows.Forms.MenuItem
            Me.menuFileSep2 = New System.Windows.Forms.MenuItem
            Me.menuExit = New System.Windows.Forms.MenuItem
            Me.menuPages = New System.Windows.Forms.MenuItem
            Me.menuAntialiasDisplay = New System.Windows.Forms.MenuItem
            Me.menuAntiNone = New System.Windows.Forms.MenuItem
            Me.menuAntiGray = New System.Windows.Forms.MenuItem
            Me.menuAntiReduction = New System.Windows.Forms.MenuItem
            Me.menuAntiFull = New System.Windows.Forms.MenuItem
            Me.menuPageSep1 = New System.Windows.Forms.MenuItem
            Me.menuAddPage = New System.Windows.Forms.MenuItem
            Me.menuInsertPage = New System.Windows.Forms.MenuItem
            Me.menuRemovePage = New System.Windows.Forms.MenuItem
            Me.menuPageSep2 = New System.Windows.Forms.MenuItem
            Me.menuAppend = New System.Windows.Forms.MenuItem
            Me.menuPageSep3 = New System.Windows.Forms.MenuItem
            Me.menuTiffTags = New System.Windows.Forms.MenuItem
            Me.menuItem3 = New System.Windows.Forms.MenuItem
            Me.menuModify = New System.Windows.Forms.MenuItem
            Me.menuUndo = New System.Windows.Forms.MenuItem
            Me.menuModifySep2 = New System.Windows.Forms.MenuItem
            Me.menuFlipHorizontal = New System.Windows.Forms.MenuItem
            Me.menuFlipVertical = New System.Windows.Forms.MenuItem
            Me.menuInvert = New System.Windows.Forms.MenuItem
            Me.menuRotate = New System.Windows.Forms.MenuItem
            Me.menuRotate90 = New System.Windows.Forms.MenuItem
            Me.menuRotate180 = New System.Windows.Forms.MenuItem
            Me.menuRotate270 = New System.Windows.Forms.MenuItem
            Me.menuRotateCustom = New System.Windows.Forms.MenuItem
            Me.menuModifySep1 = New System.Windows.Forms.MenuItem
            Me.menuAutoDeskew = New System.Windows.Forms.MenuItem
            Me.mnuDeskew1 = New System.Windows.Forms.MenuItem
            Me.mnuDeskew2 = New System.Windows.Forms.MenuItem
            Me.mnuDeskew4 = New System.Windows.Forms.MenuItem
            Me.mnuDeskew10 = New System.Windows.Forms.MenuItem
            Me.menuMorpho = New System.Windows.Forms.MenuItem
            Me.menuDilation = New System.Windows.Forms.MenuItem
            Me.menuErosion = New System.Windows.Forms.MenuItem
            Me.menuMorphoOpen = New System.Windows.Forms.MenuItem
            Me.menuMorphoClose = New System.Windows.Forms.MenuItem
            Me.menuBoundaryExtraction = New System.Windows.Forms.MenuItem
            Me.menuDespeckle = New System.Windows.Forms.MenuItem
            Me.menuMedian = New System.Windows.Forms.MenuItem
            Me.menuItem1 = New System.Windows.Forms.MenuItem
            Me.menuThresholding = New System.Windows.Forms.MenuItem
            Me.menuThresholdingAdaptive = New System.Windows.Forms.MenuItem
            Me.menuThresholdingDynamic = New System.Windows.Forms.MenuItem
            Me.menuThresholdingGlobal = New System.Windows.Forms.MenuItem
            Me.menuThresholdingSimple = New System.Windows.Forms.MenuItem
            Me.menuBorderRemoval = New System.Windows.Forms.MenuItem
            Me.menuBorderRemovalH = New System.Windows.Forms.MenuItem
            Me.menuBorderRemovalV = New System.Windows.Forms.MenuItem
            Me.menuBorderRemovalAll = New System.Windows.Forms.MenuItem
            Me.menuItem2 = New System.Windows.Forms.MenuItem
            Me.menuAbout = New System.Windows.Forms.MenuItem
            Me.statusBar1 = New System.Windows.Forms.StatusBar
            Me.statusMessage = New System.Windows.Forms.StatusBarPanel
            Me.statusPage = New System.Windows.Forms.StatusBarPanel
            Me.Viewer = New Atalasoft.Imaging.WinControls.WorkspaceViewer
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.toolBar1 = New System.Windows.Forms.ToolBar
            Me.tbOpen = New System.Windows.Forms.ToolBarButton
            Me.tbSave = New System.Windows.Forms.ToolBarButton
            Me.tbSep = New System.Windows.Forms.ToolBarButton
            Me.tbPrevious = New System.Windows.Forms.ToolBarButton
            Me.tbNext = New System.Windows.Forms.ToolBarButton
            Me.toolBarButton1 = New System.Windows.Forms.ToolBarButton
            Me.tbArrow = New System.Windows.Forms.ToolBarButton
            Me.tbSelectRectangle = New System.Windows.Forms.ToolBarButton
            Me.tbSelectEllipse = New System.Windows.Forms.ToolBarButton
            Me.tbPan = New System.Windows.Forms.ToolBarButton
            Me.tbMagnifier = New System.Windows.Forms.ToolBarButton
            Me.tbZoom = New System.Windows.Forms.ToolBarButton
            Me.tbZoomSelection = New System.Windows.Forms.ToolBarButton
            Me.contextMenu1 = New System.Windows.Forms.ContextMenu
            Me.menuAutoZoomNone = New System.Windows.Forms.MenuItem
            Me.menuAutoZoomBestFit = New System.Windows.Forms.MenuItem
            Me.menuAutoZoomBestFitShrink = New System.Windows.Forms.MenuItem
            Me.menuAutoZoomFitWidth = New System.Windows.Forms.MenuItem
            Me.menuAutoZoomFitHeight = New System.Windows.Forms.MenuItem
            Me.ellipseSelect = New Atalasoft.Imaging.WinControls.EllipseRubberband
            Me.rectangleSelect = New Atalasoft.Imaging.WinControls.RectangleSelection
            Me.progressBar1 = New System.Windows.Forms.ProgressBar
            Me.cmbAutoZoom = New System.Windows.Forms.ComboBox
            Me.thumbnailView1 = New Atalasoft.Imaging.WinControls.ThumbnailView
            Me.imagePrintDocument1 = New Atalasoft.Imaging.WinControls.ImagePrintDocument
            Me.printDialog1 = New System.Windows.Forms.PrintDialog
            CType(Me.statusMessage, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.statusPage, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' mainMenu1
            ' 
            Me.mainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuFile, Me.menuPages, Me.menuModify, Me.menuItem2})
            ' 
            ' menuFile
            ' 
            Me.menuFile.Index = 0
            Me.menuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuOpen, Me.menuSaveAs, Me.menuFileSep1, Me.menuTiffCompression, Me.menuPrint, Me.menuFileSep2, Me.menuExit})
            Me.menuFile.Text = "&File"
            ' 
            ' menuOpen
            ' 
            Me.menuOpen.Index = 0
            Me.menuOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO
            Me.menuOpen.Text = "&Open"
            '			Me.menuOpen.Click += New System.EventHandler(Me.menuOpen_Click);
            ' 
            ' menuSaveAs
            ' 
            Me.menuSaveAs.Index = 1
            Me.menuSaveAs.Shortcut = System.Windows.Forms.Shortcut.CtrlS
            Me.menuSaveAs.Text = "&Save As..."
            '			Me.menuSaveAs.Click += New System.EventHandler(Me.menuSaveAs_Click);
            ' 
            ' menuFileSep1
            ' 
            Me.menuFileSep1.Index = 2
            Me.menuFileSep1.Text = "-"
            ' 
            ' menuTiffCompression
            ' 
            Me.menuTiffCompression.Index = 3
            Me.menuTiffCompression.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuCompressNone, Me.menuCompressDeflate, Me.menuCompressGroup3, Me.menuCompressGroup4, Me.menuCompressJpeg, Me.menuCompressLzw, Me.menuCompressMac, Me.menuCompressHuffman})
            Me.menuTiffCompression.Text = "TIFF Compression"
            ' 
            ' menuCompressNone
            ' 
            Me.menuCompressNone.Index = 0
            Me.menuCompressNone.RadioCheck = True
            Me.menuCompressNone.Text = "None"
            '			Me.menuCompressNone.Click += New System.EventHandler(Me.menuCompressNone_Click);
            ' 
            ' menuCompressDeflate
            ' 
            Me.menuCompressDeflate.Index = 1
            Me.menuCompressDeflate.RadioCheck = True
            Me.menuCompressDeflate.Text = "Deflate"
            '			Me.menuCompressDeflate.Click += New System.EventHandler(Me.menuCompressDeflate_Click);
            ' 
            ' menuCompressGroup3
            ' 
            Me.menuCompressGroup3.Index = 2
            Me.menuCompressGroup3.RadioCheck = True
            Me.menuCompressGroup3.Text = "Group 3 Fax Encoding"
            '			Me.menuCompressGroup3.Click += New System.EventHandler(Me.menuCompressGroup3_Click);
            ' 
            ' menuCompressGroup4
            ' 
            Me.menuCompressGroup4.Checked = True
            Me.menuCompressGroup4.Index = 3
            Me.menuCompressGroup4.RadioCheck = True
            Me.menuCompressGroup4.Text = "Group 4 Fax Encoding"
            '			Me.menuCompressGroup4.Click += New System.EventHandler(Me.menuCompressGroup4_Click);
            ' 
            ' menuCompressJpeg
            ' 
            Me.menuCompressJpeg.Index = 4
            Me.menuCompressJpeg.RadioCheck = True
            Me.menuCompressJpeg.Text = "JPEG"
            '			Me.menuCompressJpeg.Click += New System.EventHandler(Me.menuCompressJpeg_Click);
            ' 
            ' menuCompressLzw
            ' 
            Me.menuCompressLzw.Index = 5
            Me.menuCompressLzw.RadioCheck = True
            Me.menuCompressLzw.Text = "LZW"
            '			Me.menuCompressLzw.Click += New System.EventHandler(Me.menuCompressLzw_Click);
            ' 
            ' menuCompressMac
            ' 
            Me.menuCompressMac.Index = 6
            Me.menuCompressMac.RadioCheck = True
            Me.menuCompressMac.Text = "Macintosh Packbits"
            '			Me.menuCompressMac.Click += New System.EventHandler(Me.menuCompressMac_Click);
            ' 
            ' menuCompressHuffman
            ' 
            Me.menuCompressHuffman.Index = 7
            Me.menuCompressHuffman.RadioCheck = True
            Me.menuCompressHuffman.Text = "Modified Huffman"
            '			Me.menuCompressHuffman.Click += New System.EventHandler(Me.menuCompressHuffman_Click);
            ' 
            ' menuPrint
            ' 
            Me.menuPrint.Index = 4
            Me.menuPrint.Text = "Print"
            '			Me.menuPrint.Click += New System.EventHandler(Me.menuPrint_Click);
            ' 
            ' menuFileSep2
            ' 
            Me.menuFileSep2.Index = 5
            Me.menuFileSep2.Text = "-"
            ' 
            ' menuExit
            ' 
            Me.menuExit.Index = 6
            Me.menuExit.Text = "E&xit"
            '			Me.menuExit.Click += New System.EventHandler(Me.menuExit_Click);
            ' 
            ' menuPages
            ' 
            Me.menuPages.Index = 1
            Me.menuPages.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuAntialiasDisplay, Me.menuPageSep1, Me.menuAddPage, Me.menuInsertPage, Me.menuRemovePage, Me.menuPageSep2, Me.menuAppend, Me.menuPageSep3, Me.menuTiffTags, Me.menuItem3})
            Me.menuPages.Text = "&Pages"
            ' 
            ' menuAntialiasDisplay
            ' 
            Me.menuAntialiasDisplay.Index = 0
            Me.menuAntialiasDisplay.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuAntiNone, Me.menuAntiGray, Me.menuAntiReduction, Me.menuAntiFull})
            Me.menuAntialiasDisplay.Text = "Antialias Display"
            ' 
            ' menuAntiNone
            ' 
            Me.menuAntiNone.Checked = True
            Me.menuAntiNone.Index = 0
            Me.menuAntiNone.RadioCheck = True
            Me.menuAntiNone.Text = "None"
            '			Me.menuAntiNone.Click += New System.EventHandler(Me.menuAnti_Click);
            ' 
            ' menuAntiGray
            ' 
            Me.menuAntiGray.Index = 1
            Me.menuAntiGray.RadioCheck = True
            Me.menuAntiGray.Text = "Scale To Gray"
            '			Me.menuAntiGray.Click += New System.EventHandler(Me.menuAnti_Click);
            ' 
            ' menuAntiReduction
            ' 
            Me.menuAntiReduction.Index = 2
            Me.menuAntiReduction.RadioCheck = True
            Me.menuAntiReduction.Text = "Reduction Only"
            '			Me.menuAntiReduction.Click += New System.EventHandler(Me.menuAnti_Click);
            ' 
            ' menuAntiFull
            ' 
            Me.menuAntiFull.Index = 3
            Me.menuAntiFull.RadioCheck = True
            Me.menuAntiFull.Text = "Full"
            '			Me.menuAntiFull.Click += New System.EventHandler(Me.menuAnti_Click);
            ' 
            ' menuPageSep1
            ' 
            Me.menuPageSep1.Index = 1
            Me.menuPageSep1.Text = "-"
            ' 
            ' menuAddPage
            ' 
            Me.menuAddPage.Index = 2
            Me.menuAddPage.Shortcut = System.Windows.Forms.Shortcut.F2
            Me.menuAddPage.Text = "&Add..."
            '			Me.menuAddPage.Click += New System.EventHandler(Me.menuAddPage_Click);
            ' 
            ' menuInsertPage
            ' 
            Me.menuInsertPage.Index = 3
            Me.menuInsertPage.Text = "&Insert..."
            '			Me.menuInsertPage.Click += New System.EventHandler(Me.menuInsertPage_Click);
            ' 
            ' menuRemovePage
            ' 
            Me.menuRemovePage.Index = 4
            Me.menuRemovePage.Shortcut = System.Windows.Forms.Shortcut.Del
            Me.menuRemovePage.Text = "&Remove"
            '			Me.menuRemovePage.Click += New System.EventHandler(Me.menuRemovePage_Click);
            ' 
            ' menuPageSep2
            ' 
            Me.menuPageSep2.Index = 5
            Me.menuPageSep2.Text = "-"
            ' 
            ' menuAppend
            ' 
            Me.menuAppend.Index = 6
            Me.menuAppend.Text = "Append to file..."
            '			Me.menuAppend.Click += New System.EventHandler(Me.menuAppend_Click);
            ' 
            ' menuPageSep3
            ' 
            Me.menuPageSep3.Index = 7
            Me.menuPageSep3.Text = "-"
            ' 
            ' menuTiffTags
            ' 
            Me.menuTiffTags.Index = 8
            Me.menuTiffTags.Text = "View TIFF Tags"
            '			Me.menuTiffTags.Click += New System.EventHandler(Me.menuTiffTags_Click);
            ' 
            ' menuItem3
            ' 
            Me.menuItem3.Index = 9
            Me.menuItem3.Text = "-"
            ' 
            ' menuModify
            ' 
            Me.menuModify.Index = 2
            Me.menuModify.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuUndo, Me.menuModifySep2, Me.menuFlipHorizontal, Me.menuFlipVertical, Me.menuInvert, Me.menuRotate, Me.menuModifySep1, Me.menuAutoDeskew, Me.menuMorpho, Me.menuDespeckle, Me.menuMedian, Me.menuItem1, Me.menuThresholding, Me.menuBorderRemoval})
            Me.menuModify.Text = "&Modify"
            ' 
            ' menuUndo
            ' 
            Me.menuUndo.Index = 0
            Me.menuUndo.Shortcut = System.Windows.Forms.Shortcut.CtrlZ
            Me.menuUndo.Text = "&Undo"
            '			Me.menuUndo.Click += New System.EventHandler(Me.menuUndo_Click);
            ' 
            ' menuModifySep2
            ' 
            Me.menuModifySep2.Index = 1
            Me.menuModifySep2.Text = "-"
            ' 
            ' menuFlipHorizontal
            ' 
            Me.menuFlipHorizontal.Index = 2
            Me.menuFlipHorizontal.Text = "&Flip Horizontal"
            '			Me.menuFlipHorizontal.Click += New System.EventHandler(Me.menuFlipHorizontal_Click);
            ' 
            ' menuFlipVertical
            ' 
            Me.menuFlipVertical.Index = 3
            Me.menuFlipVertical.Text = "Flip &Vertical"
            '			Me.menuFlipVertical.Click += New System.EventHandler(Me.menuFlipVertical_Click);
            ' 
            ' menuInvert
            ' 
            Me.menuInvert.Index = 4
            Me.menuInvert.Text = "Invert"
            '			Me.menuInvert.Click += New System.EventHandler(Me.menuInvert_Click);
            ' 
            ' menuRotate
            ' 
            Me.menuRotate.Index = 5
            Me.menuRotate.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuRotate90, Me.menuRotate180, Me.menuRotate270, Me.menuRotateCustom})
            Me.menuRotate.Text = "&Rotate"
            ' 
            ' menuRotate90
            ' 
            Me.menuRotate90.Index = 0
            Me.menuRotate90.Text = "90 Degrees"
            '			Me.menuRotate90.Click += New System.EventHandler(Me.menuRotate90_Click);
            ' 
            ' menuRotate180
            ' 
            Me.menuRotate180.Index = 1
            Me.menuRotate180.Text = "180 Degrees"
            '			Me.menuRotate180.Click += New System.EventHandler(Me.menuRotate180_Click);
            ' 
            ' menuRotate270
            ' 
            Me.menuRotate270.Index = 2
            Me.menuRotate270.Text = "270 Degrees"
            '			Me.menuRotate270.Click += New System.EventHandler(Me.menuRotate270_Click);
            ' 
            ' menuRotateCustom
            ' 
            Me.menuRotateCustom.Index = 3
            Me.menuRotateCustom.Text = "Custom"
            '			Me.menuRotateCustom.Click += New System.EventHandler(Me.menuRotateCustom_Click);
            ' 
            ' menuModifySep1
            ' 
            Me.menuModifySep1.Index = 6
            Me.menuModifySep1.Text = "-"
            ' 
            ' menuAutoDeskew
            ' 
            Me.menuAutoDeskew.Index = 7
            Me.menuAutoDeskew.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDeskew1, Me.mnuDeskew2, Me.mnuDeskew4, Me.mnuDeskew10})
            Me.menuAutoDeskew.Text = "Binary Auto Deskew"
            ' 
            ' mnuDeskew1
            ' 
            Me.mnuDeskew1.Index = 0
            Me.mnuDeskew1.Text = "1 degree Precision"
            '			Me.mnuDeskew1.Click += New System.EventHandler(Me.mnuDeskew_Click);
            ' 
            ' mnuDeskew2
            ' 
            Me.mnuDeskew2.Index = 1
            Me.mnuDeskew2.Text = "0.5 degree Precision"
            '			Me.mnuDeskew2.Click += New System.EventHandler(Me.mnuDeskew_Click);
            ' 
            ' mnuDeskew4
            ' 
            Me.mnuDeskew4.Index = 2
            Me.mnuDeskew4.Text = "0.25 degree Precision"
            '			Me.mnuDeskew4.Click += New System.EventHandler(Me.mnuDeskew_Click);
            ' 
            ' mnuDeskew10
            ' 
            Me.mnuDeskew10.Index = 3
            Me.mnuDeskew10.Text = "0.1 degree Precision"
            '			Me.mnuDeskew10.Click += New System.EventHandler(Me.mnuDeskew_Click);
            ' 
            ' menuMorpho
            ' 
            Me.menuMorpho.Index = 8
            Me.menuMorpho.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuDilation, Me.menuErosion, Me.menuMorphoOpen, Me.menuMorphoClose, Me.menuBoundaryExtraction})
            Me.menuMorpho.Text = "Binary Morphological Filters"
            ' 
            ' menuDilation
            ' 
            Me.menuDilation.Index = 0
            Me.menuDilation.Text = "Dilation"
            '			Me.menuDilation.Click += New System.EventHandler(Me.menuMorpho_Click);
            ' 
            ' menuErosion
            ' 
            Me.menuErosion.Index = 1
            Me.menuErosion.Text = "Erosion"
            '			Me.menuErosion.Click += New System.EventHandler(Me.menuMorpho_Click);
            ' 
            ' menuMorphoOpen
            ' 
            Me.menuMorphoOpen.Index = 2
            Me.menuMorphoOpen.Text = "Open"
            '			Me.menuMorphoOpen.Click += New System.EventHandler(Me.menuMorpho_Click);
            ' 
            ' menuMorphoClose
            ' 
            Me.menuMorphoClose.Index = 3
            Me.menuMorphoClose.Text = "Close"
            '			Me.menuMorphoClose.Click += New System.EventHandler(Me.menuMorpho_Click);
            ' 
            ' menuBoundaryExtraction
            ' 
            Me.menuBoundaryExtraction.Index = 4
            Me.menuBoundaryExtraction.Text = "Boundary Extraction"
            '			Me.menuBoundaryExtraction.Click += New System.EventHandler(Me.menuMorpho_Click);
            ' 
            ' menuDespeckle
            ' 
            Me.menuDespeckle.Index = 9
            Me.menuDespeckle.Text = "Binary Despeckle"
            '			Me.menuDespeckle.Click += New System.EventHandler(Me.menuDespeckle_Click);
            ' 
            ' menuMedian
            ' 
            Me.menuMedian.Index = 10
            Me.menuMedian.Text = "Binary Remove Noise"
            '			Me.menuMedian.Click += New System.EventHandler(Me.menuMedian_Click);
            ' 
            ' menuItem1
            ' 
            Me.menuItem1.Index = 11
            Me.menuItem1.Text = "-"
            ' 
            ' menuThresholding
            ' 
            Me.menuThresholding.Index = 12
            Me.menuThresholding.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuThresholdingAdaptive, Me.menuThresholdingDynamic, Me.menuThresholdingGlobal, Me.menuThresholdingSimple})
            Me.menuThresholding.Text = "Thresholding"
            ' 
            ' menuThresholdingAdaptive
            ' 
            Me.menuThresholdingAdaptive.Index = 0
            Me.menuThresholdingAdaptive.Text = "Adaptive"
            '			Me.menuThresholdingAdaptive.Click += New System.EventHandler(Me.menuThresholdingAdaptive_Click);
            ' 
            ' menuThresholdingDynamic
            ' 
            Me.menuThresholdingDynamic.Index = 1
            Me.menuThresholdingDynamic.Text = "Dynamic"
            '			Me.menuThresholdingDynamic.Click += New System.EventHandler(Me.menuThresholdingDynamic_Click);
            ' 
            ' menuThresholdingGlobal
            ' 
            Me.menuThresholdingGlobal.Index = 2
            Me.menuThresholdingGlobal.Text = "Global"
            '			Me.menuThresholdingGlobal.Click += New System.EventHandler(Me.menuThresholdingGlobal_Click);
            ' 
            ' menuThresholdingSimple
            ' 
            Me.menuThresholdingSimple.Index = 3
            Me.menuThresholdingSimple.Text = "Simple"
            '			Me.menuThresholdingSimple.Click += New System.EventHandler(Me.menuThresholdingSimple_Click);
            ' 
            ' menuBorderRemoval
            ' 
            Me.menuBorderRemoval.Index = 13
            Me.menuBorderRemoval.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuBorderRemovalH, Me.menuBorderRemovalV, Me.menuBorderRemovalAll})
            Me.menuBorderRemoval.Text = "Border Removal"
            ' 
            ' menuBorderRemovalH
            ' 
            Me.menuBorderRemovalH.Index = 0
            Me.menuBorderRemovalH.Text = "Horizontal"
            '			Me.menuBorderRemovalH.Click += New System.EventHandler(Me.menuBorderRemovalH_Click);
            ' 
            ' menuBorderRemovalV
            ' 
            Me.menuBorderRemovalV.Index = 1
            Me.menuBorderRemovalV.Text = "Vertical"
            '			Me.menuBorderRemovalV.Click += New System.EventHandler(Me.menuBorderRemovalV_Click);
            ' 
            ' menuBorderRemovalAll
            ' 
            Me.menuBorderRemovalAll.Index = 2
            Me.menuBorderRemovalAll.Text = "All Sides"
            '			Me.menuBorderRemovalAll.Click += New System.EventHandler(Me.menuBorderRemovalAll_Click);
            ' 
            ' menuItem2
            ' 
            Me.menuItem2.Index = 3
            Me.menuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuAbout})
            Me.menuItem2.Text = "&Help"
            ' 
            ' menuAbout
            ' 
            Me.menuAbout.Index = 0
            Me.menuAbout.Text = "About ..."
            '			Me.menuAbout.Click += New System.EventHandler(Me.menuAbout_Click);
            ' 
            ' statusBar1
            ' 
            Me.statusBar1.Location = New System.Drawing.Point(0, 395)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusMessage, Me.statusPage})
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(456, 22)
            Me.statusBar1.TabIndex = 1
            ' 
            ' statusMessage
            ' 
            Me.statusMessage.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
            Me.statusMessage.Text = "dotImage Multipage TIFF Demo"
            Me.statusMessage.Width = 320
            ' 
            ' statusPage
            ' 
            Me.statusPage.Alignment = System.Windows.Forms.HorizontalAlignment.Center
            Me.statusPage.MinWidth = 120
            Me.statusPage.Text = "Page 0  of 0"
            Me.statusPage.Width = 120
            ' 
            ' Viewer
            ' 
            Me.Viewer.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
            Me.Viewer.AntialiasDisplay = Atalasoft.Imaging.WinControls.AntialiasDisplayMode.ScaleToGray
            Me.Viewer.DisplayProfile = Nothing
            Me.Viewer.Location = New System.Drawing.Point(160, 24)
            Me.Viewer.Magnifier.BackColor = System.Drawing.Color.White
            Me.Viewer.Magnifier.BorderColor = System.Drawing.Color.Black
            Me.Viewer.Magnifier.Size = New System.Drawing.Size(100, 100)
            Me.Viewer.Name = "Viewer"
            Me.Viewer.OutputProfile = Nothing
            Me.Viewer.Selection = Nothing
            Me.Viewer.Size = New System.Drawing.Size(295, 366)
            Me.Viewer.TabIndex = 7
            Me.Viewer.UndoLevels = 3
            '			Me.Viewer.Progress += New Atalasoft.Imaging.ProgressEventHandler(Me.Viewer_Progress);
            '			Me.Viewer.ImageChanged += New Atalasoft.Imaging.ImageEventHandler(Me.Viewer_ChangedImage);
            ' 
            ' imageList1
            ' 
            Me.imageList1.ImageSize = New System.Drawing.Size(16, 16)
            Me.imageList1.ImageStream = (CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer))
            Me.imageList1.TransparentColor = System.Drawing.Color.Transparent
            ' 
            ' toolBar1
            ' 
            Me.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbOpen, Me.tbSave, Me.tbSep, Me.tbPrevious, Me.tbNext, Me.toolBarButton1, Me.tbArrow, Me.tbSelectRectangle, Me.tbSelectEllipse, Me.tbPan, Me.tbMagnifier, Me.tbZoom, Me.tbZoomSelection})
            Me.toolBar1.DropDownArrows = True
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(456, 24)
            Me.toolBar1.TabIndex = 3
            Me.toolBar1.Wrappable = False
            '			Me.toolBar1.ButtonClick += New System.Windows.Forms.ToolBarButtonClickEventHandler(Me.toolBar1_ButtonClick);
            ' 
            ' tbOpen
            ' 
            Me.tbOpen.ImageIndex = 3
            Me.tbOpen.ToolTipText = "Open"
            ' 
            ' tbSave
            ' 
            Me.tbSave.ImageIndex = 4
            Me.tbSave.ToolTipText = "Save"
            ' 
            ' tbSep
            ' 
            Me.tbSep.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            ' 
            ' tbPrevious
            ' 
            Me.tbPrevious.Enabled = False
            Me.tbPrevious.ImageIndex = 11
            Me.tbPrevious.ToolTipText = "Previous"
            ' 
            ' tbNext
            ' 
            Me.tbNext.Enabled = False
            Me.tbNext.ImageIndex = 12
            Me.tbNext.ToolTipText = "Next"
            ' 
            ' toolBarButton1
            ' 
            Me.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
            ' 
            ' tbArrow
            ' 
            Me.tbArrow.ImageIndex = 0
            Me.tbArrow.Pushed = True
            Me.tbArrow.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
            Me.tbArrow.ToolTipText = "Arrow"
            ' 
            ' tbSelectRectangle
            ' 
            Me.tbSelectRectangle.ImageIndex = 1
            Me.tbSelectRectangle.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
            Me.tbSelectRectangle.ToolTipText = "Rectangle Selection"
            ' 
            ' tbSelectEllipse
            ' 
            Me.tbSelectEllipse.ImageIndex = 10
            Me.tbSelectEllipse.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
            Me.tbSelectEllipse.ToolTipText = "Ellipse Selection"
            ' 
            ' tbPan
            ' 
            Me.tbPan.ImageIndex = 2
            Me.tbPan.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
            Me.tbPan.ToolTipText = "Pan"
            ' 
            ' tbMagnifier
            ' 
            Me.tbMagnifier.ImageIndex = 6
            Me.tbMagnifier.ToolTipText = "Magnifier"
            ' 
            ' tbZoom
            ' 
            Me.tbZoom.ImageIndex = 7
            Me.tbZoom.ToolTipText = "Zoom"
            ' 
            ' tbZoomSelection
            ' 
            Me.tbZoomSelection.ImageIndex = 8
            Me.tbZoomSelection.ToolTipText = "Zoom Selection"
            ' 
            ' contextMenu1
            ' 
            Me.contextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuAutoZoomNone, Me.menuAutoZoomBestFit, Me.menuAutoZoomBestFitShrink, Me.menuAutoZoomFitWidth, Me.menuAutoZoomFitHeight})
            ' 
            ' menuAutoZoomNone
            ' 
            Me.menuAutoZoomNone.Index = 0
            Me.menuAutoZoomNone.Text = "None"
            '			Me.menuAutoZoomNone.Click += New System.EventHandler(Me.mnuAutoZoom_Click);
            ' 
            ' menuAutoZoomBestFit
            ' 
            Me.menuAutoZoomBestFit.Index = 1
            Me.menuAutoZoomBestFit.Text = "Best Fit"
            '			Me.menuAutoZoomBestFit.Click += New System.EventHandler(Me.mnuAutoZoom_Click);
            ' 
            ' menuAutoZoomBestFitShrink
            ' 
            Me.menuAutoZoomBestFitShrink.Index = 2
            Me.menuAutoZoomBestFitShrink.Text = "Best Fit Shrink Only"
            '			Me.menuAutoZoomBestFitShrink.Click += New System.EventHandler(Me.mnuAutoZoom_Click);
            ' 
            ' menuAutoZoomFitWidth
            ' 
            Me.menuAutoZoomFitWidth.Index = 3
            Me.menuAutoZoomFitWidth.Text = "Fit To Width"
            '			Me.menuAutoZoomFitWidth.Click += New System.EventHandler(Me.mnuAutoZoom_Click);
            ' 
            ' menuAutoZoomFitHeight
            ' 
            Me.menuAutoZoomFitHeight.Index = 4
            Me.menuAutoZoomFitHeight.Text = "Fit To Height"
            '			Me.menuAutoZoomFitHeight.Click += New System.EventHandler(Me.mnuAutoZoom_Click);
            ' 
            ' ellipseSelect
            ' 
            Me.ellipseSelect.ActiveButtons = System.Windows.Forms.MouseButtons.Left
            Me.ellipseSelect.AspectRatio = 0.0F
            Me.ellipseSelect.BackgroundColor = System.Drawing.Color.Transparent
            Me.ellipseSelect.ConstrainPosition = False
            Me.ellipseSelect.Fill = Nothing
            Me.ellipseSelect.MoveCursor = System.Windows.Forms.Cursors.SizeAll
            Me.ellipseSelect.Parent = Me.Viewer
            Me.ellipseSelect.Pen.Color = System.Drawing.Color.Black
            Me.ellipseSelect.Pen.CustomDashPattern = New Integer() {8, 8}
            Me.ellipseSelect.Persist = True
            Me.ellipseSelect.SnapToPixelGrid = False
            ' 
            ' rectangleSelect
            ' 
            Me.rectangleSelect.ActiveButtons = System.Windows.Forms.MouseButtons.Left
            Me.rectangleSelect.Animated = True
            Me.rectangleSelect.AspectRatio = 0.0F
            Me.rectangleSelect.BackgroundColor = System.Drawing.Color.White
            Me.rectangleSelect.Inverted = False
            Me.rectangleSelect.MoveCursor = System.Windows.Forms.Cursors.SizeAll
            Me.rectangleSelect.Parent = Me.Viewer
            Me.rectangleSelect.Pen.Color = System.Drawing.Color.Black
            Me.rectangleSelect.Pen.CustomDashPattern = New Integer() {8, 8}
            Me.rectangleSelect.Pen.LineStyle = Atalasoft.Imaging.Drawing.LineStyle.Custom
            Me.rectangleSelect.Persist = True
            Me.rectangleSelect.SelectionNESWCursor = System.Windows.Forms.Cursors.SizeNESW
            Me.rectangleSelect.SelectionNSCursor = System.Windows.Forms.Cursors.SizeNS
            Me.rectangleSelect.SelectionNWSECursor = System.Windows.Forms.Cursors.SizeNWSE
            Me.rectangleSelect.SelectionWECursor = System.Windows.Forms.Cursors.SizeWE
            ' 
            ' progressBar1
            ' 
            Me.progressBar1.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
            Me.progressBar1.Location = New System.Drawing.Point(8, 400)
            Me.progressBar1.Name = "progressBar1"
            Me.progressBar1.Size = New System.Drawing.Size(304, 16)
            Me.progressBar1.TabIndex = 5
            ' 
            ' cmbAutoZoom
            ' 
            Me.cmbAutoZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbAutoZoom.Items.AddRange(New Object() {"None", "Best Fit", "Best Fit Shrink Only", "Fit to Width", "Fit to Height"})
            Me.cmbAutoZoom.Location = New System.Drawing.Point(280, 2)
            Me.cmbAutoZoom.Name = "cmbAutoZoom"
            Me.cmbAutoZoom.Size = New System.Drawing.Size(121, 21)
            Me.cmbAutoZoom.TabIndex = 6
            '			Me.cmbAutoZoom.SelectedIndexChanged += New System.EventHandler(Me.cmbAutoZoom_SelectedIndexChanged);
            ' 
            ' thumbnailView1
            ' 
            Me.thumbnailView1.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
            Me.thumbnailView1.AutoDragDrop = True
            Me.thumbnailView1.BackColor = System.Drawing.SystemColors.ControlDark
            Me.thumbnailView1.CaptionLines = 0
            Me.thumbnailView1.DisplayText = Atalasoft.Imaging.WinControls.ThumbViewAttribute.None
            Me.thumbnailView1.DragSelectionColor = System.Drawing.Color.Red
            Me.thumbnailView1.DropPositionIndicator = Atalasoft.Imaging.WinControls.ThumbnailDropPositionIndicator.IBeam
            Me.thumbnailView1.ForeColor = System.Drawing.SystemColors.WindowText
            Me.thumbnailView1.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight
            Me.thumbnailView1.HighlightTextColor = System.Drawing.SystemColors.HighlightText
            Me.thumbnailView1.LoadErrorMessage = ""
            Me.thumbnailView1.LoadMethod = Atalasoft.Imaging.WinControls.ThumbLoadMethod.EntireFolder
            Me.thumbnailView1.Location = New System.Drawing.Point(0, 24)
            Me.thumbnailView1.Margins = New Atalasoft.Imaging.WinControls.Margin(4, 4, 4, 4)
            Me.thumbnailView1.SelectionMode = ThumbnailSelectionMode.MultiSelect
            Me.thumbnailView1.Name = "thumbnailView1"
            Me.thumbnailView1.SelectionRectangleBackColor = System.Drawing.Color.Transparent
            Me.thumbnailView1.SelectionRectangleDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
            Me.thumbnailView1.SelectionRectangleLineColor = System.Drawing.Color.Black
            Me.thumbnailView1.Size = New System.Drawing.Size(152, 366)
            Me.thumbnailView1.TabIndex = 0
            Me.thumbnailView1.ThumbnailBackground = Nothing
            Me.thumbnailView1.ThumbnailOffset = New System.Drawing.Point(0, 0)
            Me.thumbnailView1.ThumbnailSize = New System.Drawing.Size(100, 100)
            '			Me.thumbnailView1.ThumbnailDrop += New Atalasoft.Imaging.WinControls.ThumbnailDropEventHandler(Me.thumbnailView1_ThumbnailDrop);
            '			Me.thumbnailView1.ItemDrag += New System.Windows.Forms.ItemDragEventHandler(Me.thumbnailView1_ItemDrag);
            '			Me.thumbnailView1.SelectedIndexChanged += New System.EventHandler(Me.thumbnailView1_SelectedIndexChanged);
            ' 
            ' imagePrintDocument1
            ' 
            Me.imagePrintDocument1.ScaleMode = Atalasoft.Imaging.WinControls.PrintScaleMode.None
            '			Me.imagePrintDocument1.GetImage += New Atalasoft.Imaging.WinControls.PrintImageEventHandler(Me.imagePrintDocument1_GetImage);
            ' 
            ' printDialog1
            ' 
            Me.printDialog1.Document = Me.imagePrintDocument1
            ' 
            ' Form1
            ' 
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(456, 417)
            Me.Controls.Add(Me.thumbnailView1)
            Me.Controls.Add(Me.cmbAutoZoom)
            Me.Controls.Add(Me.progressBar1)
            Me.Controls.Add(Me.toolBar1)
            Me.Controls.Add(Me.Viewer)
            Me.Controls.Add(Me.statusBar1)
            Me.Menu = Me.mainMenu1
            Me.Name = "Form1"
            Me.Text = "Atalasoft dotImage Multipage TIFF Demo"
            CType(Me.statusMessage, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.statusPage, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub


        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread()> _
        Shared Sub Main()
            Application.Run(New Form1)
        End Sub
#End Region

#Region "File Menu Code"

        Private Sub menuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuExit.Click
            Me.Close()
        End Sub

        Private Sub menuOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuOpen.Click
            OpenImage()
        End Sub

        Private Sub OpenImage()
            ' Display the open file dialog.
            Dim openFile As OpenFileDialog = New OpenFileDialog
            openFile.Filter = "TIFF Images|*.tif;*.tiff"

            ' try to locate images folder
            Dim imagesFolder As String = Application.ExecutablePath
            ' we assume we are running under the DotImage install folder
            Dim pos As Integer = imagesFolder.IndexOf("DotImage ")
            If pos <> -1 Then
                imagesFolder = imagesFolder.Substring(0, imagesFolder.IndexOf("\", pos)) & "\Images"
            End If

            'use this folder as starting point			
            openFile.InitialDirectory = imagesFolder

            If openFile.ShowDialog() = DialogResult.OK Then
                Try
                    LoadPages(openFile.FileName)
                Catch err As Exception
                    MessageBox.Show(Me, err.Message)
                End Try
            End If
            openFile.Dispose()
        End Sub

        Private Sub LoadPages(ByVal fileName As String)
            'Copy image to temp file for instert/delete/remove functions.
            System.IO.File.Copy(fileName, Me.currentFile, True)
            ' use the temp file as the current file, so we don't change the opened file
            ' until it is saved.

            'clear the thumb list
            Me.thumbnailView1.Items.Clear()
            Me.currentPage = 1

            ' Use the TiffDecoder for more control and information.
            Dim decoder As TiffDecoder = New TiffDecoder

            'INSTANT VB NOTE: The following 'using' block is replaced by its pre-VB.NET 2005 equivalent:
            '			using (AtalaFileStream fs = New AtalaFileStream(fileName, FileMode.Open, FileAccess.Read))
            Dim fs As AtalaFileStream = New AtalaFileStream(fileName, FileMode.Open, FileAccess.Read)
            Try
                If fs Is Nothing Then
                    MessageBox.Show("We were unable to open the file.")
                    Return
                End If

                ' Check to see if the file is a valid TIFF.
                If decoder.IsValidFormat(fs) = False Then
                    MessageBox.Show("This is not a valid TIFF image.")
                    fs.Close()
                    Return
                End If

                ' Seek back to the beginning of the file.
                fs.Seek(0, SeekOrigin.Begin)

                ' Get the page count.
                Me.pageCount = decoder.GetFrameCount(fs)

                fs.Seek(0, SeekOrigin.Begin)
                ' Load thumbs asynchronously and open first page in viewer.
                Me.thumbnailView1.Asynchronous = True
                Dim i As Integer = 0
                'ORIGINAL LINE: for (int i = 0; i < Me.pageCount; i += 1)
                'INSTANT VB NOTE: This 'for' loop was translated to a VB 'Do While' loop:
                Do While i < Me.pageCount
                    Me.thumbnailView1.Items.Add(fileName, i, "Page " & (i + 1))
                    If i = 0 Then
                        Me.thumbnailView1.Items(i).Selected = True
                        ' Setting the thumbnail Selected property raises the
                        ' SelectedIndexChanged event.  We load the image there.
                        'Viewer.Image = new AtalaImage(fs, i, null);
                    End If
                    i += 1
                Loop
                fs.Close()
            Finally
                Dim disp As IDisposable = fs
                disp.Dispose()
            End Try
            'INSTANT VB NOTE: End of the original C# 'using' block
            ' Setup the Page menu.
            ResetPageMenuItems()
        End Sub

        Private Sub menuSaveAs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSaveAs.Click
            SaveImage()
        End Sub

        Private Sub SaveImage()
            If Me.imageChanged Then ' save this image first
                ' open the file for edit
                Dim localTempFile As String = System.IO.Path.GetTempFileName()

                Dim editFile As TiffFile = New TiffFile
                'INSTANT VB NOTE: The following 'using' block is replaced by its pre-VB.NET 2005 equivalent:
                '				using (FileStream fs = New FileStream(Me.currentFile, FileMode.Open, FileAccess.ReadWrite))
                Dim fs As FileStream = New FileStream(Me.currentFile, FileMode.Open, FileAccess.ReadWrite)
                Try
                    editFile.Read(fs)

                    editFile.Images(Me.currentPage - 1) = New TiffDirectory(Viewer.Image, New TiffEncoder)
                    'INSTANT VB NOTE: The following 'using' block is replaced by its pre-VB.NET 2005 equivalent:
                    '					using (FileStream tempFS = New FileStream(localTempFile, FileMode.Open, FileAccess.ReadWrite))
                    Dim tempFS As FileStream = New FileStream(localTempFile, FileMode.Open, FileAccess.ReadWrite)
                    Try
                        editFile.Save(tempFS)
                    Finally
                        Dim disp As IDisposable = tempFS
                        disp.Dispose()
                    End Try
                    'INSTANT VB NOTE: End of the original C# 'using' block
                Finally
                    Dim disp As IDisposable = fs
                    disp.Dispose()
                End Try
                'INSTANT VB NOTE: End of the original C# 'using' block

                File.Copy(localTempFile, Me.currentFile, True)
                Me.imageChanged = False
            End If

            ' Display the save editFile dialog.
            Dim saveFile As SaveFileDialog = New SaveFileDialog
            saveFile.Filter = "TIFF Images|*.tif;*.tiff"
            saveFile.DefaultExt = "tif"
            saveFile.AddExtension = True

            If saveFile.ShowDialog() = DialogResult.OK Then
                ' Copy from temp file to save file
                System.IO.File.Copy(Me.currentFile, saveFile.FileName, True)
                Me.statusMessage.Text = Me.currentFile
            End If
            saveFile.Dispose()
        End Sub

        'this code will get the frame index, either from the file, or from in memory
        Private Sub imagePrintDocument1_GetImage(ByVal sender As Object, ByVal e As Atalasoft.Imaging.WinControls.PrintImageEventArgs) Handles imagePrintDocument1.GetImage
            'only one image is loaded in memory at a time
            Dim fs As AtalaFileStream = Nothing
            Try
                If Me.currentPage - 1 <> e.ImageIndex Then
                    'load the frame from a file
                    fs = New AtalaFileStream(Me.currentFile, FileMode.Open, FileAccess.Read)
                    Dim image As AtalaImage = New AtalaImage(fs, e.ImageIndex, Nothing)
                    fs.Close()
                    e.Image = image
                Else
                    'can use the image in the viewer
                    e.Image = Viewer.Image
                End If
            Finally
                If Not fs Is Nothing Then
                    fs.Close()
                End If
            End Try

            'tell the print controller to print more images (or not)
            If e.ImageIndex = Me.pageCount - 1 Then
                e.HasMorePages = False
            Else
                e.HasMorePages = True
            End If
        End Sub

        Private Sub menuPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuPrint.Click
            ' Allow for a page range.
            Me.printDialog1.AllowSomePages = True
            Me.printDialog1.PrinterSettings.FromPage = 1
            Me.printDialog1.PrinterSettings.ToPage = Me.pageCount
            Me.printDialog1.PrinterSettings.MinimumPage = 1
            Me.printDialog1.PrinterSettings.MaximumPage = Me.pageCount

            If Me.printDialog1.ShowDialog(Me) = DialogResult.OK Then
                Me.imagePrintDocument1.ScaleMode = PrintScaleMode.FitToEdges
                Me.imagePrintDocument1.Print()
            End If
        End Sub

#End Region

#Region "AntialiasDisplayMode"

        Private Sub RemoveAntialiasCheckmarks()
            Me.menuAntiNone.Checked = False
            Me.menuAntiFull.Checked = False
            Me.menuAntiGray.Checked = False
            Me.menuAntiReduction.Checked = False
        End Sub

        Private Sub menuAnti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAntiNone.Click, menuAntiGray.Click, menuAntiReduction.Click, menuAntiFull.Click
            Dim item As MenuItem = CType(sender, MenuItem)
            Dim display As Atalasoft.Imaging.WinControls.AntialiasDisplayMode = CType(item.Index, Atalasoft.Imaging.WinControls.AntialiasDisplayMode)
            Viewer.AntialiasDisplay = display
            RemoveAntialiasCheckmarks()
            menuAntialiasDisplay.MenuItems(CInt(display)).Checked = True
        End Sub

#End Region

#Region "Pages Code"

        ' Rebuild the Pages menu.
        Private Sub ResetPageMenuItems()
            ' Remove the current menu items.
            If Not Me.pageMenus Is Nothing Then
                menuPages.MenuItems.Remove(Me.pageMenus)
                Me.pageMenus.MenuItems.Clear()
            Else
                Me.pageMenus = New MenuItem("Current Page")
            End If

            ' Add up to maxPageMenus menu items.
            Dim topItem As Integer
            If Me.pageCount > Me.maxPageMenus Then
                topItem = (Me.maxPageMenus)
            Else
                topItem = (Me.pageCount)
            End If

            Dim item As MenuItem = Nothing
            Dim i As Integer = 1
            'ORIGINAL LINE: for (int i = 1; i <= topItem; i += 1)
            'INSTANT VB NOTE: This 'for' loop was translated to a VB 'Do While' loop:
            Do While i <= topItem
                item = Me.pageMenus.MenuItems.Add("Page " & i.ToString(), Me.onPageMenuClick_Renamed)
                item.RadioCheck = True
                i += 1
            Loop

            ' Check the first page.
            Me.pageMenus.MenuItems(0).Checked = True

            ' Add the menu only if there is more than 1 page.
            If topItem > 1 Then
                Me.menuPages.MenuItems.Add(Me.pageMenus)
            End If
        End Sub

        ' Update the radio checkmarks.
        Private Sub SetPageMenu(ByVal index As Integer)
            If IsNothing(pageMenus) Or IsNothing(pageMenus.MenuItems) Then Return

            If Me.pageMenus.MenuItems.Count < 1 Then
                Return
            End If

            Dim i As Integer = 0
            'ORIGINAL LINE: for (int i = 0; i < Me.pageMenus.MenuItems.Count; i += 1)
            'INSTANT VB NOTE: This 'for' loop was translated to a VB 'Do While' loop:
            Do While i < Me.pageMenus.MenuItems.Count
                Me.pageMenus.MenuItems(i).Checked = False
                i += 1
            Loop

            Me.pageMenus.MenuItems(index).Checked = True

        End Sub

        ' Remove one menu item and rename the text.
        Private Sub RemovePageMenu(ByVal index As Integer)
            If Me.pageMenus.MenuItems.Count < 1 Then
                Return
            End If

            ' Because the page menu handler uses the menu text,
            ' all we have to do is remove the last menu item.
            Me.pageMenus.MenuItems.RemoveAt(Me.pageCount - 1)

            ' Remove the menu if there is only 1 item.
            If Me.pageMenus.MenuItems.Count < 2 Then
                Me.menuPages.MenuItems.Remove(Me.pageMenus)
                Me.pageMenus.MenuItems.Clear()
            End If
        End Sub

        ' Event handler for the Pages menu.
        Private Sub OnPageMenuClick(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim item As MenuItem = CType(sender, MenuItem)

            ' Get the page to load.
            ' Remember, the pages are zero based.
            Dim page As Integer = item.Index

            ' Update the statusbar.
            Me.currentPage = page + 1

            ' Since we are loading one page at a time, replace the current page.
            Viewer.Images.Clear()
            Viewer.Open(Me.currentFile, page)
            If Me.thumbnailView1.SelectionMode = ThumbnailSelectionMode.MultiSelect Or Me.thumbnailView1.SelectionMode = ThumbnailSelectionMode.MultiSelectNoKeys Then
                Me.thumbnailView1.ClearSelection()
            End If
            Me.thumbnailView1.Items(page).Selected = True
            Me.thumbnailView1.EnsureVisible(page)

            ' Update the menu.
            SetPageMenu(page)

        End Sub

        Private Sub menuAddPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAddPage.Click
            ' Add another page to the ImageCollection.
            Dim editFile As TiffFile = Nothing
            Dim fs As FileStream = Nothing
            Dim tempFS As FileStream = Nothing
            Dim localTempFile As String = System.IO.Path.GetTempFileName()
            Try
                fs = New FileStream(Me.currentFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)
                tempFS = New FileStream(localTempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)
                editFile = New TiffFile
                editFile.Read(fs)
            Catch ex As Exception
                MessageBox.Show(Me, ex.Message)
                If Not fs Is Nothing Then
                    fs.Close()
                End If
                If Not tempFS Is Nothing Then
                    tempFS.Close()
                End If
                Return
            End Try

            Dim openFile As OpenFileDialog = New OpenFileDialog
            openFile.Filter = "TIFF Images|*.tif;*.tiff"

            If openFile.ShowDialog() = DialogResult.OK Then
                Dim image As AtalaImage = New AtalaImage(openFile.FileName)
                If Not image Is Nothing Then
                    'this.Viewer.Images.Add(image);
                    editFile.Images.Add(New TiffDirectory(image))
                    fs.Seek(0, SeekOrigin.Begin)
                    editFile.Save(tempFS)

                    Me.pageCount += 1
                    Me.currentPage = Me.pageCount

                    Me.thumbnailView1.Items.Add(image, "Page " & Me.pageCount.ToString())

                    ' Make sure the menu has been added.
                    If Me.pageMenus Is Nothing OrElse Me.pageCount = 2 Then
                        ResetPageMenuItems()
                    Else
                        ' Add the menu item.
                        If Me.pageCount <= Me.maxPageMenus Then
                            Me.pageMenus.MenuItems.Add("Page " & Me.pageCount.ToString(), onPageMenuClick_Renamed)
                        End If
                    End If

                    fs.Close()
                    tempFS.Close()

                    ' copy temp file
                    System.IO.File.Copy(localTempFile, Me.currentFile, True)

                    ' We want to view the image we just added.
                    Me.thumbnailView1.Items(Me.currentPage - 1).Selected = True
                    'Viewer.Open(this.currentFile, this.currentPage - 1);

                    ' Update the Pages menu selection.
                    SetPageMenu(Me.currentPage - 1)


                End If

            End If

            openFile.Dispose()

            ' Update the statusbar.
            Me.statusPage.Text = "Page " & Me.currentPage.ToString() & " of " & Me.pageCount.ToString()
        End Sub

        Private Sub menuRemovePage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuRemovePage.Click
            ' Remove the current page.
            Dim fs As FileStream = Nothing
            Dim tempFS As FileStream = Nothing
            Dim editFile As TiffFile = Nothing
            Dim localTempFile As String = System.IO.Path.GetTempFileName()
            Try
                ' remove from temp image
                fs = New FileStream(Me.currentFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)
                tempFS = New FileStream(localTempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)
                editFile = New TiffFile
                editFile.Read(fs)
            Catch ex As Exception
                MessageBox.Show(Me, ex.Message)
                If Not fs Is Nothing Then
                    fs.Close()
                End If
                If Not tempFS Is Nothing Then
                    fs.Close()
                End If
                Return
            End Try


            editFile.Images.RemoveAt(Me.currentPage - 1)
            ' don't save it if the last page was deleted
            If editFile.Images.Count > 0 Then
                editFile.Save(tempFS)
            End If

            fs.Close()
            tempFS.Close()
            ' copy from temp stream.
            System.IO.File.Copy(localTempFile, Me.currentFile, True)

            Me.thumbnailView1.Items.RemoveAt(Me.currentPage - 1)

            ' Remove that menu item.
            RemovePageMenu(Me.currentPage - 1)

            Me.pageCount -= 1
            Me.currentPage -= 1

            ' Make sure the current page and viewed page are correct.
            If Me.pageCount > 0 Then
                If Me.currentPage = 0 Then
                    Me.currentPage = 1
                End If

                ' The image is loaded in the SelectedIndexChanged event.
                Me.thumbnailView1.Items(Me.currentPage - 1).Selected = True
                'Viewer.Open(this.currentFile,this.currentPage);
                SetPageMenu(Me.currentPage - 1)
            Else
                Viewer.Images.Clear()
                Me.toolBar1.Buttons(3).Enabled = False
            End If

            ' Update the statusbar.
            Me.statusPage.Text = "Page " & Me.currentPage.ToString() & " of " & Me.pageCount.ToString()
        End Sub

        Private Sub menuAppend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAppend.Click
            ' This will add the current image to an existing TIFF image.
            Dim openFile As OpenFileDialog = New OpenFileDialog
            openFile.Filter = "TIFF Images|*.tif;*.tiff"

            If openFile.ShowDialog() = DialogResult.OK Then
                Viewer.Image.Save(openFile.FileName, New TiffEncoder(Me.tiffCompression, True), Nothing)
            End If

            openFile.Dispose()
        End Sub

        Private Sub menuInsertPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuInsertPage.Click
            ' Insert a page into the ImageCollection.
            Dim fs As FileStream = Nothing
            Dim tempFS As FileStream = Nothing
            Dim editFile As TiffFile = Nothing
            Dim localTempFile As String = System.IO.Path.GetTempFileName()
            Try
                fs = New FileStream(Me.currentFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)
                tempFS = New FileStream(localTempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)
                editFile = New TiffFile
                editFile.Read(fs)
            Catch ex As Exception
                MessageBox.Show(Me, ex.Message)
                If Not fs Is Nothing Then
                    fs.Close()
                End If
                If Not tempFS Is Nothing Then
                    fs.Close()
                End If
                Return
            End Try

            Dim openFile As OpenFileDialog = New OpenFileDialog
            openFile.Filter = "TIFF Images|*.tif;*.tiff"

            If openFile.ShowDialog() = DialogResult.OK Then
                Dim image As AtalaImage = New AtalaImage(openFile.FileName)
                If Not image Is Nothing Then
                    'Viewer.Images.Insert(this.currentPage - 1, image);
                    editFile.Images.Insert(Me.currentPage - 1, New TiffDirectory(image))
                    editFile.Save(tempFS)
                    fs.Close()
                    tempFS.Close()
                    ' copy from temp stream.
                    System.IO.File.Copy(localTempFile, Me.currentFile, True)

                    Me.thumbnailView1.Items.Insert(Me.currentPage - 1, openFile.FileName, 0, "Page " & Me.currentPage.ToString(), "")
                    Me.pageCount += 1

                    ' Make sure the menu has been added.
                    If Me.pageMenus Is Nothing OrElse Me.pageCount = 2 Then
                        ResetPageMenuItems()
                    Else
                        ' Add the menu item.
                        If Me.pageCount <= Me.maxPageMenus Then
                            Me.pageMenus.MenuItems.Add("Page " & Me.pageCount.ToString(), onPageMenuClick_Renamed)
                        End If
                    End If

                    ' We want to view the image we just added.
                    Me.thumbnailView1.Items(Me.currentPage - 1).Selected = True
                    'Viewer.Open(this.currentFile,this.currentPage - 1);

                    ' Update the Pages menu selection.
                    SetPageMenu(Me.currentPage - 1)
                End If

            End If

            openFile.Dispose()

            ' Update the statusbar.
            Me.statusPage.Text = "Page " & Me.currentPage.ToString() & " of " & Me.pageCount.ToString()
        End Sub

#End Region

#Region "Metadata Code"

        Private Sub menuTiffTags_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuTiffTags.Click
            ' Display the tag info form.
            If Me.currentFile.Length = 0 Then
                MessageBox.Show(Me, "You need to load an image first.")
                Return
            End If

            Dim frm As TagInfo = New TagInfo(Me.currentFile, Me.currentPage - 1)
            frm.ShowDialog()
            frm.Dispose()
        End Sub

#End Region

#Region "TIFF Compression"

        ' Clear all of the radio checkmarks.
        Private Sub RemoveTiffCompressionCheckmarks()
            Me.menuCompressDeflate.Checked = False
            Me.menuCompressGroup3.Checked = False
            Me.menuCompressGroup4.Checked = False
            Me.menuCompressHuffman.Checked = False
            Me.menuCompressJpeg.Checked = False
            Me.menuCompressLzw.Checked = False
            Me.menuCompressMac.Checked = False
            Me.menuCompressNone.Checked = False
        End Sub

        Private Sub menuCompressNone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCompressNone.Click
            Me.tiffCompression = TiffCompression.NoCompression
            RemoveTiffCompressionCheckmarks()
            menuCompressNone.Checked = True
        End Sub

        Private Sub menuCompressDeflate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCompressDeflate.Click
            Me.tiffCompression = TiffCompression.Deflate
            RemoveTiffCompressionCheckmarks()
            menuCompressDeflate.Checked = True
        End Sub

        Private Sub menuCompressGroup3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCompressGroup3.Click
            Me.tiffCompression = TiffCompression.Group3FaxEncoding
            RemoveTiffCompressionCheckmarks()
            menuCompressGroup3.Checked = True
        End Sub

        Private Sub menuCompressGroup4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCompressGroup4.Click
            Me.tiffCompression = TiffCompression.Group4FaxEncoding
            RemoveTiffCompressionCheckmarks()
            menuCompressGroup4.Checked = True
        End Sub

        Private Sub menuCompressJpeg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCompressJpeg.Click
            Me.tiffCompression = TiffCompression.JpegCompression
            RemoveTiffCompressionCheckmarks()
            menuCompressJpeg.Checked = True
        End Sub

        Private Sub menuCompressLzw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCompressLzw.Click
            Me.tiffCompression = TiffCompression.Lzw
            RemoveTiffCompressionCheckmarks()
            menuCompressLzw.Checked = True
        End Sub

        Private Sub menuCompressMac_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCompressMac.Click
            Me.tiffCompression = TiffCompression.MacintoshPackbits
            RemoveTiffCompressionCheckmarks()
            menuCompressMac.Checked = True
        End Sub

        Private Sub menuCompressHuffman_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCompressHuffman.Click
            Me.tiffCompression = TiffCompression.ModifiedHuffman
            RemoveTiffCompressionCheckmarks()
            menuCompressHuffman.Checked = True
        End Sub

#End Region

#Region "Modify Menu"

        Private Sub CheckAndApplyCommand(ByVal cmd As ImageCommand, ByVal name As String)
            If Viewer Is Nothing OrElse Viewer.Image Is Nothing Then
                Return
            End If
            If (Not cmd.IsPixelFormatSupported(Viewer.Image.PixelFormat)) Then
                MessageBox.Show(cmd.GetType().Name & " can't be applied to an image of type " & System.Enum.GetName(GetType(PixelFormat), Viewer.Image.PixelFormat) & ".")
            Else
                Viewer.ApplyCommand(cmd, name)
            End If
        End Sub

        Private Sub menuFlipHorizontal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuFlipHorizontal.Click
            Me.imageChanged = True
            Dim flip As FlipCommand = New FlipCommand(FlipDirection.Horizontal)
            CheckAndApplyCommand(flip, "Horizontal Flip")
        End Sub

        Private Sub menuFlipVertical_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuFlipVertical.Click
            Me.imageChanged = True
            Dim flip As FlipCommand = New FlipCommand(FlipDirection.Vertical)
            CheckAndApplyCommand(flip, "Vertical Flip")
        End Sub

        Private Sub menuRotate90_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuRotate90.Click
            Me.imageChanged = True
            Dim rotate As RotateCommand = New RotateCommand(90)
            CheckAndApplyCommand(rotate, "Undo Rotate 90")
        End Sub

        Private Sub menuRotate180_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuRotate180.Click
            Me.imageChanged = True
            Dim rotate As RotateCommand = New RotateCommand(180)
            CheckAndApplyCommand(rotate, "Undo Rotate 180")
        End Sub

        Private Sub menuRotate270_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuRotate270.Click
            Me.imageChanged = True
            Dim rotate As RotateCommand = New RotateCommand(270)
            CheckAndApplyCommand(rotate, "Undo Rotate 270")
        End Sub

        Private Sub menuRotateCustom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuRotateCustom.Click
            Me.imageChanged = True
            Dim rotate As RotateCommand = New RotateCommand(1.5)
            CheckAndApplyCommand(rotate, "Undo Rotate 1.5")
        End Sub

        Private Sub menuMorpho_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuDilation.Click, menuErosion.Click, menuMorphoOpen.Click, menuMorphoClose.Click, menuBoundaryExtraction.Click
            Me.imageChanged = True
            ' This requires a 1-bit image.
            If Viewer.Image.PixelFormat <> PixelFormat.Pixel1bppIndexed Then
                MessageBox.Show(Me, "This command requires a 1-bit image.")
                Return
            End If

            Dim item As MenuItem = CType(sender, MenuItem)

            Dim mode As MorphoDocumentMode = CType(item.Index, MorphoDocumentMode)

            Dim morpho As MorphoDocumentCommand = New MorphoDocumentCommand(mode)
            If Not Viewer.Selection Is Nothing AndAlso Viewer.Selection.Visible Then
                morpho.RegionOfInterest = New RegionOfInterest(Viewer.Selection.GetRegion())
            End If
            CheckAndApplyCommand(morpho, "Undo Morpho")
        End Sub

        Private Sub menuUndo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuUndo.Click
            Viewer.Undos.Undo()
        End Sub

        Private Sub menuInvert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuInvert.Click
            Me.imageChanged = True
            Dim invert As InvertCommand = New InvertCommand
            CheckAndApplyCommand(invert, "Undo Invert")
        End Sub
        Private Sub mnuDeskew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeskew1.Click, mnuDeskew2.Click, mnuDeskew4.Click, mnuDeskew10.Click
            Me.imageChanged = True
            Try
                'get precision
                Dim precision As Integer = 1
                Dim item As MenuItem = CType(sender, MenuItem)
                Select Case item.Index
                    Case 0
                        precision = 1
                    Case 1
                        precision = 2
                    Case 2
                        precision = 4
                    Case 3
                        precision = 10
                End Select

                Dim deskew As AutoDeskewCommand = Nothing
                If Not Viewer.Selection Is Nothing AndAlso Viewer.Selection.Visible Then
                    deskew = New AutoDeskewCommand(Viewer.Selection.Bounds)
                Else
                    deskew = New AutoDeskewCommand
                End If

                deskew.Precision = precision

                CheckAndApplyCommand(deskew, "Undo Deskew")
            Catch err As Exception
                MessageBox.Show(Me, err.Message)
            End Try
        End Sub

        Private Sub menuDespeckle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuDespeckle.Click
            Me.imageChanged = True
            Dim despeckle As DocumentDespeckleCommand = New DocumentDespeckleCommand
            If Not Viewer.Selection Is Nothing AndAlso Viewer.Selection.Visible Then
                despeckle.RegionOfInterest = New RegionOfInterest(Viewer.Selection.GetRegion())
            End If
            CheckAndApplyCommand(despeckle, "Despeckle")
        End Sub

        Private Sub menuMedian_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuMedian.Click
            Me.imageChanged = True
            Dim median As DocumentMedianCommand = New DocumentMedianCommand(3)
            If Not Viewer.Selection Is Nothing AndAlso Viewer.Selection.Visible Then
                median.RegionOfInterest = New RegionOfInterest(Viewer.Selection.GetRegion())
            End If
            CheckAndApplyCommand(median, "Remove Noise")
        End Sub

        Private Sub menuThresholdingAdaptive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuThresholdingAdaptive.Click
            Me.imageChanged = True
            If Not Me.Viewer.Image Is Nothing Then
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As AdaptiveThresholdCommand = New AdaptiveThresholdCommand

                If Me.Viewer.MouseTool = MouseToolType.Selection AndAlso Me.Viewer.Selection.Visible Then
                    cmd.RegionOfInterest = New RegionOfInterest(Me.Viewer.Selection.Bounds)
                End If

                CheckAndApplyCommand(cmd, "Adaptive Thresholding")
                Me.Cursor = Cursors.Default
            End If
        End Sub

        Private Sub menuThresholdingGlobal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuThresholdingGlobal.Click
            Me.imageChanged = True
            If Not Me.Viewer.Image Is Nothing Then
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As GlobalThresholdCommand = New GlobalThresholdCommand

                If Me.Viewer.MouseTool = MouseToolType.Selection AndAlso Me.Viewer.Selection.Visible Then
                    cmd.RegionOfInterest = New RegionOfInterest(Me.Viewer.Selection.Bounds)
                End If

                CheckAndApplyCommand(cmd, "Global Thresholding")
                Me.Cursor = Cursors.Default
            End If
        End Sub

        Private Sub menuThresholdingDynamic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuThresholdingDynamic.Click
            Me.imageChanged = True
            If Not Me.Viewer.Image Is Nothing Then
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As DynamicThresholdCommand = New DynamicThresholdCommand

                If Me.Viewer.MouseTool = MouseToolType.Selection AndAlso Me.Viewer.Selection.Visible Then
                    cmd.RegionOfInterest = New RegionOfInterest(Me.Viewer.Selection.Bounds)
                End If

                CheckAndApplyCommand(cmd, "Dynamic Thresholding")
                Me.Cursor = Cursors.Default
            End If
        End Sub

        Private Sub menuThresholdingSimple_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuThresholdingSimple.Click
            Me.imageChanged = True
            If Not Me.Viewer.Image Is Nothing Then
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As SimpleThresholdCommand = New SimpleThresholdCommand

                If Me.Viewer.MouseTool = MouseToolType.Selection AndAlso Me.Viewer.Selection.Visible Then
                    cmd.RegionOfInterest = New RegionOfInterest(Me.Viewer.Selection.Bounds)
                End If

                CheckAndApplyCommand(cmd, "Simple Thresholding")
                Me.Cursor = Cursors.Default
            End If
        End Sub

        Private Sub menuBorderRemovalAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuBorderRemovalAll.Click
            Me.imageChanged = True
            If Not Me.Viewer.Image Is Nothing Then
                Dim cmd As BorderRemovalCommand = New BorderRemovalCommand(BorderRemovalEdges.AllSides, 5, False)
                CheckAndApplyCommand(cmd, "Border Removal")
            End If
        End Sub

        Private Sub menuBorderRemovalV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuBorderRemovalV.Click
            Me.imageChanged = True
            If Not Me.Viewer.Image Is Nothing Then
                Dim cmd As BorderRemovalCommand = New BorderRemovalCommand(BorderRemovalEdges.Vertical, 5, False)
                CheckAndApplyCommand(cmd, "Border Removal (Vertical)")
            End If
        End Sub

        Private Sub menuBorderRemovalH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuBorderRemovalH.Click
            Me.imageChanged = True
            If Not Me.Viewer.Image Is Nothing Then
                Dim cmd As BorderRemovalCommand = New BorderRemovalCommand(BorderRemovalEdges.Horizontal, 5, False)
                CheckAndApplyCommand(cmd, "Border Removal (Horizontal)")
            End If
        End Sub


#End Region

#Region "Form Events"

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles toolBar1.ButtonClick
            Select Case e.Button.ToolTipText
                Case "Open"
                    OpenImage()

                Case "Save"
                    SaveImage()

                Case "Previous"
                    If Me.currentPage > 1 Then
                        'load the next page from the file
                        Me.currentPage -= 1
                        Viewer.Open(Me.currentFile, Me.currentPage - 1)
                        ' change thumnail selection.
                        Me.thumbnailView1.Items(Me.currentPage).Selected = False
                        Me.thumbnailView1.Items(Me.currentPage - 1).Selected = True
                        Me.thumbnailView1.EnsureVisible(Me.currentPage - 1)
                    End If

                Case "Next"

                    If Me.currentPage < Me.pageCount Then
                        'load the next page from the file
                        Me.currentPage += 1
                        Viewer.Open(Me.currentFile, Me.currentPage - 1)
                        ' change thumnail selection.
                        Me.thumbnailView1.Items(Me.currentPage - 2).Selected = False
                        Me.thumbnailView1.Items(Me.currentPage - 1).Selected = True
                        Me.thumbnailView1.EnsureVisible(Me.currentPage - 1)
                    End If

                Case "Arrow"
                    Viewer.MouseTool = MouseToolType.None
                    tbSelectRectangle.Pushed = False
                    tbSelectEllipse.Pushed = False
                    tbPan.Pushed = False
                    tbArrow.Pushed = True
                    tbMagnifier.Pushed = False
                    tbZoom.Pushed = False
                    tbZoomSelection.Pushed = False
                Case "Rectangle Selection"
                    Viewer.MouseTool = MouseToolType.Selection
                    Viewer.Selection = Me.rectangleSelect
                    tbArrow.Pushed = False
                    tbPan.Pushed = False
                    tbSelectRectangle.Pushed = True
                    tbSelectEllipse.Pushed = False
                    tbMagnifier.Pushed = False
                    tbZoom.Pushed = False
                    tbZoomSelection.Pushed = False
                Case "Ellipse Selection"
                    Viewer.MouseTool = MouseToolType.Selection
                    Viewer.Selection = Me.ellipseSelect
                    tbArrow.Pushed = False
                    tbPan.Pushed = False
                    tbSelectRectangle.Pushed = False
                    tbSelectEllipse.Pushed = True
                    tbMagnifier.Pushed = False
                    tbZoom.Pushed = False
                    tbZoomSelection.Pushed = False
                Case "Pan"
                    Viewer.MouseTool = MouseToolType.Pan
                    tbArrow.Pushed = False
                    tbSelectRectangle.Pushed = False
                    tbSelectEllipse.Pushed = False
                    tbPan.Pushed = True
                    tbMagnifier.Pushed = False
                    tbZoom.Pushed = False
                    tbZoomSelection.Pushed = False
                Case "Magnifier"
                    Viewer.MouseTool = MouseToolType.Magnifier
                    tbArrow.Pushed = False
                    tbSelectRectangle.Pushed = False
                    tbSelectEllipse.Pushed = False
                    tbPan.Pushed = False
                    tbMagnifier.Pushed = True
                    tbZoom.Pushed = False
                    tbZoomSelection.Pushed = False
                Case "Zoom"
                    Viewer.MouseTool = MouseToolType.Zoom
                    tbArrow.Pushed = False
                    tbSelectRectangle.Pushed = False
                    tbSelectEllipse.Pushed = False
                    tbPan.Pushed = False
                    tbMagnifier.Pushed = False
                    tbZoom.Pushed = True
                    tbZoomSelection.Pushed = False
                Case "Zoom Selection"
                    Viewer.MouseTool = MouseToolType.ZoomArea
                    tbArrow.Pushed = False
                    tbSelectRectangle.Pushed = False
                    tbSelectEllipse.Pushed = False
                    tbPan.Pushed = False
                    tbMagnifier.Pushed = False
                    tbZoom.Pushed = False
                    tbZoomSelection.Pushed = True
            End Select
        End Sub


        Private Sub Viewer_Progress(ByVal sender As Object, ByVal e As Atalasoft.Imaging.ProgressEventArgs) Handles Viewer.Progress
            If e.Total = 0 Then
                e.Total = 1
            End If
            progressBar1.Value = e.Current * 100 / e.Total
            If progressBar1.Value = 100 Then
                progressBar1.Visible = False
                progressBar1.Value = 0
            ElseIf progressBar1.Value = 0 Then
                progressBar1.Visible = True
            End If
        End Sub

        Private Sub Viewer_ChangedImage(ByVal sender As Object, ByVal e As Atalasoft.Imaging.ImageEventArgs) Handles Viewer.ImageChanged
            ' Update the statusbar.
            statusPage.Text = "Page " & Me.currentPage.ToString() & " of " & Me.pageCount.ToString()
            statusMessage.Text = Me.currentFile

            'enable or disable previous/next buttons

            If Me.currentPage = 1 Then
                toolBar1.Buttons(3).Enabled = False
            Else
                toolBar1.Buttons(3).Enabled = True
            End If

            If Me.currentPage = Me.pageCount Then
                toolBar1.Buttons(4).Enabled = False
            Else
                toolBar1.Buttons(4).Enabled = True
            End If

            If Me.imageChanged Then ' image data changed, reload thumbs
                Me.thumbnailView1.Items.Update(Me.thumbnailView1.Items(Me.currentPage - 1), e.Image)
            End If
        End Sub

        Private Sub mnuAutoZoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAutoZoomNone.Click, menuAutoZoomBestFit.Click, menuAutoZoomBestFitShrink.Click, menuAutoZoomFitWidth.Click, menuAutoZoomFitHeight.Click
            For Each mitem As MenuItem In contextMenu1.MenuItems
                mitem.Checked = False
            Next mitem

            'set the AutoZoom mode based on menu pick
            Dim item As MenuItem = CType(sender, MenuItem)
            Dim zoomMode As AutoZoomMode = CType(item.Index, AutoZoomMode)
            Viewer.AutoZoom = zoomMode
            item.Checked = True
        End Sub


        Private Sub cmbAutoZoom_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAutoZoom.SelectedIndexChanged
            'set the AutoZoom mode based on menu pick
            Dim zoomMode As AutoZoomMode = CType(cmbAutoZoom.SelectedIndex, AutoZoomMode)
            Viewer.AutoZoom = zoomMode
            If zoomMode = AutoZoomMode.None Then Viewer.Zoom = 1
        End Sub

        Private Sub menuAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAbout.Click
            Dim aboutBox As AtalaDemos.AboutBox.About = New AtalaDemos.AboutBox.About("About Atalasoft DotImage Multipage TIFF Demo", "DotImage Multipage TIFF Demo")
            aboutBox.Description = "The Multipage Tiff Demo shows how to view and save multipage Tiff images using the TiffFile class, which allows editing of the image, without the need to open the entire image into memory or the need to re-encode the image data.  Some of this functionality includes adding, reordering, or removing pages from the image.  The demo also demonstrates many of the Document Imaging functions that are provided with DotImage.  In addition, this demo makes great use of the ThumbnailView control, displaying all pages in a multipage TIFF in the thumbnail control, as well as allowing the user to reorder and manipulate the individual pages with a nice GUI."
            aboutBox.ShowDialog()
        End Sub

        Private Sub thumbnailView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles thumbnailView1.SelectedIndexChanged
            If thumbnailView1.SelectedItems.Count > 0 Then
                If Me.imageChanged Then ' save this image first
                    ' open the file for edit
                    Dim localTempFile As String = System.IO.Path.GetTempFileName()

                    Dim editFile As TiffFile = New TiffFile
                    'INSTANT VB NOTE: The following 'using' block is replaced by its pre-VB.NET 2005 equivalent:
                    '					using (FileStream fs = New FileStream(Me.currentFile, FileMode.Open, FileAccess.ReadWrite))
                    Dim fs As FileStream = New FileStream(Me.currentFile, FileMode.Open, FileAccess.ReadWrite)
                    Try
                        editFile.Read(fs)
                        editFile.Images(Me.currentPage - 1) = New TiffDirectory(Viewer.Image, New TiffEncoder)
                        'INSTANT VB NOTE: The following 'using' block is replaced by its pre-VB.NET 2005 equivalent:
                        '						using (FileStream tempFS = New FileStream(localTempFile, FileMode.Open, FileAccess.ReadWrite))
                        Dim tempFS As FileStream = New FileStream(localTempFile, FileMode.Open, FileAccess.ReadWrite)
                        Try
                            editFile.Save(tempFS)
                        Finally
                            Dim disp As IDisposable = tempFS
                            disp.Dispose()
                        End Try
                        'INSTANT VB NOTE: End of the original C# 'using' block
                    Finally
                        Dim disp As IDisposable = fs
                        disp.Dispose()
                    End Try
                    'INSTANT VB NOTE: End of the original C# 'using' block

                    File.Copy(localTempFile, Me.currentFile, True)
                    Me.imageChanged = False
                End If
                ' load selected page
                Me.currentPage = Me.thumbnailView1.SelectedIndices(0) + 1
                Try
                    Viewer.Open(Me.currentFile, Me.currentPage - 1)
                    SetPageMenu(Me.currentPage - 1)
                Catch e1 As Exception
                    MessageBox.Show(Me, "Page " & (Me.currentPage - 1).ToString() & " is an invalid or corupt TIFF image.", "Error Loading Page")
                End Try
            End If
        End Sub

#End Region

#Region "Drag and Drop"

        Private Sub thumbnailView1_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles thumbnailView1.ItemDrag
            ' This event fires when a thumbnail drag operation is starting.
            Me.dragDropIndex = Me.thumbnailView1.SelectedIndices
        End Sub

        Private Sub thumbnailView1_ThumbnailDrop(ByVal sender As Object, ByVal e As Atalasoft.Imaging.WinControls.ThumbnailDropEventArgs) Handles thumbnailView1.ThumbnailDrop
            ' This event fires when a thumbnail has been dropped on the control.

            ' This demo doesn't handle dragging from one ThumbnailView to another.
            If Me.dragDropIndex.Length = 0 Then
                Return
            End If
            ' make sure we have a valid location
            If e.InsertIndex < 0 OrElse e.InsertIndex > Me.thumbnailView1.Items.Count Then
                ' do nothing
                Return
            End If

            ' Move the image in the WorkspaceViewer so it will match the ThumbnailView.
            Try
                Dim localTempFile As String = System.IO.Path.GetTempFileName()

                Dim editFile As TiffFile = New TiffFile
                'INSTANT VB NOTE: The following 'using' block is replaced by its pre-VB.NET 2005 equivalent:
                '				using (FileStream fs = New FileStream(Me.currentFile, FileMode.Open, FileAccess.ReadWrite))
                Dim fs As FileStream = New FileStream(Me.currentFile, FileMode.Open, FileAccess.ReadWrite)
                Try
                    editFile.Read(fs)

                    ' Remove original location
                    Dim theImage As TiffDirectory = editFile.Images(Me.dragDropIndex(0))
                    editFile.Images.RemoveAt(Me.dragDropIndex(0))
                    ' Insert at new location
                    If e.InsertIndex > editFile.Images.Count Then
                        editFile.Images.Insert((editFile.Images.Count), theImage)
                    Else
                        editFile.Images.Insert((e.InsertIndex), theImage)
                    End If

                    ' save the editFile
                    'INSTANT VB NOTE: The following 'using' block is replaced by its pre-VB.NET 2005 equivalent:
                    '					using (FileStream tempFS = New FileStream(localTempFile, FileMode.Open, FileAccess.ReadWrite))
                    Dim tempFS As FileStream = New FileStream(localTempFile, FileMode.Open, FileAccess.ReadWrite)
                    Try
                        editFile.Save(tempFS)
                    Finally
                        Dim disp As IDisposable = tempFS
                        disp.Dispose()
                    End Try
                    'INSTANT VB NOTE: End of the original C# 'using' block
                Finally
                    Dim disp As IDisposable = fs
                    disp.Dispose()
                End Try
                'INSTANT VB NOTE: End of the original C# 'using' block
                File.Copy(localTempFile, Me.currentFile, True)
            Catch ex As Exception
                MessageBox.Show(Me, ex.Message, "Error Updating ImageCollection")
            End Try

            Me.dragDropIndex = New Integer() {}
        End Sub

#End Region



    End Class
End Namespace
