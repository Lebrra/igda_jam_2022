using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(DialogueManager.getInstance().getTutorial())
            DialogueManager.getInstance().continueTheStory();
        //Debug.Log("I clicked");
    }

}
