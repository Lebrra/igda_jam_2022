using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static string SAVE_NAME = "saveGame";

    public SaveData playerdata;

    void Awake()
    {
        if (instance) Destroy(instance);
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public static void SaveData()
    {
        JSONEditor.DataToJSON(instance.playerdata, SAVE_NAME);
        Debug.Log("PLAYER DATA SAVED");
    }
}
