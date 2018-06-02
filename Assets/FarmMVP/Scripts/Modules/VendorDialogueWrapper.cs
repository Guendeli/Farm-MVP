using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.InventoryPro;
using PixelCrushers.DialogueSystem;
using UnityEngine.Events;
public class VendorDialogueWrapper : MonoBehaviour {

    public UnityEvent onTrade;

    public void OnConversationLine(Subtitle subtitle)
    {
        Debug.Log(string.Format("{0}: {1}", subtitle.speakerInfo.transform.name, subtitle.formattedText.text));
        if (subtitle.formattedText.text.Equals("Trade"))
        {
            if(onTrade != null)
            {
                onTrade.Invoke();            }
        }
    }

    public void ShowVendorUI()
    {
        Devdog.General.Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Devdog.General.Player>();
        VendorTrigger trigger = GetComponent<VendorTrigger>();
        trigger.OnTriggerUsed(player);
    }
}
