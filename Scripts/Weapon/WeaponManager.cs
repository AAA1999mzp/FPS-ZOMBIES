using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Animator anim;
    public PlayerMove player;

    void Update()
    {
        if (player.isidle == true)
        {
            anim.SetBool("Breath", true);
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);
        }
        else
        {
            if (player.iswalking == false)
            {
                anim.SetBool("Breath", false);
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
            }
            if (player.iswalking == true)
            {
                anim.SetBool("Breath", false);
                anim.SetBool("Run", false);
                anim.SetBool("Walk", true);
            }
        }
    }
}
