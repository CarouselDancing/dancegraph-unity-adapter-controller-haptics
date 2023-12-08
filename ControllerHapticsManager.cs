using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerHapticsManager : MonoBehaviour
{
    public List<ActionBasedController> xrControllers = new();

    void OnEnable()
    {

    }

    void OnDisable()
    {
    }

    void FindAndStoreControllers()
    {
        foreach (var go in Resources.FindObjectsOfTypeAll(typeof(GameObject)).Where(go_ => go_.GetComponent<ActionBasedController>() != null).Select(go_ => ((GameObject)go_)))
        {
            var cmp = go.GetComponent<ControllerHaptics>();
            if(cmp == null)
                cmp = go.AddComponent<ControllerHaptics>();
            cmp.Init(gameObject);
        }

    }

    private void Start()
    {
        FindAndStoreControllers();
    }

    private void Update()
    {
    }

    private void OnGUI()
    {
    }
}