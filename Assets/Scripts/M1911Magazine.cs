using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M1911Magazine : MonoBehaviour
{
    public int ammo = 8;
    public GameObject loadedMagazine;
    public GameObject emptyMagazine;

    public void OnEnable()
    {
        ammo = 8;
        UpdateMagazineState();
    }

    public void UseAmmo()
    {
        ammo--;
        UpdateMagazineState();
    }

    

    private void UpdateMagazineState()
    {
        if (ammo > 0)
        {
            loadedMagazine.SetActive(true);
            emptyMagazine.SetActive(false);
        }
        else
        {
            loadedMagazine.SetActive(false);
            emptyMagazine.SetActive(true);
        }
    }
}
