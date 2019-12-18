using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marchenko_Lab3
{
    public struct SyntaxError
    {
        public string expected;
    }

    public class Node
    {
        public Lexem Lexem { get; set; }
        public string Nnorm { get; set; }
        public bool IsTerminal { get; set; }
        public bool IsError { get; set; }
        public int Level { get; set; }
        public List<Node> Children { get; set; }

        public Node(int level, Lexem lexem)
        {
            this.Level = level;
            this.Lexem = lexem;
            this.Nnorm = "";
            this.IsTerminal = true;
            this.IsError = false;
            Children = new List<Node>();
        }

        public Node(int level, string nodeNameOrErrorMessage, bool isError = false)
        {
            this.Level = level;
            this.Nnorm = nodeNameOrErrorMessage;
            this.IsTerminal = false;
            this.IsError = isError;
            Children = new List<Node>();
        }

        public void display()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            for (int i = 0; i < Level; i++)
            {
                Console.Write("*   ");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            if (IsTerminal)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(Lexem.value + " ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(Lexem.code);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(" (" + Lexem.line + "," + Lexem.column + ")");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (IsError)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.WriteLine(Nnorm);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.WriteLine(Nnorm);
            }
            foreach (Node item in Children)
            {
                item.display();
            }
        }
    }

    public class SyntaxAnalyst
    {
        private int lexemListPos;
        Dictionary<string, Dictionary<string, int>> tables;
        public List<Lexem> LexemList { get; set; }
        public Node Tree;
        public event Action Error;
        SyntaxError err = new SyntaxError();
        public bool IsError { get; set; }

        public SyntaxAnalyst(Dictionary<string, Dictionary<string, int>> tables, List<Lexem> lexemList)
        {
            int initialNodeLevel = 0;
            this.Tree = new Node(initialNodeLevel, "<signal-program>");
            this.LexemList = lexemList;
            this.tables = tables;
            this.lexemListPos = 0;
            IsError = false;
        }

        public void display()
        {
            Tree.display();
        }

        public Node getTree()
        {
            return this.Tree;
        }

        public bool start()
        {
            return signalProgram(Tree);
        }

        private void Errors(SyntaxError err) {
            Error?.Invoke();
            IsError = true;
        }

        private void checkEndOfFile(Node node)
        {
            if (lexemListPos >= LexemList.Count)
            {
                err.expected = "EOF";
                node.Children.Add(new Node(node.Level + 1, err.expected));
                Errors(err);
            }
        }
        
        private bool signalProgram(Node node)
        {
            node.Children.Add(new Node(node.Level + 1, "<program>"));
            return program(node.Children[0]);
        }
        
        private bool program(Node node)
        {
            checkEndOfFile(node);
            if (LexemList[lexemListPos].code == tables["Keywords"]["PROGRAM"])
            {
                node.Children.Add(new Node(node.Level + 1, LexemList[lexemListPos]));
                lexemListPos++;
                node.Children.Add(new Node(node.Level + 1, "<procedure-identifier>"));
                if (procedureIdentifier(node.Children[1]))
                {
                    checkEndOfFile(node);
                    if (LexemList[lexemListPos].code == tables["Delimiters"][";"])
                    {
                        node.Children.Add(new Node(node.Level + 1, LexemList[lexemListPos]));
                        lexemListPos++;
                        node.Children.Add(new Node(node.Level + 1, "<block>"));
                        if (block(node.Children[3]))
                        {
                            checkEndOfFile(node);
                            if (LexemList[lexemListPos].code == tables["Delimiters"]["."])
                            {
                                node.Children.Add(new Node(node.Level + 1, LexemList[lexemListPos]));
                                lexemListPos++;
                                return true;
                            }
                            err.expected = "delimiter '.'";
                            node.Children.Add(new Node(node.Level + 1, "Error: " + err.expected + " expected", true));
                            Errors(err);
                            return false;
                        }
                        return false;
                    }
                    err.expected = "delimiter ';'";
                    node.Children.Add(new Node(node.Level + 1, "Error: " + err.expected + " expected", true));
                    Errors(err);
                    return false;
                }
                return false;
            }
            err.expected = "keyword 'PROGRAM'";
            node.Children.Add(new Node(node.Level + 1, "Error: " + err.expected + " expected", true));
            Errors(err);
            return false;
        }
        
        private bool block(Node node)
        {
            node.Children.Add(new Node(node.Level + 1, "<declarations>"));
            if (declarations(node.Children[0]))
            {
                checkEndOfFile(node);
                if (LexemList[lexemListPos].code == tables["Keywords"]["BEGIN"])
                {
                    node.Children.Add(new Node(node.Level + 1, LexemList[lexemListPos]));
                    lexemListPos++;
                    node.Children.Add(new Node(node.Level + 1, "<statements-list>"));
                    if (statementsList(node.Children[2]))
                    {
                        checkEndOfFile(node);
                        if (LexemList[lexemListPos].code == tables["Keywords"]["END"])
                        {
                            node.Children.Add(new Node(node.Level + 1, LexemList[lexemListPos]));
                            lexemListPos++;
                            return true;
                        }
                        err.expected = "keyword 'END'";
                        node.Children.Add(new Node(node.Level + 1, "Error: " + err.expected + " expected", true));
                        Errors(err);
                        return false;
                    }
                    return false;
                }
                err.expected = "keyword 'BEGIN'";
                node.Children.Add(new Node(node.Level + 1, "Error: " + err.expected + " expected", true));
                Errors(err);
                return false;
            }
            return false;
        }
        
        private bool statementsList(Node node)
        {
            node.Children.Add(new Node(node.Level + 1, "<empty>"));
            return true;
        }
        
        private bool declarations(Node node)
        {
            node.Children.Add(new Node(node.Level + 1, "<math-function-declarations>"));
            return mathFunctionDeclarations(node.Children[0]);
        }
        
        private bool mathFunctionDeclarations(Node node) {
            if (LexemList[lexemListPos].code == tables["Keywords"]["DEFFUNC"])
            {
                node.Children.Add(new Node(node.Level + 1, LexemList[lexemListPos]));
                lexemListPos++;
                node.Children.Add(new Node(node.Level + 1, "<function-list>"));
                if (functionList(node.Children[1]))
                {
                    checkEndOfFile(node);
                    return true;
                }
                return false;
            }
            else
            {
                node.Children.Clear();
                node.Children.Add(new Node(node.Level + 1, "<empty>"));
                return true;
            }
        }
        
        private bool functionList(Node node) {
            node.Children.Add(new Node(node.Level + 1, "<function>"));
            if (function(node.Children[0]))
            {
                checkEndOfFile(node);
                node.Children.Add(new Node(node.Level + 1, "<function-list>"));
                if (functionList(node.Children[1]))
                {
                    checkEndOfFile(node);
                    return true;
                }
                return false;
            }
            else
            {
                node.Children.Clear();
                node.Children.Add(new Node(node.Level + 1, "<empty>"));
                return true;
            }
        }
        
        private bool function(Node node) {
            node.Children.Add(new Node(node.Level + 1, "<function-identifier>"));
            if (functionIdentifier(node.Children[0]))
            {
                checkEndOfFile(node);
                if (LexemList[lexemListPos].code == tables["Delimiters"]["="])
                {
                    node.Children.Add(new Node(node.Level + 1, LexemList[lexemListPos]));
                    lexemListPos++;
                    node.Children.Add(new Node(node.Level + 1, "<constant>"));
                    if (constant(node.Children[2]))
                    {
                        checkEndOfFile(node);
                        node.Children.Add(new Node(node.Level + 1, "<function-characteristic>"));
                        if (functionCharacteristic(node.Children[3]))
                        {
                            checkEndOfFile(node);
                            if (LexemList[lexemListPos].code == tables["Delimiters"][";"])
                            {
                                node.Children.Add(new Node(node.Level + 1, LexemList[lexemListPos]));
                                lexemListPos++;
                                return true;
                            }
                            err.expected = "delimiter ';'";
                            node.Children.Add(new Node(node.Level + 1, "Error: " + err.expected + " expected", true));
                            Errors(err);
                            return false;
                        }
                        return false;
                    }
                    return false;
                }
                err.expected = "delimiter '='";
                node.Children.Add(new Node(node.Level + 1, "Error: " + err.expected + " expected", true));
                Errors(err);
                return false;
            }
            return false;
        }
        
        private bool functionCharacteristic(Node node)
        {
            if (LexemList[lexemListPos].code == tables["Delimiters"]["\u005C"]) // '\' symbol
            {
                node.Children.Add(new Node(node.Level + 1, LexemList[lexemListPos]));
                lexemListPos++;
                node.Children.Add(new Node(node.Level + 1, "<unsigned-integer>"));
                if (unsignedInt(node.Children[1]))
                {
                    checkEndOfFile(node);
                    if (LexemList[lexemListPos].code == tables["Delimiters"][","])
                    {
                        node.Children.Add(new Node(node.Level + 1, LexemList[lexemListPos]));
                        lexemListPos++;
                        node.Children.Add(new Node(node.Level + 1, "<unsigned-integer>"));
                        if (unsignedInt(node.Children[3]))
                        {
                            checkEndOfFile(node);
                            return true;
                        }
                        return false;
                    }
                    err.expected = "delimiter ','";
                    node.Children.Add(new Node(node.Level + 1, "Error: " + err.expected + " expected", true));
                    Errors(err);
                    return false;
                }
                return false;
            }
            err.expected = "delimiter '\u005C'";
            node.Children.Add(new Node(node.Level + 1, "Error: " + err.expected + " expected", true));
            Errors(err);
            return false;
        }
        
        private bool constant(Node node)
        {
            node.Children.Add(new Node(node.Level + 1, "<unsigned-integer>"));
            return unsignedInt(node.Children[0]);
        }
        
        private bool procedureIdentifier(Node node)
        {
            node.Children.Add(new Node(node.Level + 1, "<identifier>"));
            return identifier(node.Children[0]);
        }
        
        private bool functionIdentifier(Node node)
        {
            node.Children.Add(new Node(node.Level + 1, "<identifier>"));
            return identifier(node.Children[0]);
        }
        
        private bool identifier(Node node)
        {
            foreach (var item in tables["Identifiers"])
            {
                if (item.Value == LexemList[lexemListPos].code)
                {
                    node.Children.Add(new Node(node.Level + 1, LexemList[lexemListPos]));
                    lexemListPos++;
                    return true;
                }
            }
            return false;
        }
        
        private bool unsignedInt(Node node)
        {
            foreach (var item in tables["Constants"])
            {
                if (item.Value == LexemList[lexemListPos].code)
                {
                    node.Children.Add(new Node(node.Level + 1, LexemList[lexemListPos]));
                    lexemListPos++;
                    return true;
                }
            }
            return false;
        }
    }
}
