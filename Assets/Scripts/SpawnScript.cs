using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnScript : MonoBehaviour {

	public GameObject[] ground;
	public float spawnMin = 0.5f;
	int pooledAmount = 15;
	List <GameObject> grounds;
	GameObject ground1;

	void Start() {
		grounds = new List<GameObject> ();
		for (int i = 0; i < pooledAmount; i++) {
			//ground1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
			GameObject obj = (GameObject)Instantiate (ground1);
			obj.SetActive (false);
			grounds.Add (obj);
		}
		InvokeRepeating ("Spawn", spawnMin, spawnMin);
	}

	void Update () {
		GameObject[] planeObjects = GameObject.FindGameObjectsWithTag("Plane");
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		foreach (GameObject p in planeObjects) {
			if (player.transform.position.z > (p.transform.position.z + 50)) {
				p.SetActive (false);
			}
		}
	}

	void Spawn() {
		//Instantiate(ground[Random.Range (0,ground.GetLength(0))],transform.position, Quaternion.identity);
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		for (int i = 0; i < grounds.Count; i++){
			if (!grounds [i].activeInHierarchy) {
				grounds [i].SetActive (true);
				var ground = grounds [i];
				ground.transform.position =  new Vector3 (player.transform.position.x, 0F,player.transform.position.z);
				//Vector3.Scale(new Vector3(5, 0.5, 5),
				ground.tag = "Plane";
				break;
			}
		}
	}
}

