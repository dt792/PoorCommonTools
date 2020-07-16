using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PoorCommonTools
{
    public static class TextSaver
    {
        public static int DirUpOffsetNum = 2;
        private static string path;

        public static string Path
        {
            get { return path; }
            set
            {
                var UpStr = "";
                for (int i = 0; i < DirUpOffsetNum; i++)
                {
                    UpStr += "..\\";
                }
                path = UpStr + value;
            }
        }

        public static string FileName;
        public static string FileExtension = "txt";

        public static bool IsDirExist()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path);
            return directoryInfo.Exists;
        }

        public static void SaveText(string content, bool addMode)
        {
            if (addMode)
            {
                var UpStr = "";
                for (int i = 0; i < DirUpOffsetNum; i++)
                {
                    UpStr += "..\\";
                }
                FileInfo fileInfo = new FileInfo(UpStr + FileName + "." + FileExtension);
                if (fileInfo.Exists)
                    Console.WriteLine($"Add content after {FileName + "." + FileExtension}.");
                var sr = File.AppendText(fileInfo.FullName);
                sr.Write(content);
                sr.Close();
            }
            else
            {
                SaveText(content);
            }
        }
        public static void SaveText(string content)
        {
            var UpStr = "";
            for (int i = 0; i < DirUpOffsetNum; i++)
            {
                UpStr += "..\\";
            }
            FileInfo fileInfo = new FileInfo(UpStr+ FileName + "." + FileExtension);
            if (fileInfo.Exists)
                Console.WriteLine($"Cover {FileName + "." + FileExtension} file.");
            var sr = File.CreateText(fileInfo.FullName);
            sr.Write(content);
            sr.Close();


        }
    }
}
