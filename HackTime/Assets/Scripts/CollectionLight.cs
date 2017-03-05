using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionLight : MonoBehaviour {
	public GameObject collectorSphere;
	public Material new_color;
	public Light old_light;
	public Light new_light;
	private CollectorSphere script;
	// Use this for initialization
	void Start () {
		script = collectorSphere.GetComponent<CollectorSphere> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (script.hasBeenCollected) {
			old_light.enabled = false;
			GetComponent<Renderer>().material = new_color;
			new_light.enabled = true;
		}
	}
}
