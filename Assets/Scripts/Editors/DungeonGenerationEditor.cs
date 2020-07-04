using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Threading.Tasks;

[CustomEditor(typeof(DungeonManager))]
public class DungeonGenerationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DungeonManager dungeonGen = (DungeonManager)target;

        if (DrawDefaultInspector())
        {
            if (dungeonGen.autoUpdate)
            {
                dungeonGen.DestroyMap();
                dungeonGen.GenerateMap();
            }
        }
        if (GUILayout.Button("Generate Dungeon Seed"))
        {
            dungeonGen.GenerateRandomSeed();
        }
        if (GUILayout.Button("Generate"))
        {
            dungeonGen.DestroyMap();
            dungeonGen.GenerateMap();
        }
        if (GUILayout.Button("Destroy"))
        {
            dungeonGen.DestroyMap();
        }
        if (GUILayout.Button("Set Camera Pos"))
        {
            dungeonGen.SetCameraPos();
        }
    }
}
