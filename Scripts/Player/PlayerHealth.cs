using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;
    public float currentHealth;
    public float maxHealth = 100f;
    public Slider healthSlider;
    public Text healthCounter;
    public bool isDead = false;
    public static bool isGameOver = false;
    public GameObject gameOverPanel;
    public Animator animator;


    [Header("Damage Screen")]
    public Color damageColor;
    public Image damageImage;
    float colorSmoothing = 6f;
    bool isTakingDamage = false;

    private void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.value = maxHealth;
        UpdateHealthCounter();
        damageImage.color = Color.clear;
        gameOverPanel.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            currentHealth = maxHealth;
            healthSlider.value = maxHealth;
            UpdateHealthCounter();
        }
        if (isTakingDamage && currentHealth != maxHealth)
        {
            damageImage.color = damageColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, colorSmoothing * Time.deltaTime);
        }
        isTakingDamage = false;
    }
    public void PlayerDamage(float damage)
    {
        if (currentHealth > 0)
        {
            if (damage > currentHealth)
            {
                isDead = true;
                Debug.Log("Player is Dead!");           
            }
            else
            {
                isTakingDamage = true;
                currentHealth -= damage;
                healthSlider.value -= damage;
            }
            UpdateHealthCounter();
        }
        else if (currentHealth == 0)
        {
            Dead();
        }
    }
    public void Dead()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        animator.SetBool("GameOver", true);
        Debug.Log("GAME OVER!!");
        StartCoroutine(NewGame());
    }
    IEnumerator NewGame()
    {
        yield return new WaitForSeconds(10.0f);
        Debug.Log("NEW GAME!");
        SceneManager.LoadScene("MAIN MENU");
    }
    void UpdateHealthCounter()
    {
        healthCounter.text = currentHealth.ToString();
    }
}

