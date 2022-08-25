using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ColliderSpawner : EditorWindow
{
    private int _selectedColliderType = 0;
    private string[] _colliderTypes = new string[] { "Box", "Sphere", "Capsule" };

    private int _direction = 0;
    private string[] _directionTypes = new string[] { "X-axis", "Y-axis", "Z-axis" };

    [Header("Center")]
    private float _centerX, _centerY, _centerZ;

    [Header("Box Collider")]
    private float _sizeX, _sizeY, _sizeZ;

    [Header("Sphere Collider")]
    private float _radius;

    [Header("CapsuleCollider")]
    private float _height;


    [MenuItem("Personal Editor Tools/Collider Spawner")]
    static void Init()
    {
        ColliderSpawner window = (ColliderSpawner)GetWindow(typeof(ColliderSpawner));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Collider Type", EditorStyles.boldLabel);

        _selectedColliderType = EditorGUILayout.Popup(_selectedColliderType, _colliderTypes);

        EditorGUILayout.Space();

        GUILayout.Label("Collider Variables", EditorStyles.boldLabel);
        EditCollider(_selectedColliderType);

        if (GUILayout.Button("Add Collider"))
        {
            foreach (GameObject selectedGO in Selection.gameObjects)
            {
                AddCollider(_selectedColliderType, selectedGO);
            }
        }

    }

    private void AddCollider(int index, GameObject selectedGO)
    {
        switch (index)
        {
            case 0:
                if (!selectedGO.GetComponent<BoxCollider>())
                {
                    selectedGO.AddComponent<BoxCollider>();
                }
                selectedGO.GetComponent<BoxCollider>().center = new Vector3(_centerX, _centerY, _centerZ);
                selectedGO.GetComponent<BoxCollider>().size = new Vector3(_sizeX, _sizeY, _sizeZ);
                break;
            case 1:
                if (!selectedGO.GetComponent<SphereCollider>())
                {
                    selectedGO.AddComponent<SphereCollider>();
                }
                selectedGO.GetComponent<SphereCollider>().center = new Vector3(_centerX, _centerY, _centerZ);
                selectedGO.GetComponent<SphereCollider>().radius = _radius;
                break;
            case 2:
                if (!selectedGO.GetComponent<CapsuleCollider>())
                {
                    selectedGO.AddComponent<CapsuleCollider>();
                }
                selectedGO.GetComponent<CapsuleCollider>().center = new Vector3(_centerX, _centerY, _centerZ);
                selectedGO.GetComponent<CapsuleCollider>().radius = _radius;
                selectedGO.GetComponent<CapsuleCollider>().height = _height;
                selectedGO.GetComponent<CapsuleCollider>().direction = _direction;
                break;
        }
    }

    private void EditCollider(int index)
    {
        _centerX = EditorGUILayout.FloatField("Center X", _centerX);
        _centerY = EditorGUILayout.FloatField("Center Y", _centerY);
        _centerZ = EditorGUILayout.FloatField("Center Z", _centerZ);

        EditorGUILayout.Space();

        switch (index)
        {
            case 0:
                _sizeX = EditorGUILayout.FloatField("Size X", _sizeX);
                _sizeY = EditorGUILayout.FloatField("Size Y", _sizeY);
                _sizeZ = EditorGUILayout.FloatField("Size Z", _sizeZ);
                break;
            case 1:
                _radius = EditorGUILayout.FloatField("Radius", _radius);
                break;
            case 2:
                _radius = EditorGUILayout.FloatField("Radius", _radius);
                EditorGUILayout.Space();

                _height = EditorGUILayout.FloatField("Height", _height);
                EditorGUILayout.Space();

                _direction = EditorGUILayout.Popup(_direction, _directionTypes);
                EditorGUILayout.Space();
                break;
        }
    }
}
