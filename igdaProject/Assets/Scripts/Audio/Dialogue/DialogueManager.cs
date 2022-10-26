using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using BeauRoutine;

public class DialogueManager : MonoBehaviour
{ 
    [Header("Dialogue UI")]
    [SerializeField] private TextAsset inkAsset;
    static DialogueManager instance;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    private Story CurrentStory;
    [SerializeField]
    private bool Tutorial;
    private bool dialogueisPlaying;
    [SerializeField]private Button continueButton;
    [SerializeField]
    private bool starttheTutorial = false;
    private Button choice1;
    private Button choice2;
    private Button choice3;

    // Start is called before the first frame update
    void Start()
    {
        //if(GameManager.instance.getTutorial())
        if(instance == null)
        instance = this;
        else Destroy(this);

        dialogueisPlaying = false;
        dialoguePanel.SetActive(false);
        /*
        if (Tutorial)
        {
            Routine.Start(startTutorial());
        }*/
        
    }
    /*
    IEnumerator startTutorial()
    {
        while (true)
        {
            if (!Tutorial)
                break;
            EnterDialogueMode();
        }
        yield return null;
    }*/
    public bool getTutorial()
    {
        return Tutorial;
    }
    public static DialogueManager getInstance()
    {
        return instance;
    }
    // Update is called once per frame
    void Update()
    {
        if (starttheTutorial)
        {
            starttheTutorial = false;
            EnterDialogueMode();
        }
        
            
    }
    public void continueTheStory()
    {
        if (!dialogueisPlaying)
        {
            return;
        }
        else
        {
            if (CurrentStory.canContinue)
            {
                dialogueText.text = CurrentStory.Continue();
            }
            else
            {
                ExitDialogueMode();
            }
        }
    }
    public void EnterDialogueMode()
    {
        CurrentStory= new Story(inkAsset.text);
        dialogueisPlaying=true;
        dialoguePanel.SetActive(true);

        continueTheStory();
    }
    public void ExitDialogueMode()
    {
        dialogueisPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

}

