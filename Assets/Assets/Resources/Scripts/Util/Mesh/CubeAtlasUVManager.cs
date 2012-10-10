using UnityEngine;
using System.Collections;

public delegate void CubeAtlasUVManagerPropertyChange(string propertyName);

//works only with cube;
[ExecuteInEditMode]
public class CubeAtlasUVManager : MonoBehaviour {
	
	public CubeAtlasUVManagerPropertyChange onCubeAtlasUVManagerPropertyChange;
	
	[SerializeField]
	private int _rowCount=4;
	[SerializeField]
	private int _columnCount=4;
	
	[SerializeField]
	private UVPosition _front=new UVPosition(0,0);
	[SerializeField]
	private UVPosition _back=new UVPosition(0,0);
	[SerializeField]
	private UVPosition _left=new UVPosition(0,0);
	[SerializeField]
	private UVPosition _right=new UVPosition(0,0);
	[SerializeField]
	private UVPosition _top=new UVPosition(0,0);
	[SerializeField]
	private UVPosition _bottom=new UVPosition(0,0);

	public UVPosition back {
		get {
			return this._back;
		}
		set {			
			value.onUVPositionPropertyChange +=broadcastPropetyChange;
			_back = value;
		}
	}

	public UVPosition bottom {
		get {
			return this._bottom;
		}
		set {
			value.onUVPositionPropertyChange+=broadcastPropetyChange;
			_bottom = value;
		}
	}

	public UVPosition front {
		get {
			return this._front;
		}
		set {
			value.onUVPositionPropertyChange+=broadcastPropetyChange;
			_front = value;
		}
	}

	public UVPosition left {
		get {
			return this._left;
		}
		set {
			value.onUVPositionPropertyChange+=broadcastPropetyChange;
			_left = value;
		}
	}

	public UVPosition right {
		get {
			return this._right;
		}
		set {
			value.onUVPositionPropertyChange+=broadcastPropetyChange;
			_right = value;
		}
	}

	
	public UVPosition top {
		get {
			return this._top;
		}
		set {
			value.onUVPositionPropertyChange+=broadcastPropetyChange;
			_top = value;
		}
	}	
	
	public int rowCount {
		get {			
			return this._rowCount;
		}
		set {
			if (value<=0){
				Debug.LogWarning("rowCount cannot be less than 1, assign 1 to it");
				value=1;
			}
			
			broadcastPropetyChange("RowCount");
			_rowCount = value;
		}
	}
	
	public int columnCount {
		get {
			return this._columnCount;
		}
		set {
			if (value<=0){
				Debug.LogWarning("columnCount cannot be less than 1, assign 1 to it");
				value=1;
			}			
			broadcastPropetyChange("columnCount");
			_columnCount = value;
		}
	}

	public static int[] backIndexes=new int[]{1,0,3,3,0,2};
	public static int[] topIndexes=new int[]{9,8,5,5,8,4};	
	public static int[] frontIndexes=new int[]{11,10,7,7,10,6};
	public static int[] bottomIndexes=new int[]{14,12,13,13,12,15};
	public static int[] leftIndexes=new int[]{18,16,17,17,16,19};
	public static int[] rightIndexes=new int[]{22,20,21,21,20,23};	
	
	public float uvScaleX{
		get{
			return 1f/_columnCount;
		}
	}
	
	public float uvScaleY{
		get{
			return 1f/_rowCount;			
		}
	}
	
	void Awake(){
		onCubeAtlasUVManagerPropertyChange+=onPropertyChangeListener;		
	}
	
	public void randomizeFaces(int atlasIdLimit){
		UVPosition[] positions=new UVPosition[]{_front,_back,_left,_right,_top,_bottom};
		for (int i=0;i<positions.Length;i++){		
			int face=Random.Range(0,atlasIdLimit);
			int rotation=Random.Range(0,4);			
			positions[i].position=face;
			positions[i].rotation=rotation;			
		}		
		updateMesh();
		
	}

	private void onPropertyChangeListener(string propertyName){
		updateMesh();	
	}
	

	public void updateMesh ()
	{
		Mesh mesh = getMesh ();
      	Vector2[] uvs = mesh.uv;			
		updateUVs(ref uvs,backIndexes,back);
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
	
	
	void broadcastPropetyChange(string propertyName){
		if(onCubeAtlasUVManagerPropertyChange!=null)
			onCubeAtlasUVManagerPropertyChange(propertyName);
	}
	
}
