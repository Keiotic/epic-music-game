using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float gridSquareSize = 13.5f;
    public Vector2 gridSize = new Vector2(9, 15);
    private List<GameObject> gridReps = new List<GameObject>();
    public GameObject gridRep;
    void Start()
    {
        for (int j = 0; j < gridSize.y; j++)
        {
            for (int i = 0; i < gridSize.x; i++)
            {
                Vector2 gridPos = new Vector2((i - (gridSize.x-1)/2) * gridSquareSize / 2, (j - (gridSize.y - 1) / 2) * gridSquareSize/2);
                GameObject g = Instantiate(gridRep, gridPos, Quaternion.identity);
                gridReps.Add(g);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
