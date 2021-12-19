using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntity : MonoBehaviour
{
    [SerializeField] private Vector2 gridPosition = new Vector2();
    private GridManager gridManager;
    private BeatManager beatManager;
    private bool caged;
    [SerializeField] private float interpolateSpeed = 2;
    [SerializeField] private bool interpolateEntity = true;

    void Awake()
    {
        gridManager = GridManager.current;
        beatManager = BeatManager.current;
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
    public float GetInterpolationSpeed()
    {
        return interpolateSpeed;
    }

    public void LinearilyInterpolatePosition()
    {
        if(interpolateEntity)
            transform.position = Vector2.MoveTowards(transform.position, gridManager.GridToWorldCoordinates(gridPosition), Time.deltaTime * interpolateSpeed);
    }

    public void LinearilyInterpolateToPosition(Vector2 targetPosition)
    {
        if (interpolateEntity)
            transform.position = Vector2.MoveTowards(transform.position, gridManager.GridToWorldCoordinates(targetPosition), Time.deltaTime * interpolateSpeed);
    }

    public void SmoothInterpolateToPosition(Vector2 startPos, Vector2 endPos)
    {
        /*
        float t = 1-(beatManager.GetRelativeBeatTimePerc()-0.5f)-1;
        float p = -1.9638279604492f * Mathf.Pow(t, 3) + 2.98159355255391f * Mathf.Pow(t, 2) - 0.0177655920898f * Mathf.Pow(t, 1);
        Debug.Log(t + "-" + p);

        if (interpolateEntity)
            transform.position = Vector2.Lerp(startPos, endPos, p);
       */
    }

    public void SetCaged(bool value)
    {
        caged = value;
    }

    public Vector2 GetPosition()
    {
        return gridPosition;
    }

    public void OnDestroy()
    {
        
    }
}
