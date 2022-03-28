using DanaAnalysis.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace DataAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var homePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                List<string> files = Directory.GetFiles($"{homePath}\\data\\in", "*.*", SearchOption.AllDirectories).ToList();

                if (files.Count == 0)
                {
                    Console.WriteLine("No files found");
                }
                else
                {
                    foreach (var file in files)
                    {
                        Console.WriteLine("Reading file");
                        if (".dat".Equals(file.Substring(file.Length - 4)))
                        {
                            // Encoding that can read ç
                            var encoding = Encoding.GetEncoding("iso-8859-1");

                            string streamContent = "";

                            using (StreamReader stream = new StreamReader(file, encoding))
                            {
                                streamContent = stream.ReadToEnd().Trim();
                            }

                            string[] rows = Regex.Split(streamContent, "\r\n", RegexOptions.None);

                            var analysis = new AnalysisService();
                            Console.WriteLine("Processing Lines");
                            analysis.RegisterRows(rows);

                            using (StreamWriter stream = new StreamWriter(file, false, encoding))
                            {
                                // Write the result of the analysis in the file
                                stream.Write(analysis.DataAnalysisResult());
                            }

                            // Get the file name
                            var filepathSplit = file.Split("\\");
                            var fullFileName = filepathSplit[filepathSplit.Length - 1];
                            var fileName = fullFileName.Substring(0, fullFileName.Length - 4);
                            var newName = DateTime.Now.Ticks;

                            var newFileName = file.Replace(fileName, newName.ToString()).Replace(".dat", ".done.dat");
                            var newPath = newFileName.Replace("in", "out");

                            Console.WriteLine($"Moving file to: {newPath}");
                            File.Move(file, newPath);
                        }
                    }
                }

                Thread.Sleep(10000);
                Console.Clear();
            }
        }
    }
}
