using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOUCHW : MonoBehaviour
{
    public FixedButton shootButton;
    public FixedButton reloadButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var shoot = GetComponent<RayCast>();
        shoot.ShootAxis = shootButton.Pressed;
        shoot.ReloadAxis = reloadButton.Pressed;
    }
}
