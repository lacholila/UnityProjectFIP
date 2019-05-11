using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasController : MonoBehaviour
{
    public GameObject mainMenuCanvas;

    public GameObject playMenuCanvas;
    public GameObject optionsMenuCanvas;
    public GameObject creditsMenuCanvas;
    public GameObject exitMenuCanvas;

    public GameObject multiplayerMenuCanvas;
    public GameObject arcadeMenuCanvas;

    public GameObject controlsMenuCanvas;
    public GameObject videoMenuCanvas;
    public GameObject audioMenuCanvas;

    public Transform currentCanvasTransform, nextCanvasTransform, previousCanvasTransform;

    public void MakeMenuTransition(GameObject currentCanvas, GameObject nextCanvas)
    {
        currentCanvas.SetActive(false);
        nextCanvas.SetActive(true);
    }

    public void MakeMenuTransition(GameObject currentCanvas, GameObject nextCanvas, float time, bool newMenu)
    {
        currentCanvas.SetActive(true);
        nextCanvas.SetActive(true);

        if (newMenu)
        {
            nextCanvas.transform.position = nextCanvasTransform.position;

            currentCanvas.GetComponent<MovableCanvas>().StartCoroutine(currentCanvas.GetComponent<MovableCanvas>().MoveCanvas(previousCanvasTransform, time, false));
            nextCanvas.GetComponent<MovableCanvas>().StartCoroutine(nextCanvas.GetComponent<MovableCanvas>().MoveCanvas(currentCanvasTransform, time, true));
        }
        else
        {
            nextCanvas.transform.position = previousCanvasTransform.position;

            currentCanvas.GetComponent<MovableCanvas>().StartCoroutine(currentCanvas.GetComponent<MovableCanvas>().MoveCanvas(nextCanvasTransform, time, false));
            nextCanvas.GetComponent<MovableCanvas>().StartCoroutine(nextCanvas.GetComponent<MovableCanvas>().MoveCanvas(currentCanvasTransform, time, true));
        }
    }

    //Funciones para los botones
    //-------------------------------------------------- MAIN MENU --------------------------------------------------
    public void MenuPressPlay()
    {
        Debug.Log("play");
        MakeMenuTransition(mainMenuCanvas, playMenuCanvas, 0.5f, true);
    }

    public void MenuPressOptions()
    {
        Debug.Log("options");
        MakeMenuTransition(mainMenuCanvas, optionsMenuCanvas, 0.5f, true);
    }

    public void MenuPressCredits()
    {
        Debug.Log("credits");
        MakeMenuTransition(mainMenuCanvas, creditsMenuCanvas, 0.5f, true);
    }

    public void MenuPressQuit()
    {
        Debug.Log("quit");
        MakeMenuTransition(mainMenuCanvas, exitMenuCanvas, 0.5f, false);
    }

    //-------------------------------------------------- PLAY MENU --------------------------------------------------
    public void PlayPressMultiplayer()
    {
        Debug.Log("multiplayer");
        MakeMenuTransition(playMenuCanvas, multiplayerMenuCanvas, 0.5f, true);
    }

    public void PlayPressArcade()
    {
        Debug.Log("arcade");
        MakeMenuTransition(playMenuCanvas, arcadeMenuCanvas, 0.5f, true);
    }

    public void PlayPressBack()
    {
        Debug.Log("back");
        MakeMenuTransition(playMenuCanvas, mainMenuCanvas, 0.5f, false);
    }

    //-------------------------------------------------- OPTIONS MENU --------------------------------------------------
    public void OptionsPressControls()
    {
        Debug.Log("controls");
        MakeMenuTransition(optionsMenuCanvas, controlsMenuCanvas, 0.5f, true);
    }

    public void OptionsPressVideo()
    {
        Debug.Log("video");
        MakeMenuTransition(optionsMenuCanvas, videoMenuCanvas, 0.5f, true);
    }

    public void OptionsPressAudio()
    {
        Debug.Log("audio");
        MakeMenuTransition(optionsMenuCanvas, audioMenuCanvas, 0.5f, true);
    }

    public void OptionsPressBack()
    {
        Debug.Log("back");
        MakeMenuTransition(optionsMenuCanvas, mainMenuCanvas, 0.5f, false);
    }

    //-------------------------------------------------- CREDITS MENU --------------------------------------------------
    public void CreditPressBack()
    {
        Debug.Log("back");
        MakeMenuTransition(creditsMenuCanvas, mainMenuCanvas, 0.5f, false);
    }

    //-------------------------------------------------- EXIT MENU --------------------------------------------------
    public void ExitPressBack()
    {
        Debug.Log("back");
        MakeMenuTransition(exitMenuCanvas, mainMenuCanvas, 0.5f, true);
    }

    //-------------------------------------------------- MULTIPLAYER MENU --------------------------------------------------
    public void MultiplayerPressBack()
    {
        Debug.Log("back");
        MakeMenuTransition(multiplayerMenuCanvas, playMenuCanvas, 0.5f, false);
    }

    //-------------------------------------------------- ARCADE MENU --------------------------------------------------
    public void ArcadePressBack()
    {
        Debug.Log("back");
        MakeMenuTransition(arcadeMenuCanvas, playMenuCanvas, 0.5f, false);
    }

    //-------------------------------------------------- CONTROLS MENU --------------------------------------------------
    public void ControlsPressBack()
    {
        Debug.Log("back");
        MakeMenuTransition(controlsMenuCanvas, optionsMenuCanvas, 0.5f, false);
    }

    //-------------------------------------------------- VIDEO MENU --------------------------------------------------
    public void VideoPressBack()
    {
        Debug.Log("back");
        MakeMenuTransition(videoMenuCanvas, optionsMenuCanvas, 0.5f, false);
    }

    //-------------------------------------------------- AUDIO MENU --------------------------------------------------
    public void AudioPressBack()
    {
        Debug.Log("back");
        MakeMenuTransition(audioMenuCanvas, optionsMenuCanvas, 0.5f, false);
    }
}
