using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Prev { get; set; }

        public Node(T d)
        {
            Data = d;
        }
    }
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private Node<T> tail;
        private Node<T> head;
        public DoublyLinkedList()
        {
            head = null;
            tail = null;
            Length = 0;
        }
        public int Length { get; private set; }

        public void Add(T e)
        {
            Node<T> node = new Node<T>(e);

            if (head == null)
            {
                head = node;
            }
            else
            {
                tail.Next = node;
                node.Prev = tail;
            }

            tail = node;
            Length++;
        }

        public void AddAt(int index, T e)
        {
            Node<T> node = new Node<T>(e);

            if (index == 0)
            {
                node.Next = head;
                head.Prev = node;
                head = node;
            }
            else if (index == Length)
            {
                tail.Next = node;
                node.Prev = tail;
                tail = node;
            }
            else
            {
                Node<T> current = GetNodeAt(index);
                node.Prev = current.Prev;
                node.Next = current;
                current.Prev.Next = node;
                current.Prev = node;
            }

            Length++;
        }

        public T ElementAt(int index)
        {
            if (index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            return GetNodeAt(index).Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> node = head;

            while (node != null)
            {
                yield return node.Data;
                node = node.Next;
            }
        }

        public void Remove(T item)
        {
            Node<T> current = head;

            while (current != null)
            {
                if (current.Data.Equals(item))
                {
                    RemoveItem(current);
                    break;
                }
                current = current.Next;
            }
        }

        public T RemoveAt(int index)
        {
            if (index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            Node<T> current = GetNodeAt(index);
            RemoveItem(current);
            return current.Data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Node<T> node = head;

            while (node != null)
            {
                yield return node.Data;
                node = node.Next;
            }
        }

        private Node<T> GetNodeAt(int index)
        {
            Node<T> node = head;

            for (int i = 0; i <= index; i++)
            {
                if (i == index)
                {
                    break;
                }
                node = node.Next;
            }
            return node;
        }

        private void RemoveItem(Node<T> node)
        {
            if (node.Prev != null)
            {
                node.Prev.Next = node.Next;
            }
            else
            {
                head = node.Next;
            }

            if (node.Next != null)
            {
                node.Next.Prev = node.Prev;
            }
            else
            {
                tail = node.Prev;
            }
            Length--;
        }
    }
}