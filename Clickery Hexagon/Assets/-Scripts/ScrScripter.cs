using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// # Script that controls the general behaviour of the game
public class ScrScripter : MonoBehaviour
{
	// # Variables
	[SerializeField] NewgroundsManager ngMan;
	[SerializeField] GameObject StatsBoardCanvas;
	[SerializeField] Animator HexAnimator; 
	[SerializeField] public TMP_Text ScoreText;
	public int ClickScore;
	public int TimePlayed;
	public bool[] gotMedal;
	public bool Clickable;
	bool StatsBoardCanvasOpen = false;
	bool isDone = false;
	float SaveTimer;

	#region UnityEvents
	
	// Update is called once per frame
	void Update()
	{
		// # Very important
		HexagonClickStuff();
		StatsBoardCheck();

		SaveTimer += Time.deltaTime;

		if (SaveTimer >= 15f) {
			SaveManager.DataSaver(this);
		}

		TimePlayed++;
		ScoreText.text = ClickScore + "";
	
		#region MedalChecks lol

		if (ClickScore == 1 && !isDone) {			
			if (!gotMedal[1]) {
				gotMedal[1] = true;
				ngMan.MedalChecks();
				isDone = true;
			} 
		}
		
		#endregion
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

	// # Coroutines that does all the little things and timing when the game starts
	public IEnumerator Start_NG() {

		// wait for the amount of seconds both things take the unity splash and my animation

		// # Checks when the NGcore is ready
		ngMan.NGcore.onReady(() => {
			ngMan.ChecksLogin();			
		});

		yield return 0;
	}
	IEnumerator Start_Scripter() {

		// then open the black thingy upwards

		// then start doing the important stuff 

		yield return 0;
	}

	// Function to check if statsboard should open and method to do stuff with it
	void StatsBoardCheck() {
		if (Input.GetKeyDown(KeyCode.RightArrow) && StatsBoardCanvasOpen == false) {
			StatsBoardManager(true);
		}

		else if ( (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Escape)) && StatsBoardCanvasOpen == true) {
			StatsBoardManager(false);
		}
	}
	void StatsBoardManager(bool StateOfBoard) {
		
		if (StateOfBoard) {
 			StatsBoardCanvasOpen = true;

			Debug.Log("The stats board state is " + StatsBoardCanvasOpen);

			// string[] usersOnLeaderBoard = new string[];
			
			// int[] scoreOfUsers;
			// StatsBoardCanvas.SetActive(true);		
			// for (int i = 0; i > scoreOfUsers.Length; i++) {
			// 	usersOnLeaderBoard = ngMan.GetScoreBoardScores(1);
			// } 
		
			// pass by index the container that has all the text elements
			// for (int i = 0; i > StatsBoardCanvas.transform.GetChild(1).childCount; i++) {
			// 	StatsBoardCanvas.transform.GetChild(1).GetChild(i).GetComponent<TextMeshPro>().text = usersOnLeaderBoard[i];
			// }
		}

		else {
 			StatsBoardCanvasOpen = false;

			Debug.Log("The stats board state is " + StatsBoardCanvasOpen);

			// trigger "ending" animation

			// after animation ends setactive false to the canvas
		}
	}	

	// # Function that sets the clickable bool
	public void SetClickableBool(bool boolState) {
		if (boolState) {
			Clickable = true;
			Debug.Log("Clickbale is " + Clickable);
		}

		else {
			Clickable = false;
			Debug.Log("Clickbale is " + Clickable);
		}
	}

	#endregion

} // END OF MAIN
