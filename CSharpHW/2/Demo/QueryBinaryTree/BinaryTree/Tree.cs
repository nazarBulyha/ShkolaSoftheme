using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTree
{
    public class Tree<TItem> : IEnumerable<TItem> where TItem : IComparable<TItem>
    {
        public Tree(TItem nodeValue)
        {
            NodeData = nodeValue;
            LeftTree = null;
            RightTree = null;
        }

        public TItem NodeData { get; set; }

        public Tree<TItem> LeftTree { get; set; }

        public Tree<TItem> RightTree { get; set; }

        public void Insert(TItem newItem)
        {
            TItem currentNodeValue = NodeData;
            if (currentNodeValue.CompareTo(newItem) > 0)
            {
                if (LeftTree == null)
                {
                    LeftTree = new Tree<TItem>(newItem);
                }
                else
                {
                    LeftTree.Insert(newItem);
                }
            }
            else
            {
                if (RightTree == null)
                {
                    RightTree = new Tree<TItem>(newItem);
                }
                else
                {
                    RightTree.Insert(newItem);
                }
            }
        }

        public void WalkTree()
        {
            if (LeftTree != null)
            {
                LeftTree.WalkTree();
            }

            Console.WriteLine(NodeData.ToString());

            if (RightTree != null)
            {
                RightTree.WalkTree();
            }
        }

        #region IEnumerable<TItem> Members

        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
        {
            if (LeftTree != null)
            {
                foreach (TItem item in LeftTree)
                {
                    yield return item;
                }
            }

            yield return NodeData;

            if (RightTree != null)
            {
                foreach (TItem item in RightTree)
                {
                    yield return item;
                }
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
