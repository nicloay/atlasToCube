using UnityEngine;
using System.Collections;

public class SpawnPrefabController : MonoBehaviour {
	public GameObject prefab;
	public int numberOfObjects=10;
	public float rndFactor=1.2f;
	public float timeout=0.5f;
	public int atalasRandomIndexMax=10;
	
	private GameObject parentGO;
	
	// Use this for initialization
	void Start () {
		StartCoroutine("startSpawning");
	}

	IEnumerator startSpawning ()
	{
		if (parentGO==null){
			parentGO=new GameObject("GeneratedObjects");			
		}
				
		for (int i=0;i<numberOfObjects;i++){
			Vector3 spawnPosition=transform.position;			
			float rnd=Random.Range(-rndFactor,rndFactor);
			spawnPosition.z+=rnd;
			spawnPosition.x+=rnd;
			
			GameObject go=Instantiate(prefab,spawnPosition,Quaternion.identity) as GameObject;			
			go.transform.position=spawnPosition;
			CubeAtlasUVManager cm=go.GetComponent<CubeAtlasUVManager>();
			if (cm==null){
				throw new System.Exception("Prefab must have CubeAtlasUVManager on it");	
			}
			cm.randomizeFaces(10);
			go.transform.parent=parentGO.transform;
			yield return new WaitForSeconds(timeout);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
