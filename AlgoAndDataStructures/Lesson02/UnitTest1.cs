using System;
using Xunit;

namespace Lesson02
{
    public class UnitTest1
    {
        // TASK 01 : Write LinkedList
        [Fact]
        public void TestCount()
        {
            MyLinkedList list = new();
            Random rnd = new Random();
            int actualCount = rnd.Next(1, 1000);
            for (int i = 0; i < actualCount; i++)
                list.AddNode(33);
            Assert.Equal(actualCount, list.GetCount());

            list.AddNodeAfter(list.Head.NextNode, rnd.Next(actualCount));
            Assert.Equal(++actualCount, list.GetCount());

            list.RemoveNode(2);
            Assert.Equal(--actualCount, list.GetCount());
        }

        [Fact]
        public void TestFindNode()
        {
            MyLinkedList list = new();
            Random rnd = new Random();
            int[] a = new int[] { 104, 2, 5, 44, 0, 10 };
            foreach (int e in a)
                list.AddNode(e);

            Assert.Equal(104, list.FindNode(104).Value);
            Assert.Equal(0, list.FindNode(0).Value);
            Assert.Equal(10, list.FindNode(10).Value);
            Assert.IsType<Node>(list.FindNode(44));

            Assert.Null(list.FindNode(13));
        }

        [Fact]
        public void TestAddNodeAfter()
        {
            MyLinkedList list = new();
            Random rnd = new Random();
            int[] a = new int[] { 104, 2, 5, 44, 0, 10 };
            foreach (int e in a)
                list.AddNode(e);

            list.AddNodeAfter(list.Head.NextNode.NextNode, 666);
            Assert.Equal(666, list.Head.NextNode.NextNode.NextNode.Value);

            list.AddNodeAfter(list.Head, 111);
            Assert.Equal(111, list.Head.NextNode.Value);

            list.AddNodeAfter(list.Tail, 999);
            Assert.Equal(999, list.Tail.Value);
        }

        [Fact]
        public void TestRemoveNode()
        {
            MyLinkedList list = new();
            Random rnd = new Random();
            int[] a = new int[] { 104, 2, 5, 44, 0, 10 };
            foreach (int e in a)
                list.AddNode(e);

            list.RemoveNode(5);
            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveNode(5));

            list.RemoveNode(0);
            Assert.Equal(2, list.Head.Value);

            list.RemoveNode(2);
            Assert.Equal(2, list.Head.Value);
            Assert.Equal(0, list.Head.NextNode.NextNode.Value);

            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveNode(-3));
        }

        // TASK 02 : Write binary search
        [Fact]
        public void TestBinarySearch()
        {
            // ------------------ 0  1  2  3  4  5  6   7   8    9  10  11  12
            int[] array = new[] { 1, 3, 4, 5, 7, 8, 10, 13, 15, 66, 88, 89, 90 };

            Assert.Equal(0, Search.BinarySearch(array, 1));
            Assert.Equal(4, Search.BinarySearch(array, 7));
            Assert.Equal(5, Search.BinarySearch(array, 8));
            Assert.Equal(6, Search.BinarySearch(array, 10));
            Assert.Equal(7, Search.BinarySearch(array, 13));
            Assert.Equal(12, Search.BinarySearch(array, 90));

            Assert.Equal(-1, Search.BinarySearch(array, 101));
            Assert.Equal(-1, Search.BinarySearch(array, 0));
        }
    }

    public interface ILinkedList
    {
        int GetCount(); // возвращает количество элементов в списке
        void AddNode(int value);  // добавляет новый элемент списка
        void AddNodeAfter(Node node, int value); // добавляет новый элемент списка после определённого элемента
        void RemoveNode(int index); // удаляет элемент по порядковому номеру
        void RemoveNode(Node node); // удаляет указанный элемент
        Node FindNode(int searchValue); // ищет элемент по его значению
    }

    public class MyLinkedList : ILinkedList
    {
        public Node Head { get; private set; } = null;

        public Node Tail { get; private set; } = null;

        private int _count = 0;

        public void AddNode(int value)
        {
            Node newNode = new Node() { Value = value };
            if (Head == null)
                Head = newNode;
            else
            {
                Tail.NextNode = newNode;
                newNode.PrevNode = Tail;
            }
            Tail = newNode;
            _count++;
        }

        public void AddNodeAfter(Node node, int value)
        {
            Node newNode = new Node() { Value = value, NextNode = node.NextNode, PrevNode = node };
            if (node.NextNode != null)
                node.NextNode.PrevNode = newNode;
            node.NextNode = newNode;

            if (node == Tail)
                Tail = newNode;

            _count++;
        }

        public Node FindNode(int searchValue)
        {
            var currentNode = Head;

            while (currentNode != null)
            {
                if (currentNode.Value == searchValue)
                    return currentNode;
                currentNode = currentNode.NextNode;
            }
            return null;
        }

        public void RemoveNode(int index)
        {
            if (index < 0 || index > _count - 1) throw new ArgumentOutOfRangeException();

            Node node = Head;

            while (node != null && index > 0)
            {
                node = node.NextNode;
                index--;
            }
            if (node != null)
                RemoveNode(node);
        }

        public void RemoveNode(Node node)
        {
            if (node == Head)
            {
                Head.NextNode.PrevNode = null;
                Head = Head.NextNode;
            }
            else if (node == Tail)
            {
                Tail.PrevNode.NextNode = null;
                Tail = Tail.PrevNode;
            }
            else
            {
                node.PrevNode.NextNode = node.NextNode;
                node.NextNode.PrevNode = node.PrevNode;
            }
            _count--;
        }

        public int GetCount() => _count;
    }

    public class Node
    {
        public int Value { get; set; }
        public Node NextNode { get; set; }
        public Node PrevNode { get; set; }
    }

    public class Search
    {
        // Асимптотическая сложность бинарного поиска O(logN)
        public static int BinarySearch(int[] array, int searchValue)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException();

            int min = 0, max = array.Length - 1;

            while (min <= max)
            {
                int mid = (max + min) / 2;
                if (array[mid] == searchValue)
                    return mid;
                else if (array[mid] < searchValue)
                    min = mid + 1;
                else
                    max = mid - 1;
            }
            return -1;
        }
    }
}
