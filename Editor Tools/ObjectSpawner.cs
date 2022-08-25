using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectSpawner : EditorWindow
{
    [Header("Object Basics")]
    private GameObject _objectToSpawn;
    private string _objectName;
    private float _objectScale;

    [Header("Object Spawn")]
    private int _selectedSpawnMethod = 0;
    private string[] _spawnMethods = new string[] { "Zero", "Fixed", "Radius"};
    private float _fixedX, _fixedY, _fixedZ;
    private float _radius;

    [MenuItem("Personal Editor Tools/Object Spawner")]
    static void Init()
    {
        ObjectSpawner window = (ObjectSpawner)GetWindow(typeof(ObjectSpawner));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Spawn Objects", EditorStyles.boldLabel);
        _objectName = EditorGUILayout.TextField("Object Name", _objectName);
        _objectScale = EditorGUILayout.Slider("Object Scale", _objectScale, 0.1f, 10);
        _objectToSpawn = EditorGUILayout.ObjectField("Object Prefab", _objectToSpawn, typeof(GameObject), false) as GameObject;

        EditorGUILayout.Space();

        GUILayout.Label("Spawn Method", EditorStyles.boldLabel);

        _selectedSpawnMethod = EditorGUILayout.Popup(_selectedSpawnMethod, _spawnMethods);

        EditorGUILayout.Space();

        ChangeSpawnMethod(_selectedSpawnMethod);


        if (GUILayout.Button("Spawn Object"))
        {
            CheckSpawnMethod(_selectedSpawnMethod);
        }
    }

    private void CheckSpawnMethod(int index)
    {
        GameObject spawnedObject = null;

        switch (index)
        {
            case 0:
                spawnedObject = Instantiate(_objectToSpawn, Vector3.zero, Quaternion.identity);
                SetObjectScale(spawnedObject);
                break;
            case 1:
                spawnedObject = Instantiate(_objectToSpawn, new Vector3(_fixedX, _fixedY, _fixedZ), Quaternion.identity);
                SetObjectScale(spawnedObject);
                break;
            case 2:
                Vector2 spawnCircle = Random.insideUnitCircle * _radius;
                spawnedObject = Instantiate(_objectToSpawn, new Vector3(spawnCircle.x, 0, spawnCircle.y), Quaternion.identity);
                SetObjectScale(spawnedObject);
                break;
        }
        spawnedObject.name = _objectName;
    }

    private void SetObjectScale(GameObject spawnedObject)
    {
        spawnedObject.transform.localScale = new Vector3(_objectScale, _objectScale, _objectScale);
    }

    private void ChangeSpawnMethod(int index)
    {
        switch(index)
        {
            case 0:
                GUILayout.Label("Object will be spawned at position (0,0,0)");
                break;
            case 1:
                _fixedX = EditorGUILayout.FloatField("X Position", _fixedX);
                _fixedY = EditorGUILayout.FloatField("Y Position", _fixedY);
                _fixedZ = EditorGUILayout.FloatField("Z Position", _fixedZ);
                break;
            case 2:
                GUILayout.Label("Object will be spawned at randomly in the radius");
                _radius = EditorGUILayout.FloatField("Radius", _radius);
                break;
        }
    }

}
