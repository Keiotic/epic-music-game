using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntity : MonoBehaviour
{
    [SerializeField] private Vector2 gridPosition = new Vector2();
    private GridManager gridManager;
    private bool caged;
    [SerializeField] private float interpolateSpeed = 2;
    [SerializeField] private bool interpolateEntity = true;

    void Awake()
    {
        gridManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GridManager>();
        //gridPosition = gridManager.FindNearestGridPos(this.transform.position);
    }

    void Update()
    {

    }

    public void MoveRelativeToCurrentPosition(Vector2 movement)
    {
        gridPosition = gridManager.GetRelativeGridTranslation(gridPosition, movement, caged);
    }

    public void SetAutomaticInterpolation(bool value)
    {
        interpolateEntity = value;
    }

    public void MoveToAbsolutePosition(Vector2 gridpos)
    {
        gridPosition = gridpos;
    }

    public void Warp()
    {
        if (!gridManager)
        {
            gridManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GridManager>();
        }
        transform.position = gridManager.GridToWorldCoordinates(gridPosition);
    }

    public void SetInterpolationSpeed(float interpolationSpeed)
    {
        interpolateSpeed = interpolationSpeed;
    }

    public void LinearilyInterpolatePosition()
    {
        if(interpolateEntity)
            transform.position = Vector2.MoveTowards(transform.position, gridManager.GridToWorldCoordinates(gridPosition), Time.deltaTime * interpolateSpeed);
    }

    public void SmoothInterpolatePosition()
    {

    }

    public void SetCaged(bool value)
    {
        caged = value;
    }

    public Vector2 GetPosition()
    {
        return gridPosition;
    }
}
