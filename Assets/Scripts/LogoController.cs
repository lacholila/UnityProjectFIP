using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoController : MonoBehaviour
{
    private void Start()
    {
        Invoke("GoToMenu", 8f);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
