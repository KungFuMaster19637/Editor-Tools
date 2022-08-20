using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectRandomiser : EditorWindow
{
    private bool _randomX, _randomY, _randomZ;
    private bool _randomScale;
    private float _minScale, _maxScale;

    [MenuItem("Personal Editor Tools/Object Randomiser")]
    static void Init()
    {
        ObjectRandomiser window = (ObjectRandomiser)GetWindow(typeof(ObjectRandomiser));
        window.Show();

    }

    private void OnGUI()
    {
        GUILayout.Label("Randomise Selected Objects", EditorStyles.boldLabel);

        GUILayout.Label("Rotations");
        _randomX = EditorGUILayout.Toggle("Randomise X", _randomX);
        _randomY = EditorGUILayout.Toggle("Randomise Y", _randomY);
        _randomZ = EditorGUILayout.Toggle("Randomise Z", _randomZ);


        GUILayout.Label("Scaling");
        _randomScale = EditorGUILayout.Toggle("Randomise Scale", _randomScale);
        _minScale = EditorGUILayout.FloatField("Min Scale", _minScale);
        _maxScale = EditorGUILayout.FloatField("Max Scale", _maxScale);

        if (GUILayout.Button("Randomise Objects"))
        {
            foreach (GameObject selectedGO in Selection.gameObjects)
            {
                selectedGO.transform.rotation = Quaternion.Euler(GetRandomRotations(selectedGO.transform.rotation.eulerAngles));

                if (_randomScale)
                {
                    float scaleVal = Random.Range(_minScale, _maxScale);
                    selectedGO.transform.localScale = new Vector3(scaleVal, scaleVal, scaleVal);
                }
            }
        }
    }

    private Vector3 GetRandomRotations(Vector3 currentRotation)
    {
        float xRotation = _randomX ? Random.Range(0f, 360f) : currentRotation.x;
        float yRotation = _randomY ? Random.Range(0f, 360f) : currentRotation.y;
        float zRotation = _randomZ ? Random.Range(0f, 360f) : currentRotation.z;

        return new Vector3(xRotation, yRotation, zRotation);
    }
}
