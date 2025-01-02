using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimation : MonoBehaviour
{
    public InputActionProperty activateAction;
    public InputActionProperty selectAction;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float triggerValue = activateAction.action.ReadValue<float>();
        anim.SetFloat("Trigger", triggerValue);

        float gripValue = selectAction.action.ReadValue<float>();
        anim.SetFloat("Grip", gripValue);
    }
}
