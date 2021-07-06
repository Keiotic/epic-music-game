using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntity : MonoBehaviour
{
    private Vector2 gridPosition = new Vector2();
    private GridManager gridManager;
    private bool caged;
    public float interpolateSpeed = 2;

    void Start()
    {
        gridManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GridManager>();
        //gridPosition = gridManager.FindNearestGridPos(Vector2.zero);
    }

    void Update()
    {
        
    }

    public void MoveRelativeToCurrentPosition(Vector2 movement)
    {
       gridPosition = gridManager.GetRelativeGridTranslation(gridPosition, movement, caged);  
    }

    public void MoveToAbsolutePosition(Vector2 gridpos)
    {
        gridPosition = gridpos;
    }

    public void Warp()
    {
        if(!gridManager)
        {
            gridManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GridManager>();
        }
        transform.position = gridManager.GridToWorldCoordinates(gridPosition);
    }

    public void LinearilyInterpolatePosition ()
    {
        transform.position = Vector2.MoveTowards(transform.position, gridManager.GridToWorldCoordinates(gridPosition), Time.deltaTime*interpolateSpeed);
    }

    public void SmoothInterpolatePosition()
    {

    }

    public void SetCaged (bool value)
    {
        caged = value;
    }
}
