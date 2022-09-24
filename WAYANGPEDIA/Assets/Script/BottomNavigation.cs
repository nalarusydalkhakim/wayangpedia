using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomNavigation : MonoBehaviour
{

    public GameObject wayangPanel;
    public GameObject tokohPanel;
    public GameObject museumPanel;
    //public GameObject kelanaPanel;

    public RectTransform wayangTransform;
    public RectTransform tokohTransform;
    public RectTransform museumTransform;

    public Image wayangImage;
    public Image tokohImage;
    public Image museumImage;

    public Sprite wayangSprite;
    public Sprite tokohSprite;
    public Sprite museumSprite;

    public Sprite wayangSpriteActive;
    public Sprite tokohSpriteActive;
    public Sprite museumSpriteActive;

    Transform defaultWayangTransform;
    Transform defaultTokohTransform;
    Transform defautMuseumTransform;

    private void Start()
    {
        defaultWayangTransform = wayangTransform;
        defaultTokohTransform = tokohTransform;    
        defautMuseumTransform = museumTransform;
    }

    public void changePanel(string panelName)
    {
        if(panelName == "wayang")
        {
            wayangPanel.SetActive(true);
            tokohPanel.SetActive(false);
            museumPanel.SetActive(false);
            //Set Image Active and Non Active
            wayangImage.sprite = wayangSpriteActive;
            changeTransform(wayangTransform, wayangSpriteActive);
            tokohImage.sprite = tokohSprite;
            changeTransform(tokohTransform, tokohSprite);
            museumImage.sprite = museumSprite;
            changeTransform(museumTransform, museumSprite);
        }else if(panelName == "tokoh"){
            wayangPanel.SetActive(false);
            tokohPanel.SetActive(true);
            museumPanel.SetActive(false);
            //Set Image Active and Non Active
            wayangImage.sprite = wayangSprite;
            changeTransform(wayangTransform, wayangSprite);
            tokohImage.sprite = tokohSpriteActive;
            changeTransform(tokohTransform,tokohSpriteActive);
            museumImage.sprite = museumSprite;
            changeTransform(museumTransform, museumSprite);
        }
        else if(panelName == "museum"){
            wayangPanel.SetActive(false);
            tokohPanel.SetActive(false);
            museumPanel.SetActive(true);
            //Set Image Active and Non Active
            wayangImage.sprite = wayangSprite;
            changeTransform(wayangTransform, wayangSprite);
            tokohImage.sprite = tokohSprite;
            changeTransform(tokohTransform, tokohSprite);
            museumImage.sprite = museumSpriteActive;
            changeTransform (museumTransform, museumSpriteActive);
        }
    }
    
    public void changeTransform(RectTransform buttonTransform, Sprite buttonSprite)
    {
        float width = buttonSprite.rect.width/2;
        float height = buttonSprite.rect.height/2;
        buttonTransform.sizeDelta = new Vector2(width, height);
    }
}
