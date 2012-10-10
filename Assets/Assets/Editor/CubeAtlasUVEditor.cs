using UnityEditor;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
	

[CustomEditor(typeof(CubeAtlasUVManager))]	
class CubeAtlasUVEditor : Editor {
	Dictionary<string,UVPosition> UVPositions=new Dictionary<string,UVPosition>();
	
	
	void OnEnable(){				
		CubeAtlasUVManager cm=(CubeAtlasUVManager)target;
		UVPositions.Add("front",cm.front);
		UVPositions.Add("back",cm.back);
		UVPositions.Add("left",cm.left);
		UVPositions.Add("right",cm.right);
		UVPositions.Add("top",cm.top);
		UVPositions.Add("botoom",cm.bottom);		
	}
	
	
	public override void OnInspectorGUI(){
		
		CubeAtlasUVManager cm=(CubeAtlasUVManager)target;
		
		int numberOfTiles=cm.rowCount * cm.columnCount-1;
		
		if(GUILayout.Button("Instantiate Shared Mesh"))
			instantiateMesh();
		
		
		int newRowCountIntValue=EditorGUILayout.IntField("Row Count",cm.rowCount);
		if (newRowCountIntValue!=cm.rowCount){
			cm.rowCount=newRowCountIntValue;
			updateTargetMesh();
		}
		
		int newColumnCountIntValue=EditorGUILayout.IntField("Column Count",cm.columnCount);
		if (newColumnCountIntValue!=cm.columnCount){
			cm.columnCount=newColumnCountIntValue;
			updateTargetMesh();
		}
		
		foreach(KeyValuePair<string,UVPosition> uvPosition in UVPositions){
			
			EditorGUILayout.LabelField("--------------------------------------------------------------");
			EditorGUILayout.PrefixLabel(uvPosition.Key);			
			int newPosition=EditorGUILayout.IntSlider(uvPosition.Key,uvPosition.Value.position,0,numberOfTiles);
			if (newPosition!=uvPosition.Value.position){
				uvPosition.Value.position=newPosition;
				updateTargetMesh();					
			}
			
			int newRotation=EditorGUILayout.IntSlider(uvPosition.Key,uvPosition.Value.rotation,0,3);
			if (newRotation!=uvPosition.Value.rotation){
				uvPosition.Value.rotation=newRotation;
				updateTargetMesh();				
			}						
		}		
		
	}

	void updateTargetMesh ()
	{
		CubeAtlasUVManager cm=(CubeAtlasUVManager)target;
		cm.updateMesh();
	}
	
	
	
	private void instantiateMesh(){		
		CubeAtlasUVManager cam=(CubeAtlasUVManager)target;
		GameObject gameObject = cam.gameObject;
		MeshFilter mf=gameObject.GetComponent<MeshFilter>();
		Mesh sharedMesh=mf.sharedMesh;
		if (sharedMesh!=null){
			Mesh m=new Mesh();	
			m.vertices=sharedMesh.vertices;
			m.uv=sharedMesh.uv;
			m.triangles=sharedMesh.triangles;
			m.normals=sharedMesh.normals;
			m.name=sharedMesh.name+" Instance";
			mf.sharedMesh=m;
		}
		
	}
}
