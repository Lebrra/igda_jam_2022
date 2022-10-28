using BeauRoutine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    Animator titleAnim;

    [SerializeField]
    Button continueButton;
    [SerializeField]
    Button newButton;

    [SerializeField]
    AnimalPrefabBuilder animBuilder;
    [SerializeField]
    Biome defaultAnimals;
    AnimalPartsObject[] carouselAnimals = null;
    Routine carousel;

    [SerializeField]
    SaveData defaultSave;

    bool hasSave = false;

    public void Open()
    {
        titleAnim.SetBool("Status", true);

        hasSave = JSONEditor.DoesFileExist(GameManager.SAVE_NAME);

        if (hasSave)
        {
            // do the cool animal thing here
            var saveData = JSONEditor.JSONToData<SaveData>(GameManager.SAVE_NAME);
            carouselAnimals = saveData.animalPresets;
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            continueButton.gameObject.SetActive(false);
            carouselAnimals = null;
        }
        carousel.Replace(CarouselAnimals());

        continueButton.onClick.AddListener(ContinueGame);
        newButton.onClick.AddListener(NewGameWarning);
    }

    public void Close()
    {
        continueButton.onClick.RemoveListener(ContinueGame);
        newButton.onClick.RemoveListener(NewGameWarning);

        carousel.Stop();
        animBuilder.DestroyAnimal();

        titleAnim.SetBool("Status", false);
    }

    void ContinueGame()
    {
        GameManager.instance.playerdata = JSONEditor.JSONToData<SaveData>(GameManager.SAVE_NAME);
        LoadGame();
    }

    void NewGameWarning()
    {
        if (!hasSave) NewGame();
        else
        {
            var popup = GameManager.instance.GeneralPopup;
            popup.FillContent("Starting a new game will delete your old save, are you sure?", () => {
                NewGame();
                popup.Close();
                }, popup.Close);
            popup.Open();
        }
    }

    void NewGame()
    {
        GameManager.SetLoadingScreen(true);
        DialogueManager.getInstance().startTutorial();
        GameManager.instance.playerdata = defaultSave;
        GameManager.instance.playerdata.inventoryStr = GameManager.DEFAULT_INVENTORY;
        GameManager.SaveData();
        if (hasSave)
        {
            // reset inventory 
            InventoryManager.instance.ConvertFromString(GameManager.DEFAULT_INVENTORY);
            MonsterMakerGenerator.instance.Setup();
        }
        LoadGame();
    }

    void LoadGame()
    {
        Close();
        GameManager.ToMainMenu();
    }

    IEnumerator CarouselAnimals()
    {
        yield return 0.4F;

        int current = 0;
        while (true)
        {
            if (carouselAnimals == null) 
            {
                // generate random from cat/dog
                var animal = LevelGenerator.CreateOpponent(defaultAnimals);
                animBuilder.CreateAnimal(animal, true, true, AnimalPrefabBuilder.AnimationType.HeadBob);
            }
            else
            {
                // load preset
                animBuilder.CreateAnimal(carouselAnimals[current], true, true, AnimalPrefabBuilder.AnimationType.HeadBob);
            }
            //animBuilder.SetAnimationState(AnimalPrefabBuilder.AnimationType.HeadBob);

            yield return 5F;
            current = (current + 1) % 3;
        }
    }
}
