using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 100f;
    public bool isDead = false;
    public Animator animator;
    public AudioSource takingDamage;
    public Text enemyCounterText;
    public GameObject levelEndPanel;
    public Animator anim;
    GameObject[] Enemies;
    float Timer;

    public ENEMY enemy;
    private void Start()
    {
        levelEndPanel.SetActive(false);
    }
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        enemy = GetComponent<ENEMY>();
    }
    void Update()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Timer += Time.deltaTime;
        if (Timer > 5f)
        {
            enemyCounterText.text = "ZOMBIES LEFT : " + Enemies.Length.ToString();
        }
        if (Enemies.Length <= 1)
        {
            EndGame();           
        }
        if (enemyHealth <= 0f)
        {
            takingDamage.Stop();
            animator.SetBool("Dead", true);      
            isDead = true;
            EnemyDead();
        }
    }
    public void EnemyDamage(float amount)
    {
        takingDamage.Play();
        enemyHealth -= amount;
        if (!isDead)
            enemy.isAware = true;
    }
    public void EnemyDead()
    {
        Destroy(gameObject, 3f);
    }
    public void EndGame()
    {
        levelEndPanel.SetActive(true);
        anim.SetBool("LevelEnd", true);
        StartCoroutine(NextLevel());
    }
    IEnumerator NextLevel()
    {    
        yield return new WaitForSeconds(10.0f);
        Debug.Log("LOADING...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
