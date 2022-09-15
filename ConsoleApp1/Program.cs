namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] firstNames = new[] { "Jim", "Adam", "Bob" };


for (var index = 0; index < firstNames.Length; index++)
{
    var firstName = firstNames[index];
    Console.WriteLine(firstName);
}

            Console.ReadLine();

        }
    }


}