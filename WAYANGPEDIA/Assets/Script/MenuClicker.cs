using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClicker : MonoBehaviour
{
    public GameObject thisPanel;
    public GameObject targetPanel;

    public void changePanel()
    {
        thisPanel.SetActive(false);
        targetPanel.SetActive(true);
    }

    public void showPanel()
    {
        targetPanel.SetActive(true);
    }

    public void hidePanel()
    {
        targetPanel.SetActive(false);
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
