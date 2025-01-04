using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustDestroy : MonoBehaviour
{
    public float time = 3f;
    private void Start()
    {
        Destroy(gameObject, time);
    }
}
