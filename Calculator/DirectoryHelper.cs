using System;
using System.IO;
using System.Reflection;

namespace MEF_Calculator.Calculator
{
    internal static class DirectoryHelper
    {
        public static string GetAbsolutePath(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (Path.IsPathRooted(fileName))
            {
                return fileName;
            }

            string baseDirectory = GetBaseDirectory();
            fileName = Path.Combine(baseDirectory, fileName);

            return fileName;
        }

        /// <summary>
        /// Returns the base directory of the current running assembly.
        /// </summary>
        /// <returns></returns>
        public static string GetBaseDirectory()
        {
            string baseDirectory = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory?.FullName!;
            return baseDirectory;
        }
    }
}
