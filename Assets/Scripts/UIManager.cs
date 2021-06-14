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

    public void BeatUpdate()
    {
       for(int i = 0; i < beatIndicators.Count; i++)
       {
            if(beatIndicators[i].gameObject)
            {

            }
       }
    }

    public void NewBeat (int beat)
    {
        if (beat > 0 && beatIndicators.Count > beat)
        {
            Destroy(beatIndicators[beat].gameObject);
        }
    }

    public void CreateBeatIndicator (int beat)
    {
        BeatIndicatorObject bo = new BeatIndicatorObject(beat, Instantiate(beatIndicator, beatWrapper));
        bo.gameObject.GetComponent<RectTransform>().anchoredPosition = (Vector2)beatWrapper.position - new Vector2(beatWrapper.sizeDelta.x / 2, 0);
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

