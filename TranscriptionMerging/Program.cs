using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TranscriptionMerging
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            StreamReader file = new StreamReader("D:\\backend-template\\ConsoleApplication1\\NewFile1.txt");
            List<string> lines = new List<string>();

            string inputline;
            while ((inputline = file.ReadLine()) != null)
            {
                if(inputline.Equals(String.Empty)) continue;
                if (!Int16.TryParse(inputline, out short _)) 
                {
                   lines.Add(inputline);   
                }
            }

            List<Chunk> chunkList = new List<Chunk>();
            for (int i = 0; i < lines.Count; i++)
            {
                Chunk chunk = new Chunk();
                chunk.startTime = lines[i].Split(new [] { "-->" }, StringSplitOptions.None)[0];
                
                StringBuilder content = new StringBuilder();
                Console.WriteLine(i);
                if (i == lines.Count - 1) // Short circuit
                {
                    break;
                }

                string chunkOf = lines[i + 1].Split(':')[0] + ":";
                
                // if (lines[i+1].StartsWith("David Lopez:"))
                // {
                //     chunkOf = "David Lopez:";
                // } else if (lines[i+1].StartsWith("Steven McCartney:"))
                // {
                //     chunkOf = "Steven McCartney:";
                // }
                // else
                // {
                //     chunkOf = "Na Fu:";
                // }
                content.Append(lines[i+1]);
                for (int j = i+3; j < lines.Count; j=j+2)
                {
                    if (lines[j].StartsWith(chunkOf))
                    {
                        content.Append(lines[j].Split(new [] { chunkOf }, StringSplitOptions.None)[1]);
                    }
                    else
                    {
                        chunk.endTime= lines[j-3].Split(new [] { "-->" }, StringSplitOptions.None)[1];
                        i = j - 2;
                        break;
                    }
                }

                chunk.chunkContent = content.ToString();
                chunkList.Add(chunk);
            }

            List<string> complete = new List<string>();
            foreach (Chunk chunk in chunkList)
            {   
                complete.Add(chunk.startTime + "-->" + chunk.endTime + "\n" + chunk.chunkContent + "\n");
            }

            File.WriteAllLines("D:\\backend-template\\ConsoleApplication1\\WriteLines2.txt", complete);
        }
    }

    internal  class Chunk
    {
        public string startTime = "";
        public string endTime = "";
        public string chunkContent = "";
    }
}


