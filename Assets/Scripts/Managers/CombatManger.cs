using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManger : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemies;
    public GameObject[] enemyPlatforms;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < enemies.Length; i++)
            enemyPlatforms[i].transform.GetChild(1).GetComponent<Image>().sprite = enemies[i].GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
