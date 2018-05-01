using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeCreator : MonoBehaviour {

    public Light myLight;
    int zInt = 0;
	int max = 300;
	int pooledAmount = 300;
	List <GameObject> cubes;
	List <GameObject> initialCubes;
	GameObject initialCube;
	GameObject cube;
	// Use this for initialization
	void Start () {
		/*for (i = 0; i < 5; i++) {
			GameObject initialCube = GameObject.Instantiate (initialCube);
			initialCubes.Add (cube);
		}*/
        //RandomCubeGenerator();
        //RandomCubeGenerator(1);
        //RandomCubeGenerator(2);
        //RandomCubeGenerator(3);
        //RandomCubeGenerator(4);

        zInt += 5;
		cubes = new List<GameObject> ();
		for (int i = 0; i < pooledAmount; i++) {
			//cube = GameObject.Instantiate (cube);
			cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.SetActive (false);
			cubes.Add (cube);
		}
        CubeInvoker();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space")){
            myLight.enabled = !myLight.enabled;
        }
		IsCubeBehind ();
	}

	void IsCubeBehind(){
		GameObject[] cubeObjects = GameObject.FindGameObjectsWithTag("Cube");
		foreach (GameObject c in cubeObjects) {
			if (transform.position.z > (c.transform.position.z + 15)) {
				c.SetActive (false);
			}
		}
	}

    void RandomCubeGenerator(int max = 25, int zAddition = 0){
        int i = 0;
        float[] cubeXList = new float[max];
        while (i < max) {
            var cube = CreateCubeWithBound1(zAddition, 3);

            while (CubeCollision(cubeXList, cube, i)){
                Destroy(cube);
                CreateCubeWithBound1(zAddition, 3);
            }
            i++;
        }
        zInt++;
    }

    bool CubeCollision(float[] cubeXList, GameObject cube, int i)
    {
        GameObject[] cubeObjects = GameObject.FindGameObjectsWithTag("Cube");
        if (i == 0) { return false; }
        else {
            foreach (GameObject c in cubeObjects)
            {
                if (c.GetComponent<Renderer>().bounds.Intersects(cube.GetComponent<Renderer>().bounds) && (c.GetComponent<BoxCollider>().center != cube.GetComponent<BoxCollider>().center))
                {
                    return true;
                }
            }
        }
        return false;
    }

    Color CubeColour(int value)  //ROYGBIV
    {
        switch (value)
        {
            case 1:
                return Color.red;
            case 2:
                return new Color32(255,165,0,255);
            case 3:
                return Color.yellow;
            case 4:
                return Color.green;
            case 5:
                return Color.blue;
            case 6:
                return Color.magenta;
            case 7:
                return new Color32(135, 206, 250, 255);
            default:
                return Color.white;
        }
    }

	GameObject CreateCubeWithBound1(int zAddition = 0, int bound = 1)
    {
			
        var cubeD = GameObject.CreatePrimitive(PrimitiveType.Cube);
		for (int i = 0; i < cubes.Count; i++){
			if (!cubes [i].activeInHierarchy) {
				cubes [i].SetActive (true);
				var cube = cubes [i];
				var randColour = Random.Range (1, 8);
				var x = Random.Range (-60F, 60F);
				var z = Random.Range (0F, 1F);
				var currPlayerX = GameObject.FindGameObjectWithTag ("Player").transform.position.x;
				var currPlayerZ = GameObject.FindGameObjectWithTag ("Player").transform.position.z;
				cube.transform.position = new Vector3 (x + currPlayerX, 0.5F, z + zInt + zAddition + currPlayerZ); // puts it in position
				cube.AddComponent<BoxCollider> ().bounds.Expand (bound);                    // add bound for collision detection
				cube.GetComponent<Renderer> ().material.color = CubeColour (randColour); // gives the cube a color
				cube.tag = "Cube";
				return cube;
			}
    }
			return cubeD;
	}

    IEnumerator ContinuousCube()
    {
        while (zInt < max) {
            yield return new WaitForSeconds(0.6F);
            RandomCubeGenerator();
			RandomCubeGenerator();
        }
    }

    void CubeInvoker()
    {
      StartCoroutine("ContinuousCube");
    }
}
