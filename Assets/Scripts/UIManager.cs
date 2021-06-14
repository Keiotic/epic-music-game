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
        public GameObject gameObject;
        public BeatIndicatorObject(int id, GameObject gameObject)
        {
            this.id = id;
            this.gameObject = gameObject;
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
            if(beatIndicators[i].gameObject)
            {
                BeatIndicatorObject bo = beatIndicators[i];
                RectTransform boTransform = bo.gameObject.GetComponent<RectTransform>();
                Vector2 intendedPos = boTransform.anchoredPosition;
                intendedPos.x += beatMoveSpeed*Time.deltaTime;
                boTransform.anchoredPosition = intendedPos;
            }
       }
    }

    public void NewBeat (int beat)
    {

    }

    public void CreateBeatIndicator (int beat)
    {
        BeatIndicatorObject bo = new BeatIndicatorObject(beat, Instantiate(beatIndicator, beatWrapper));
        bo.gameObject.GetComponent<RectTransform>().anchoredPosition = beatWrapper.anchoredPosition - new Vector2(GetBeatWrapperWidth() / 2, 0);
        bo.gameObject.transform.name = "Beat-" + beat;
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

