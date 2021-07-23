using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Healthunits : UI_Healthrenderer
{
    [SerializeField] protected Sprite[] stateRenders; //highest render last.
    [SerializeField] protected int healthPerUnit;
    [SerializeField] protected float padding;
    [SerializeField] protected RectTransform layoutGroupTransform;
    [SerializeField] protected GameObject indicator;

    protected List<Image> renderImages = new List<Image>();
    public override void Initialize(float health, float maxHealth)
    {
        //layoutGroupTransform.GetComponent<GridLayoutGroup>().
        int imageIndex = 0;
        int units = (int)Mathf.Ceil(maxHealth / (stateRenders.Length * healthPerUnit));
        int fullUnits = (int)Mathf.Floor(health / (stateRenders.Length * healthPerUnit));
        print(units);
        int semiFullUnits = (int)Mathf.Floor(health / (healthPerUnit));
        for (int i = 0; i < units; i++)
        {
            renderImages.Add(CreateNewUnit());
        }
        for(int i = 0; i < fullUnits; i++)
        {
            renderImages[i].sprite = stateRenders[stateRenders.Length-1];
        }
        
    }
    public override void UpdateHealth(float health, float maxHealth)
    {
        int imageIndex = 0;


    }

    public Image CreateNewUnit()
    {
        GameObject go = Instantiate(indicator);
        Image img;
        if (!go.GetComponent<Image>())
        {
            img = go.AddComponent<Image>();
        }
        else
        {
            img = go.GetComponent<Image>();
        }
        go.transform.SetParent(layoutGroupTransform);
        return img;
    }

    public override void Start()
    {

    }
    public override void Update()
    {

    }
}
