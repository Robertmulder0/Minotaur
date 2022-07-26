using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Grid grid;
    public Transform startPos;
    public Transform endPos;
    private List<Node> openList;
    private List<Node> closedList;

    public Node nextNode;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void Update()
    {
        grid.FinalPath = FindPath(startPos, endPos);
        if (grid.FinalPath.Count > 1){
            nextNode = grid.FinalPath[1];
        }
    }

    public List<Node> FindPath(Transform _startPos, Transform _endPos)
    {
        Node startNode = grid.NodeFromWorldPosition(_startPos);
        Node endNode = grid.NodeFromWorldPosition(_endPos);

        openList = new List<Node>() {startNode};
        closedList = new List<Node>();

        for (int x = 0; x < grid.gridSizeX; x++) {
            for (int y = 0; y < grid.gridSizeY; y++){
                Node pathNode = grid.GetGridNode(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0) {
            Node currentNode = GetLowestFCost(openList);
            if (currentNode == endNode){
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Node neighborNode in GetNeighbors(currentNode)){
                if (closedList.Contains(neighborNode)) continue;
                int tentativeGCost = currentNode.gCost + CalculateDistance(currentNode, neighborNode);
                if (tentativeGCost < neighborNode.gCost) {
                    neighborNode.cameFromNode = currentNode;
                    neighborNode.gCost = tentativeGCost;
                    neighborNode.hCost = CalculateDistance(neighborNode, endNode);
                    neighborNode.CalculateFCost();

                    if (!openList.Contains(neighborNode)){
                        openList.Add(neighborNode);
                    }
                }
            }
        }
        return null;

    }

    private List<Node> GetNeighbors(Node currentNode){
        List<Node> Neighbors = new List<Node>();

        if (currentNode.isWall == false){
            if (currentNode.gridX - 1 >= 0){
                Neighbors.Add(grid.GetGridNode(currentNode.gridX - 1, currentNode.gridY));
            }
            if (currentNode.gridX + 1 < grid.gridSizeX) {
                Neighbors.Add(grid.GetGridNode(currentNode.gridX + 1, currentNode.gridY));
            }
            if (currentNode.gridY- 1 >= 0) {
                Neighbors.Add(grid.GetGridNode(currentNode.gridX, currentNode.gridY - 1));
            }
            if (currentNode.gridY + 1 < grid.gridSizeY){
                Neighbors.Add(grid.GetGridNode(currentNode.gridX, currentNode.gridY + 1));
            }
        }

        return Neighbors;
        
    }

    private List<Node> CalculatePath(Node endNode){
        List<Node> path = new List<Node>();

        path.Add(endNode);
        Node currentNode = endNode;
        while (currentNode.cameFromNode != null){
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistance(Node nodeA, Node nodeB)
    {
        int ix = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int iy = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return 10 * Mathf.Abs(ix - iy);
    }

    private Node GetLowestFCost(List<Node> nodeList) {
        Node lowestCostNode = nodeList[0];
        for (int i = 1; i < nodeList.Count; i++){
            if (nodeList[i].fCost < lowestCostNode.fCost){
                lowestCostNode = nodeList[i];
            }
        }
        return lowestCostNode;
    }
}
