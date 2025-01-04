using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class TargetControl : MonoBehaviour
{
    public XRSlider slider;
    public Transform target;
    public GameObject hitMarker;

    private float speed;
    private bool isMax;
    private bool isMin;

    private void Update()
    {
        //Debug.Log(slider.value);
        if (slider.value > 0.9f)
        {
            if (isMax)
                speed = 0;
            else
                speed = 5;
        }
        else if (slider.value < 0.1f)
        {
            if (isMin)
                speed = 0;
            else
                speed = -5;
        }
        else
        {

            speed = 0;
        }

        Vector3 velocity = new Vector3(0, 0, speed * Time.deltaTime);
        //Debug.Log($"Speed : {speed} , velocity : {velocity}");
        target.position = target.position + velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter");
        if (other.CompareTag("Min"))
        {
            isMin = true;
        }
        if (other.CompareTag("Max"))
        {
            isMax = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("TriggerExit");

        if (other.CompareTag("Min"))
        {
            isMin = false;
        }
        if (other.CompareTag("Max"))
        {
            isMax = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("CollisionEnter");
        if (other.gameObject.CompareTag("Bullet"))
        {
            ContactPoint contactPoint = other.GetContact(0);
            Vector3 hitPos = contactPoint.point;
            Instantiate(hitMarker, hitPos, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
