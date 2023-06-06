using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControll : MonoBehaviour
{
    public Slider SoundSlider;
    AudioSource themeSound;

    void Start()
    {
        themeSound = FindObjectOfType<DontDestroyThemeSong>().GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SoundSlider != null && SoundSlider.gameObject.activeSelf)
        {
            themeSound.volume = SoundSlider.value;
        }
    }
    public void SoundButton()
    {
        if (!SoundSlider.gameObject.activeSelf)
        {
            SoundSlider.gameObject.SetActive(true);
        }
        else
        {
            SoundSlider.gameObject.SetActive(false);
        }
    }
}
