using UnityEngine;
using System.Collections;


//works only with cube;
[ExecuteInEditMode]
public class CubeAtlasUVManager : MonoBehaviour {
	
	public int rowCount=4;
	public int columnCount=4;

	public int frontFacePosition=0;
	public int backFacePosition=0;
	public int leftFacePosition=0;
	public int rightFacePosition=0;
	public int topFacePosition=0;
	public int bottomFacePosition=0;
	
	
	public static int[] backIndexes=new int[]{1,0,3,3,0,2};
	public static int[] topIndexes=new int[]{9,8,5,5,8,4};	
	public static int[] frontIndexes=new int[]{11,10,7,7,10,6};
	public static int[] bottomIndexes=new int[]{14,12,13,13,12,15};
	public static int[] leftIndexes=new int[]{18,16,17,17,16,19};
	public static int[] rightIndexes=new int[]{22,20,21,21,20,23};
	
	public float uvScaleX{
		get{
			return 1f/columnCount;
		}
	}
	
	public float uvScaleY{
		get{
			return 1f/rowCount;			
		}
	}
	
	
	void Start() {
	 	Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        Vector2[] uvs = mesh.uv;			
		updateMesh(ref uvs,backIndexes,frontFacePosition);
		updateMesh(ref uvs,topIndexes,topFacePosition);
		updateMesh(ref uvs,frontIndexes,frontFacePosition);
		updateMesh(ref uvs,bottomIndexes,bottomFacePosition);
		updateMesh(ref uvs,leftIndexes,leftFacePosition);
		updateMesh(ref uvs,rightIndexes,rightFacePosition);
		mesh.uv=uvs;
	}
	
	private void updateMesh(ref Vector2[] uv,int[] indexes, int atlasPosition){
		int y=rowCount - atlasPosition/columnCount-1;		
		int x=atlasPosition%columnCount;		
		Vector2 botLeft=new Vector2(x*uvScaleX,(y+1)*uvScaleY);
		Vector2 botRight=new Vector2((x+1)*uvScaleX,(y+1)*uvScaleY);
		Vector2 topRight=new Vector2((x+1)*uvScaleX,y*uvScaleY);
		Vector2 topLeft=new Vector2(x*uvScaleX,y*uvScaleY);
		
		uv[indexes[0]]=botLeft;
		uv[indexes[1]]=botRight;
		uv[indexes[2]]=topLeft;
		uv[indexes[3]]=topLeft;
		uv[indexes[4]]=botRight;
		uv[indexes[5]]=topRight;
		
	}
	
	
	
	
	void Update(){
		if (!Application.isPlaying)	{
			Start();	
		}
	}
	
	
	void __debugShowVerteces(){	
        Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        int i = 0;
        while (i < vertices.Length) {
            i++;
        }
		
		int[] triangles=mesh.triangles;
		for (int v=0;v<triangles.Length;v++){
			Debug.Log(v+"  =>"+triangles[v]);	
		}		        
    }
	
	public GameObject get(){
		return this.gameObject;	
	}
	
}
