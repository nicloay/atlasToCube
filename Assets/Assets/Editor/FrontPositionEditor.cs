using UnityEditor;
using System.Collections;
using UnityEngine;


[CustomEditor(typeof(UVPosition))]	
public class FrontPositionEditor : Editor {

	public override void OnInspectorGUI(){
		EditorGUILayout.LabelField("test  .....");
	}
}
