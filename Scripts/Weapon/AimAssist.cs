using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AimAssist : MonoBehaviour
{
    public Animator aimAnim;
    public bool aim;

    public GameObject Gun;
    public GameObject crossHair;
    public GameObject shotgunAim;
    public GameObject scopeOverlay;
    public GameObject mark;

    public GameObject weaponCamera;
    public Camera mainCamera;

    public float scopedFOV = 15f;
    public float normalFOV = 60f;

    public SwitchWeapon switchWeapon;
    [HideInInspector]
    public bool AimAxis;

    void Start()
    {
        aim = false;
        aimAnim.SetBool("Aim", false);
    }
    void OnEnable()
    {
        aim = false;
        aimAnim.SetBool("Aim", false);
    }
    void Update()
    {
        RayCast shoot = Gun.GetComponent<RayCast>();
        if (switchWeapon.selectedWeapon >= 0 && switchWeapon.selectedWeapon <= 3)
        {
            if (CrossPlatformInputManager.GetButtonDown("Aim") || AimAxis && shoot.isReloading == false)
            {
                aim = !aim;
            }
            if (aim)
            {
                crossHair.SetActive(false);
                shotgunAim.SetActive(false);
                mark.SetActive(false);
                scopeOverlay.SetActive(false);
                aimAnim.SetBool("Aim", true);
            }
            else if (!aim)
            {
                crossHair.SetActive(true);
                shotgunAim.SetActive(false);
                mark.SetActive(false);
                scopeOverlay.SetActive(false);
                aimAnim.SetBool("Aim", false);
            }
        }
        if (switchWeapon.selectedWeapon == 4)
        {
            if (CrossPlatformInputManager.GetButtonDown("Aim") || AimAxis && shoot.isReloading == false)
            {
                aim = !aim;
            }
            if (aim)
            {
                shotgunAim.SetActive(false);
                crossHair.SetActive(false);
                mark.SetActive(false);
                scopeOverlay.SetActive(false);
                aimAnim.SetBool("Aim", true);
            }
            else if (!aim)
            {
                shotgunAim.SetActive(true);
                crossHair.SetActive(false);
                mark.SetActive(false);
                scopeOverlay.SetActive(false);
                aimAnim.SetBool("Aim", false);
            }
        }
        if (switchWeapon.selectedWeapon == 5)
        {
            if (CrossPlatformInputManager.GetButtonDown("Aim") || AimAxis && shoot.isReloading == false)
            {
                aim = !aim;
            }
            if (aim)
            {
                StartCoroutine(Scoped());
                mark.SetActive(false);
                shotgunAim.SetActive(false);
                crossHair.SetActive(false);
                aimAnim.SetBool("Aim", aim);
            }
            else if (!aim)
            {
                UnScoped();
                mark.SetActive(true);
                shotgunAim.SetActive(false);
                crossHair.SetActive(false);
                aimAnim.SetBool("Aim", aim);
            }
        }
    }
    void UnScoped()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
        mainCamera.fieldOfView = normalFOV;

    }
    IEnumerator Scoped()
    {
        yield return new WaitForSeconds(0.15f);
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);
        mainCamera.fieldOfView = scopedFOV;
    }
    public void AimButton()
    {
        AimAxis = true;
    }
}