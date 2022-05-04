using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class RayCast : MonoBehaviour
{
    public Camera Playercamera;
    public float FireRate = 10f;
    public float timebetweenNextShot = 0;
    public float Damage = 20f;
    public float Range = 100f;
    public float Force = 10f;

    public MouseLook mouse;
    public GunSound gunSound;
    public SwitchWeapon switchWeapon;

    [Header("Recoil")]
    public float vRecoil = 1f;
    public float hRecoil = 1f;

    [Header("Ammo Management")]
    public int ammoCount = 25;
    public int availableAmmo = 100;
    public int maxAmmo = 25;
    public int remainingAmmo;
    public float reloadTime = 2.20f;

    public Animator anim;
    public Text currentammotext;
    public bool isReloading;
    public bool isShooting;

    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletShell;
    public GameObject impactEffect;
    public GameObject impactEnemy;
    public GameObject impactTree;
    [HideInInspector]
    public bool ShootAxis;
    [HideInInspector]
    public bool ReloadAxis;

    // Start is called before the first frame update
    void Start()
    {
        ammoCount = maxAmmo;
        isShooting = false;
    }
    void OnEnable()
    {
        anim.SetBool("Reload", false);
        isReloading = false;
    }
    // Update is called once per frame
    void Update()
    {
        remainingAmmo = maxAmmo - ammoCount;
        currentammotext.text = ammoCount.ToString() + "/" + availableAmmo.ToString();
        if (isReloading)
        {
            muzzleFlash.Stop();
            bulletShell.Stop();
        }
        if (CrossPlatformInputManager.GetButtonDown("Reload") || ReloadAxis && ammoCount < maxAmmo)
        {
            mouse.AddRecoil(0, 0);
            isReloading = true;

            anim.SetBool("Reload", true);
            anim.SetBool("Recoil", false);
        }
        if (ammoCount <= 0 && availableAmmo > 0)
        {
            mouse.AddRecoil(0, 0);
            isReloading = true;
            anim.SetBool("Reload", true);
            anim.SetBool("Recoil", false);
            return;
        }
        if (CrossPlatformInputManager.GetButton("Attack") || ShootAxis && Time.time >= timebetweenNextShot && switchWeapon.selectedWeapon >= 0 && switchWeapon.selectedWeapon <= 3)
        {
            
            isShooting = true;
            timebetweenNextShot = Time.time + (1f / FireRate);
            float h = Random.Range(-hRecoil, hRecoil);
            if (availableAmmo >= 0 && ammoCount > 0)
            {
                anim.SetBool("Recoil", true);
                mouse.AddRecoil(vRecoil, h);
                SwitchPlay();
            }
        }
        if (CrossPlatformInputManager.GetButtonDown("Attack") || ShootAxis && Time.time >= timebetweenNextShot && switchWeapon.selectedWeapon >= 4)
        {
            isShooting = true;
            timebetweenNextShot = Time.time + (1f / FireRate);
            float v = Random.Range(-vRecoil, vRecoil);
            if (availableAmmo >= 0 && ammoCount > 0)
            {
                anim.SetBool("Recoil", true);
                mouse.AddRecoil(v, hRecoil);
                SwitchPlay();
            }
        }
        if (CrossPlatformInputManager.GetButtonUp("Attack") || !ShootAxis)
        {
            isShooting = false;
            muzzleFlash.Stop();
            bulletShell.Stop();
            mouse.AddRecoil(0, 0);
            anim.SetBool("Recoil", false);
        }
    }
    void SwitchPlay()
    {
        switch(switchWeapon.selectedWeapon)
        {
            case 0: if (isReloading == false) 
                { 
                    gunSound.UMPFire.Play();
                    gunSound.UMPBDrop.Play();         
                }
                if (isReloading == true)
                {
                    gunSound.UMPReload.Play();
                    gunSound.UMPReady.Play();
                }
                WeaponFire(); 
                break;
            case 1: if (isReloading == false) 
                {
                    gunSound.AKFire.Play();
                    gunSound.AKBDrop.Play();
                }
                if (isReloading == true)
                {
                    gunSound.AKReload.Play();
                    gunSound.AKReady.Play();
                }             
                WeaponFire(); 
                break;
            case 2: if (isReloading == false) 
                {
                    gunSound.M4Fire.Play();
                    gunSound.M4BDRop.Play();
                }
                if (isReloading == true)
                {
                    gunSound.M4Reload.Play();
                }
                WeaponFire(); 
                break;
            case 3: if (isReloading == false) 
                {
                    gunSound.BPFire.Play();
                    gunSound.BPBDrop.Play();
                }
                if (isReloading == true)
                {
                    gunSound.BPReload.Play();
                }
                WeaponFire(); 
                break;
            case 4: if (isReloading == false) 
                {
                    gunSound.SGFire.Play();
                    gunSound.SGBDrop.Play();
                }
                if (isReloading == true)
                {
                    gunSound.SGReload.Play();
                    gunSound.SGReady.Play();
                }
                WeaponFire(); 
                break;
             case 5: if (isReloading == false)
                {
                    gunSound.SFire.Play();
                    gunSound.SBDrop.Play();
                }
                if (isReloading == true)
                {
                    gunSound.SReload.Play();
                    gunSound.SReady.Play();
                }
                WeaponFire();
                break;
        }
    }
    void WeaponFire()
    {
        ammoCount--;
        muzzleFlash.Play();
        bulletShell.Play();
        RaycastHit hit;
        if (Physics.Raycast(Playercamera.transform.position, Playercamera.transform.forward, out hit,Range))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Enemy")
            {
                EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.EnemyDamage(Damage);
                }
                GameObject impactE = Instantiate(impactEnemy, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactE, 0.5f);
            }
            if (hit.transform.tag == "Tree")
            {
                GameObject impactT = Instantiate(impactTree, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactT, 0.5f);
            }
            if (hit.transform.tag != "Enemy")
            {
                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 0.5f);
            }            
        }     
    }
    void Play()
    {
        SwitchPlay();
        ammoCount++;
    }
    void Ready()
    {
        SwitchPlay();
        ammoCount++;
    }
    void Reload()
    {
        isReloading = true;
        ammoCount = maxAmmo;
        availableAmmo -= remainingAmmo;
        anim.SetBool("Reload", false);
        isReloading = false;
    }
    public void ReloadButton()
    {
        ReloadAxis = true;
    }public void ShootButton()
    {
        ShootAxis = true;
    }
}
