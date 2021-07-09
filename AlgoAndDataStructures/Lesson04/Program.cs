using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Lesson04
{
    class Program
    {
        private const int arraySize = 10_000;
        private const int stringSize = 3;
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        static string[] array = new string[arraySize];
        static HashSet<string> hs = new HashSet<string>(arraySize);

        static void Main(string[] args)
        {
            #region Task01
            FillBothWithStrings();
            Console.WriteLine($"Поиск по массиву, мс: {MyBenchmarkRunner.Test(TestArray)}");
            Console.WriteLine($"Поиск по hashset, мс: {MyBenchmarkRunner.Test(TestHashset)}");
            #endregion

            #region Task02
            Tree tree = new Tree(6, 2, 11, 3, 9, 30, -115);
            tree.PrintTree();
            tree.RemoveItem(6);
            tree.PrintTree();
            #endregion
        }

        private static void FillBothWithStrings()
        {
            var random = new Random();
            for (int i = 0; i < arraySize; i++)
            {
                string newString = new string(Enumerable.Repeat(chars, stringSize)
                                                        .Select(s => s[random.Next(s.Length)])
                                                        .ToArray());
                array[i] = newString;
                hs.Add(newString);
            }
        }

        static void TestArray()
        {
            string search = "notExist";
            for (int i = 0; i < 1_000_000; i++)
            {
                array.Contains(search);
            }
        }
        static void TestHashset()
        {
            string search = "notExist";
            for (int i = 0; i < 1_000_000; i++)
            {
                hs.Contains(search);
            }
        }
    }

    #region Task01 Under the hood
    class MyBenchmarkRunner
    {
        public static long Test(Action func)
        {
            var sw = new Stopwatch();
            sw.Start();
            func();
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
    #endregion

    #region Task02 Under the hood
    public class Tree : ITree
    {
        private TreeNode _root;

        public Tree(params int[] nums)
        {
            foreach (var item in nums)
                AddItem(item);
        }

        public void AddItem(int value)
        {
            TreeNode parent = null, child = _root;

            while (child != null)
            {
                parent = child;

                if (child.Value > value)
                    child = child.LeftChild;
                else
                    child = child.RightChild;
            }

            TreeNode newNode = new TreeNode() { Value = value };

            if (_root == null)
                _root = newNode;
            else
            {
                if (value < parent.Value)
                    parent.LeftChild = newNode;
                else
                    parent.RightChild = newNode;
            }
        }

        public TreeNode GetNodeByValue(int value)
        {
            if (_root.Value == value)
                return _root;

            var next = _root;

            while (next != null)
            {
                if (next.Value == value)
                    return next;
                else
                {
                    if (value < next.Value)
                        next = next.LeftChild;
                    else
                        next = next.RightChild;
                }
            }

            return null;
        }

        public TreeNode GetRoot() => _root;

        public void PrintTree()
        {
            void preOrderTraverse(TreeNode root)
            {
                if (root == null) return;

                Console.Write(root.Value);
                if (root.LeftChild != null || root.RightChild != null)
                {
                    Console.Write('(');
                    if (root.LeftChild != null)
                        preOrderTraverse(root.LeftChild);
                    else
                        Console.Write("nil");
                    Console.Write(',');
                    if (root.RightChild != null)
                        preOrderTraverse(root.RightChild);
                    else
                        Console.Write("nil");
                    Console.Write(')');
                }
            }

            preOrderTraverse(_root);
            Console.WriteLine();
        }

        public void RemoveItem(int value)
        {
            _root = RemoveItem(_root, value);
        }

        private TreeNode RemoveItem(TreeNode root, int value)
        {
            if (root == null) return root;
            if (value < root.Value)
            {
                root.LeftChild = RemoveItem(root.LeftChild, value);
            }
            else if (value > root.Value)
            {
                root.RightChild = RemoveItem(root.RightChild, value);
            }
            else
            {
                if (root.LeftChild == null)
                {
                    return root.RightChild;
                }
                else if (root.RightChild == null)
                {
                    return root.LeftChild;
                }
                root.Value = FindMinValue(root.RightChild);
                root.RightChild = RemoveItem(root.RightChild, root.Value);
            }

            return root;
        }

        private int FindMinValue(TreeNode root)
        {
            int minValue = root.Value;
            while (root.LeftChild != null)
            {
                minValue = root.LeftChild.Value;
                root = root.LeftChild;
            }
            return minValue;
        }
    }

    public class TreeNode
    {
        public int Value { get; set; }
        public TreeNode LeftChild { get; set; }
        public TreeNode RightChild { get; set; }

        public override bool Equals(object obj)
        {
            var node = obj as TreeNode;

            if (node == null)
                return false;

            return node.Value == Value;
        }
    }

    public interface ITree
    {
        TreeNode GetRoot();
        void AddItem(int value); // добавить узел
        void RemoveItem(int value); // удалить узел по значению
        TreeNode GetNodeByValue(int value); //получить узел дерева по значению
        void PrintTree(); //вывести дерево в консоль
    }

    public static class TreeHelper
    {
        public static NodeInfo[] GetTreeInLine(ITree tree)
        {
            var bufer = new Queue<NodeInfo>();
            var returnArray = new List<NodeInfo>();
            var root = new NodeInfo() { Node = tree.GetRoot() };
            bufer.Enqueue(root);

            while (bufer.Count != 0)
            {
                var element = bufer.Dequeue();
                returnArray.Add(element);

                var depth = element.Depth + 1;

                if (element.Node.LeftChild != null)
                {
                    var left = new NodeInfo()
                    {
                        Node = element.Node.LeftChild,
                        Depth = depth,
                    };
                    bufer.Enqueue(left);
                }
                if (element.Node.RightChild != null)
                {
                    var right = new NodeInfo()
                    {
                        Node = element.Node.RightChild,
                        Depth = depth,
                    };
                    bufer.Enqueue(right);
                }
            }

            return returnArray.ToArray();
        }
    }

    public class NodeInfo
    {
        public int Depth { get; set; }
        public TreeNode Node { get; set; }
    }
    #endregion
}
