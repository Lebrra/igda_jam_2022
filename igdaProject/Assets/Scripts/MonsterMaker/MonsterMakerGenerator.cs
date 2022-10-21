using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMakerGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject head;
    [SerializeField]
    GameObject tail;
    [SerializeField]
    GameObject body;
    [SerializeField]
    GameObject Leg;
    AnimalPart[] partlist;
    GameObject[] partObjectList;
    [SerializeField]
    GameObject ButtonTemplate;
    // Start is called before the first frame update
    void Start()
    {
        partlist = Resources.LoadAll<AnimalPart>("Parts/Data");
        partObjectList = Resources.LoadAll<GameObject>("Parts/Prefabs");
        UpdatePetParts();
    }
    void UpdatePetParts()
    {
        foreach (KeyValuePair<string, bool> inst in InventoryManager.instance.getDict())
        {
            if (inst.Value)
            {

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
