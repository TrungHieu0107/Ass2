using System;
using BusinessObject.Models
namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FStoreDBContext db = new FStoreDBContext();
            var members = from member in db.Members select member;
            foreach (var member in members)
            {
                Console.WriteLine(member.ToString());
            }
            Console.ReadLine();
        }
    }
}
