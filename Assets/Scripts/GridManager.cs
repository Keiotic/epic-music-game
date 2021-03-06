using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager current;
    [SerializeField] private float gridSquareSize = 13.5f;
    [SerializeField] private Vector2 gridSize = new Vector2(9, 15);
    [SerializeField] private Vector2 innerGridSize = new Vector2(9, 15);
    private List<GameObject> gridReps = new List<GameObject>();
    [SerializeField] private GameObject gridRep;
    private Grid grid;
    private GridADT<PathNode> navGrid;
    private Grid innerGrid;
    private BeatManager beatManager;

    void Start()
    {
        current = this;
        beatManager = GetComponent<BeatManager>();
        grid = new Grid(gridSquareSize, gridSize, new Vector2(0, 0));
        PathNode[,] nodes = new PathNode[(int)gridSize.x, (int)gridSize.y];

        for (int j = 0; j < gridSize.y; j++)
        {

            for (int i = 0; i < gridSize.x; i++)
            {
                PathNode node = new PathNode();
                node.position = new Vector2(i, j);
                nodes[i, j] = node;
            }
        }
        navGrid = new GridADT<PathNode>((int)gridSize.x, (int)gridSize.y, nodes);
        innerGrid = new Grid(gridSquareSize, innerGridSize, new Vector2(0, 0));

        for (int j = 0; j < innerGridSize.y; j++)
        {

            for (int i = 0; i < innerGridSize.x; i++)
            {
                Vector2 gridPos = innerGrid.GridToWorldCoordinates(new Vector2(i, j));
                GameObject g = Instantiate(gridRep, gridPos, Quaternion.identity);
                gridReps.Add(g);
                g.transform.parent = this.transform;
                g.transform.name = "gridpos_" + i + "-" + j;
            }
        }
    }

    public GridADT<PathNode> GetNavGrid()
    {
        return navGrid;
    }

    public Vector2 GridToWorldCoordinates(Vector2 gridpos)
    {
        if (grid != null)
            return grid.GetWorldCoordinates(gridpos);
        return Vector2.zero;
    }

    public Vector2 FindClampedNearestGridPos(Vector2 worldpos)
    {
        Vector2 position = grid.FindClampedNearestGridPos(worldpos);

        return ClampToInnerGrid(position);
    }

    public Vector2 ClampToGridSize(Vector2 gridPos)
    {
        gridPos.x = Mathf.Clamp(gridPos.x, 0, gridSize.x - 1);
        gridPos.y = Mathf.Clamp(gridPos.y, 0, gridSize.y - 1);
        return gridPos;
    }

    public float DistanceToNearestGridPos(Vector2 worldPos)
    {
        Vector2 gridPos = FindNearestGridPos(worldPos);
        gridPos = GridToWorldCoordinates(gridPos);
        return Vector2.Distance(gridPos, worldPos);
    }

    public Vector2 FindNearestGridPos(Vector2 worldpos)
    {
        return grid.FindClampedNearestGridPos(worldpos);
    }

    public Vector2 GetRelativeGridTranslation(Vector2 gridpos, Vector2 translation, bool clampedToGrid)
    {
        if (!clampedToGrid)
            return grid.GetRelativeGridTranslation(gridpos, translation, true);
        else
            return ClampToInnerGrid(grid.GetRelativeGridTranslation(gridpos, translation, true));

    }

    public Vector2 ClampToInnerGrid(Vector2 position)
    {
        int xPadding = ((int)gridSize.x - (int)innerGridSize.x) / 2;
        int yPadding = ((int)gridSize.y - (int)innerGridSize.y) / 2;

        position.x = Mathf.Clamp(position.x, xPadding, gridSize.x - 1 - xPadding);
        position.y = Mathf.Clamp(position.y, yPadding, gridSize.y - 1 - yPadding);
        return position;
    }

    public float GetGridSpeedCoefficient()
    {
        return this.GetGridBoxSize() / 2 / beatManager.GetTimeBetweenBeats();
    }

    public Vector2 GetGridSize()
    {
        return gridSize;
    }

    public Vector2 GetInnerGridSize()
    {
        return innerGridSize;
    }

    public Vector2 GetGridOffset()
    {
        return (gridSize - innerGridSize) / 2;
    }
    public float GetGridBoxSize()
    {
        return gridSquareSize;
    }

    public Grid GetGrid()
    {
        return grid;
    }

    public Vector2 GetMiddle()
    {
        Vector2 midpoint = new Vector2();
        midpoint.x = (grid.GetSize().x-1) / 2;
        midpoint.y = (grid.GetSize().y-1) / 2;
        return midpoint;
    }

    public GameObject GetGridRep(Vector2 position)
    {
        int arrayIndex = (int)position.y * (int)innerGridSize.y + (int)position.x;
        GameObject returnObject = gridReps[arrayIndex];
        if (returnObject)
            return returnObject;
        return null;
    }
}

public class Grid
{
    private float squareSize;
    private Vector2 dimensions;
    private Vector2[,] positions;
    private Vector2 offset;
    public Grid(float squareSize, Vector2 dimensions, Vector2 offset)
    {
        positions = new Vector2[(int)dimensions.x, (int)dimensions.y];
        this.dimensions = dimensions;
        this.offset = offset;
        this.squareSize = squareSize;
        for (int y = 0; y < dimensions.y; y++)
        {
            for (int x = 0; x < dimensions.x; x++)
            {
                Vector2 gridPos = GridToWorldCoordinates(new Vector2(x, y));
                positions[x, y] = gridPos;
            }
        }
    }

    public Vector2 GridToWorldCoordinates(Vector2 gridpos)
    {
        float xPos = (gridpos.x - (dimensions.x - 1) / 2) * squareSize / 2 + offset.x;
        float yPos = (gridpos.y - (dimensions.y - 1) / 2) * squareSize / 2 + offset.y;
        return new Vector2(xPos, yPos);
    }

    public Vector2 GetWorldCoordinates(Vector2 gridpos)
    {
        return GridToWorldCoordinates(gridpos);
    }

    public Vector2 FindNearestGridPos(Vector2 worldpos)
    {
        Vector2 roundedPos = new Vector2();
        roundedPos.x = Mathf.RoundToInt(2 * worldpos.x / squareSize + (dimensions.x - 1) / 2);
        roundedPos.y = Mathf.RoundToInt(2 * worldpos.y / squareSize + (dimensions.y - 1) / 2);
        return roundedPos;
    }

    public Vector2 FindClampedNearestGridPos(Vector2 worldpos)
    {
        Vector2 clampedPos = FindNearestGridPos(worldpos);
        clampedPos.x = Mathf.Clamp(clampedPos.x, 0, dimensions.x - 1);
        clampedPos.y = Mathf.Clamp(clampedPos.y, 0, dimensions.y - 1);
        return clampedPos;
    }

    public Vector2 GetRelativeGridTranslation(Vector2 gridpos, Vector2 translation, bool clampedToGrid)
    {
        gridpos.x += translation.x;
        gridpos.y += translation.y;
        if (clampedToGrid)
        {
            gridpos.x = Mathf.Clamp(gridpos.x, 0, dimensions.x - 1);
            gridpos.y = Mathf.Clamp(gridpos.y, 0, dimensions.y - 1);
        }
        return gridpos;
    }

    public Vector2[,] GetPositions()
    {
        return positions;
    }

    public Vector2 GetSize()
    {
        return dimensions;
    }
}
