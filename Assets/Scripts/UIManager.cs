using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject beatIndicator;
    public Vector2 beatPos;
    public RectTransform beatWrapper;
    public float beatMoveSpeed;
    public List<BeatIndicatorObject> beatIndicators = new List<BeatIndicatorObject>();

    public class BeatIndicatorObject
    {
        public int id;
        public GameObject gameObjectLeft;
        public GameObject gameObjectRight;
        public BeatIndicatorObject(int id, GameObject gameObjectLeft, GameObject gameObjectRight)
        {
            this.id = id;
            this.gameObjectLeft = gameObjectLeft;
            this.gameObjectRight = gameObjectRight;
        }
    }
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void BeatUpdate(float passedTime, float timeBetweenBeats)
    {
       for(int i = 0; i < beatIndicators.Count; i++)
       {
            if(beatIndicators[i].gameObjectLeft && beatIndicators[i].gameObjectRight)
            {
                BeatIndicatorObject bo = beatIndicators[i];
                SetBOPosition(bo.gameObjectLeft.GetComponent<RectTransform>(), bo.id, passedTime, timeBetweenBeats, 1);
                SetBOPosition(bo.gameObjectRight.GetComponent<RectTransform>(), bo.id, passedTime, timeBetweenBeats, -1);
            }
       }
    }

    public void SetBOPosition (RectTransform boTransform, int id, float passedTime, float timeBetweenBeats,int directionModifier)
    {
        Vector2 intendedPos = boTransform.anchoredPosition;
        Vector2 border = new Vector2();
        if(directionModifier>0)
        {
            border.x = -Mathf.Infinity;
            border.y = 0;
        }
        else
        {
            border.x = 0;
            border.y = Mathf.Infinity;
        }
        intendedPos.x = Mathf.Clamp((passedTime - timeBetweenBeats * id) * GetBeatWrapperWidth() / 2*directionModifier, border.x, border.y);
        boTransform.anchoredPosition = intendedPos;
    }

    public void RemoveBeat (int beat)
    {
        BeatIndicatorObject b = beatIndicators[0];
        beatIndicators.Remove(b);
        Destroy(b.gameObjectLeft);
        Destroy(b.gameObjectRight);
    }

    public void CreateBeatIndicator (int beat)
    {
        BeatIndicatorObject bo = new BeatIndicatorObject(beat, Instantiate(beatIndicator, beatWrapper), Instantiate(beatIndicator, beatWrapper));
        bo.gameObjectLeft.GetComponent<RectTransform>().anchoredPosition = beatWrapper.anchoredPosition - new Vector2(GetBeatWrapperWidth() / 2, 0);
        bo.gameObjectLeft.transform.name = "BeatL-" + beat;

        bo.gameObjectRight.GetComponent<RectTransform>().anchoredPosition = beatWrapper.anchoredPosition + new Vector2(GetBeatWrapperWidth() / 2, 0);
        bo.gameObjectRight.transform.name = "BeatR" + beat;
        beatIndicators.Add(bo);
    }

    public float GetBeatMovementSpeed ()
    {
        return beatMoveSpeed;
    }
    public void SetBeatMovementSpeed(float speed)
    {
        beatMoveSpeed = speed;
    }

    public float GetBeatWrapperWidth ()
    {
        return beatWrapper.sizeDelta.x;
    }
}

