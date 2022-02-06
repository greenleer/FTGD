using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SceneCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        //Set cinemachine.Target =
        cinemachineVirtualCamera.Follow = NewPlayer.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
