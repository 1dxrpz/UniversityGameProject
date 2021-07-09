using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject InterractMark;
    public GameObject CreateInterractMark()
	{
        return Instantiate(InterractMark);
	}
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
