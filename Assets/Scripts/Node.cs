using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    public Node(T value)
    {
        this.value = value;
        this.Parent = null;
    }

    public T value { get; }
    public Node<T> Parent { get; set; }
    private List<Node<T>> children = new List<Node<T>>();
    public List<Node<T>> Children {
        get { return children; }
        set { children = value; }
    }

    public Node<T> Add(Node<T> node)
    {
        this.Children.Add(node);
        node.Parent = this;
        return node;
    }
}
