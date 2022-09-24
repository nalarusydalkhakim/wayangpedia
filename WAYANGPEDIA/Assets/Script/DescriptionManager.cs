using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionManager : MonoBehaviour
{
    public Text titleText;
    public Text descriptText;
    public string[] title;
    public string[] descript;

    public void changeText(int position)
    {
        titleText.text = title[position - 1];
        descriptText.text = descript[position - 1];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
