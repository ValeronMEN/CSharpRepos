using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpMetrics
{
    public class ClassObject
    {
        public string Name { get; }
        public List<MethodObject> Methodos { get; set; }
        public int PublicAttribCount { get; set; }
        public int PrivateAttribCount { get; set; }
        public List<string> Data { get; }
        public string[] Ancestors { get; }
        public int InheritedMethodsCount { set; get; }

        public int DIT { get; set; } // a length from the current class to the root class
        public int NOC { get; set; } // an amount of direct successors

        public ClassObject(string name, string[] ascentors, List<string> data)
        {
            PublicAttribCount = PrivateAttribCount = 0;
            this.Name = name.Trim();
            this.Ancestors = ascentors;
            this.Data = data;
            Methodos = new List<MethodObject>();
            if (Ancestors != null)
            {
                for(int i=0; i< Ancestors.Length; i++)
                {
                    Ancestors[i] = Ancestors[i].Trim();
                }
            }
            this.NOC = 0;
            InheritedMethodsCount = 0;
        }

        public void display()
        {
            Console.Write("class " + this.Name);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" NOC: " + this.NOC);
            Console.ForegroundColor = ConsoleColor.Gray;
            if (Ancestors != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" (ancestors: ");
                for (int i=0; i< Ancestors.Length; i++)
                {
                    Console.Write(Ancestors[i]);
                    if(i != Ancestors.Length - 1)
                    {
                        Console.Write(", ");
                    }
                }
                Console.Write(")");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine();
            foreach (MethodObject methodObj in Methodos)
            {
                Console.Write("* * * " + methodObj.Name);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" (hiding: " + methodObj.IsHid + ", overrided: " + methodObj.IsOverrided + ")");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }

    public class MethodObject
    {
        public string Name { get; }
        public bool IsOverrided { get; }
        public bool IsHid { get; }

        public MethodObject(string name, bool isOverrided, bool isHid)
        {
            this.Name = name;
            this.IsOverrided = isOverrided;
            this.IsHid = isHid;
        }
    }

    public class OpMetricsAnalyst
    {
        private int mDIT;

        private double mMHF;
        private double mAHF;
        private double mMIF;
        private double mAIF;
        private double mPOF;

        private List<ClassObject> classObjects = new List<ClassObject>();

        public OpMetricsAnalyst()
        {
            mMHF = mAHF = mMIF = mPOF = 0;
            mDIT = 0;
        }

        public int parseClassesFromDirectoryFiles(string dirPath, string fileType)
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
                    parseClassesFromOneFile(filePath);
                    Console.WriteLine("File name to handle: " + filePath);
                    fileCounter++;
                }
            }
            return fileCounter;
        }

        public void parseClassesFromOneFile(string filePath)
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader(filePath))
            {
                int lineNumber = 0;
                string line;

                string className = "";
                string ancestorsStr = "";
                string[] ancestors = null;
                List<string> data = new List<string>();
                bool isClass = false;
                int openBrackets = 0;
                int closeBrackets = 0;

                while ((line = file.ReadLine()) != null)
                {
                    if (isClass)
                    {
                        for(int i=0; i<line.Length; i++)
                        {
                            if (line[i] == '{')
                            {
                                openBrackets++;
                            }
                            else if (line[i] == '}')
                            {
                                closeBrackets++;
                                if (openBrackets == closeBrackets)
                                {
                                    string oldStr = line.Substring(0, i+1);
                                    data.Add(oldStr);
                                    line = line.Substring(i+1);
                                    classObjects.Add(new ClassObject(className, ancestors, data));

                                    className = "";
                                    ancestors = null;
                                    data = new List<string>();
                                    isClass = false;
                                    openBrackets = 0;
                                    closeBrackets = 0;
                                    break;
                                }
                            }
                        }
                        if (isClass)
                        {
                            data.Add(line);
                            continue;
                        }
                    }
                    if(line.Contains(" class "))
                    {
                        string lineWithClassReplaced = line.Replace("class ", "%");
                        int index = lineWithClassReplaced.IndexOf("%");
                        string lineWithoutPublicAndClass = lineWithClassReplaced.Substring(index + 1);
                        if (lineWithoutPublicAndClass.Contains("%"))
                        {
                            Console.WriteLine("More than 1 class declaration in " + lineNumber);
                            break;
                        }
                        if (lineWithoutPublicAndClass.Contains("{") && lineWithoutPublicAndClass.Contains("}"))
                        {
                            Console.WriteLine("Empty or one-line class in " + lineNumber);
                            continue;
                        }

                        bool isWord = true;
                        for (int i=0; i<lineWithoutPublicAndClass.Length; i++)
                        {
                            if (lineWithoutPublicAndClass[i] == '\n' || lineWithoutPublicAndClass[i] == '\n')
                            {
                                break;
                            }
                            else if (lineWithoutPublicAndClass[i] == '{')
                            {
                                openBrackets++;
                                break;
                            }
                            else if (lineWithoutPublicAndClass[i] == ':')
                            {
                                isWord = false;
                            }
                            else if(isWord)
                            {
                                className += lineWithoutPublicAndClass[i];
                            }
                            else
                            {
                                ancestorsStr += lineWithoutPublicAndClass[i];
                            }
                        }
                        if (!isWord)
                        {
                            ancestors = ancestorsStr.Split(',');
                        }
                        ancestorsStr = "";
                        isClass = true;
                        data.Add(line);
                    }
                    lineNumber++;
                }
                file.Close();
            }
        }

        public void parseMethodsInClasses()
        {
            foreach (ClassObject classObj in classObjects)
            {
                string[] classObjArr = classObj.Data.ToArray();
                for (int i=1; i<classObjArr.Length; i++)
                {
                    string methodName = "";
                    if ((classObjArr[i].Contains("public ") || classObjArr[i].Contains("private ") || classObjArr[i].Contains("protected ") || classObjArr[i].Contains("internal "))
                        && classObjArr[i].Contains("(") && classObjArr[i].Contains(")") && classObjArr[i].Contains('{') && !classObjArr[i].Contains('}'))
                    {
                        bool isName = false;
                        for (int j=classObjArr[i].Length - 1; j>=0; j--)
                        {
                            if(classObjArr[i][j] == '(' && !isName)
                            {
                                isName = true;
                            }
                            else if (classObjArr[i][j] == ' ' && isName)
                            {
                                break;
                            }
                            else if (isName)
                            {
                                methodName = classObjArr[i][j] + methodName;
                            }
                        }
                        bool isOverrided = false;
                        bool isHid = false;
                        if (classObjArr[i].Contains(" override "))
                            isOverrided = true;
                        if (classObjArr[i].Contains("private ") || classObjArr[i].Contains("protected ") || classObjArr[i].Contains("internal "))
                            isHid = true;
                        classObj.Methodos.Add(new MethodObject(methodName, isOverrided, isHid));
                    }
                    else if (classObjArr[i].Contains("private ") || classObjArr[i].Contains("protected ") || classObjArr[i].Contains("internal ")){
                        classObj.PrivateAttribCount++;
                    }
                    else if (classObjArr[i].Contains("public "))
                    {
                        classObj.PublicAttribCount++;
                    }
                }
            }
        }
          
        public void countMIF()
        {
            int inheritedAndNotOverridedMethods = 0;
            int allMethods = 0;
            foreach (ClassObject classObj in classObjects)
            {
                inheritedAndNotOverridedMethods += getInheritedAndNotOverridedMethods(classObj);
                allMethods += inheritedAndNotOverridedMethods;
                foreach (MethodObject methodObj in classObj.Methodos)
                {
                    if (!methodObj.IsOverrided)
                        allMethods++;
                }
            }
            mMIF = (double)inheritedAndNotOverridedMethods / allMethods;
        }

        public int getInheritedAndNotOverridedMethods(ClassObject classObj, bool IsAll = false)
        {
            int inheritedAndNotOverridedMethods = 0;
            if (classObj.Ancestors != null)
            {
                foreach (string ascentor in classObj.Ancestors)
                {
                    foreach (ClassObject classObjForAsc in classObjects)
                    {
                        if (classObjForAsc.Name == ascentor)
                        {
                            inheritedAndNotOverridedMethods += getInheritedAndNotOverridedMethods(classObjForAsc, true);
                            break;
                        }
                    }
                }
            }
            if (IsAll)
            {
                foreach (MethodObject methodObj in classObj.Methodos)
                {
                    if (!methodObj.IsOverrided)
                        inheritedAndNotOverridedMethods++;
                }
            }
            return inheritedAndNotOverridedMethods;
        }

        public void countAIF()
        {
            int inheritedAndNotOverridedAttributes = 0;
            int allAttributes = 0;
            foreach (ClassObject classObj in classObjects)
            {
                inheritedAndNotOverridedAttributes += getInheritedAndNotOverridedAttributes(classObj);
                allAttributes += inheritedAndNotOverridedAttributes;
                allAttributes += classObj.PrivateAttribCount;
                allAttributes += classObj.PublicAttribCount;
            }
            mAIF = (double)inheritedAndNotOverridedAttributes / allAttributes;
        }

        public int getInheritedAndNotOverridedAttributes(ClassObject classObj, bool IsAll = false)
        {
            int inheritedAndNotOverridedAttributes = 0;
            if (classObj.Ancestors != null)
            {
                foreach (string ascentor in classObj.Ancestors)
                {
                    foreach (ClassObject classObjForAsc in classObjects)
                    {
                        if (classObjForAsc.Name == ascentor)
                        {
                            inheritedAndNotOverridedAttributes += getInheritedAndNotOverridedAttributes(classObjForAsc, true);
                            break;
                        }
                    }
                }
            }
            if (IsAll)
            {
                inheritedAndNotOverridedAttributes += classObj.PrivateAttribCount;
                inheritedAndNotOverridedAttributes += classObj.PublicAttribCount;
            }
            return inheritedAndNotOverridedAttributes;
        }

        public void countNOC()
        {
            foreach (ClassObject classObj in classObjects)
            {
                if(classObj.Ancestors != null)
                {
                    foreach (string ascentor in classObj.Ancestors)
                    {
                        foreach (ClassObject classObjForAsc in classObjects)
                        {
                            if (classObjForAsc.Name == ascentor)
                            {
                                classObjForAsc.NOC++;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void countMHF()
        {
            int hidMethodsSum = 0;
            int openMethodsSum = 0;
            foreach (ClassObject classObj in classObjects)
            {
                foreach (MethodObject methodObj in classObj.Methodos)
                {
                    if (methodObj.IsHid)
                        hidMethodsSum++;
                    else
                        openMethodsSum++;
                }
            }
            mMHF = (double)hidMethodsSum / (hidMethodsSum + openMethodsSum);
        }

        public void countAHF()
        {
            int hidAttribSum = 0;
            int openAttribSum = 0;
            foreach (ClassObject classObj in classObjects)
            {
                hidAttribSum += classObj.PrivateAttribCount;
                openAttribSum += classObj.PublicAttribCount;
            }
            if (hidAttribSum == 0 && openAttribSum == 0)
            {
                Console.WriteLine("There aren't opened and hid attributes at all!");
                return;
            }
            mAHF = (double)hidAttribSum / (hidAttribSum + openAttribSum);
        }

        public void countDIT()
        {
            List<int> dits = new List<int>();
            dits.Add(0);
            foreach (ClassObject classObj in classObjects)
            {
                dits.Add(getDIT(classObj));
            }
            mDIT = 1 + dits.Max();
        }

        public int getDIT(ClassObject classObj, int level = 0)
        {
            if (classObj.Ancestors != null)
            {
                List<int> dits = new List<int>();
                dits.Add(0);
                foreach (string ascentor in classObj.Ancestors)
                {
                    foreach (ClassObject classObjForAsc in classObjects)
                    {
                        if (classObjForAsc.Name == ascentor)
                        {
                            dits.Add(getDIT(classObjForAsc, level+1));
                            break;
                        }
                    }
                }
                return dits.Max();
            }
            else
            {
                return level;
            }
        }

        // requires NOC data
        public void countPOF()
        {
            int overridedMethodsSum = 0;
            int newMethodsSum = 0;
            foreach (ClassObject classObj in this.classObjects)
            {
                int newMethodsCount = 0;
                foreach (MethodObject methodObj in classObj.Methodos)
                {
                    if (methodObj.IsOverrided)
                        overridedMethodsSum++;
                    else
                        newMethodsCount++;
                }
                newMethodsSum += newMethodsCount * classObj.NOC;
            }
            if (newMethodsSum == 0)
            {
                Console.WriteLine("In setting POF metric, error occured: newMethodsSum = 0");
                return;
            }
            mPOF = (double)overridedMethodsSum / newMethodsSum;
        }

        public void displayClassTree()
        {
            foreach(ClassObject classObj in classObjects)
            {
                classObj.display();
            }
        }

        public void displayMetrics()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("DIT: " + mDIT);
            Console.WriteLine("MHF: " + mMHF);
            Console.WriteLine("AHF: " + mAHF);
            Console.WriteLine("MIF: " + mMIF);
            Console.WriteLine("POF: " + mPOF);
            Console.WriteLine("AIF: " + mAIF);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
