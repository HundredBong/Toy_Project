using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class M1911Slide : MonoBehaviour
{
    public UnityEvent pullSlide;
    public Transform target;
    public float offset;
    public bool wasPulled;
    private bool grabSlide;
    private XRGrabInteractable interactable;
    private void Awake()
    {
        interactable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(GrabSlide);
        interactable.selectExited.AddListener(ReleaseSlide);
    }

    private void ReleaseSlide(SelectExitEventArgs arg0)
    {
        grabSlide = false;
    }

    private void GrabSlide(SelectEnterEventArgs arg0)
    {
        grabSlide = true;
    }

    private void FixedUpdate()
    {
        if(grabSlide == false) { return; }
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance < offset && wasPulled == false)
        {
            //Debug.Log("³¢¾æÈ£¿ì");
            pullSlide.Invoke();
            wasPulled = true;
        }
        else if (offset <= distance)
        {
            wasPulled = false;
        }
        //Debug.Log(distance);
    }
}
