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
    [SerializeField] private TextAsset tutorialScript;
    [SerializeField] private TextAsset FactScript;
    static DialogueManager instance;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    private Story CurrentStory;
    [SerializeField]
    private bool Tutorial;
    public bool dialogueisPlaying { get; private set; }
    [SerializeField]
    private bool starttheTutorial = false;
    [SerializeField]
    private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    [SerializeField]
    GameObject tutorialPanelInner;
    [SerializeField]
    GameObject TutorialPanel;
    [SerializeField]
    GameObject baseGamePanel;
    [SerializeField]
    GameObject creationPanel;
    [SerializeField]
    GameObject battlePanel;
    [SerializeField]
    bool dialogueScene;
    [SerializeField]
    float textTime = 0.05f;
    bool ableMakeChoice = false;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
        if(dialogueScene)
        Routine.Start(initializeDialogue());
    }
    public IEnumerator initializeDialogue()
    {
        print("In Dialogue Beginning");
        dialogueisPlaying = false;
        dialoguePanel.SetActive(false);
        Tutorial = true; 
        choicesText = new TextMeshProUGUI[choices.Length];
        int idex = 0;
        print("In Dialogue Middle");
        foreach (GameObject choice in choices)
        {
            choicesText[idex] = choice.GetComponentInChildren<TextMeshProUGUI>();
            idex++;
        }
        print("In Dialogue after loop");
        yield return new WaitForEndOfFrame();

    }
    public void startTutorial()
    {
        starttheTutorial = true;
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
    public void setChange(bool tr)
    {
        ableMakeChoice = tr;
    }
    public IEnumerator readDialogue(string[] text, string str)
    {
        //Whenever there's a < load until > and then add to text
        int index = 0;
        dialogueText.text = "";
        
        bool bracket = false;
        string bracketed = "";
        ableMakeChoice = false;
        while (index < text.Length)
        {
            if (ableMakeChoice)
                break;
            if (index == 0)
            {
                dialogueText.text = text[index++] + " ";
            }
            char[] word = text[index++].ToCharArray();
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == '<')
                    bracket = true;
                if (!bracket)
                {
                    dialogueText.text += word[i];
                    yield return new WaitForSeconds(textTime);
                }
                else
                {
                    bracketed += word[i];
                    if (word[i] == '>')
                    {
                        //bracketed += word[i];
                        dialogueText.text += bracketed;
                        bracket = false;
                        bracketed = "";
                    }
                }
            }
            dialogueText.text += " ";
        }
        dialogueText.text = str;
        DisplayChoices();
        yield return null;
    }
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
            TutorialPanel.SetActive(true);
            starttheTutorial = false;
            inkAsset = tutorialScript;
            tutorialPanelInner.GetComponent<Animator>().SetBool("Status", true);
            EnterDialogueMode();
        }
        
            
    }
    public void continueTheStory()
    {
        if (CurrentStory.canContinue)
        {
            string str = "";
            //dialogueText.text = CurrentStory.Continue();
            Routine.Stop("readDialogue");
            if (str != "")
                dialogueText.text = str;
            str = CurrentStory.Continue();
            Routine.Start(readDialogue(str.Split(" "), str));
            TagHandler();
            //DisplayChoices();
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
            foreach(string tag in tags)
                switch (tags[index++])
                {
                    case "TutorialDone":
                        ExitDialogueMode();
                        //Tutorial = false;
                        tutorialPanelInner.GetComponent<Animator>().SetBool("Status", false);
                        TutorialPanel.SetActive(false);
                        inkAsset = FactScript;
                        //GameManager.instance.Tutorial = Tutorial;
                        //GameManager.instance.playerdata.Tutorial = Tutorial;
                        //GameManager.SaveData();
                        GameManager.SetLoadingScreen(false);
                        break;
                    case "openerPanel":
                        if(baseGamePanel != null)
                        {
                            baseGamePanel.SetActive(true);
                        }
                        break;
                    case "closeOpener":
                        if (baseGamePanel != null)
                        {
                            baseGamePanel.SetActive(false);
                        }
                        break;
                    case "creatureCreationPanel":
                        if(creationPanel != null)
                        creationPanel.SetActive(true);
                        break;
                    case "closeAll":
                        if (baseGamePanel != null)
                            baseGamePanel.SetActive(false);
                        if (creationPanel != null)
                            creationPanel.SetActive(false);
                        if(battlePanel != null)
                            battlePanel.SetActive(false);
                        break;
                    case "combatPanel":
                        if (battlePanel != null)
                            battlePanel.SetActive(true);
                        break;
                    case "mumble":
                        if (AudioManager.audioManager != null)
                        {


                            int i = Random.Range(0, 1);
                            if (i == 0)
                                AudioManager.audioManager.playDialogClip("Mumble1");
                            else
                                AudioManager.audioManager.playDialogClip("Mumble3");
                        }
                        break;
                    case "mumbleAngry":
                        if (AudioManager.audioManager != null)
                            AudioManager.audioManager.playDialogClip("Mumble2");
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
    IEnumerator getChoiceBool()
    {
        while (true)
        {
            if (ableMakeChoice)
            {
                break;
            }
        }
        yield return null;
    }
    private void DisplayChoices()
    {

        List<Choice> currentChoices = CurrentStory.currentChoices;
        //if (currentChoices.Count == 0)
            //return;
        //if(currentChoices.Count > choices.Length)
        //{
        //Debug.Log("To many choices");
        //}
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
            {
                choice.gameObject.SetActive(false);
                choice.GetComponent<Button>().onClick.RemoveAllListeners();
            }
            continueTheStory();
        }
        catch(System.Exception e)
        {
            print(ChoiceIndex);
            Debug.Log(e.ToString());
        }
    }
}

