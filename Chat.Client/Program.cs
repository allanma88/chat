using Chat.Client;

Console.WriteLine("Please input command, type -h to help");

var client = new Client();

while (true)
{
    var line = Console.ReadLine();
    var parts = line.Split(" ");
    if (parts.Length == 0)
    {
        Console.WriteLine("unknown command, type -h to help");
    }
    else if (parts[0] == "connect")
    {
        if (parts.Length < 2)
        {
            Console.WriteLine("error command, type -h to help");
        }
        else
        {
            await client.ConnectAsync(parts[1]);
        }
    }
    else if (parts[0] == "send")
    {
        if (parts.Length < 3)
        {
            Console.WriteLine("error command, type -h to help");
        }
        else if (!client.IsConnected)
        {
            Console.WriteLine("not connected, please connect first, type -h to help");
        }
        else
        {
            await client.SendAsync(parts[1], parts[2]);
        }
    }
    else if (parts[0] == "exit" || parts[0] == "quit")
    {
        break;
    }
    else
    {
        Console.WriteLine("unknown command, type -h to help");
    }
}
