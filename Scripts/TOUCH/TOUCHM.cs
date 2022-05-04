using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOUCHM : MonoBehaviour
{
    public FixedTouchField fixedTouchPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mouselook = GetComponent<MouseLook>();
        mouselook.LookAxis = fixedTouchPanel.TouchDist;
    }
}
