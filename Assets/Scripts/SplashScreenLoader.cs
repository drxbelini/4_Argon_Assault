using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke ("loadScene", 4F);
    }

    // Update is called once per frame
    void loadScene()
    {
        SceneManager.LoadScene(1);
    }
}
