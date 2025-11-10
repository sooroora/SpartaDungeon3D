using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionInfoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] TextMeshProUGUI textDescription;

    public void SetInteractionInfo(string textNameString, string textDescriptionString)
    {
        textName.text = textNameString;
        textDescription.text = textDescriptionString;
    }
}
