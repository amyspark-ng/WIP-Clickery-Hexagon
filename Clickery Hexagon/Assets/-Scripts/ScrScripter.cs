using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// # Script that controls the general behaviour of the game
// I kinda regret deciding to have everything in only 1 script LOL
public class ScrScripter : MonoBehaviour
{
	
	#region VariablesStuff

	[Header("IMPORTANT STUFF")]
	// # Variables
	[SerializeField] public NewgroundsManager ngMan;
	[SerializeField] public ThirdScript thirdScriptRef;
	[SerializeField] GameObject HexagonObj;
	[SerializeField] Animator HexAnimator; 

	[SerializeField] GameObject StatsBoardCanvas;
	[SerializeField] public TMP_Text ScoreText;
	[SerializeField] public TMP_Text DebugText;

	// white, red, orange, yellow, green, blue, purple, black, pfp
	[SerializeField] Color[] selectableColors;

	[Header("Else stuff")]

	// Important
	public int ClickScore;
	public int TimePlayed;
	// Very important

	int circleColorIndex;
	
	[HideInInspector] public bool[] gotMedal;
	bool StatsBoardCanvasOpen;
	[SerializeField] public bool canChangeColor;
	[SerializeField] bool canSetToPicture;
	public bool Clickable;
	public bool isDone;

	float SaveTimer;

	#endregion

	#region UnityEvents
	
	// # Start is called when the script gets called
	void Start() {
		StartCoroutine(thirdScriptRef.Start_Scripter());
	}

	// # Update is called once per frame
	void Update()
	{

		// // DEBUG STUFF
		// // delete data
		// if (Input.GetKeyDown(KeyCode.LeftControl)) {
		// 	SaveManager.DataDeleter();
		// }

		// // save data
		// if (Input.GetKeyDown(KeyCode.RightControl)) {
		// 	SaveManager.DataSaver(this);
		// 	ngMan.SubmitBothScores();
		// }

		// // load data
		// if (Input.GetKeyDown(KeyCode.LeftAlt)) {
		// 	thirdScriptRef.SaveLoader();
		// }

		// # Very important
		HexagonClickStuff();
		StatsBoardCheck();
		ngMan.Medal_ConditionChecks();

		// # Timer stuff 
		SaveTimer += Time.deltaTime;
		TimePlayed++;

		if (SaveTimer >= 20f) {
			SaveTimer = 0;
			SaveManager.DataSaver(this);
		}

		// # Color stuff
		if (canChangeColor) {
			if (Input.GetKeyDown(KeyCode.RightArrow)) {
				circleColorIndex++;

				if (!canSetToPicture) {
					Medal_SwitchConditions(1);
				}

				else {
					Medal_SwitchConditions(2);
				}
			}

			else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
				circleColorIndex--;

				if (!canSetToPicture) {
					Medal_SwitchConditions(1);
				}

				else {
					Medal_SwitchConditions(2);
				}
			}
		}

		// # Application focus stuff
		if (!Application.isFocused) {
			// Debug.Log("Application has lost focus");
		}

		// # Else stuff???

		// Score text
		ScoreText.text = ClickScore + "";
	}

	#endregion

	#region SomeFunctions
	
	// # Function that checks all the clicks and does the stuff
	public void HexagonClickStuff() {
		if (Input.GetKeyDown(KeyCode.Space) && !ngMan.LogOutCanvas.gameObject.activeSelf) {
			ClickScore++;
			HexAnimator.SetTrigger("ClickTrig");
		}

		if (Input.GetMouseButtonDown(0) && Clickable) {
			ClickScore++;
			HexAnimator.SetTrigger("ClickTrig");
			Debug.Log("CLICKED");
		}
	}

	// # Functions to check if statsboard should open and method to do stuff with it
	void StatsBoardCheck() {
		if (Input.GetKeyDown(KeyCode.Return) && StatsBoardCanvasOpen == false) {
			StatsBoardManager(true);
		}

		else if ( (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape)) && StatsBoardCanvasOpen == true) {
			StatsBoardManager(false);
		}
	}

	void StatsBoardManager(bool StateOfBoard) {
		
		if (StateOfBoard) {
 			StatsBoardCanvasOpen = true;

			Debug.Log("The stats board state is " + StatsBoardCanvasOpen);

			// TODO: Make this actually work lmao

			// string[] usersOnLeaderBoard = new string[];
			
			// int[] scoreOfUsers;
			// StatsBoardCanvas.SetActive(true);		
			// for (int i = 0; i > scoreOfUsers.Length; i++) {
			// 	usersOnLeaderBoard = ngMan.GetScoreBoardScores(1);
			// } 
		
			// TODO: Do the objects on this thing lol
			// pass by index the container that has all the text elements
			// for (int i = 0; i > StatsBoardCanvas.transform.GetChild(1).childCount; i++) {
			// 	StatsBoardCanvas.transform.GetChild(1).GetChild(i).GetComponent<TextMeshPro>().text = usersOnLeaderBoard[i];
			// }
		
			// triggeranim of opening
		}

		else {
 			StatsBoardCanvasOpen = false;

			Debug.Log("The stats board state is " + StatsBoardCanvasOpen);

			// trigger "ending" animation

			// after animation ends setactive false to the canvas
		}
	}	

	// # Function that manages the color in the hexagon
	void Medal_SwitchConditions(int index) {

		if (index == 1) {
			
			if (circleColorIndex > 7) {
				circleColorIndex = 0;
			}

			else if (circleColorIndex < 0) {
				circleColorIndex = 7;
			} 
			
			switch (circleColorIndex) {
				
				// white
				case 0:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[0];
				break;
				
				// red
				case 1:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[1];
				break;
				
				// orange
				case 2:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[2];
				break;

				// yellow
				case 3:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[3];
				break;
				
				// green
				case 4:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[4];
				break;

				// blue
				case 5:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[5];
				break;

				// purple
				case 6:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[6];
				break;

				// black
				case 7:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[7];
				break;
			}
		}
	
		else {
					
			if (circleColorIndex > 8) {
				circleColorIndex = 0;
			}

			else if (circleColorIndex < 0) {
				circleColorIndex = 8;
			} 
			
			switch (circleColorIndex) {

				// white
				case 0:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[0];
				break;
				
				// red
				case 1:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[1];
				break;
				
				// orange
				case 2:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[2];
				break;

				// yellow
				case 3:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[3];
				break;
				
				// green
				case 4:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[4];
				break;

				// blue
				case 5:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[5];
				break;

				// purple
				case 6:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[6];
				break;

				// black
				case 7:
				HexagonObj.GetComponent<SpriteRenderer>().color = selectableColors[7];
				break;

				// pfp
				case 8:
				Debug.Log("Went to 8");
				// HexagonObj.GetComponent<SpriteRenderer>().sprite = blah blah blah idk how will i do this;
				break;
			}
		}
	}

	#endregion

} // END OF MAIN
