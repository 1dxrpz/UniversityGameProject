using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject Tile;
    private GameObject[] Tiles = new GameObject[10];
    void Start()
    {
		for (int i = 0; i < 5; i++)
		{
            Tiles[i] = Instantiate(Tile);
            Tiles[i].GetComponent<Transform>().position = new Vector3(
                transform.position.x + i * (Tiles[i].GetComponent<Renderer>().bounds.size.x + .05f),
                transform.position.y,
                transform.position.z
                );
		}
        for (int i = 0; i < 5; i++)
		{
            Tiles[i + 5] = Instantiate(Tile);
            Tiles[i + 5].GetComponent<Transform>().position = new Vector3(
                transform.position.x + i * (Tiles[i + 5].GetComponent<Renderer>().bounds.size.x + .05f) + Tiles[i + 5].GetComponent<Renderer>().bounds.size.x / 2,
                transform.position.y,
                transform.position.z + Tiles[i + 5].GetComponent<Renderer>().bounds.size.z * Mathf.Sin(Mathf.PI / 3)
                );
		}
    }
    void Update()
    {
        
    }
}
