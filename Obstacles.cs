using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacles : MonoBehaviour 
{
	public List<GameObject> elements		= new List<GameObject>();		//A list that holds the obstacle childs
	
	//Called at the beginning of the game
	void Start()
	{
		//Loop through children
		foreach (Transform child in transform)
		{
			//If the child is not a triggerer
            if (child.name != "SpawnTriggerer" && child.name != "ResetTriggerer")
            {
                //Add it to the elements list, and deactivate it
                elements.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }
		}
	}
	//Called when the obstacle is reseting
	public void DeactivateChild()
	{
		//Loop through the elements list
		foreach (GameObject child in elements)
		{			
			//Look for the child's explosion
			Transform go = child.transform.FindChild("ExplosionParticle");
			
			//If we found it
			if (go != null)
				//Stop the particle
				go.GetComponent<ParticleSystem>().Stop();

			//Deactivate the child
            child.SetActive(false);
		}
	}
	//Called when the obstacle is spawned
	public void ActivateChild()
	{
		//Loop through the elements array
		foreach (GameObject child in elements)
		{
			//Active the child's renderer and collider
            child.SetActive(true);
			child.GetComponent<Renderer>().enabled = true;
			child.GetComponent<Collider2D>().enabled = true;
		}
	}
}
