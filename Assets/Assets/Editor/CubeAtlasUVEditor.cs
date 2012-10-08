using UnityEditor;
using System.Collections;
using UnityEngine;
	

[CustomEditor(typeof(CubeAtlasUVManager))]	
class CubeAtlasUVEditor : Editor {
	SerializedProperty[] UVPositions=new SerializedProperty[6];
	SerializedProperty rowCount;
	SerializedProperty columnCount;
	
	
	
	void OnEnable(){		
		
		UVPositions[0]=serializedObject.FindProperty("front");
		UVPositions[1]=serializedObject.FindProperty("back");
		UVPositions[2]=serializedObject.FindProperty("left");
		UVPositions[3]=serializedObject.FindProperty("right");
		UVPositions[4]=serializedObject.FindProperty("top");
		UVPositions[5]=serializedObject.FindProperty("bottom");	
		
		
		rowCount=serializedObject.FindProperty("rowCount");
		columnCount=serializedObject.FindProperty("columnCount");
	}
	
	
	public override void OnInspectorGUI(){
		serializedObject.Update ();
		CubeAtlasUVManager cubeUVManager=(CubeAtlasUVManager)target;		
		int numberOfTiles=cubeUVManager.rowCount*cubeUVManager.columnCount-1;
		
		EditorGUILayout.PropertyField(rowCount);
		EditorGUILayout.PropertyField(columnCount);
		
		foreach(SerializedProperty uvPosition in UVPositions){
			EditorGUILayout.LabelField("--------------------------------------------------------------");
			EditorGUILayout.PrefixLabel(uvPosition.name);
			EditorGUILayout.IntSlider(uvPosition.FindPropertyRelative("position"),0,numberOfTiles);
			EditorGUILayout.IntSlider(uvPosition.FindPropertyRelative("rotation"),0,3);			
		}		
		serializedObject.ApplyModifiedProperties ();		
		CubeAtlasUVManager cm=(CubeAtlasUVManager)target;
		cm.updateMesh();
	}
	
}
