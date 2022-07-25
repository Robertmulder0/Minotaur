using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindTest : MonoBehaviour
{

    Grid grid;
    private Pathfinding pathfinding;
    public Transform startPos;
    public Transform endPos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<Node> path = pathfinding.FindPath(startPos, endPos);
        if (path != null) {
            foreach (Node pathNode in path) {
                for (int i=0; i<path.Count - 1; i++) {
                   // Debug.Log(path[i]);
                }
            }
        }
    }
}
