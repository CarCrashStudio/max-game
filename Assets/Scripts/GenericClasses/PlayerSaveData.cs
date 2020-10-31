using UnityEngine;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using JetBrains.Annotations;

public class PlayerSaveData : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Inventory inventory;
    [SerializeField] Health health;
    [SerializeField] Experience experience;
    [SerializeField] QuestLog questLog;

    struct _3Vector
    {
        public float x, y, z;
    }
    class player_data
    {
        public _3Vector position;
        public byte level;
        public Attributes attributes;
        public float gold;
        public float currentHealth;
        public float maxHealth;
        public float currentExp;
        public float maxExp;

        public InventoryItem[] inventory;
        public Equipment[] equipment;
        public List<Quest> quests;
    }

    private void Awake()
    {
        GetGameComponents();
    }

    private void GetGameComponents()
    {
        player = FindObjectOfType<Player>();
        inventory = player.gameObject.GetComponent<Inventory>();
        health = player.gameObject.GetComponent<Health>();
        experience = player.gameObject.GetComponent<Experience>();
        questLog = player.gameObject.GetComponent<QuestLog>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Save ()
    {
        if (!Directory.Exists($"{Application.persistentDataPath}/saves")) { Directory.CreateDirectory($"{Application.persistentDataPath}/saves"); }
        if (!Directory.Exists($"{Application.persistentDataPath}/saves/savefile1")) { Directory.CreateDirectory($"{Application.persistentDataPath}/saves/savefile1"); }

        Debug.Log($"{Application.persistentDataPath}/saves/savefile1/player.json");

        player_data data = new player_data();
        data.position = new _3Vector()
        {
            x = player.gameObject.transform.position.x,
            y = player.gameObject.transform.position.y,
            z = player.gameObject.transform.position.z
        };

        data.level = player.Level;
        data.gold = player.gold;
        data.attributes = player.attributes;
        data.currentHealth = health.CurrentHealth;
        data.maxHealth = health.MaxHealth.runtimeValue;
        data.currentExp = experience.CurrentExp.runtimeValue;
        data.maxExp = experience.MaxExp.runtimeValue;

        data.inventory = inventory.inventory;
        data.equipment = inventory.equipment;
        data.quests = questLog.Quests;

        using (StreamWriter writer = new StreamWriter($"{Application.persistentDataPath}/saves/savefile1/player.json"))
        {
            writer.WriteLine(JsonConvert.SerializeObject(data));
            writer.Close();
        }
    }
    public void Load ()
    {
        GetGameComponents();

        if (!Directory.Exists($"{Application.persistentDataPath}/saves")) { return; }
        if (!Directory.Exists($"{Application.persistentDataPath}/saves/savefile1")) { return; }
        if (!File.Exists($"{Application.persistentDataPath}/saves/savefile1/player.json")) { return; }

        Debug.Log($"{Application.persistentDataPath}/saves/savefile1/player.json");
        using (StreamReader reader = new StreamReader($"{Application.persistentDataPath}/saves/savefile1/player.json"))
        {
            string json = reader.ReadLine();
            reader.Close();

            player_data temp = JsonConvert.DeserializeObject<player_data>(json);

            player.gameObject.transform.position = new Vector3(temp.position.x, temp.position.y, temp.position.z);

            player.gold = temp.gold;
            player.Level = temp.level;

            player.attributes = temp.attributes;
            player.attributes.Start();

            health.Load(temp.currentHealth, temp.maxHealth);
            experience.Load(temp.currentExp, temp.maxExp);

            inventory.inventory = temp.inventory;
            inventory.equipment = temp.equipment;

            questLog.Quests.Clear();
            questLog.Quests.AddRange(temp.quests);
        }
    }
}
