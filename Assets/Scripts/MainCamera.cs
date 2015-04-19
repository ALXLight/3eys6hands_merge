using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
    public Transform Player;
    public Vector3 Displacement;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = Player.position + Displacement;    
	}
}