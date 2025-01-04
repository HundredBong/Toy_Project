using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTarget : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            ButtonControlManager.Instance.score++;
            ButtonControlManager.Instance.UpdateScore();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    
}
