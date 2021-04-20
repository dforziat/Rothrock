using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Inventory")]
    [SerializeField] public int healthKits = 0;
     public int healthKitsMax = 3;
    [SerializeField]  int handgunAmmo = 10;
     int handgunAmmoMax = 20;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
