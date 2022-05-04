using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] weaponIndicator;
    public SwitchWeapon switchWeapon;
    public PlayerMove player;
    public GameObject[] crouched;

    // Start is called before the first frame update
    void Start()
    {
        weaponIndicator[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < weaponIndicator.Length; i++)
        {
            if (i == switchWeapon.selectedWeapon)
                weaponIndicator[i].SetActive(true);
            else
                weaponIndicator[i].SetActive(false);
        }
        if (!player.Crouched)
        {
            crouched[0].SetActive(true);
            crouched[1].SetActive(false);
        }
        else if (player.Crouched)
        {
            crouched[0].SetActive(false);
            crouched[1].SetActive(true);
        }
    }
}
