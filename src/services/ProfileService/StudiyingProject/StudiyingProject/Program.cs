using System.Text;

namespace StudiyingProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string testString = "tobeencoded";
            byte[] bytes = Encoding.UTF8.GetBytes(testString);
            Console.WriteLine(Encoding.UTF8.GetString(bytes, 0, bytes.Length));

            var a = testString.Length < 0 ? 543 * 3232 <  111 * 9229 ? 'c' : 'a' : 's';
            Console.WriteLine(a);


            Person person = new()
            {
                Id = 1,
                Name= "test",
            };
            
            foreach (var p in person.GetType().GetProperties())
            {
                Console.WriteLine(p);
                Console.WriteLine(p);
                Console.WriteLine(p);
            }

        }
    }
}