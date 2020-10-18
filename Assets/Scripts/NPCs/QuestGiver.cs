using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPC
{
    public override void Interact(GameObject interacter)
    {
        interacter.GetComponent<QuestLog>().AddQuest(new Quest("Test Quest", "This is a Test"));

        Debug.Log(interacter.GetComponent<QuestLog>().questCount);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
