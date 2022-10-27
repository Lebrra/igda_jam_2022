using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
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
    public bool dialogueisPlaying { get; private set; }
    [SerializeField]private Button continueButton;
    [SerializeField]
    private bool starttheTutorial = false;
    [SerializeField]
    private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    [SerializeField]
    GameObject tutorialPanelInner;
    [SerializeField]
    GameObject TutorialPanel;
    // Start is called before the first frame update
    void Start()
    {
        //if(GameManager.instance.getTutorial())
        if(instance == null)
        instance = this;
        else Destroy(this);

        dialogueisPlaying = false;
        dialoguePanel.SetActive(false);
        choicesText = new TextMeshProUGUI[choices.Length];
        int idex = 0;
        foreach(GameObject choice in choices)
        {/*
            choice.gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                MakeChoice(idex);
            });*/
            choicesText[idex] = choice.GetComponentInChildren<TextMeshProUGUI>();
            idex++;
        }
        
        print(choicesText.Length);
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
            tutorialPanelInner.GetComponent<Animator>().SetBool("Status", true);
            EnterDialogueMode();
        }
        
            
    }
    public void continueTheStory()
    {
        /*
        if (!dialogueisPlaying)
        {
            return;
        }
        else
        {
        */
        
        if (CurrentStory.canContinue)
        {
            dialogueText.text = CurrentStory.Continue();
            TagHandler();
            DisplayChoices();
        }
        else if (CurrentStory.currentChoices.Count > 0)
        {
            return;
        }
        else
        {
            ExitDialogueMode();
        }
        
    }
    public void TagHandler()
    {
        List<string> tags = CurrentStory.currentTags;
        if (tags.Count > 0)
        {
            int index = 0;
            switch(tags[index++])
            {
                case "TutorialDone":
                    ExitDialogueMode();
                    Tutorial = false;
                    tutorialPanelInner.GetComponent<Animator>().SetBool("Status", false);
                    TutorialPanel.SetActive(false);
                    break;
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
    private void DisplayChoices()
    {
        List<Choice> currentChoices = CurrentStory.currentChoices;
        //print(currentChoices.Count);
        //if(currentChoices.Count > choices.Length)
        //{
        //Debug.Log("To many choices");
        //}
        /*
        if (CurrentStory.currentChoices.Count > 0)
        {
            for (int i = 0; i < CurrentStory.currentChoices.Count; i++)
            {
                Choice choice = CurrentStory.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());
                // Tell the button what to do when we press it
                button.onClick.AddListener(delegate {
                    OnClickChoiceButton(choice);
                });
            }
        }*/
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choices[index].GetComponent<Button>().onClick.AddListener(() =>
            {
                MakeChoice(choice.index);
            });
            choicesText[index].text = choice.text;
            index++;
        }
        for(int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        //StartCoroutine(SelectFirstChoice());
        

    }
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0]);
    }
    public void MakeChoice(int ChoiceIndex)
    {
        try
        {
            CurrentStory.ChooseChoiceIndex(ChoiceIndex);
            foreach (GameObject choice in choices)
                choice.GetComponent<Button>().onClick.RemoveAllListeners();
            continueTheStory();
        }
        catch(System.Exception e)
        {
            print(ChoiceIndex);
            Debug.Log(e.ToString());
        }
    }
}

