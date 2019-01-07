Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Module sv

	Sub Main()
		Dim port As Integer = 8005
		Dim ipPoint As New IPEndPoint(IPAddress.Parse("127.0.0.1"), port)
		Dim listenSocket As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
		Try
			listenSocket.Bind(ipPoint)
			listenSocket.Listen(10)
			Console.WriteLine("Сервер запущен. Ожидание подключений...")
			While (True)
				Dim handler As Socket = listenSocket.Accept
				Dim builder As New StringBuilder()
				Dim bytes As Integer = 0
				Dim data(256) As Byte
				Do
					bytes = handler.Receive(data)
					builder.Append(Encoding.Unicode.GetString(data, 0, bytes))
				Loop While (handler.Available > 0)
				Console.WriteLine(DateTime.Now.ToShortTimeString() & ": " & builder.ToString())
				Dim message As String = "Ваше сообщение доставленно"
				data = Encoding.Unicode.GetBytes(message)
				handler.Send(data)
				handler.Shutdown(SocketShutdown.Both)
				handler.Close()
			End While
		Catch ex As Exception
			Console.WriteLine(ex.Message)
		End Try
	End Sub

End Module
