using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOUCHP: MonoBehaviour
{
    //public FixedJoystick fixedJoystick;
    //public FloatingJoystick floatingJoystick;
    public VariableJoystick variableJoystick;
    public FixedButton sprintButton;
    public FixedButton jumpButton;
    public FixedButton crouchButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var playercontroller = GetComponent<PlayerMove>();
        //playercontroller.RunAxis = fixedJoystick.Direction;
        playercontroller.RunAxis = variableJoystick.Direction;
        playercontroller.SprintAxis = sprintButton.Pressed;
        playercontroller.JumpAxis = jumpButton.Pressed;
        playercontroller.CrouchAxis = crouchButton.Pressed;
    }
}
