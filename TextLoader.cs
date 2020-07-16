using System;
using System.Collections.Generic;
using System.IO;

namespace PoorCommonTools
{
    public static class TextLoader
    {
        public static int FileLookUpNum = 3;
        public static int DirLookUpNum = 3;
        //public static int MaxLookUpNum=-1;

        /// <summary>
        /// 给定文件名，如果无后缀名则自动添加.txt或手动指定
        /// 如无法从当前文件夹找到则自动向上查找（LookUpNum为向上的层数）
        /// 仍无法找到则返回null
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public static string LoadTextFromFile(string filePath, string fileExtension)
        {
            var dotPos = filePath.LastIndexOf('.');
            if (dotPos == -1)
            {
                filePath += "." + fileExtension;
            }
            try
            {
                File.ReadAllText(filePath);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Could not find dir({filePath}) in .exe Dir,try to look up {FileLookUpNum} folder.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Could not find txtfile({filePath}) in .exe Dir,try to look up {FileLookUpNum} folder.");
            }
            finally
            {
                var lookUpStr = "";
                for (int i = 0; i < FileLookUpNum; i++)
                {
                    lookUpStr += "..\\";
                }
                filePath = lookUpStr + filePath;
            }
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception)
            {
                Console.WriteLine("Still could not find file");
                return null;
            }

        }

        public static string LoadTextFromFile(string filePath)
        {
            string text;
            try
            {
                text = LoadTextFromFile(filePath, "txt");
            }
            catch (Exception)
            {
                return null;
            }
            return text;
        }

        /// <summary>
        /// 给定文件名列表返回文件内容（列表）
        /// 可通过指定文件后缀名自动添加后缀名
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static List<string> LoadTextsFromFiles(List<string> paths)
        {
            List<string> loadedTexts = new List<string>();
            foreach (var path in paths)
            {
                loadedTexts.Add(LoadTextFromFile(path));
            }
            return loadedTexts;
        }

        public static List<string> LoadTextsFromFiles(List<string> paths, string fileExtension)
        {
            List<string> loadedTexts = new List<string>();
            foreach (var path in paths)
            {
                loadedTexts.Add(LoadTextFromFile(path, fileExtension));
            }
            return loadedTexts;
        }

        /// <summary>
        /// 给定文件夹名，以及文件后缀名（或多个），返回文件夹下为指定后缀名的文件的内容
        /// 若不能在当前文件夹找到则自动向上查找（DirLookUpNum）
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        /// 
        public static List<(FileInfo, string)> LoadTextsInDir(string dirPath, string fileExtension)
        {
            List<(FileInfo, string)> results = new List<(FileInfo, string)>();
            DirectoryInfo folder;
            folder = new DirectoryInfo(dirPath);
            if (!folder.Exists)
            {
                Console.WriteLine($"Could not find dir({dirPath}) in .exe dir, try to look up {DirLookUpNum} folder.");
                var lookUpStr = "";
                for (int i = 0; i < DirLookUpNum; i++)
                {
                    lookUpStr += "..\\";
                }
                dirPath = lookUpStr + dirPath;
            }
            folder = new DirectoryInfo(dirPath);
            if (!folder.Exists)
            {
                Console.WriteLine($"Still could not find :/ .");
                return null;
            }

            foreach (var fileInfo in folder.GetFiles())
            {
                if (fileInfo.Extension == "." + fileExtension)
                {
                    var item = (fileInfo, File.ReadAllText(fileInfo.FullName));
                    results.Add(item);
                }
            }
            return results;
        }

        public static List<(FileInfo, string)> LoadTextsInDir(string dirPath)
        {
            return LoadTextsInDir(dirPath, "txt");
        }

        public static List<(FileInfo, string)> LoadTextsInDir(string dirPath, params string[] fileExtensions)
        {
            List<(FileInfo, string)> allResult = new List<(FileInfo, string)>();
            foreach (var fileExtension in fileExtensions)
            {
                allResult.AddRange(LoadTextsInDir(dirPath, fileExtension));
            }
            return allResult;
        }

    }
}
