using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public GameObject scrollbar;
    float scrollPos = 0;
    float[] pos;
    int posisi;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void choose(int thisPos)
    {
        posisi = thisPos - 1;
        scrollPos = pos[posisi];
    }

    public void next()
    {
        if(posisi < pos.Length - 1)
        {
            posisi += 1;
            scrollPos = pos[posisi];

            GameObject.Find("Canvas/Wayang Kulit/Denah Pentas/Denah Manager").GetComponent<DenahWayangManager>().clickImage(posisi + 1);
        }
    }
    public void prev()
    {
        if(posisi > 0)
        {
            posisi -= 1;
            scrollPos = pos[posisi];

            GameObject.Find("Canvas/Wayang Kulit/Denah Pentas/Denah Manager").GetComponent<DenahWayangManager>().clickImage(posisi + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f/ (pos.Length - 1);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp (scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.15f);
                }
            }
        }
    }
}
