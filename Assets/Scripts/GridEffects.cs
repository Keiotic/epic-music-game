using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEffects : MonoBehaviour
{
    public static GridEffects current;
    [SerializeField] private List<SpriteRenderer> resetTiles = new List<SpriteRenderer>();
    [SerializeField] private Color returnColor = new Color();
    [SerializeField] private float returnSpeed = 1.5f;
    GridManager grid;

    public class ColorFade
    {
        public Color targetColor = new Color();
        public SpriteRenderer renderer;
    }

    void Start()
    {
        grid = GridManager.current;
        returnSpeed = 1/BeatManager.current.GetTimeBetweenBeats();
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(resetTiles.Count>0)
        {
            for(int i = resetTiles.Count-1; i >= 0; i--)
            {
                if (!resetTiles[i])
                    resetTiles.RemoveAt(i);
                
      
                SpriteRenderer rTile = resetTiles[i];

                Color c = rTile.color;
                c.r = Mathf.MoveTowards(c.r, returnColor.r, Time.deltaTime * returnSpeed);
                c.g = Mathf.MoveTowards(c.g, returnColor.g, Time.deltaTime * returnSpeed);
                c.b = Mathf.MoveTowards(c.b, returnColor.b, Time.deltaTime * returnSpeed);

                c.a = Mathf.MoveTowards(c.a, returnColor.a, Time.deltaTime * returnSpeed);

                rTile.color = c;

                if (c.Equals(returnColor))
                    resetTiles.RemoveAt(i);
            }
        }
    }

    public void CreateColorBurst(Vector2 position, Color color)
    {
        SpriteRenderer sprite = grid.GetGridRep(position).GetComponent<SpriteRenderer>();
        sprite.color = color;
        if(!resetTiles.Contains(sprite)) resetTiles.Add(sprite);
    }
    
    
    public void SetStandardColor(Color color)
    {
        returnColor = color;
    }

}
