using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// # SCRIPT THAT CONTROLS ALL THE NEWGROUNDS THINGS ON THE PROJECT #
public class NewgroundsManager : MonoBehaviour
{

	#region NewgroundsVariablesStuff

	[Header("IMPORTANT NG STUFF")]
	public io.newgrounds.core NGcore;
	[SerializeField] ScrScripter scripterRef;
	[SerializeField] int[] medal_IDs;
	// IDS of both scoreboards
	int ScoreBoardID = 11612;
	int TimeBoardID = 11613;
	// int medalsUnlocked = 0;

	// Is the player logged in? 
	public bool PlayerLoggedIn;
	
	// Time it takes to submit both scores
	float SubmitScoreTimer;

	[Header("Else stuff")]
	[SerializeField] public GameObject LogOutCanvas;

	#endregion
	
	// # ALL THE FUNCTIONS HERE LOL

	#region UnityEvents

	// # Start is called when the script gets called
	void Start()
	{
		StartCoroutine(scripterRef.thirdScriptRef.Start_NG());
	}

	// # Update is called each frame
	void Update()
	{
		// Timer for submitting scores
		SubmitScoreTimer += Time.deltaTime;

		// submitting the scores every 30 seconds, always submitting the goddamn scores
		if (SubmitScoreTimer >= 30f) {
			SubmitScoreTimer = 0;
			SubmitBothScores();
		}	

		// # stuff for the log out canvas
		if (Input.GetKeyDown(KeyCode.Return) && !PlayerLoggedIn && LogOutCanvas.activeSelf) {
			RequestLogin();
		}

		else if (Input.GetKeyDown(KeyCode.Escape)) {
			scripterRef.ScoreText.gameObject.SetActive(true);
			LogOutCanvas.SetActive(false);
			// trigger fadening animation for the object and after some time destroy it
			// yield return new WaitForSeconds(5.0f);
			// Destroy(LogOutCanvas);
		}
	}

	#endregion
	
	#region LoginFunctions

	// # Function that checks if the player is logged in using NGcore built-in checkLogin method
	public void ChecksLogin() {
		NGcore.checkLogin((bool logged_in) => {
			
			if (logged_in) {
				OnLoggedIn();
			}

			else {
				OnNOTLoggedIn();
			}
		});
	}

	// # Requests login using newgrounds passport 
	public void RequestLogin() {
		
		/* Parameters;
		1: The function that gets called when there's a new session ID
		2: The function that calls when it fails on create a new session ID
		3: The function that calls when the user cancels the login
		*/

		NGcore.requestLogin(OnLoggedIn, OnLoginFailed, OnLoginCancelled);
	}

	// # Function that says what happens when you're logged in
	public void OnLoggedIn() {
		if (LogOutCanvas.gameObject.activeSelf) {
			// TODO: then do the fade out anim and do some sort of stuff like transparency background and thingies
		}
	
		scripterRef.thirdScriptRef.SaveLoader();
		
		SaveManager.DataSaver(scripterRef);
		scripterRef.DebugText.text = "Data Got Saved";

		// Creates a new player instance and sets it to the current one 
		io.newgrounds.objects.user player = NGcore.current_user;

		// Sets that the player is logged in
		PlayerLoggedIn = true;

		Debug.Log("Logged player is " + player.name + " And it's " + PlayerLoggedIn);
		
		SubmitBothScores();
	}

	// # Function that says what happens when login fails
	void OnLoginFailed() {
		Debug.Log("Loggin error what");
	}

	// # Function that says what happens when login gets cancelled
	void OnLoginCancelled() {
		Debug.Log("Login got cancelled");
	}

	// # Function that says what happens when you're not logged in
	void OnNOTLoggedIn() {
		Debug.Log("Player is not logged in");
		LogOutCanvas.SetActive(true);
	}

	#endregion 

	#region MedalFunctions 

	// # Method that unlocks medal taking the medal id as a parameter
	public void UnlockMedal(int medalID) {

		// Creates the unlock component
		io.newgrounds.components.Medal.unlock medalUnlock = new io.newgrounds.components.Medal.unlock();

		// Sets the medalUnlock.id to the one you want to unlock
		medalUnlock.id = medalID;
		
		// Unlocks the medal and calls to the core so these changes can be made
		medalUnlock.callWith(NGcore);
		
		// And fires this when everything up it's done
		OnMedalUnlocked();
	}

	// # Function that checks what happens when you unlock a medal
	public void OnMedalUnlocked() {
		// Play cool anim with stuff
		Debug.Log("MEDAL GOT UNLOCKED");
	}

	// # Function that constantly checks if you got a certin medal
	public void Medal_ConditionChecks() {
		
		// Player is logged in newgrounds (very cool)
		if (PlayerLoggedIn) {
			if (!scripterRef.gotMedal[0]) {
				scripterRef.gotMedal[0] = true;
				UnlockMedal(medal_IDs[0]);
			}
		}
		
		// If has over 1 score
		if (scripterRef.ClickScore >= 1) {			
			if (!scripterRef.gotMedal[1]) {
				scripterRef.gotMedal[1] = true;
				UnlockMedal(medal_IDs[1]);
				scripterRef.isDone = true;
			} 
		}

		// If has over 100 score
		if (scripterRef.ClickScore >= 100) {
			if (!scripterRef.gotMedal[2]) {
				scripterRef.gotMedal[2] = true;
				UnlockMedal(medal_IDs[2]);
				scripterRef.isDone = true;
				scripterRef.canChangeColor = true;
			}
		}
		
		// TODO: MAKE THIS ACTUALLY WORK VERY IMPORTANT and set them using the scripterRef thingy

		// else if (ClickScore == 1000 && !isDone) {
		// 	if (!scripterRef.gotMedal[3]) {
		// 		scripterRef.gotMedal[3] = true;
		// 		ngMan.MedalChecks();
		// 		isDone = true;
				
		// 		if (PlayerLoggedIn) {
		// 			canSetToPicture = true;
		// 		}
		
		// 	}
		// }

		// else if (ClickScore == 2000 && !isDone) {
		// 	if (!scripterRef.gotMedal[4]) {
		// 		scripterRef.gotMedal[4] = true;
		// 		ngMan.MedalChecks();
		// 		isDone = true;
		// 	}
		// }

		// else if (ClickScore == 6000 && !isDone) {
		// 	if (!scripterRef.gotMedal[5]) {
		// 		scripterRef.gotMedal[5] = true;
		// 		ngMan.MedalChecks();
		// 		isDone = true;
		// 	}
		// }
	}

	#endregion

	#region ScoreFunctions

	// # Function to submit both scores
	public void SubmitBothScores() {
		// Creates the ScoreBoard.postScore component
		io.newgrounds.components.ScoreBoard.postScore submit_score = new io.newgrounds.components.ScoreBoard.postScore();
		io.newgrounds.components.ScoreBoard.postScore submit_time = new io.newgrounds.components.ScoreBoard.postScore();
		
		// Sets the submitScoreID to the one in your project
		submit_score.id = ScoreBoardID;
		submit_time.id = TimeBoardID;
		
		// Sets the value to send to the server to the one in your game
		submit_score.value = scripterRef.ClickScore;
		submit_time.value = scripterRef.TimePlayed;
		
		// Calls the server with the core and makes these changes
		submit_score.callWith(NGcore);
		submit_time.callWith(NGcore);
	
		Debug.Log("NewgroundsManager | Scores got submitted");
		scripterRef.DebugText.text = "Data Got Saved";
	}

	// TODO: Make this work too lol
	// # Function to get the #5 best scores from scoreboard
	public int[] GetScoreBoardScores() {
		// io.newgrounds.objects.score stringr = Object.getGlobalScores("A", 5, 0); 

		// return scores;

		// Debug.Log(stringr[0]);
	
		// return scores;
	
		return new int[0];
	}

	// # Function to get the #5 names from scoreboard
	public string[] GetScoreBoardNames() {
		// io.newgrounds.objects.score stringr = Object.getGlobalScores("A", 5, 0); 

		// return scores;

		// Debug.Log(stringr[0]);

		// return names;
	
		return new string[4];
	}

	#endregion

} // END OF MAIN