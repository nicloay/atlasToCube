using UnityEngine;
using System;

public delegate void UVPositionPropertyChange(string propertyName);

[Serializable]
public class UVPosition {
	public UVPositionPropertyChange onUVPositionPropertyChange;
	
	[SerializeField]
	private int _position;
	
	[SerializeField]
	private int _rotation;

	public int position {
		get {
			return this._position;
		}
		set {		
			broadcastPropertyChange("position");
			_position = value;
		}
	}

	public int rotation {
		get {
			return this._rotation;
		}
		set {
			broadcastPropertyChange("rotation");
			_rotation = value;
		}
	}	
	
	public UVPosition(int position,int rotation){
		this._position=position;
		this._rotation=rotation;
	}
	
	private void broadcastPropertyChange(string propertyName){
		if(onUVPositionPropertyChange!=null)	
			onUVPositionPropertyChange(propertyName);
	}
}
