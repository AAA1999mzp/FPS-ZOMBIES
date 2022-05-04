using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PauseMenu : MonoBehaviour
{
    public static bool GameisPaused = false; 
    public AudioMixer audioMixer;
    public GameObject pauseMenuPanel;
    public GameObject markImage;
    public GameObject ammoText;
    public GameObject player;
    public GameObject mouse;
    public GameObject weapon;
    [HideInInspector]
    public bool PauseAxis;

    public MouseLook mouseLook;
    public Slider sensitivitySlider;

    void Start()
    {
        mouseLook = GetComponent<MouseLook>();
        pauseMenuPanel.SetActive(false);
        sensitivitySlider.value = PlayerPrefs.GetFloat("Mouse Sensitivity", 10f);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullScreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }
    public void ApplySenitivity(float Sensitivity)
    {
        mouseLook.MouseSensitiviy(sensitivitySlider.value);
    }
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Pause") || PauseAxis)
        {
            if(GameisPaused)
                Resume();
            else
                Pause();
        }
    }
    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        markImage.SetActive(false);
        ammoText.SetActive(false);
        Time.timeScale = 0f;
        GameisPaused = true;
        player.GetComponent<PlayerMove>().enabled = false;
        mouse.GetComponent<MouseLook>().enabled = false;
        weapon.GetComponent<RayCast>().enabled = false;
        //Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        markImage.SetActive(true);
        ammoText.SetActive(true);
        Time.timeScale = 1f;
        GameisPaused = false;
        player.GetComponent<PlayerMove>().enabled = true;
        mouse.GetComponent<MouseLook>().enabled = true;
        weapon.GetComponent<RayCast>().enabled = true;
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Restart()
    {
        Debug.Log("Restart!");
        SceneManager.LoadScene("NEW MAP");
    }
    public void Menu()
    {
        Debug.Log("Loading Menu!!");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MAIN MENU");
    }
    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    public void PauseButton()
    {
        PauseAxis = true;
    }
}
