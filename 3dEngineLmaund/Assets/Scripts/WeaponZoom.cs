using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    //params
    float zoomIn = 20f;
    float zoomOut = 60f;

    //cached refs
    [SerializeField] Camera playerCam;

    //states
    bool zoomToggle = false;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!zoomToggle)
            {
                zoomToggle = true;
                playerCam.fieldOfView = zoomIn;

            }

            else if (zoomToggle)
            {
                zoomToggle = false;
                playerCam.fieldOfView = zoomOut;
            }
        }
    }
}
