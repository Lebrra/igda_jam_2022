using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {

        if (DialogueManager.getInstance().getTutorial())
        {
            //if (DialogueManager.getInstance().dialogueisPlaying)
            //{
            DialogueManager.getInstance().setChange(true);
            DialogueManager.getInstance().continueTheStory();
            // }
        }
        //Debug.Log("I clicked");
    }

}
