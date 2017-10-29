using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSpawner : MonoBehaviour {
    public GameObject[] cars;
    int carNumber;
    public float maxPos = 2.2f;
    public float delayTimer = 1f;
    float timer;
	// Use this for initialization
	void Start () {
        timer = delayTimer;
	}
	
	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Vector3 carPos = new Vector3(Random.Range(-maxPos, maxPos), transform.position.y, transform.position.z);
            carNumber = Random.Range(0, cars.Length + 1);
            try {
                Instantiate(cars[carNumber], carPos, transform.rotation);
            }
            catch(System.IndexOutOfRangeException e)
            {
                carNumber = carNumber - 1;
                Instantiate(cars[carNumber], carPos, transform.rotation);
            }
            
            timer = delayTimer;
        }
    }
}
