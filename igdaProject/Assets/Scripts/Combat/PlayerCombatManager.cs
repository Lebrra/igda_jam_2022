using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{

    public static PlayerCombatManager instance;

    private void Awake() {
        if (instance != null) instance = this;
        else Destroy(this.gameObject);
    }

}
