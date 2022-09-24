using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNavigation : MonoBehaviour
{
    public bool navigationActive = false;
    public GameObject navigationPanel;

    // Update is called once per frame
    void Update()
    {
        if (navigationActive)
        {
            navigationPanel.SetActive(true);
        }
        else
        {
            navigationPanel.SetActive(false);
        }
    }
}
