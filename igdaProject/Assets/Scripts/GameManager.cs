using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static string SAVE_NAME = "saveGame";

    public SaveData playerdata;

    public GenericPopupLogic GeneralPopup;

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

    public static void ToGame()
    {
        // we might need a loading screen for this
        
    }

    public static void ToTitle()
    {
        GameDirector.instance.LoadTitle();
    }

    #endregion
}
