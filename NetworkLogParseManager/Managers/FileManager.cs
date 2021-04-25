using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Managers
{
    public class FileManager
    {
        public string GetDirectoryPath(string path)
        {
            int lastIndex = path.Replace("/", "\\").LastIndexOf("\\");
            if(lastIndex > 0)
            {
                return path.Substring(0, lastIndex);
            }
            return "./";
        }

        public void CreateDirectory(string filePath)
        {
            try
            {
                Directory.CreateDirectory(GetDirectoryPath(filePath));
            }
            catch
            {
                throw;
            }
        }

        public void DeleteFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
