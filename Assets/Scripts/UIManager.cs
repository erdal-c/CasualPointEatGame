using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text rightClickText;

    public Slider expSlider;
    public Slider healthSlider;
    float sliderValue;

    PlayerContoler player;
    GameManager gameManager;

    public GameObject LevelUpPanel;
    public TMP_Text SkillPointText;
    public Button button1;
    public TMP_Text button1Text;
    public Button button2;
    public TMP_Text button2Text;

    public GameObject MenuPanel;
    public TMP_Text LevelText;
    public TMP_Text PointText;
    public TMP_Text HealthStatText;
    public TMP_Text SpeedStatText;
    public TMP_Text DashPowerStatText;
    public TMP_Text DashTimeStatText;

    public GameObject GameOverPanel;
    public TMP_Text GmOverLevelText;
    public TMP_Text GmOverPointText;

    public AudioSource SelectBipSound;

    float x, y;
    bool randomized = true;
    bool isAlphaHigh = true;

    public bool RandomizedProperty { get { return randomized; } set { randomized = value; } }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        player = FindObjectOfType<PlayerContoler>();
        gameManager= FindObjectOfType<GameManager>();

        SelectBipSound.ignoreListenerPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)) 
        {
            MenuOpen();
            if (gameManager.LevelSkillPointProperty > 0 || LevelUpPanel.activeSelf == true)
            {
                LevelUpMenuOpen();
            }

        }
        if(SceneManager.GetActiveScene().buildIndex== 1) 
        {
            RightClickLevelUp();
        }
    }

    public void HealthText(float playerhealth)
    {
        healthSlider.value = playerhealth;
    }

    public void RightClickLevelUp()
    {
        Color color;

        if (gameManager.LevelSkillPointProperty > 0)
        {
            rightClickText.gameObject.SetActive(true);

            if (isAlphaHigh == true)
            {
                color = rightClickText.color;
                color.a -= Time.deltaTime;
                rightClickText.color = color;
                if (rightClickText.color.a <= 0.2)
                {
                    isAlphaHigh = false;
                }
            }

            if(isAlphaHigh == false)
            {
                color = rightClickText.color;
                color.a += Time.deltaTime;
                rightClickText.color = color;
                if(rightClickText.color.a >= 1f)
                {
                    isAlphaHigh = true;
                }
            }
        }
        else
        {
            rightClickText.gameObject.SetActive(false);
            rightClickText.color = new Color(rightClickText.color.r,
                                                 rightClickText.color.g,
                                                 rightClickText.color.b,
                                                 1);
        }
    }

    public void SliderControl(int maxValue, int currentValue, float sliderMoveAcceleretor)
    {
        if (sliderValue <= currentValue)
        {
            sliderValue = currentValue;
            if (expSlider.value < sliderValue)
            {
                expSlider.value += Time.deltaTime * sliderMoveAcceleretor;
            }
        }
        else if (sliderValue > currentValue + 0.1f)
        {
            expSlider.value += Time.deltaTime * sliderMoveAcceleretor;
            if (expSlider.value == expSlider.maxValue)
            {
                expSlider.maxValue = maxValue;
                sliderValue = 0;
                expSlider.value = 0;
            }
        }
    }

    void LevelUpMenuOpen()
    {
        if (!LevelUpPanel.activeSelf)
        {
            Time.timeScale = 0;
            LevelUpPanel.SetActive(true);

            SkillPointText.text="Skill Points:" + gameManager.LevelSkillPointProperty.ToString();
        }
        else
        {
            Time.timeScale = 1;
            LevelUpPanel.SetActive(false);
        }
        if(randomized == true)
        {
            RandomButtonGet();
        }
    }

    void MenuOpen()
    {
        if (!MenuPanel.activeSelf)
        {
            SelectBipSound.Play();
            Time.timeScale = 0;
            MenuPanel.SetActive(true);
            MenuTextSet();
        }
        else
        {
            SelectBipSound.Play();
            Time.timeScale = 1;
            MenuPanel.SetActive(false);
        }
    }

    void MenuTextSet()
    {
        LevelText.text = "Level: " + gameManager.LevelProperty.ToString();
        PointText.text = "Total Point: " + player.TotalPoint.ToString();
        HealthStatText.text = "Health: " + player.PlayerHealthProperty.ToString();
        SpeedStatText.text = "Speed: " + player.PlayerSpeedProperty/1000 + "x";
        DashPowerStatText.text = "Dash Power: " + player.DashSpeedProperty/500 + "x";
        DashTimeStatText.text = "Dash Cooldown: " + player.DashTimeProperty.ToString("0.0");
    }


    void RandomButtonGet()
    {
        x = Random.Range(1, 5);
        while (true)
        {
            y = Random.Range(1, 5);
            if (x != y)
            {
                break;
            }
        }

        switch (x)
        {
            case 1:
                button1Text.text = "Health Up";
                break;
            case 2:
                button1Text.text = "Speed Up";
                break;
            case 3:
                button1Text.text = "Dash Power";
                break;
            case 4:
                button1Text.text = "Dash Cooldown";
                break;
        }
        switch (y)
        {
            case 1:
                button2Text.text = "Health Up";
                break;
            case 2:
                button2Text.text = "Speed Up";
                break;
            case 3:
                button2Text.text = "Dash Power";
                break;
            case 4:
                button2Text.text = "Dash Cooldown";
                break;
        }

        randomized = false;
    }

    public void ButtonClicked1()
    {
        SelectBipSound.Play();
        switch (x)
        {
            case 1:
                healthSlider.maxValue += 50;
                healthSlider.value = healthSlider.maxValue;
                LevelUpManager.instance.PlayerHealthIncrease((int)healthSlider.maxValue);
                break;
            case 2:
                LevelUpManager.instance.PlayerSpeedUp(); 
                break;
            case 3:
                LevelUpManager.instance.PlayerDashPowerUp();
                break;
            case 4:
                LevelUpManager.instance.PlayerDashTime();
                break;
        }

        gameManager.LevelSkillPointProperty--;
        if (gameManager.LevelSkillPointProperty > 0)
        {
            RandomButtonGet();
            SkillPointText.text = "Skill Points:" + gameManager.LevelSkillPointProperty;
        }
        if(gameManager.LevelSkillPointProperty == 0)
        {
            SkillPointText.text = "Skill Points:" + gameManager.LevelSkillPointProperty;
            button1.interactable = false;
            button2.interactable = false;
        }

        MenuTextSet();
    }
    public void ButtonClicked2()
    {
        SelectBipSound.Play();
        switch (y)
        {
            case 1:
                healthSlider.maxValue += 50;
                healthSlider.value = healthSlider.maxValue;
                LevelUpManager.instance.PlayerHealthIncrease((int)healthSlider.maxValue);
                break;
            case 2:
                LevelUpManager.instance.PlayerSpeedUp();
                break;
            case 3:
                LevelUpManager.instance.PlayerDashPowerUp();
                break;
            case 4:
                LevelUpManager.instance.PlayerDashTime();
                break;
        }

        gameManager.LevelSkillPointProperty--;
        if (gameManager.LevelSkillPointProperty > 0)
        {
            RandomButtonGet();
            SkillPointText.text = "Skill Points:" + gameManager.LevelSkillPointProperty;
        }
        if (gameManager.LevelSkillPointProperty == 0)
        {
            SkillPointText.text = "Skill Points:" + gameManager.LevelSkillPointProperty;
            button1.interactable = false;
            button2.interactable = false;
        }
        MenuTextSet();
    }

    public void GameOverMenu()
    {
        Time.timeScale = 0;
        GameOverPanel.gameObject.SetActive(true);
        GmOverLevelText.text = "        Level: " + gameManager.LevelProperty;
        GmOverPointText.text = "Total Point: " + player.TotalPoint;

    }

    public void MainMenuButton()
    {
        SelectBipSound.Play();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        
    }

    public void RestartButton()
    {
        SelectBipSound.Play();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void PlayButton()
    {
        SelectBipSound.Play();
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
