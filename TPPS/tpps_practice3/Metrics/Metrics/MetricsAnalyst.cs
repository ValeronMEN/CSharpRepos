using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics
{
    class MetricsAnalyst
    {
        private List<string> linesList;

        public MetricsAnalyst()
        {
            linesList = new List<string>();
        }

        public int getSentencesFromDirectory(string dirPath, string fileType)
        {
            string[] filePaths = System.IO.Directory.GetFiles(dirPath);
            int fileCounter = 0;
            foreach (string filePath in filePaths)
            {
                bool toExit = false;
                int fileTypeIndex = fileType.Length - 1;
                for (int i = filePath.Length - 1; i >= 0; i--)
                {
                    if (filePath[i] != fileType[fileTypeIndex])
                    {
                        toExit = true;
                        break;
                    }
                    if (fileTypeIndex == 0)
                        break;
                    fileTypeIndex--;
                }
                if (toExit)
                {
                    continue;
                }
                else
                {
                    getSentencesFromOneFile(filePath);
                    Console.WriteLine("File name to handle: " + filePath);
                    fileCounter++;
                }
            }
            return fileCounter;
        }

        public void getSentencesFromOneFile(string filePath)
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader(filePath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    linesList.Add(line);
                }
                file.Close();
            }
        }

        public void createLinesFile(string filePath)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
            {
                foreach (string line in linesList)
                {
                    file.WriteLine(line);
                }
            }
        }

        public int getLinesOfCode()
        {
            return linesList.Count();
        }

        public int getBlankLinesOfCode()
        {
            int blankLinesCounter = 0;
            foreach (string line in linesList)
            {
                if (String.IsNullOrEmpty(line.Trim()))
                {
                    blankLinesCounter++;
                }
            }
            return blankLinesCounter;
        }

        public int getCommentsLinesOfCode()
        {
            int commentsLinesCounter = 0;
            bool isComment = false;
            foreach (string line in linesList)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '/' && !isComment)
                    {
                        if (line[i + 1] == '/')
                        {
                            commentsLinesCounter++;
                            break;
                        }
                        else if (line[i + 1] == '*')
                        {
                            commentsLinesCounter++;
                            i++;
                            isComment = true;
                            continue;
                        }
                    }
                    if (isComment && i == 0)
                    {
                        commentsLinesCounter++;
                    }
                    if (line[i] == '*' && line[i + 1] == '/')
                    {
                        isComment = false;
                        i++;
                        continue;
                    }
                }
            }
            return commentsLinesCounter;
        }

        public int getLogicalLinesOfCodeKeywords()
        {
            int counterLoc = 0;
            string[] keywords = new string[] { "if", "else", "for", "while", "switch", "break", "return", "goto", "continue", "exit", "throw", "try", "catch", "finally", "(", ";", "#" };
            foreach (string line in linesList)
            {
                foreach(string keyword in keywords)
                {
                    if (line.Contains(keyword))
                    {
                        counterLoc++;
                        break;
                    }
                }
            }
            return counterLoc;
        }
    }
}
