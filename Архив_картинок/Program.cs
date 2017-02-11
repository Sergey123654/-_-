using System;
using System.IO;
using System.IO.Compression;
namespace Photo_Archive
{ 
    class Picture
    {
        private string SourcePath { get; set; }
        private string TargetPath { get; set; }
        private string nameOfZip { get; set; }
        public Picture(string source, string target, string Name)
        {
            SourcePath = source;
            TargetPath = target;
            nameOfZip = Name;
        }
        public void CreatePhotoArchive()
        {
            string[]  files= Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                FileInfo fileInf = new FileInfo(file);
                if ((fileInf.Extension == ".png") ||
               (fileInf.Extension == ".jpg") ||
               (fileInf.Extension == ".bmp") ||
               (fileInf.Extension == ".jpeg"))
                {
                    AddToArchive(file, fileInf.Name);
                }
            }
        }
        private void AddToArchive(string PuthOfPicture,string nameOfPicture)
        {
            using (FileStream zipToOpen = new FileStream($"{TargetPath}\\{nameOfZip}.zip", FileMode.OpenOrCreate))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    archive.CreateEntryFromFile(PuthOfPicture,nameOfPicture);
                }
                zipToOpen.Close();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input path of source:");
            string sourcePath = Console.ReadLine();
            while (!Directory.Exists(sourcePath))
            {
                Console.WriteLine("Error!Input path of source:");
                sourcePath = Console.ReadLine();
            }
            Console.WriteLine("Input path of target:");
            string targetPath = Console.ReadLine();
            while (!Directory.Exists(targetPath))
            {
                Console.WriteLine("Error!Input path of target:");
                targetPath = Console.ReadLine();
            }
            Console.WriteLine("Input name of zip:");
            string nameOfZip = Console.ReadLine();
            Picture search = new Picture(sourcePath, targetPath, nameOfZip);
            search.CreatePhotoArchive();
            Console.ReadLine();
        }
    }
}
