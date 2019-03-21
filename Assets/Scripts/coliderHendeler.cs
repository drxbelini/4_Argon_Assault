using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class coliderHendeler : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] float loadSceneDelay = 1f;


    
    private void OnTriggerEnter(Collider other)
    {
        deathSequece();
    }

    private void deathSequece()
    {
        
        SendMessage("disableControl");
        deathFX.SetActive(true);
        Invoke("loadScene", loadSceneDelay);
        
    }

    private void loadScene()
    {
        SceneManager.LoadScene(1);
    }
}
