using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BeauRoutine;

public class GenericPopupLogic : MonoBehaviour
{
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
        gameObject.SetActive(true);
        popupAnim.SetBool("Status", true);
    }

    public void Close()
    {
        popupAnim.SetBool("Status", false);
        Routine.Start(DelayClose());
    }

    IEnumerator DelayClose()
    {
        yield return 0.5F;
        gameObject.SetActive(false);
    }

    public void FillContent(string prompt, Action yesAction, Action noAction, string yesPrompt = "Yes", string noPrompt = "No")
    {
        promptText.text = prompt;

        yesButton.gameObject.SetActive(true);
        yesButton.onClick.RemoveAllListeners();
        if (yesAction != null) yesButton.onClick.AddListener(() => yesAction());
        else yesButton.onClick.AddListener(Close);
        yesText.text = yesPrompt;

        noButton.gameObject.SetActive(true);
        noButton.onClick.RemoveAllListeners();
        if (noAction != null) noButton.onClick.AddListener(() => noAction());
        else noButton.onClick.AddListener(Close);
        noText.text = noPrompt;
    }

    public void FillContent(string prompt, Action endAction, string closePrompt = "Okay") 
    {
        if (closePrompt == null)
        {
            FillContent(prompt, endAction, () => noButton.onClick.AddListener(Close));
            return;
        }

        promptText.text = prompt;

        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(false);
        yesButton.onClick.RemoveAllListeners();
        if (endAction != null) yesButton.onClick.AddListener(() => endAction());
        else yesButton.onClick.AddListener(Close);
        yesText.text = closePrompt;
    }
}

/*
 * Use example:
 * 
public class PopupExample : MonoBehaviour
{
    public GenericPopupLogic popup;

    void UseSimplePopup()
    {
        popup.FillContent("this is a popup", null);
        popup.Open();
    }

    void UseQuestionPopup()
    {
        popup.FillContent("this popup is a question", null, null);
        popup.Open();
    }
}

 * 
 * 
 * 
 * 
 */