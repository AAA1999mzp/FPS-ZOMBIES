using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class MouseLook : MonoBehaviour
{
    public float sensitivity;
    public Transform player;
    private float rot;
    private float vRecoil;
    private float hRecoil;  
    [HideInInspector]
    public Vector2 LookAxis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = LookAxis.x * sensitivity * Time.deltaTime + vRecoil;
        float y = LookAxis.y * sensitivity * Time.deltaTime + hRecoil;

        rot -= y;
        rot = Mathf.Clamp(rot, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rot, 0f, 0f);
        player.Rotate(player.up * x);

    }
    public void MouseSensitiviy(float S)
    {
        sensitivity = S;
    }
    public void AddRecoil(float v,float h)
    {
        vRecoil = v;
        hRecoil = h;
    }
    private bool IsPointerOverUIObject()
    {

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;

    }
}
