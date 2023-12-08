using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
//using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerHaptics : MonoBehaviour
{
    public GameObject avatarRoot;
    public ActionBasedController actionBasedController;

    public void Init(GameObject go)
    {
        avatarRoot = go;
        actionBasedController = GetComponent<ActionBasedController>();

        var rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.drag = 1000.0f;
            rb.angularDrag = 1000.0f;
        }

        var coll = gameObject.GetComponent<Collider>();
        if (coll == null)
            coll = gameObject.AddComponent<BoxCollider>();
        // Check the collider type and set bounds accordingly
        if (coll is BoxCollider boxCollider)
        {
            // Set bounds for BoxCollider
            boxCollider.size = new Vector3(0.1f,0.1f,0.1f); // Adjust the size as needed
        }
        else if (coll is SphereCollider sphereCollider)
        {
            // Set bounds for SphereCollider
            sphereCollider.radius = 0.05f; // Adjust the radius as needed
        }
        else if (coll is CapsuleCollider capsuleCollider)
        {
            // Set bounds for CapsuleCollider
            capsuleCollider.radius = 0.05f; // Adjust the radius as needed
            capsuleCollider.height = 1.0f; // Adjust the height as needed
        }
        else
        {
            Debug.LogWarning("Unsupported collider type. Bounds not set.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision detected with {collision.collider.gameObject.name} while at position {transform.position} with collider with bounds {GetComponent<Collider>().bounds}");
        // Skip ones we don't care about
        if (collision.collider.gameObject.name.StartsWith("XR Origin") || // XR Origin?
            avatarRoot == collision.collider.gameObject ||  // is this the avatar?
            collision.collider.gameObject.transform.IsChildOf(avatarRoot.transform) || // if the collided is a child of the avatar
            transform.IsChildOf(collision.collider.gameObject.transform) ||  // is this a child of the collided geometry 
            transform.IsChildOf(avatarRoot.transform) // is this a child of the avatar?
            )
            return;
        Debug.Log($"Impulse sent with {collision.collider.gameObject.name}");
        actionBasedController.SendHapticImpulse(1.0f, 0.5f);
    }
}