﻿//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RotateBuilding : MonoBehaviour {


    public void RotateToAttractor(GameObject world) {
        if (world == null)
            world = GameObject.FindGameObjectWithTag("Attractor");
        Vector3 gravityUp = (transform.position - world.transform.position).normalized;
        Vector3 targetUp = transform.up;
        Quaternion targetRotation = Quaternion.FromToRotation(targetUp, gravityUp) * transform.rotation;
        transform.rotation = targetRotation;
    }
    
}

#if UNITY_EDITOR
[CustomEditor(typeof(RotateBuilding))]
public class RotateBuildingEditor : Editor
{
    GameObject world;
    public override void OnInspectorGUI()
    {
        RotateBuilding myTarget = (RotateBuilding)target;
        world = (GameObject)EditorGUILayout.ObjectField(world, typeof(GameObject), true);
        if (GUILayout.Button("ROTATE")) {
            myTarget.RotateToAttractor(world);
        }

    }
}
#endif