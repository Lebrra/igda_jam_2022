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
        ToTitle();
    }

    public static void SaveData()
    {
        JSONEditor.DataToJSON(instance.playerdata, SAVE_NAME);
        Debug.Log("PLAYER DATA SAVED");
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
