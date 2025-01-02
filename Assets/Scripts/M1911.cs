using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class M1911 : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")][SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")][SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")][SerializeField] private float ejectPower = 150f;

    private XRGrabInteractable interactable;
    public XRSocketInteractor socketInteractor;
    public M1911Magazine mag;

    private bool isReloaded = false;
    public GameObject chamberBullet;

    private void Awake()
    {
        interactable = GetComponentInParent<XRGrabInteractable>();
        //socketInteractor = GetComponentInChildren<XRSocketInteractor>();
    }

    public void OnEnable()
    {
        //Debug.Log(interactable.gameObject.name);
        interactable.activated.AddListener(ShootEvent);

        socketInteractor.selectEntered.AddListener(AddMag);
        socketInteractor.selectExited.AddListener(RemoveMag);
    }



    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
    }


    private void ShootEvent(ActivateEventArgs args)
    {
        //Debug.Log("이벤트 호출됨");
        //Shoot();
        if (mag == null)
        {
            Debug.LogError("탄창 없음");
            return;
        }

        if (0 < mag.ammo && isReloaded)// && gunAnimator.GetBool("SlideStop") == false)
        {
            gunAnimator.SetTrigger("Fire");
            
        }
        //else if (mag.ammo == 1 && isReloaded)
        //{
        //    gunAnimator.SetBool("SlideStop", true);
        //}
        else
        {
            Debug.Log("탄약이엄서요");
        }

    }

    public void AddMag(SelectEnterEventArgs args)
    {
        Debug.Log("Add Mag 이벤트 호출됨");

        mag = args.interactableObject.transform.GetComponent<M1911Magazine>();
        if (mag.ammo > 0)
        {
            chamberBullet.SetActive(true);
        }
    }

    public void RemoveMag(SelectExitEventArgs args)
    {
        Debug.Log("Remove Mag 이벤트 호출됨");

        mag = null;
        isReloaded = false;
    }

    public void PullSlide()
    {
        //gunAnimator.SetBool("SlideStop", false);
        isReloaded = true;
    }

    //애니메이션 클립에서 이벤트로 호출됨
    void Shoot()
    {
        mag.UseAmmo();
        if (mag.ammo < 0)
        {
            chamberBullet.SetActive(false);
        }
        Debug.Log($"CurrentAmmo : {mag.ammo}");
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

}
