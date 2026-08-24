Public Class frmMain
    Inherits System.Windows.Forms.Form

#Region "Structures"
    Public Structure Fileinfo
        Dim strFilename As String
    End Structure

    Public Structure User
        Dim Name As String
        Dim ConnectTime As String
        Dim IdleTime As Int16
        Dim strFiles() As Fileinfo
    End Structure

    Public Structure ComputerFileInfo
        Dim strName As String
        Dim strUsers() As User
    End Structure
#End Region
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents tvwConnections As System.Windows.Forms.TreeView
    Friend WithEvents btnGetConnections As System.Windows.Forms.Button
    Friend WithEvents txtComputer As System.Windows.Forms.TextBox
    Friend WithEvents radComputer As System.Windows.Forms.RadioButton
    Friend WithEvents radUser As System.Windows.Forms.RadioButton
    Friend WithEvents imlConnections As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMain))
        Me.tvwConnections = New System.Windows.Forms.TreeView
        Me.btnGetConnections = New System.Windows.Forms.Button
        Me.txtComputer = New System.Windows.Forms.TextBox
        Me.radComputer = New System.Windows.Forms.RadioButton
        Me.radUser = New System.Windows.Forms.RadioButton
        Me.imlConnections = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'tvwConnections
        '
        Me.tvwConnections.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwConnections.ImageIndex = 1
        Me.tvwConnections.ImageList = Me.imlConnections
        Me.tvwConnections.Location = New System.Drawing.Point(8, 56)
        Me.tvwConnections.Name = "tvwConnections"
        Me.tvwConnections.SelectedImageIndex = 7
        Me.tvwConnections.Size = New System.Drawing.Size(368, 368)
        Me.tvwConnections.Sorted = True
        Me.tvwConnections.TabIndex = 0
        '
        'btnGetConnections
        '
        Me.btnGetConnections.Location = New System.Drawing.Point(8, 8)
        Me.btnGetConnections.Name = "btnGetConnections"
        Me.btnGetConnections.Size = New System.Drawing.Size(104, 32)
        Me.btnGetConnections.TabIndex = 1
        Me.btnGetConnections.Text = "Enumerate Connections"
        '
        'txtComputer
        '
        Me.txtComputer.Location = New System.Drawing.Point(120, 8)
        Me.txtComputer.Name = "txtComputer"
        Me.txtComputer.Size = New System.Drawing.Size(136, 20)
        Me.txtComputer.TabIndex = 2
        Me.txtComputer.Text = ""
        '
        'radComputer
        '
        Me.radComputer.Checked = True
        Me.radComputer.Location = New System.Drawing.Point(264, 8)
        Me.radComputer.Name = "radComputer"
        Me.radComputer.Size = New System.Drawing.Size(112, 16)
        Me.radComputer.TabIndex = 3
        Me.radComputer.TabStop = True
        Me.radComputer.Text = "By Computer"
        '
        'radUser
        '
        Me.radUser.Location = New System.Drawing.Point(264, 32)
        Me.radUser.Name = "radUser"
        Me.radUser.Size = New System.Drawing.Size(112, 16)
        Me.radUser.TabIndex = 4
        Me.radUser.Text = "By User"
        '
        'imlConnections
        '
        Me.imlConnections.ImageSize = New System.Drawing.Size(18, 18)
        Me.imlConnections.ImageStream = CType(resources.GetObject("imlConnections.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlConnections.TransparentColor = System.Drawing.Color.FromArgb(CType(214, Byte), CType(8, Byte), CType(189, Byte))
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(384, 430)
        Me.Controls.Add(Me.radUser)
        Me.Controls.Add(Me.radComputer)
        Me.Controls.Add(Me.txtComputer)
        Me.Controls.Add(Me.btnGetConnections)
        Me.Controls.Add(Me.tvwConnections)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Connections"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnGetConnections_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetConnections.Click
        Dim oConnection As New clsConnection
        Dim OpenSessions() As clsConnection.OpenSessions
        Dim OpenFiles() As clsConnection.OpenFiles
        Dim i As Int16
        Dim j As Int16
        Dim strUsername As String
        Dim strComputername As String
        Dim strFilename As String
        Dim oComputerNode As Windows.Forms.TreeNode
        Dim oUserNode As Windows.Forms.TreeNode
        Dim oFileNode As Windows.Forms.TreeNode
        Dim oInfoNode As Windows.Forms.TreeNode
        Dim bComputerExists As Boolean
        Dim bUserExists As Boolean
        'Dim arrComputerFileInfo As New clsOpenFileInfo

        Me.tvwConnections.Nodes.Clear()

        oConnection.Computername = Me.txtComputer.Text

        Cursor = Cursors.WaitCursor
        OpenSessions = oConnection.GetConnections()
        OpenFiles = oConnection.GetOpenFiles()

        If oConnection.Status = "OK" Then
            ' Go through the list of connected users\computers
            For i = 0 To UBound(OpenSessions)
                strUsername = OpenSessions(i).strUsername
                strComputername = OpenSessions(i).strComputername
                For Each oComputerNode In Me.tvwConnections.Nodes

                    ' Check to see if this computername is listed already
                    If oComputerNode.Text = strComputername Then
                        bComputerExists = True ' Flag to indicate computername has been found

                        If strUsername <> "" Then
                            For Each oUserNode In oComputerNode.Nodes

                                ' Check to see if this username is listed already
                                If oUserNode.Text = strUsername Then
                                    bUserExists = True ' Flag to indicate username has been found
                                Else
                                    oUserNode = oComputerNode.Nodes.Add(strUsername)
                                    If Microsoft.VisualBasic.Right(strUsername, 1) = "$" Then ' This is a computer account
                                        oUserNode.ImageIndex = 0
                                    Else
                                        oUserNode.ImageIndex = 1
                                    End If

                                    oInfoNode = oUserNode.Nodes.Add("Idle time: " & OpenSessions(i).intIdleTime & " minutes")
                                    oInfoNode.ImageIndex = 4
                                    oInfoNode = oUserNode.Nodes.Add("Connected since: " & OpenSessions(i).strConnectTime)
                                    oInfoNode.ImageIndex = 4
                                End If
                            Next
                        End If

                        Exit For
                    End If
                Next
                If bComputerExists Then
                Else
                    oComputerNode = Me.tvwConnections.Nodes.Add(strComputername)
                    oComputerNode.ImageIndex = 0
                    If strUsername <> "" Then
                        oUserNode = oComputerNode.Nodes.Add(strUsername)
                        If Microsoft.VisualBasic.Right(strUsername, 1) = "$" Then ' This is a computer account
                            oUserNode.ImageIndex = 0
                        Else
                            oUserNode.ImageIndex = 1
                        End If

                        oInfoNode = oUserNode.Nodes.Add("Idle time: " & OpenSessions(i).intIdleTime & " minutes")
                        oInfoNode.ImageIndex = 4
                        oInfoNode = oUserNode.Nodes.Add("Connected since: " & OpenSessions(i).strConnectTime)
                        oInfoNode.ImageIndex = 4
                    End If
                End If
                bComputerExists = False
            Next i

            ' Go through the list of open files
            For i = 0 To UBound(OpenFiles)
                strFilename = OpenFiles(i).strPath
                strUsername = OpenFiles(i).strUsername
                For Each oComputerNode In Me.tvwConnections.Nodes ' Computernames
                    For Each oUserNode In oComputerNode.Nodes ' Usernames
                        If oUserNode.Text = strUsername Then
                            oFileNode = oUserNode.Nodes.Add(strFilename)
                            If strFilename Like "*spool*" Then ' Printer connection
                                oFileNode.ImageIndex = 6
                            ElseIf Microsoft.VisualBasic.Left(strFilename, 1) = "\" Then ' Service connection
                                oFileNode.ImageIndex = 5
                            Else ' File or folder connection
                                If Mid(strFilename, Len(strFilename) - 3, 1) = "." Then ' This is a file
                                    oFileNode.ImageIndex = 3
                                Else ' This is a folder
                                    oFileNode.ImageIndex = 2
                                End If
                            End If

                            bUserExists = True
                            Exit For
                        End If
                    Next
                    If bUserExists Then
                    Else
                        'oNode = Me.tvwConnections.Nodes
                        'oNode.Nodes.Add(strUsername)
                    End If
                    bUserExists = False
                Next
            Next
        End If

        oConnection = Nothing

        Cursor = Cursors.Default
    End Sub

    Private Sub radComputer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radComputer.CheckedChanged
        Me.tvwConnections.Nodes.Clear()
    End Sub

    Private Sub radUser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radUser.CheckedChanged
        Me.tvwConnections.Nodes.Clear()
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
