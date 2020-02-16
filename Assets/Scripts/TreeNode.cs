using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode<T>
{
    public TreeNode(T value)
    {
        this.value = value;
        this.Parent = null;
    }

    public T value { get; }
    public TreeNode<T> Parent { get; set; }
    private List<TreeNode<T>> children = new List<TreeNode<T>>();
    public List<TreeNode<T>> Children {
        get { return children; }
        set { children = value; }
    }

    public TreeNode<T> Add(TreeNode<T> node)
    {
        this.Children.Add(node);
        node.Parent = this;
        return node;
    }
}
