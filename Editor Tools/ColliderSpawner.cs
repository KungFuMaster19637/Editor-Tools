using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public enum ColliderTypes
{
    Box,
    Sphere,
    Capsule
}

public class ColliderSpawner : EditorWindow
{
    private int _selected = 0;
    private string[] _colliderTypes =  new string[] { "Box", "Sphere", "Capsule"};

    [MenuItem("Personal Editor Tools/Collider Spawner")]
    static void Init()
    {
        ColliderSpawner window = (ColliderSpawner)GetWindow(typeof(ColliderSpawner));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Add Collider Handler", EditorStyles.boldLabel);

        _selected = EditorGUILayout.Popup(_selected, _colliderTypes);

        if (GUILayout.Button("Add Collider"))
        {
            foreach (GameObject selectedGO in Selection.gameObjects)
            {
                AddCollider(_selected, selectedGO);
            }
        }

    }

    private void AddCollider(int index, GameObject selectedGO)
    {
        switch(index)
        {
            case 0:
                if (!selectedGO.GetComponent<BoxCollider>())
                {
                    selectedGO.AddComponent<BoxCollider>();
                }
                break;
            case 1:
                if (!selectedGO.GetComponent<SphereCollider>())
                {
                    selectedGO.AddComponent<SphereCollider>();
                }
                break;
            case 2:
                if (!selectedGO.GetComponent<CapsuleCollider>())
                {
                    selectedGO.AddComponent<CapsuleCollider>();
                }
                break;
        }
    }
}
