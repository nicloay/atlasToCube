using UnityEditor;
using System.Collections;
using UnityEngine;
	
[CustomEditor(typeof(CubeAtlasUVManager))]	
class CubeAtlasUVEditor : Editor {
	SerializedProperty[] faces=new SerializedProperty[6];
	
	
	void OnEnable(){		
		faces[0]=serializedObject.FindProperty("frontFacePosition");
		faces[1]=serializedObject.FindProperty("backFacePosition");
		faces[2]=serializedObject.FindProperty("leftFacePosition");
		faces[3]=serializedObject.FindProperty("rightFacePosition");
		faces[4]=serializedObject.FindProperty("topFacePosition");
		faces[5]=serializedObject.FindProperty("bottomFacePosition");		
	}
	
	
	public override void OnInspectorGUI(){
		serializedObject.Update ();
		CubeAtlasUVManager cubeUVManager=(CubeAtlasUVManager)target;		
		int numberOfTiles=cubeUVManager.rowCount*cubeUVManager.columnCount-1;
		
		foreach (SerializedProperty face in faces)		
			EditorGUILayout.IntSlider(face, 0, numberOfTiles);
		
		
					
		
		serializedObject.ApplyModifiedProperties ();
		base.OnInspectorGUI();
	}
	
}
