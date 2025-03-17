using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node Neighbor;
    List<Node> _nodes;

    void Start()
    {
        _nodes = GameManager.Instance.Nodes;
        Neighbor = SearcNeighbor();
    }


    Node SearcNeighbor()
    {
        for (int i = 0; i < _nodes.Count; i++)
        {
            if (_nodes[i] == this && i != _nodes.Count-1) 
            { 
                return _nodes[i+1];
            }
        }
        return null;

    }

}
