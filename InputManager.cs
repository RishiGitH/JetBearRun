using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
	public bool useTouch = false;				//Use touch based controls
	
	public LayerMask mask = -1;					//Set input layer mask

	RaycastHit2D hit;							//The hit raycast
	Vector3 inputPos;							//The position of the input
	Transform button;							//The triggered button
	
	//Called at every frame
	void Update () 
	{
		if (useTouch)
			GetTouches();
		else
			GetClicks();
	}
	//If playing with mouse
	void GetClicks()
	{
		//If we pressed the mouse
		if(Input.GetMouseButtonDown(0))
		{
			//Get the position of the input
			inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			inputPos.z = 0;

			//Cast a ray
            hit = Physics2D.Raycast(inputPos, new Vector2(0, 0), 0.1f, mask);

			//If the ray hit something in the set layer
			if (hit.collider != null)
			{
				//Register it, and send it to the GUI manager
				button = hit.transform;
				GUIManager.Instance.ButtonDown(button);
			}
			//If the ray didn't hit a GUI object
			else
			{
				//Set the button to null, and move the sub up
				button = null;
                PlayerManager.Instance.MoveUp();
			}
		}
		//If the click was released
		else if (Input.GetMouseButtonUp(0))
		{
			//If there is no button registered previousely
			if (button == null)
				//Move the sub down
                PlayerManager.Instance.MoveDown();
			//If there is a button registered
			else
				//Send it to the GUI manager
                GUIManager.Instance.ButtonUp(button);
		}
		
		/*//Used in testing to reset the status
		if (Input.GetKey(KeyCode.P))
		{
			SaveManager.CreateData();
			MissionManager.Instance.ResetDataString();
		}*/
	}
	//If playing with touch screen
	void GetTouches()
	{
		//Loop through the touches
		foreach (Touch touch in Input.touches) 
		{
            //Get the position of the input
            inputPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            inputPos.z = 0;

			//If a touch has happened
            if (touch.phase == TouchPhase.Began && touch.phase != TouchPhase.Canceled)
			{
				//Cast a ray
                hit = Physics2D.Raycast(inputPos, new Vector2(0, 0), 0.1f, mask);
				
				//If the ray hit something in the set layer
                if (hit.collider != null)
				{
					//Register it, and send it to the GUI manager
					button = hit.transform;
                    GUIManager.Instance.ButtonDown(button);
				}
				//If the ray didn't hit a GUI object
				else
				{
					//Set the button to null, and move the sub up
					button = null;
                    PlayerManager.Instance.MoveUp();
				}
			}
			//If a touch has ended
			else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
			{
				//If there is no button registered previousely
				if (button == null)
					//Move the sub down
                    PlayerManager.Instance.MoveDown();
				//If there is a button registered
				else
					//Send it to the GUI manager
                    GUIManager.Instance.ButtonUp(button);
			}
		}
	}
}
