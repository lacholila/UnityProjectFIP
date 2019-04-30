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
    public Image characterIconFill;
    public List<Image> characterHearts;

    public Text characterWinner;

    public void SetPlayerHUDColor()
    {
        float colorAlpha = characterIconFill.color.a;
        characterIconCircle.color = characterColor;
        characterIconFill.color = new Color(characterColor.r, characterColor.g, characterColor.b, colorAlpha);
        characterWinner.color = characterColor;

        if (characterIconSprite != null)
        {
            characterIcon.sprite = characterIconSprite;
            characterIcon.color = Color.white;
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
