using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableRay : MonoBehaviour
{
    public GameObject leftRayInteractor;
    public GameObject rightRayInteractor;

    public XRDirectInteractor leftDirectInteractor;
    public XRDirectInteractor rightDirectInteractor;

    public InputActionProperty leftTriggerAction;
    public InputActionProperty rightTriggerAction;

    private void Update()
    {
        leftRayInteractor.SetActive(leftDirectInteractor.interactablesSelected.Count == 0 && leftTriggerAction.action.ReadValue<float>() > 0.1f);
        rightRayInteractor.SetActive(rightDirectInteractor.interactablesSelected.Count == 0 && rightTriggerAction.action.ReadValue<float>() > 0.1f);
    }
}
