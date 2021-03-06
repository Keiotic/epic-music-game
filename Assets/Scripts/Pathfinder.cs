using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    private const int MOVEMENTCOST_STRAIGHT = 10;
    private const int MOVEMENTCOST_DIAGONAL = 15;
    private GridADT<PathNode> grid;
    private Grid coordGrid;
    private List<PathNode> openList;
    private List<PathNode> closedList;
    private bool canGoDiagonally;
    public Pathfinder(Grid grid)
    {
        coordGrid = grid;
        this.grid = new GridADT<PathNode>((int)coordGrid.GetSize().x, (int)coordGrid.GetSize().y);
        this.grid.GetHeight();
        this.grid.GetWidth();
        canGoDiagonally = false;
    }

    public Pathfinder(Grid grid, bool diagonals)
    {
        coordGrid = grid;
        this.grid = new GridADT<PathNode>((int)coordGrid.GetSize().x, (int)coordGrid.GetSize().y);
        this.grid.GetHeight();
        this.grid.GetWidth();
        canGoDiagonally = diagonals;
    }

    

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode node = new PathNode();
                node.position = new Vector2(x, y);
                node.gCost = int.MaxValue;
                node.CalculateFCost();
                node.parent = null;
                grid.Set(x, y, node);
            }
        }
        PathNode startNode = grid.Get(startX, startY);
        PathNode endNode = grid.Get(endX, endY);
        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();
        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while(openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if(currentNode == endNode)
            {
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbor in GetNeighbors(currentNode))
            {
                if (closedList.Contains(neighbor)) continue;
                if(!neighbor.isWalkable)
                {
                    closedList.Add(neighbor);
                    continue;
                }
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbor);
                if(tentativeGCost < neighbor.gCost)
                {
                    neighbor.parent = currentNode;
                    neighbor.gCost = tentativeGCost;
                    neighbor.hCost = CalculateDistanceCost(neighbor, endNode);
                    neighbor.CalculateFCost();

                    if(!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }
        return null;
    }

    private List<PathNode> GetNeighbors (PathNode currentNode)
    {
        List<PathNode> neighbors = new List<PathNode>();
        int cnpX = (int)currentNode.position.x;
        int cnpY = (int)currentNode.position.y;
        if (currentNode.position.x - 1 >= 0)
        {
            //L
            neighbors.Add(GetNode(cnpX - 1, cnpY));
            /*
            //LD
            if (cnpY - 1 >= 0) neighbors.Add(GetNode(cnpX - 1, cnpY - 1));
            //LU
            if (cnpY + 1 < grid.GetHeight()) neighbors.Add(GetNode(cnpX - 1, cnpY + 1));
            */
        }
        if (cnpX + 1 < grid.GetWidth())
        {
            //R
            neighbors.Add(GetNode(cnpX + 1, cnpY));
            /*
            //RD
            if (cnpY - 1 >= 0) neighbors.Add(GetNode(cnpX + 1, cnpY - 1));
            //RU
            if (cnpY + 1 < grid.GetHeight()) neighbors.Add(GetNode(cnpX + 1, cnpY + 1));
            */
        }
        //D
        if (cnpY - 1 >= 0) neighbors.Add(GetNode(cnpX, cnpY - 1));
        //U
        if (cnpY + 1 < grid.GetHeight()) neighbors.Add(GetNode(cnpX, cnpY + 1));

        return neighbors;
    }

    private PathNode GetNode (int x, int y)
    {
        return grid.Get(x, y);
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.parent != null)
        {
            path.Add(currentNode.parent);
            currentNode = currentNode.parent;
        }
        return path;
    }

    private PathNode GetLowestFCostNode (List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for(int i = 1; i < pathNodeList.Count; i++)
        {
            if(pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = (int)Mathf.Abs(a.position.x - b.position.x);
        int yDistance = (int)Mathf.Abs(a.position.y - b.position.y);
        int remaining = (int)Mathf.Abs(xDistance - yDistance);
        return MOVEMENTCOST_DIAGONAL * Mathf.Min(xDistance, yDistance) + MOVEMENTCOST_STRAIGHT * remaining;
    }
}

public class PathNode
{
    public Vector2 position;
    public int gCost;
    public int hCost;
    public int fCost;
    public PathNode parent;
    public GridADT<PathNode> grid;
    public bool isWalkable = true;
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    public PathNode()
    {
        position = new Vector2();
        gCost = 0;
        hCost = 0;
        fCost = 0;
        parent = null;
        grid = null;
    }
}

public class GridADT<T>
{
    private int width;
    private int height;
    private T[,] gridArray;

    public GridADT(int width, int height)
    {
        this.width = width;
        this.height = height;
        gridArray = new T[width, height];
    }

    public GridADT(int width, int height, T[,] data)
    {
        this.width = width;
        this.height = height;
        gridArray = data;
    }

    public T Get(int x, int y)
    {
        return gridArray[x, y];
    }

    public T Set(int x, int y, T data)
    {
        gridArray[x, y] = data;
        return gridArray[x, y];
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public T[,] GetData()
    {
        return gridArray;
    }
}
