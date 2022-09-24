using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DenahWayangManager : MonoBehaviour
{
    public Image[] targetImage;
    public Sprite[] defaultSprite;
    public Sprite[] activeSprite;
    public Sprite[] showSprite;

    public AudioSource backsound;
    public AudioClip[] sounds;

    public Image soundButtonImage;
    public Sprite soundOffSprite;
    public Sprite soundOnSprite;

    // Start is called before the first frame update
    void Start()
    {
        /*for (int i = 0; i < targetImage.Length; i++)
        {
            defaultSprite[i] = targetImage[i].sprite;
        }*/
    }
    public void clickImage(int targetNumber)
    {
        Debug.Log("Berhasil");
        changeToDefaultSprite();
        PlaySound(targetNumber);
        targetImage[targetNumber-1].sprite = activeSprite[targetNumber-1];
        GameObject.Find("Canvas/Wayang Kulit/Denah Pentas/ijoBg/Scroll View/Viewport/Content").GetComponent<SliderController>().choose(targetNumber);
        GameObject.Find("Canvas/Wayang Kulit/Denah Pentas/Description Manager").GetComponent<DescriptionManager>().changeText(targetNumber);
    }

    void changeToDefaultSprite()
    {
        for (int i = 0; i < targetImage.Length; i++)
        {
            targetImage[i].sprite = defaultSprite[i];
        }
    }

    public void PlaySound(int thisTargetNumber)
    {
        if(sounds[thisTargetNumber-1] != null)
        {
            backsound.clip = sounds[thisTargetNumber - 1];
            backsound.Play();
        }
    }

    public void SetSound()
    {
        if (backsound.enabled)
        {
            backsound.enabled = false;
            soundButtonImage.sprite = soundOffSprite;
        }
        else
        {
            backsound.enabled = true;
            soundButtonImage.sprite = soundOnSprite;
        }
    }
}
