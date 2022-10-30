using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeauRoutine;

public class testStartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Routine.Start(DialogueManager.getInstance().initializeDialogue());
    }

}
