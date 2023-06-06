using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyFewSeconds : MonoBehaviour
{
    int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Invoke("Destroyer", 1);
        }
        if (currentSceneIndex == 1 && FindObjectsOfType<DontDestroyFewSeconds>().Length > 1)
        {
            Destroy(FindObjectsOfType<DontDestroyFewSeconds>()[0].gameObject);
        }

    }

    void Destroyer()
    {
        Destroy(gameObject);
    }
}
