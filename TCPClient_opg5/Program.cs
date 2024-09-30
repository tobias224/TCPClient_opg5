using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;

Console.WriteLine("TCP Client:");

bool Sending = true;

TcpClient socket = new TcpClient("127.0.0.1", 7);

Console.WriteLine("Connected to server!");

using NetworkStream stream = socket.GetStream();
using StreamReader reader = new StreamReader(stream);
using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

while (Sending)
{
    Console.WriteLine("Enter command (add, subtract, random, stop): ");
    string method = Console.ReadLine().ToLower();

    if (method.ToLower() == "stop")
    {
        Sending = false;
    }

    Console.WriteLine(" enter num 1");
    int x = Int32.Parse(Console.ReadLine());
    Console.WriteLine(" enter num 2");
    int y = Int32.Parse(Console.ReadLine());

    var message = new { method = method, x = x, y = y };
    string jsonMessege = JsonSerializer.Serialize(message);
    writer.WriteLine(jsonMessege);

    string response = reader.ReadLine();
    var JsonResponse = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(response);
    int result = JsonResponse["result"].GetInt32();
    Console.WriteLine(result);
}

stream.Close();

