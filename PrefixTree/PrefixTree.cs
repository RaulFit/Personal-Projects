﻿using Microsoft.VisualBasic.FileIO;

namespace MyPrefixTree
{
    public class PrefixTree<T> where T : notnull
    {
        private Node<T> Root;

        public PrefixTree()
        {
            Root = new Node<T>();
        }

        public void Insert(IEnumerable<T> word)
        {
            var current = Root;
            foreach (var ch in word)
            {
                if (!current.Children.ContainsKey(ch))
                {
                    current.Children[ch] = new Node<T>();
                }

                current = current.Children[ch];
            }

            current.IsEndOfWord = true;
        }

        public bool Search(IEnumerable<T> word)
        {
            return Contains(word);
        }

        public bool startsWith(IEnumerable<T> prefix)
        {
            return Contains(prefix, true);
        }

        public void Remove(IEnumerable<T> word)
        {
            Remove(Root, word, 0);
        }

        private bool Remove(Node<T> current, IEnumerable<T> word, int index)
        {
            if (index == word.Count())
            {
                if (!current.IsEndOfWord)
                {
                    return false;
                }

                current.IsEndOfWord = false;
                return current.Children.Count == 0;
            }

            if (!current.Children.TryGetValue(word.ElementAt(index), out Node<T> children))
            {
                return false;
            }

            bool shoulRemoveChildren = Remove(children, word, index + 1) && !children.IsEndOfWord;
            if (shoulRemoveChildren)
            {
                current.Children.Remove(word.ElementAt(index));
                return current.Children.Count == 0;
            }

            return false;
        }

        private bool Contains(IEnumerable<T> word, bool prefix = false)
        {
            var current = Root;
            foreach (var ch in word)
            {
                if (!current.Children.ContainsKey(ch))
                {
                    return false;
                }

                current = current.Children[ch];
            }

            return prefix ? true : current.IsEndOfWord;
        }
    }

    public sealed class Node<T> where T : notnull
    {
        public Dictionary<T, Node<T>> Children;
        public bool IsEndOfWord;

        public Node()
        {
            Children = new Dictionary<T, Node<T>>();
            IsEndOfWord = false;
        }
    }
}