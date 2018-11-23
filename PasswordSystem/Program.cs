using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordSystem
{
    class Program
    {
        static HashSet<User> users = new HashSet<User>();

        static void Main(string[] args)
        {
           Start();
        }

        static void Start()
        {
            Console.WriteLine("1 - Sign-in\n2 - Sign-up\nEsc - Exit");
            var option = Console.ReadKey(true);
            switch (option.Key)
            {
                case ConsoleKey.D1:
                    SignIn();
                    break;
                case ConsoleKey.D2:
                    SignUp();
                    break;
                case ConsoleKey.Escape:
                    break;
                default:
                    Console.WriteLine("\nOops...Try again!\n");
                    Start();
                    break;
            }
        }

        static void SignUp()
        {
            Console.WriteLine("Enter a login:");
            var login = Console.ReadLine();
            Console.WriteLine("Enter a password:");
            var password = Console.ReadLine();
            users.Add(new User(login, password));
            Start();
        }

        static void SignIn()
        {
            Console.WriteLine("Enter a login:");
            var login = Console.ReadLine();
            Console.WriteLine("Enter a password:");
            var password = Console.ReadLine();
            var user = new User(login, password);

            if(users.Contains(user))
            {
                Console.WriteLine("1 - Check your file\n2 - Add file");
                var option = Console.ReadKey(true);
                switch (option.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Let's check your file");
                        CheckArchive(password);
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("Let's add some files in zip");
                        AddFile(password);
                        break;
                    default:
                        Console.WriteLine("Oops try again");
                        break;
                }
            } else
            {
                Start();
            }
        }

        static void CreateZip(string password)
        {
            using (ZipFile zip = new ZipFile())
            {
                Console.WriteLine("Enter a zip name:");
                var zipName = Console.ReadLine();
                Console.WriteLine("Enter a file which you want to add in archive:");
                var fileName = Console.ReadLine();
                zip.Password = password;
                zip.Encryption = EncryptionAlgorithm.WinZipAes128;
                zip.AddFile(fileName);
                try
                {
                    zip.Save("../../" + zipName + ".zip");
                }
                catch (FileNotFoundException e)
                {
                    throw new FileNotFoundException($"There's no file {e.FileName}");
                }
                Console.WriteLine($"Archive '{zipName}.zip' was successfully created.");
            }
            Start();
        }

        static void AddFile(string password)
        {
            Console.WriteLine("Add some .txt file");
            Console.WriteLine("Enter a txt file name:");
            var fileName = Console.ReadLine();
            using (StreamWriter writer = new StreamWriter(fileName + ".txt"))
            {
                Console.Write("Write a text: ");
                string text = Console.ReadLine();
                writer.Write(text);
            }
            CreateZip(password);
        }

        static void CheckArchive(string password)
        {
            var zipFiles = Directory.EnumerateFiles("../../", "*zip");
            foreach (var file in zipFiles)
            {
                Console.WriteLine(file);
            }
            Console.WriteLine("Enter an archive name you want to open:");
            var fileName = Console.ReadLine();
            using (ZipFile zip = ZipFile.Read($"../../{fileName}.zip"))
            {
                zip.Password = password;
                zip.ExtractAll("../../");
                Console.WriteLine("Success!");
            }
        }
    }
}
