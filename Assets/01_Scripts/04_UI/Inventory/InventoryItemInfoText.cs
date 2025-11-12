using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryItemInfoText : MonoBehaviour
{
    [ SerializeField ] TextMeshProUGUI itemEffectDescriptionText;
    [ SerializeField ] TextMeshProUGUI amountText;


    public void ShowInfoText( string effectName, string amount )
    {
        this.gameObject.SetActive( true );
        itemEffectDescriptionText.text = effectName;
        amountText.text = amount;
    }

    public void HideInfoText()
    {
        this.gameObject.SetActive(false);
    }
}