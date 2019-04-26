using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

    public string characterName;
    public Color characterColor;
    public Sprite characterIconSprite;

    public Image characterIcon;
    public Image characterIconCircle;
    public List<Image> characterHearts;

    public void SetPlayerHUDColor()
    {
        characterIconCircle.color = characterColor;

        if (characterIconSprite != null)
        {
            characterIcon.sprite = characterIconSprite;
        }
        else
        {
            characterIcon.enabled = false;
        }

        for (int i = 0; i < characterHearts.Count; i++)
        {
            characterHearts[i].color = characterColor;
        }
    }
}
