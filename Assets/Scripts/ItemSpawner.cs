using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject Booster;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBooster());
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    IEnumerator SpawnBooster()
    { 
        Booster = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Booster.transform.position = new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10));
        Booster.tag = "SpeedBoost"; 
        yield return new WaitForSeconds(3);
        Booster = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Booster.transform.position = new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10));
        Booster.tag = "SpeedBoost";

        yield return new WaitForSeconds(3);
        Booster = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Booster.transform.position = new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10));
        Booster.tag = "SpeedBoost";
    }
}

