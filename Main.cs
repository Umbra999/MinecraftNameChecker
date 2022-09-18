using System.Net;

namespace LabyNameChecker
{
    internal class Main
    {
        public static void Load()
        {
            Console.Title = "HexChecker | Labymod & Minecraft Name Checker";
            CheckNames();
        }

        private static void CheckNames()
        {
            string[] Names = File.ReadAllLines("Names.txt");
            List<string> Available = new();
            int index = 1;
            foreach (string Name in Names)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Checking Account: {Name} [{index++}/{Names.Length - 1}]");
                HttpClient Client = new(new HttpClientHandler { UseCookies = false });
                Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.79 Safari/537.36");
                HttpRequestMessage Payload = new(HttpMethod.Get, $"https://api.mojang.com/users/profiles/minecraft/{Name}");
                HttpResponseMessage Resp = Client.Send(Payload);
                if (Resp.StatusCode == (HttpStatusCode)204)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Found Account: {Name} [{index++}/{Names.Length - 1}]");
                    Available.Add(Name);
                }
                Thread.Sleep(1000);
            }
            File.WriteAllLines("Available.txt", Available.ToArray());
        }
    }
}
