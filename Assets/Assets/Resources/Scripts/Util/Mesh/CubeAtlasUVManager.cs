using UnityEngine;
using System.Collections;


//works only with cube;
[ExecuteInEditMode]
public class CubeAtlasUVManager : MonoBehaviour {
	
	public int rowCount=4;
	public int columnCount=4;
	
	public UVPosition front=new UVPosition(0,0);
	public UVPosition back=new UVPosition(0,0);
	public UVPosition left=new UVPosition(0,0);
	public UVPosition right=new UVPosition(0,0);
	public UVPosition top=new UVPosition(0,0);
	public UVPosition bottom=new UVPosition(0,0);
	
	
	
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
		updateMesh ();
	}

	public void updateMesh ()
	{
		Mesh mesh = getMesh ();
      	Vector2[] uvs = mesh.uv;			
		updateUVs(ref uvs,backIndexes,front);
		updateUVs(ref uvs,topIndexes,top);
		updateUVs(ref uvs,frontIndexes,front);
		updateUVs(ref uvs,bottomIndexes,bottom);
		updateUVs(ref uvs,leftIndexes,left);
		updateUVs(ref uvs,rightIndexes,right);
		mesh.uv=uvs;
	}

	Mesh getMesh ()
	{
		Mesh result=new Mesh();
		if (Application.isPlaying){
			result = gameObject.GetComponent<MeshFilter>().mesh;
		}else{
			result=gameObject.GetComponent<MeshFilter>().sharedMesh;			
		}			
		return result;
	}
	
	
	
	
	private void updateUVs(ref Vector2[] uv,int[] indexes, UVPosition uvPosition){
		int y=rowCount - uvPosition.position/columnCount-1;		
		int x=uvPosition.position%columnCount;		
		Vector2 botLeft=new Vector2(x*uvScaleX,(y+1)*uvScaleY);
		Vector2 botRight=new Vector2((x+1)*uvScaleX,(y+1)*uvScaleY);
		Vector2 topRight=new Vector2((x+1)*uvScaleX,y*uvScaleY);
		Vector2 topLeft=new Vector2(x*uvScaleX,y*uvScaleY);
		
		Vector2[] rotationArray=new Vector2[8];
	 	rotationArray[0]=topLeft;
		rotationArray[1]=topRight;		
		rotationArray[2]=botRight;
		rotationArray[3]=botLeft;
		rotationArray[4]=topLeft;
		rotationArray[5]=topRight;		
		rotationArray[6]=botRight;
		rotationArray[7]=botLeft;
		
		topLeft=rotationArray[uvPosition.rotation];
		topRight=rotationArray[uvPosition.rotation+1];
		botRight=rotationArray[uvPosition.rotation+2];
		botLeft=rotationArray[uvPosition.rotation+3];
		
		uv[indexes[0]]=botLeft;
		uv[indexes[1]]=botRight;
		uv[indexes[2]]=topLeft;
		uv[indexes[3]]=topLeft;
		uv[indexes[4]]=botRight;
		uv[indexes[5]]=topRight;
		
	}
	
	void Update(){
		if (!Application.isPlaying)	{
			updateMesh();	
		}
	}
	
}
