using UnityEngine;
using BeauRoutine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static string SAVE_NAME = "saveGame";

    public SaveData playerdata;

    public GenericPopupLogic GeneralPopup;
    [SerializeField]
    Animator LoadingScreen;
    Routine loading;

    void Awake()
    {
        if (instance) Destroy(instance);
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
       // ToTitle();
    }

    public static void SaveData()
    {
        JSONEditor.DataToJSON(instance.playerdata, SAVE_NAME);
        Debug.Log("PLAYER DATA SAVED");
    }

    
    public string NameGenerator(AnimalPartsObject animal) {

       
        string name = "";

        AnimalPart head, body, legs, tail;
        head = Resources.Load<AnimalPart>("Parts/Data/" + animal.headID);
        body = Resources.Load<AnimalPart>("Parts/Data/" + animal.bodyID);
        legs = Resources.Load<AnimalPart>("Parts/Data/" + animal.legsID);
        tail = Resources.Load<AnimalPart>("Parts/Data/" + animal.tailID);

        if(head.partData.animal == body.partData.animal && head.partData.animal == legs.partData.animal 
            && head.partData.animal == tail.partData.animal) {
            Debug.Log(head.partData.animal);
            return head.partData.animal; //when all parts are of all the same animal. 
        }

        char[] temp = head.partData.namePart.ToCharArray();
        string capital = "";

        for (int i = 0; i < temp.Length; i++) {
            if (i == 0) {
                capital += temp[i].ToString().ToUpper();
            }
            else {
                capital += temp[i].ToString();
            }
        }

        name += capital;
        name += body.partData.namePart;
        name += legs.partData.namePart;
        name += tail.partData.namePart;
        Debug.Log(name);

        return name;
    }


    #region Menu Transitions

    public static void ToMainMenu()
    {
        instance.loading.Replace(DelayedLoadMain());
        
    }

    public static void ToLevel()
    {

    }

    static IEnumerator DelayedLoadMain()
    {
        SetLoadingScreen(true);

        // TODO: load data here
        yield return 0.7F;

        GameDirector.instance.OpenMainMenu();

        SetLoadingScreen(false);
    }

    public static void ToTitle(bool delayed = false)
    {
        GameDirector.instance.CloseMainMenu();
        if (delayed) instance.loading.Replace(DelayedLoadTitle());
        else GameDirector.instance.OpenTitle();
    }

    static IEnumerator DelayedLoadTitle()
    {
        SetLoadingScreen(true);

        yield return 0.8F;

        GameDirector.instance.OpenTitle();

        SetLoadingScreen(false);
    }

    public static void SetLoadingScreen(bool enabled)
    {
        instance.LoadingScreen.gameObject.SetActive(enabled);
        //instance.LoadingScreen.SetBool("Enabled", enabled);
    }

    #endregion
}
