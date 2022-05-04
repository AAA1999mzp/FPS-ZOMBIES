using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER : MonoBehaviour
{
    public AudioClip shootSound;
    public float soundIntensity = 20f;
    public float walkPerceptionRadius = 1f;
    public float runPerceptionRadius = 2f;
    public LayerMask isEnemy;
    public SphereCollider sphereCollider;
    public PlayerMove playerMove;
    public GameObject Weapon;
    private AudioSource audioSource;

    // Start is called before the first frame update
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sphereCollider = GetComponent<SphereCollider>();
        playerMove = GetComponent<PlayerMove>();
    }
    // Update is called once per frame
    public void Update()
    {
        RayCast rayCast = Weapon.GetComponent<RayCast>();
        if (rayCast.isShooting == true)
        {
            Fire();
        }
        if (!playerMove.iswalking)
        {
            sphereCollider.radius = runPerceptionRadius;
        }
        else if (playerMove.iswalking)
        {
            sphereCollider.radius= walkPerceptionRadius;
        }
    }
    public void Fire()
    {
        audioSource.PlayOneShot(shootSound);
        Collider[] enemies = Physics.OverlapSphere(transform.position, soundIntensity, isEnemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<ENEMY>().OnAware();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<ENEMY>().OnAware();
        }
    }
}
