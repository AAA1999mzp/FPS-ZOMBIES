using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOUCHE : MonoBehaviour
{
    public FixedButton pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pause = GetComponent<PauseMenu>();
        pause.PauseAxis = pauseButton.Pressed;
    }
}
