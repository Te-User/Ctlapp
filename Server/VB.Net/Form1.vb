Imports System.Net
Imports System.Net.Sockets
Imports System.Threading

Public Class Form1
    Private listener As System.Net.Sockets.TcpListener
    Private listenThread As System.Threading.Thread
    Private clients As New List(Of ConnectedClient)
    Public NameServer As String


    Public Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Logs.Text += "Starting server..."
        listener = New System.Net.Sockets.TcpListener(System.Net.IPAddress.Any, 7852)
        listener.Start()
        listenThread = New System.Threading.Thread(AddressOf doListen)
        listenThread.IsBackground = True
        listenThread.Start()
        Logs.AppendText("OK")
    End Sub


    Private Sub doListen()
        Dim incomingClient As System.Net.Sockets.TcpClient
        Do
            incomingClient = listener.AcceptTcpClient 'Accept the incoming connection. This is a blocking method so execution will halt here until someone tries to connect.
            Dim connClient As New ConnectedClient(incomingClient, Me) 'Create a new instance of ConnectedClient (check its constructor to see whats happening now).
            AddHandler connClient.dataReceived, AddressOf Me.messageReceived
            clients.Add(connClient) 'Adds the connected client to the list of connected clients.
        Loop
    End Sub
    Public Sub removeClient(ByVal client As ConnectedClient)
        If clients.Contains(client) Then
            clients.Remove(client)
        End If
    End Sub
    Private Sub messageReceived(ByVal sender As ConnectedClient, ByVal message As String)
        MsgBox(message)
    End Sub
    Private Function GetClientByName(ByVal name As String) As ConnectedClient
        For Each cc As ConnectedClient In clients
            If cc.Username = name Then
                Return cc
            End If
        Next
        Return Nothing
    End Function

    Private Sub Logs_TextChanged(sender As Object, e As EventArgs) Handles Logs.TextChanged

    End Sub
End Class

Public Class ConnectedClient
    Private mClient As System.Net.Sockets.TcpClient
    Private mUsername As String
    Private mParentForm As Form1
    Private readThread As System.Threading.Thread
    Private Const MESSAGE_DELIMITER As Char = ControlChars.Cr
    Public Event dataReceived(ByVal sender As ConnectedClient, ByVal message As String)

    Sub New(ByVal client As System.Net.Sockets.TcpClient, ByVal parentForm As Form1)
        mParentForm = parentForm
        mClient = client
        readThread = New System.Threading.Thread(AddressOf doRead)
        readThread.IsBackground = True
        readThread.Start()
    End Sub

    Public Property Username() As String
        Get
            Return mUsername
        End Get
        Set(ByVal value As String)
            mUsername = value
        End Set
    End Property

    Public Sub doRead()
        Const BYTES_TO_READ As Integer = 255
        Dim readBuffer(BYTES_TO_READ) As Byte
        Dim bytesRead As Integer
        Dim sBuilder As New System.Text.StringBuilder
        Do
            bytesRead = mClient.GetStream.Read(readBuffer, 0, BYTES_TO_READ)
            If (bytesRead > 0) Then
                Dim message As String = System.Text.Encoding.UTF8.GetString(readBuffer, 0, bytesRead)
                goAdd(message)
            End If
        Loop
    End Sub
    Public Sub goAdd(ByVal Message As String)
        AppendTextBox(mParentForm.Logs, "RECV: " & Message)
        Parser(Message)
    End Sub
    Private Delegate Sub AppendTextBoxDelegate(ByVal TB As TextBox, ByVal txt As String)
    Private Sub AppendTextBox(ByVal TB As TextBox, ByVal txt As String)
        If TB.InvokeRequired Then
            TB.Invoke(New AppendTextBoxDelegate(AddressOf AppendTextBox), New Object() {TB, vbCrLf & txt})
        Else
            TB.AppendText(vbCrLf & txt)
        End If
    End Sub
    Public Sub Parser(ByVal txt As String)
        Select Case txt.Substring(0, 1)
            Case "L"
                If txt.Substring(1, 1) = "A" Then
                    Dim getVals = txt.Substring(2)
                    AppendTextBox(mParentForm.Logs, "Ask log in by client .")
                    mParentForm.NameServer = getVals
                    SendMessage("L1")
                End If
            Case "F"
                Select Case txt.Substring(1, 1)
                    Case "N"
                        ' Notification.
                    Case "M"
                        ' Message box WIN.
                        Dim getVals = txt.Substring(2).Split("|")
                        Dim typeMsgbox = New MsgBoxStyle
                        If getVals(2) = "1" Then
                            MsgBox(getVals(1), MsgBoxStyle.Critical, getVals(0))
                        ElseIf getVals(2) = "2" Then
                            MsgBox(getVals(1), MsgBoxStyle.Question, getVals(0))
                        ElseIf getVals(2) = "3" Then
                            MsgBox(getVals(1), MsgBoxStyle.Exclamation, getVals(0))
                        Else
                            MsgBox(getVals(1), MsgBoxStyle.Information, getVals(0))
                        End If
                    Case Else
                        AppendTextBox(mParentForm.Logs, "No functions detected for " & txt.Substring(1, 1) & ".")
                End Select
            Case Else
                'Nada.
        End Select
        txt = ""
    End Sub

    Private Sub SendMessage(ByVal msg As String)
        Dim sw As IO.StreamWriter
        Try
            SyncLock mClient.GetStream
                sw = New IO.StreamWriter(mClient.GetStream)
                sw.Write(msg & Chr(0))
                sw.Flush()
                AppendTextBox(mParentForm.Logs, "SEND : " & msg)
            End SyncLock
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
          End Sub
End Class
