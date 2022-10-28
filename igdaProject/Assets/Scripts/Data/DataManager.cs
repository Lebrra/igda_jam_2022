using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    bool initialized = false;

    [Header("Body Part Lists")]
    public List<AnimalPart> masterList = new List<AnimalPart>();
    public List<AnimalPart> headList = new List<AnimalPart>();
    public List<AnimalPart> bodyList = new List<AnimalPart>();
    public List<AnimalPart> legsList = new List<AnimalPart>();
    public List<AnimalPart> tailList = new List<AnimalPart>();

    [Header("Body Part Prefabs")]
    public List<AnimalPartUI> allPrefabs = new List<AnimalPartUI>();
    public List<BodyPartUI> allBodyPrefabs = new List<BodyPartUI>();

    [Header("Abilities")]
    public List<Ability> allAbilities = new List<Ability>();

    private void Awake() {
        if(instance == null) instance = this;
        else Destroy(this);

        DontDestroyOnLoad(this.gameObject);
    }

    public IEnumerator LoadAllData()
    {
        //if (initialized) yield break;
        //yield return InitializeLists();
        //yield return InitializePrefabs();
        //yield return InitializeAbilities();

        initialized = true;
        yield return null;
    }

    private IEnumerator InitializeLists() {
        var animalPartList = Resources.LoadAll<AnimalPart>("Parts/Data/");

        //generate all 4 lists
        foreach (AnimalPart part in animalPartList) {
            masterList.Add(part);
            switch (part.partData.bodyPart) {
                case BodyPart.Head:
                    headList.Add(part);
                    break;
                case BodyPart.Body:
                    bodyList.Add(part);
                    break;
                case BodyPart.Legs:
                    legsList.Add(part);
                    break;
                case BodyPart.Tail:
                    tailList.Add(part);
                    break;
            }
            yield return null;
        }
    }

    IEnumerator InitializePrefabs()
    {
        var animalUIList = Resources.LoadAll<AnimalPartUI>("Parts/Prefabs/");
        foreach (var ui in animalUIList)
        {
            var bodyUI = ui.GetComponent<BodyPartUI>();
            if (bodyUI) allBodyPrefabs.Add(bodyUI);
            else allPrefabs.Add(ui);

            yield return null;
        }
    }

    IEnumerator InitializeAbilities()
    {
        var abilities = Resources.LoadAll<Ability>("Abilities/");
        foreach (var abil in abilities)
        {
            allAbilities.Add(abil);
            yield return null;
        }
    }

    public AnimalPart GetAnimalPart(string id)
    {
        var results = masterList.Where((x) => x.partData.id.ToLower() == id.ToLower()).ToList();
        if (results == null) return null;
        else if (results.Count == 0) return null;

        return results[0];
    }

    public AnimalPartUI GetAnimalPartUI(string id)
    {
        var results = allPrefabs.Where((x) => x.gameObject.name.ToLower() == id.ToLower()).ToList();
        if (results == null) return null;
        else if (results.Count == 0) return null;

        return results[0];
    }

    public BodyPartUI GetBodyPartUI(string id)
    {
        var results = allBodyPrefabs.Where((x) => x.gameObject.name.ToLower() == id.ToLower()).ToList();
        if (results == null) return null;
        else if (results.Count == 0) return null;

        return results[0];
    }

    public Ability GetAbility(string id)
    {
        var results = allAbilities.Where((x) => x.name.ToLower() == id.ToLower()).ToList();
        if (results == null) return null;
        else if (results.Count == 0) return null;

        return results[0];
    }
}
