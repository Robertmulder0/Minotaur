using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX;
    public int gridY;

    public bool isWall;
    public Vector3 position;

    public Node Parent;
    public int gCost;
    public int hCost;
    public int fCost;

    public Node cameFromNode;

    public Node(bool _isWall, Vector3 _pos, int _gridX, int _gridY)
    {
        isWall = _isWall;
        position = _pos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public void CalculateFCost() {
        fCost = gCost + hCost;
    }
}
