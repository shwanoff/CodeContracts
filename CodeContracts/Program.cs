using System;

namespace CodeContracts
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client("John", 1000);
            var message = client.Bay("Pizza", 10);
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}
