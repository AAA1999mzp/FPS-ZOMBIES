using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SwitchWeapon : MonoBehaviour
{
    public Animator takeanim;
    public GameObject[] weapons;
    public int selectedWeapon = 0;
    [HideInInspector]
    public bool lArrowAxis;
    [HideInInspector]
    public bool rArrowAxis;

    // Start is called before the first frame update
    void Start()
    {
        weapons[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;
        if (CrossPlatformInputManager.GetButtonDown("Right Arrow") || rArrowAxis)
        {
            if (selectedWeapon >= weapons.Length - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (CrossPlatformInputManager.GetButtonDown("Left Arrow") || lArrowAxis)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = weapons.Length - 1;
            else
                selectedWeapon--;
        }
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
            SelectWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Length >= 2)
        {
            selectedWeapon = 1;
            SelectWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Length >= 3)
        {
            selectedWeapon = 2;
            SelectWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && weapons.Length >= 4)
        {
            selectedWeapon = 3;
            SelectWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && weapons.Length >= 5)
        {
            selectedWeapon = 4;
            SelectWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && weapons.Length >= 6)
        {
            selectedWeapon = 5;
            SelectWeapon();
        }
    }
    void SelectWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == selectedWeapon)
            {
                StartCoroutine(TakeIN(i));
            }
            else
            {
                StartCoroutine(TakeOUT(i));
            }
        }
    }
    IEnumerator TakeIN(int i)
    {
        
        takeanim.SetBool("TakeIN", true);
        yield return new WaitForSeconds(0.5f);
        weapons[i].SetActive(true);
        takeanim.SetBool("TakeIN", false);
        takeanim.SetBool("Take", true);
        yield break;

    }
    IEnumerator TakeOUT(int i)
    {
        takeanim.SetBool("TakeOUT", true);
        yield return new WaitForSeconds(0.5f);
        weapons[i].SetActive(false);
        takeanim.SetBool("TakeOUT", false);
        takeanim.SetBool("Take", false);
        yield break;
    }
    public void LeftArrowButton()
    {
        lArrowAxis = true;
    }
    public void RightArrowButton()
    {
        rArrowAxis = true;
    }
}