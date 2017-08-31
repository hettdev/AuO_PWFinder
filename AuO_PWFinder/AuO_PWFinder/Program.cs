using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuO_PWFinder
{
    class Program
    {
        private static List<string> lineswithPassword = new List<string>();
        private static List<String> passwords = new List<string>();

        static void Main(string[] args)
        {
            
        
            bool found = false;

            while (!found)
            {
                Console.WriteLine("Please specify file to read");
                string file = Console.ReadLine();

                found = ReadLines(file);
            }

            int foundPasswords = ReadPasswords();

            Console.WriteLine(String.Format("Found {0} passwords, please specify file to save them to", foundPasswords));

            string pwFile = Console.ReadLine();

            SavePasswords(pwFile);

            Console.WriteLine("Saved passwords to {0}", pwFile);



            Environment.Exit(0);

        }

        private static bool SavePasswords(string filename)
        {
            //            System.IO.File.Create(filename);

            using (System.IO.FileStream fStream = System.IO.File.Create(filename))
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fStream))
                {
                    foreach (string pw in passwords)
                    {
                        writer.WriteLine(pw);
                    }
                }
            }

            return true;
        }

        private static int ReadPasswords()
        {
            foreach (string line in lineswithPassword)
            {
                string password = line.Substring("passwort:".Length);
                password = System.Text.RegularExpressions.Regex.Replace(password, @"\s+", "");
                passwords.Add(password);
            }

            return passwords.Count;

        }

        private static bool ReadLines(string filename)
        {

            string input;

            if (System.IO.File.Exists(filename) == true)
            {
                System.IO.StreamReader objectReader;
                objectReader = new System.IO.StreamReader(filename);

                while ((input = objectReader.ReadLine()) != null)
                {
                    if (input.ToLower().Contains("passwort:"))
                    {
                        lineswithPassword.Add(input);
                    }
                }
                objectReader.Close();

                return true;
            }
            else
            {
                Console.WriteLine(String.Format("No Such File {0}", filename));

                return false;
            }

        }
    }
}
