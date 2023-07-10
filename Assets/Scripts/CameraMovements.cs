using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovements : MonoBehaviour
{
    bool isPlayerOnTheMap = false;
    private CinemachineVirtualCamera VirtualCamera;

    void Start()
    {
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        //Create a new state in game manager to handle the camera reviewing the map, and then starting the level

    }

    // Update is called once per frame
    void Update()
    {
        // Check if player has been selected and on the map. Then the camera should follwo him/her
        if (!isPlayerOnTheMap)
        {
            if (GameObject.FindWithTag("Player"))
            {
                VirtualCamera.Follow = GameObject.FindWithTag("Player").transform;
            }
        }
    }
}
