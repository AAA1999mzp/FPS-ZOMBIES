using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOUCHS : MonoBehaviour
{
    public FixedButton LeftArrow;
    public FixedButton RightArrow;
    public FixedButton aimButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var change = GetComponent<SwitchWeapon>();
        var aim = GetComponent<AimAssist>();
        change.lArrowAxis = LeftArrow.Pressed;
        change.rArrowAxis = RightArrow.Pressed;
        aim.AimAxis = aimButton.Pressed;
    }
}
