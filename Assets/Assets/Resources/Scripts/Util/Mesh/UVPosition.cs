using UnityEngine;
using System;

[Serializable]
public class UVPosition {
	public int position;
	public int rotation;
	public UVPosition(int position,int rotation){
		this.position=position;
		this.rotation=rotation;
	}
}
