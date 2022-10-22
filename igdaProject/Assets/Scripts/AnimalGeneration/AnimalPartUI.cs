using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimalPartUI : MonoBehaviour
{
    public string id;
    public AnimalPart myPart;
    public Sprite animalIcon;
    public void FindMyPart(string myID)
    {
        id = myID;
        myPart = Resources.Load<AnimalPart>("Parts/Data/" + id);
    }
}
