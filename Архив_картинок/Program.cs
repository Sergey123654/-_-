using System;
using System.IO;
using System.IO.Compression;
namespace Архив_картинок
{
    class SearchPicture
    {
        private string SourcePath {get; set;}
        private string TargetPath { get; set; }
        private string name { get; set; }
        public SearchPicture(string source, string target, string Name)
        {
            SourcePath = source;
            TargetPath = target;
            name = Name;
        }
        public void SearchAndCopy()
        {
            string new_catalog = TargetPath + @"\" + name;
            Directory.CreateDirectory(new_catalog);
            string[] files = Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                FileInfo fileInf = new FileInfo(file);
                if ((fileInf.Extension == ".png") ||
               (fileInf.Extension == ".jpg") ||
               (fileInf.Extension == ".bmp") ||
               (fileInf.Extension == ".jpeg"))
                {
                    string names = new_catalog + @"\" + fileInf.Name;
                    File.Copy(file, names, true);
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input path of source:");
            string source = Console.ReadLine();
            while (Directory.Exists(source) == false)
            {
                Console.WriteLine("Error!Input path of source:");
                source = Console.ReadLine();
            }
            Console.WriteLine("Input path of target:");
            string target = Console.ReadLine();
            while (Directory.Exists(target) == false)
            {
                Console.WriteLine("Error!Input path of target:");
                target = Console.ReadLine();
            }
            Console.WriteLine("Input name of zip:");
            string name = Console.ReadLine();
            SearchPicture s = new SearchPicture(source, target, name);
            s.SearchAndCopy();
            ZipFile.CreateFromDirectory((target + @"\" + name), target + @"\" + name + ".zip");
            Directory.Delete(target + @"\" + name);
            Console.ReadLine();
        }
    }
}
