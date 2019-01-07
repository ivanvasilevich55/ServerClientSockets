Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Module cl

	Sub Main()
		Dim port As Integer = 8005
		Dim address As String = "127.0.0.1"
		Try
			Dim ipPoint As New IPEndPoint(IPAddress.Parse(address), port)
			Dim Socket As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
			Socket.Connect(ipPoint)
			Console.Write("Введите сообщение:")
			Dim message As String = Console.ReadLine()
			Dim data() As Byte = Encoding.Unicode.GetBytes(message)
			Socket.Send(data)
			ReDim data(256)
			Dim builder As New StringBuilder()
			Dim bytes As Integer = 0
			Do
				bytes = Socket.Receive(data, data.Length, 0)
				builder.Append(Encoding.Unicode.GetString(data, 0, bytes))
			Loop While (socket.Available > 0)
			Console.WriteLine("ответ сервера: " + builder.ToString())
			Socket.Shutdown(SocketShutdown.Both)
			Socket.Close()
		Catch ex As Exception
			Console.WriteLine(ex.Message)
		End Try
		Console.Read()
	End Sub

End Module
