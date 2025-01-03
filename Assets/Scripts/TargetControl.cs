using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class TargetControl : MonoBehaviour
{
    public XRSlider slider;
    public Transform target;
    public float speed;
    private void Update()
    {    
        //Debug.Log(slider.value);
        if (slider.value > 0.9f)
        {
            speed = 2;
        }
        else if (slider.value < 0.1f)
        {
            speed = -2;
        }
        else
        {
            speed = 0;
        }

        Vector3 velocity = new Vector3(0, 0, speed * Time.deltaTime);
        //Debug.Log($"Speed : {speed} , velocity : {velocity}");
        target.position = target.position + velocity;
    }
}
