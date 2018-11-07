using Library.Tracing;
using Library.TreeView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        public static TreeViewModel ViewModel { get; set; } = new TreeViewModel()
        {
            GetPath = new ConsolePath(),
            Logger = new FileLogger("Logs.txt", "Console")
        };
        public static ConsoleTreeView ConsoleView { get; set; }
        static void Main(string[] args)
        {
            MainMenuView(String.Empty);
        }
    
        private static void TreeViewView(string message)
        {
            System.Console.Clear();
            System.Console.Write(message);
            System.Console.WriteLine("Path:" + ViewModel.PathVariable);
            PrintData();
            System.Console.WriteLine("Type id that you want to expand, or if its already expanded, shrink it");
            System.Console.WriteLine("Type B/b if You want to go back to Menu");
            string temp = System.Console.ReadLine();
            switch (temp)
            {
                case "B":
                case "b":
                    {
                        MainMenuView(String.Empty);
                        break;
                    }
                default:
                    {
                        int parsedTemp;
                        if (!Int32.TryParse(temp, out parsedTemp) || parsedTemp < 0 || parsedTemp > ConsoleView.DataCollection.Count - 1)
                        {
                            TreeViewView("Incorrect format, try again\n");
                            return;
                        }
                        Expand(parsedTemp);
                        TreeViewView(String.Empty);
                        break;
                    }
            }
        }
        private static void MainMenuView(string message)
        {
            System.Console.Clear();
            System.Console.Write(message);
            System.Console.WriteLine("To open .dll type O/o and confirm with 'Enter'.\nTo exit the program type E/e and confirm with 'Enter'.");
            string choose = System.Console.ReadLine();
            switch (choose)
            {
                case "O":
                case "o":
                    {
                        System.Console.Clear();
                        System.Console.WriteLine("Type absolute Path of file you want to open");
                        ViewModel.HierarchicalAreas = new ObservableCollection<TreeViewItem>();
                        ViewModel.Click_Browse.Execute(null);
                        ViewModel.Click_Button.Execute(null);
                        if (ViewModel.PathVariable == null)
                            MainMenuView("Wrong Path\n");
                        else
                        {
                            ConsoleView = new ConsoleTreeView(new ObservableCollection<ConsoleTreeViewItem>(ViewModel.HierarchicalAreas.Select(n => new ConsoleTreeViewItem(n, 0))));
                            TreeViewView(String.Empty);
                        }
                        break;
                    }
                case "E":
                case "e":
                    {
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        MainMenuView("Wrong option!\n");
                        break;
                    }
            }

        }

        public static void Expand(int index)
        {
            ConsoleView.Expand(index);
            System.Console.Clear();
            PrintData();
        }
        public static void PrintData()
        {
            int index = 0;
            foreach (ConsoleTreeViewItem itemConsole in ConsoleView.DataCollection)
            {
                string[] value = new string[4];
                value[0] = "id:" + index;
                value[1] = "[" + itemConsole.TreeItem.ItemType + "]";
                value[2] = itemConsole.IsExpanded ? "[-] " : "[+] ";
                value[3] = itemConsole.TreeItem.Name;
                PrintWithIndent(value, itemConsole.Indent);
                index++;
            }
        }


        private static void PrintWithIndent(string[] value, int indent)
        {
            System.Console.Write(new string(' ', indent * 3));
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.Write(value[0]);
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.Write(value[1]);
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(value[2]);
            System.Console.ResetColor();
            System.Console.WriteLine(value[3]);
        }
    }
}
