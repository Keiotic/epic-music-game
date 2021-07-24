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
        int renderLength = stateRenders.Length-1;
        int units = (int)Mathf.Ceil(maxHealth / (renderLength * healthPerUnit));
        int fullUnits = (int)Mathf.Floor(health / (renderLength * healthPerUnit));
        for (int i = 0; i < units; i++)
        {
            renderImages.Add(CreateNewUnit());
            if(i<fullUnits)
            {
                renderImages[i].sprite = stateRenders[renderLength];
            }
            else
            {
                renderImages[i].sprite = stateRenders[0];
            }
        }
        int finalUnitHealth = (int)health - (int)fullUnits*healthPerUnit*renderLength;
        if (finalUnitHealth > 0)
        {
            int finalUnitState = (int)Mathf.Round(finalUnitHealth / healthPerUnit);
            renderImages[renderImages.Count - 1].sprite = stateRenders[finalUnitState];
        }
    }
    public override void UpdateHealth(float health, float maxHealth)
    {
        int renderLength = stateRenders.Length - 1;
        int units = (int)Mathf.Ceil(maxHealth / (renderLength * healthPerUnit));
        int fullUnits = (int)Mathf.Floor(health / (renderLength * healthPerUnit));

        for (int i = 0; i < units; i++)
        {
            if(renderImages.Count-1 < i)
                renderImages.Add(CreateNewUnit());
            if (i < fullUnits)
            {
                renderImages[i].sprite = stateRenders[renderLength];
            }
            else
            {
                renderImages[i].sprite = stateRenders[0];
            }
        }

        int finalUnitHealth = (int)health - (int)fullUnits * healthPerUnit * renderLength;
        if (finalUnitHealth > 0)
        {
            int finalUnitState = (int)Mathf.Round(finalUnitHealth / healthPerUnit);
            renderImages[fullUnits].sprite = stateRenders[finalUnitState];
        }
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
