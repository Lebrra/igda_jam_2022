using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenericPopupLogic : MonoBehaviour
{
    [SerializeField]
    GameObject popupPanel;
    [SerializeField]
    Animator popupAnim;
    [SerializeField]
    TextMeshProUGUI promptText;

    [SerializeField]
    Button yesButton;
    [SerializeField]
    TextMeshProUGUI yesText;

    [SerializeField]
    Button noButton;
    [SerializeField]
    TextMeshProUGUI noText;

    public void Open()
    {
        popupPanel.SetActive(true);
        popupAnim.SetBool("Enabled", true);
    }

    public void Close()
    {
        popupAnim.SetBool("Enabled", false);
    }
}
