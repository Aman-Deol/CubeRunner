using UnityEngine;
using System.Collections;

public class CubeCreator : MonoBehaviour {

    public Light myLight;
    int zInt = 0;
    int max = 1000;
	// Use this for initialization
	void Start () {
        RandomCubeGenerator();
        RandomCubeGenerator(1);
        RandomCubeGenerator(2);
        RandomCubeGenerator(3);
        RandomCubeGenerator(4);
        zInt += 5;
        CubeInvoker();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space")){
            myLight.enabled = !myLight.enabled;
        }
	}

    void RandomCubeGenerator(int max = 15, int zAddition = 0){
        int i = 0;
        float[] cubeXList = new float[max];
        while (i < max) {
            var cube = CreateCubeWithBound1(zAddition);

            while (CubeCollision(cubeXList, cube, i)){
                Destroy(cube);
                CreateCubeWithBound1();
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

    GameObject CreateCubeWithBound1(int zAddition = 0)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var randColour = Random.Range(1, 8);
        var x = Random.Range(-30F, 30F);
        var z = Random.Range(0F,1F);
        cube.transform.position = new Vector3(x, 0.5F, z + zInt + zAddition); // puts it in position
        cube.AddComponent<BoxCollider>().bounds.Expand(1);                    // add bound for collision detection
        cube.GetComponent<Renderer>().material.color = CubeColour(randColour); // gives the cube a color
        cube.tag = "Cube";
        return cube;
    }

    IEnumerator ContinuousCube()
    {
        while (zInt < max) {
            yield return new WaitForSeconds(1.2F);
            RandomCubeGenerator();
        }
    }

    void CubeInvoker()
    {
      StartCoroutine("ContinuousCube");
    }
}
