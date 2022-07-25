using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform startPos;
    public LayerMask wallMask;
    public Vector2 gridSize;
    public int gridSizeX;
    public int gridSizeY;
    public float nodeRadius;
    private float nodeDiameter;
    public float distance;

    Node[,] grid;
    public List<Node> FinalPath;

    // Start is called before the first frame update
    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateGrid();
    }

    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridSize.x/2 - Vector3.forward * gridSize.y/2;
        for (int x=0; x<gridSizeX; x++) {
            for (int y=0; y<gridSizeY; y++) {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward  * (y * nodeDiameter + nodeRadius);
                bool wall = false;
            
                if (Physics.CheckSphere(worldPoint, nodeRadius, wallMask)){
                    wall = true;
                }
                grid[x, y] = new Node(wall, worldPoint, x, y);
            }
        }

    }

    public Node NodeFromWorldPosition(Vector3 worldPosition) 
    {
        float xPoint = ((worldPosition.x + gridSize.x/2) / gridSize.x); // avoid out of range error
        xPoint = Mathf.Clamp01(xPoint);

        float yPoint = ((worldPosition.y + gridSize.y/2) / gridSize.y);
        yPoint = Mathf.Clamp01(yPoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xPoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * yPoint);

        return grid[x, y];
    }

    public Node GetGridNode(int x, int y){
        return grid[x, y];
    }

    private void OnDrawGizmos() //draw path for debuggging
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));
        if (grid != null){
            foreach (Node node in grid){
                if (node.isWall){
                    Gizmos.color = Color.red;
                } else {
                    Gizmos.color = Color.white;
                }

                if (FinalPath != null){
                    Gizmos.color = Color.green;
                }

                Gizmos.DrawCube(node.position, Vector3.one * (nodeDiameter - distance));
            }
        }
    }
}