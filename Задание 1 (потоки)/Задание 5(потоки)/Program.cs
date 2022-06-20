using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Задание_5_потоки_
{

    namespace Test
    {
        class Program
        {
            static int summa, value;
            static void Main(string[] args)
            {
                TreeNode tree = new TreeNode();
                Random rnd = new Random(DateTime.Now.Millisecond);
                int k = rnd.Next(8,20);
                for (int i = 0; i < k; i++)
                {
                    tree.Root = tree.AddNode(rnd.Next(-10, 50), tree.Root);
                }
                //tree.Root = tree.AddNode(rnd.Next(-10,50), tree.Root);
                //tree.Root = tree.AddNode(rnd.Next(-10, 50), tree.Root);
                //tree.Root = tree.AddNode(rnd.Next(-10, 50), tree.Root);
                //tree.Root = tree.AddNode(rnd.Next(-10, 50), tree.Root);
                //tree.Root = tree.AddNode(rnd.Next(-10, 50), tree.Root);
                //tree.Root = tree.AddNode(rnd.Next(-10, 50), tree.Root);
                //tree.Root = tree.AddNode(rnd.Next(-10, 50), tree.Root);
                //tree.Root = tree.AddNode(rnd.Next(-10, 50), tree.Root);
                tree.PrintTree(Console.WindowWidth / 2, 0, tree.Root);

                Console.SetCursorPosition(0, 25);
                List<Thread> threads = new List<Thread>();
                ParameterizedThreadStart sum = new ParameterizedThreadStart(Sum);                
                threads.Add(new Thread(sum));
                threads[0].Start(tree);
                
                //Console.WriteLine(tree.SummaElements(tree.Root));
                ParameterizedThreadStart count = new ParameterizedThreadStart(Count);
                threads.Add(new Thread(count));
                threads[1].Start(tree);

                threads[0].Join();
                threads[1].Join();
                Console.WriteLine("Сумма элементов бинарного дерева равна: {0}", summa);
                Console.WriteLine("Кол-во элементов в бинарном дереве: {0}",value);
                //Console.WriteLine(tree.CountElements(tree.Root));
                Console.WriteLine("Среднее значение среди элементов бинарного дерева: " + Math.Round((float) summa / value));

                Console.ReadKey();

            }

            static void Sum(object Tree1)
            {
                TreeNode tr = (TreeNode)Tree1;
                summa = tr.SummaElements(tr.Root);
            }

            static void Count(object Tree1)
            {
                TreeNode tr = (TreeNode)Tree1;
                value = tr.CountElements(tr.Root);
            }

        }

        public class TreeNode
        {
            private Node _root;

            public Node Root { get => _root; set => _root = value; }

            public Node AddNode(int inputDataNode, Node root)
            {
                if (root == null)
                {
                    root = new Node(inputDataNode);
                }
                else
                {
                    if (inputDataNode < root.Data)
                    {
                        root.Left = AddNode(inputDataNode, root.Left);
                    }
                    else if(inputDataNode > root.Data)
                    {
                        root.Right = AddNode(inputDataNode, root.Right);
                    }
                }

                return root;
            }

            public Node FindElement(int findData, Node root)
            {
                if (root == null || findData == root.Data)
                    return root;
                else if (root.Data < findData)
                    return FindElement(findData, root.Left);
                else
                    return FindElement(findData, root.Right);
            }

            public Node Minimum(Node root)
            {
                if (root != null)
                {
                    if (root.Left != null) root = Minimum(root.Left);
                }
                return root;
            }

            public Node DeleteNode(int deleteData, Node root)
            {
                if (root == null)
                    return root;
                if (deleteData < root.Data)
                {
                    root.Left = DeleteNode(deleteData, root.Left);
                }
                else if (deleteData > root.Data)
                {
                    root.Right = DeleteNode(deleteData, root.Right);
                }
                else if (root.Left != null && root.Right != null)
                {
                    root.Data = Minimum(root.Right).Data;
                    root.Right = DeleteNode(root.Data, root.Right);
                }
                else if (root.Left != null)
                {
                    return root.Left;
                }
                else
                {
                    return root.Right;
                }

                return root;

            }

            public void PrintTree(int x, int y, Node root, int delta = 0)
            {
                if (root != null)
                {
                    if (delta == 0) delta = x / 2;
                    Console.SetCursorPosition(x, y);
                    Console.Write(root.Data);
                    PrintTree(x - delta, y + 3, root.Left, delta / 2);
                    PrintTree(x + delta, y + 3, root.Right, delta / 2);
                }

            }

            public void ClearTree()
            {
                _root = null;
            }

            public int CountElements(Node root)
            {
                if (root == null)
                    return 0;
                else
                {
                    int count = 0;
                    count += CountElements(root.Left);
                    count += CountElements(root.Right);

                    return count + 1;
                }
            }

            public int SummaElements(Node root)
            {
                if (root == null)
                    return 0;
                else
                {
                    int count = 0;
                    count += SummaElements(root.Left);
                    count += SummaElements(root.Right);

                    return count + root.Data;
                }
            }

            public bool IsEmpty()
            {
                return _root == null ? true : false;
            }

        }

        public class Node
        {
            private int _data;
            private Node _left;
            private Node _right;

            public Node()
            {
            }

            public Node(int inputDataNode)
            {
                Data = inputDataNode;
            }

            public Node(int data, Node left, Node right)
            {
                Data = data;
                Left = left;
                Right = right;
            }

            public int Data { get => _data; set => _data = value; }
            public Node Left { get => _left; set => _left = value; }
            public Test.Node Right { get => _right; set => _right = value; }
        }
    }
}
