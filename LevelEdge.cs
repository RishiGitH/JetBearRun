using UnityEngine;
using System.Collections;

public class LevelEdge : MonoBehaviour 
{
	//Called when something triggerer the object
	void OnTriggerEnter2D(Collider2D other) 
	{
		//If a spawn triggerer is collided with this
		if (other.name == "SpawnTriggerer")
		{
			//Spawn a proper object
			switch (other.tag)
			{
				case "SecondLayer":
                    LevelGenerator.Instance.GenerateSecondLayer(0);
					break;
				
				case "ThirdLayer":
					LevelGenerator.Instance.GenerateThirdLayer(0);
					break;
					
				case "FourthLayer":
                    LevelGenerator.Instance.GenerateFourthLayer();
					break;
				
				case "Obstacles":
                    LevelGenerator.Instance.GenerateObstacles();
					break;
			}

            other.gameObject.SetActive(false);
		}
		//If a reset triggerer is collided with this
		else if (other.name == "ResetTriggerer")
		{
			//Reset the proper object
			switch (other.tag)
			{
				case "SecondLayer":
				case "ThirdLayer":
				case "FourthLayer":
                    LevelGenerator.Instance.SleepGameObject(other.transform.parent.gameObject);
					break;
				
				case "Obstacles":
					other.transform.parent.GetComponent<Obstacles>().DeactivateChild();
                    LevelGenerator.Instance.SleepGameObject(other.transform.parent.gameObject);
					break;
			}
		}
		//If a power up is collided with this
		else if (other.tag == "PowerUps")
		{
			//Reset the power up
			other.GetComponent<PowerUp>().ResetThis(2);
		}
		//If a torpedo is collided with this
		else if (other.name == "Torpedo")
		{
			//Reset the torpedo
			other.transform.parent.gameObject.GetComponent<Torpedo>().ResetThis();
		}
	}
}
