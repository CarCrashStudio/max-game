using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManger : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemies;
    public GameObject[] enemyPlatforms;
    public GameObject combatMenu;

    public GameObject menuItemTemplate;
    public GameObject actionMenu;

    private byte currentTurnIndex = 0;
    private byte totalCombatants = 0;
    private GameObject[] turnOrder;

    public GameObject debugText;
    
    /* *** TODO: ***
       Keep track of turn order of both player and enemies
       Keep focus on the current entity
       
       -- AI/Action Planning for enemies -- use psuedorandom numbers for now
       Allow menu usage on Player turn

       output all actions to combat dialog
     */

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyPlatforms[i].transform.GetChild(1).GetComponent<Image>().sprite = enemies[i].GetComponent<SpriteRenderer>().sprite;
        }
        turnOrder = TurnOrder_Init ();
    }

    // Update is called once per frame
    void Update()
    {
        bool isPlayer = false;
        GameObject currentCombatant = turnOrder[currentTurnIndex];
        debugText.GetComponent<Text>().text = currentCombatant.GetComponent<Entity>().attributes.ToString();

        // check if the combatant is player or not
        if (currentCombatant.GetComponent<Player>() != null)
        {
            isPlayer = true;
            // enable combat menu
            combatMenu.SetActive(true);
        }
        else
        {
            // this combatant is not a human
        }

        if (!isPlayer)
        {
            // add 1 to the turn order at the end of turn
            TurnOrder_Increment();
        }
    }

    // Initializes Turn order and selects first entity
    GameObject[] TurnOrder_Init()
    {
        // pull in player and all enemy gameobjects
        // for now, player will go first followed by enemies in order of the array

        var size = enemies.Length + 1;
        totalCombatants = (byte)size;
        GameObject[] turnOrder = new GameObject[size];

        for (int i = 0; i < size; i++)
        {
            if (i == 0) turnOrder[i] = player;
            else turnOrder[i] = enemies[i - 1];
        }

        currentTurnIndex = 0;
        return turnOrder;
    }

    // Increment the turn order and select next entity
    void TurnOrder_Increment()
    {
        currentTurnIndex++;
        if (currentTurnIndex == totalCombatants)
            currentTurnIndex = 0;
    }

    public void ATKbtn_Click ()
    {
        // get a list of weapons from the players equipment
        var equipment = player.GetComponent<Player>().equipment;

        // add each weapon as an attack option
        for (int i = 0; i < equipment.Length; i++)
        {
            if (equipment[i] != null)
            {
                // instantiate a new copy of the menuItemTemplate inside of the sub menu grid
                GameObject menuItem = Instantiate(menuItemTemplate, actionMenu.transform);

                // Set the text and image of this item to be to the name and image of the item at this slot
                menuItem.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = equipment[i].name;
                menuItem.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = equipment[i].GetComponent<SpriteRenderer>().sprite;

                menuItem.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                Equipment e = equipment[i];
                menuItem.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { 
                    e.Attack(player.GetComponent<Player>(), enemies[0].GetComponent<Enemy>());
                    Debug.Log(enemies[0].GetComponent<Enemy>().health.GetHealth());
                });
            }
        }
    }
}
