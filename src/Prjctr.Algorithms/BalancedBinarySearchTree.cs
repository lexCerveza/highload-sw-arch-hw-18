using System;
using System.Collections.Generic;

namespace Prjctr.Algorithms;

[Serializable]
public class BalancedBinaryTree
{
    private Node root;
    
    public int Count;
    
    public void Insert(int value, bool rebalance = true)
    {
        Node node = new Node(value);
        Insert(node);

        if (rebalance)
        {
            Rebalance();
        }
    }
    
    public void Insert(Node node)
    {
        if (root == null)
        {
            root = node;
        }
        else
        {
            Insert(node, ref root);
        }
        
        Count++;
    }
    
    public void Delete(int value)
    {
        Node parentNode;
        Node foundNode = null;
        Node tree = parentNode = root;
        
        while (tree != null)
        {
            if (value.CompareTo(tree.Value) == 0)
            {
                foundNode = tree;
                break;
            }

            if (value.CompareTo(tree.Value) < 0)
            {
                parentNode = tree;
                tree = tree.Left;
            }
            else if (value.CompareTo(tree.Value) > 0)
            {
                parentNode = tree;
                tree = tree.Right;
            }
        }
        
        if (foundNode == null)
        {
            return;
        }
        
        var leftOrRightNode = foundNode != parentNode.Left;

        if (foundNode == parentNode)
        {
            IList<Node> listOfNodes = new List<Node>();
                 
            FillListInOrder(root, listOfNodes);
            RemoveChildren(listOfNodes);
            listOfNodes.Remove(parentNode);
                
            root = null;
            int count = Count - 1;
            Count = 0;
                
            Rebalance(0, count - 1, listOfNodes);
        }
        else if (foundNode.Left == null && foundNode.Right == null)
        {
            if (leftOrRightNode)
            {
                parentNode.Right = null;
            }
            else
            {
                parentNode.Left = null;
            }
        }
        else if (foundNode.Left != null && foundNode.Right != null)
        {
            if (leftOrRightNode)
            {
                parentNode.Right = foundNode.Right;
                parentNode.Right.Left = foundNode.Left;
            }
            else
            {
                parentNode.Left = foundNode.Right;
                parentNode.Left.Left = foundNode.Left;
            }
        }
        
        else if (foundNode.Left != null || foundNode.Right != null)
        {
            if (foundNode.Left != null)
            {
                if (leftOrRightNode)
                {
                    parentNode.Right = foundNode.Left;
                }
                else
                {
                    parentNode.Left = foundNode.Left;
                }
            }
            else
            {
                if (leftOrRightNode)
                {
                    parentNode.Right = foundNode.Right;
                }
                else
                {
                    parentNode.Left = foundNode.Right;
                }
            }
        }
    }
    
    public Node Search(int value)
    {
        var tree = root;
        
        while (tree != null)
        {
            if (value.CompareTo(tree.Value) == 0)
            {
                return tree;
            }

            if (value.CompareTo(tree.Value) < 0)
            {
                tree = tree.Left;
            }
            else if (value.CompareTo(tree.Value) > 0)
            {
                tree = tree.Right;
            }
        }
        
        return null;
    }
    
    public IEnumerable<Node> InOrder()
    {
        return InOrder(root);
    }
    
    private IEnumerable<Node> InOrder(Node node)
    {
        if (node != null)
        {
            foreach(var left in InOrder(node.Left))
            {
                yield return left;
            }
            
            yield return node;
            
            foreach (var right in InOrder(node.Right))
            {
                yield return right;
            }
        }
    }

    public void Rebalance()
    {
        IList<Node> listOfNodes = new List<Node>();
        
        FillListInOrder(root, listOfNodes);
        RemoveChildren(listOfNodes);
        
        root = null;
        int count = Count;
        Count = 0;
        
        Rebalance(0, count - 1, listOfNodes);
    }
    
    private void Insert(Node node, ref Node parent)
    {
        if (parent == null)
        {
            parent = node;
        }
        else
        {
            if (node.Value.CompareTo(parent.Value) < 0)
            {
                Insert(node, ref parent.Left);
            }
            else if (node.Value.CompareTo(parent.Value) > 0)
            {
                Insert(node, ref  parent.Right);
            }
        }
    }
    
    private void Rebalance(int min, int max, IList<Node> list)
    {
        if (min <= max)
        {
            int middleNode = (int)Math.Ceiling(((double)min + max) / 2);

            if (list.Count <= middleNode)
            {
                return;
            }
            
            Insert(list[middleNode]);
            
            Rebalance(min, middleNode - 1, list);
            
            Rebalance(middleNode + 1, max, list);
        }            
    }
    
    private void FillListInOrder(Node node, ICollection<Node> list)
    {
        if (node != null)
        {
            FillListInOrder(node.Left, list);
            
            list.Add(node);
            
            FillListInOrder(node.Right, list);
        }
    }
    
    private void RemoveChildren(IEnumerable<Node> list)
    {
       foreach(var node in list)
       {
           node.Left = null;
           node.Right = null;
       }
    }

    private Node FindLeftMost(Node node, bool setParentToNull)
    {
        Node leftMost;
        Node current = leftMost = node;
        Node parent = null;
        
        while (current != null)
        {
            if (current.Left!=null)
            {
                parent = current;
                leftMost = current.Left;
            }
        
            current = current.Left;
        }
        
        if (parent!=null && setParentToNull)
        {
            parent.Left = null;
        }
        
        return leftMost;
    }    
}

[Serializable]
public class Node
{
    public Node(int value) => Value = value;

    public int Value { get; set; }
    
    public Node Left;
    
    public Node Right;
}