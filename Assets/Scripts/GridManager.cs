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
                Vector2 gridPos = GridToWorldCoordinates(new Vector2(i, j));
                GameObject g = Instantiate(gridRep, gridPos, Quaternion.identity);
                gridReps.Add(g);
                g.transform.parent = this.transform;
                g.transform.name = "gridpos_" + i + "-" + j;
            }
        }
    }

    public Vector2 GridToWorldCoordinates (Vector2 gridpos)
    {
        return new Vector2((gridpos.x - (gridSize.x - 1) / 2) * gridSquareSize / 2, (gridSize.y - gridpos.y - (gridSize.y + 1) / 2) * gridSquareSize / 2);
    }

    public Vector2 FindClampedNearestGridPos (Vector2 worldpos)
    {
        Vector2 clampedPos = FindNearestGridPos(worldpos);
        clampedPos.x = Mathf.Clamp(clampedPos.x, 0, gridSize.x-1);
        clampedPos.y = Mathf.Clamp(clampedPos.y, 0, gridSize.y-1);
        return clampedPos;
    }

    public Vector2 FindNearestGridPos(Vector2 worldpos)
    {
        Vector2 roundedPos = new Vector2();
        roundedPos.x = Mathf.RoundToInt(2 * worldpos.x / gridSquareSize + (gridSize.x - 1) / 2);
        roundedPos.y = Mathf.RoundToInt(gridSize.y - 2 * worldpos.y / gridSquareSize - (gridSize.y + 1) / 2);
        return roundedPos;
    }

    public Vector2 GetRelativeGridTranslation(Vector2 gridpos, Vector2 translation, bool clampedToGrid)
    {
        gridpos.x += translation.x;
        gridpos.y -= translation.y;
        if (clampedToGrid)
        {
            gridpos.x = Mathf.Clamp(gridpos.x, 0, gridSize.x-1);
            gridpos.y = Mathf.Clamp(gridpos.y, 0, gridSize.y-1);
        }
        return gridpos;
    }

    public Vector2 GetGridSize()
    {
        return gridSize;
    }
    public float GetGridBoxSize ()
    {
        return gridSquareSize;
    }
}
