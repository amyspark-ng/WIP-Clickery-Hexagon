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
		StartCoroutine(scripterRef.Start_NG());
	}

	// # Update is called each frame
	void Update()
	{ 

		if (LogOutCanvas.activeSelf) {
			scripterRef.ScoreText.gameObject.SetActive(false);
			
			if (Input.GetKeyDown(KeyCode.Return) && !PlayerLoggedIn) {
				RequestLogin();
			}

			else if (Input.GetKeyDown(KeyCode.Escape)) {
				scripterRef.ScoreText.gameObject.SetActive(true);
				// trigger fadening animation for the object and after some time destroy it
				// yield return new WaitForSeconds(5.0f);
				// Destroy(LogOutCanvas);
			}
		}

		SubmitScoreTimer += Time.deltaTime;

		// submitting the scores every 30 seconds, always submitting the goddamn scores
		if (SubmitScoreTimer >= 30f) {
			SubmitScoreTimer = 0;
			SubmitBothScores();
		}	
	}

	#endregion

	#region DataLoader
	public void SaveLoader() {
		SaveData saveData = SaveManager.LoadData(); 

		scripterRef.ClickScore = saveData.ClickScore;
		scripterRef.TimePlayed = saveData.TimePlayed;
		
		for (int i = 0; i > scripterRef.gotMedal.Length; i++) {
			scripterRef.gotMedal[i] = saveData.gotMedal[i];
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
				Debug.Log("Player is not logged in");
				LogOutCanvas.SetActive(true);
			}
		});
	}

	// # Function that says what happens when you're logged in
	public void OnLoggedIn() {
		
		SaveLoader();
		
		SaveManager.DataSaver(scripterRef);

		// Creates a new player instance and sets it to the current one 
		io.newgrounds.objects.user player = NGcore.current_user;

		// Sets that the player is logged in
		PlayerLoggedIn = true;

		Debug.Log("Logged player is " + player.name + " And it's " + PlayerLoggedIn);
		
		SubmitBothScores();
	
		if (!scripterRef.gotMedal[0]) {
			scripterRef.gotMedal[0] = true;
			MedalChecks();
		}
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

	// # Function that says what happens when login fails
	void OnLoginFailed() {
		Debug.Log("Loggin error what");
	}

	// # Function that says what happens when login gets cancelled
	void OnLoginCancelled() {
		Debug.Log("Login got cancelled");
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

	public void MedalChecks() {
				
		if (scripterRef.gotMedal[0]) {
			UnlockMedal(medal_IDs[0]);
		}
		
		if (scripterRef.gotMedal[1]) {
			UnlockMedal(medal_IDs[1]);
		}
		
		if (scripterRef.gotMedal[2]) {
			UnlockMedal(medal_IDs[2]);
		}
		
		if (scripterRef.gotMedal[3]) {
			UnlockMedal(medal_IDs[3]);
		}
		
		if (scripterRef.gotMedal[4]) {
			UnlockMedal(medal_IDs[4]);
		}
		
		if (scripterRef.gotMedal[5]) {
			UnlockMedal(medal_IDs[5]);
		}

		if (scripterRef.gotMedal[6]) {
			UnlockMedal(medal_IDs[6]);
		}

		if (scripterRef.gotMedal[7]) {
			UnlockMedal(medal_IDs[7]);
		}

		if (scripterRef.gotMedal[8]) {
			UnlockMedal(medal_IDs[8]);
		}

		if (scripterRef.gotMedal[9]) {
			UnlockMedal(medal_IDs[9]);
		}

		if (scripterRef.gotMedal[10]) {
			UnlockMedal(medal_IDs[10]);
		}

		if (scripterRef.gotMedal[11]) {
			UnlockMedal(medal_IDs[11]);
		}

		if (scripterRef.gotMedal[12]) {
			UnlockMedal(medal_IDs[12]);
		}

		if (scripterRef.gotMedal[13]) {
			UnlockMedal(medal_IDs[13]);
		}
	}

	#endregion

	#region ScoreFunctions

	// # Not using this one but having in there just in case lol
	// # Method that submits the score taking the ScoreBoardID from newgrounds developer tools and the actual score value
	public void SubmitScore(int ScoreBoardID, int score) {
		
		// Creates the ScoreBoard.postScore component
		io.newgrounds.components.ScoreBoard.postScore submit_score = new io.newgrounds.components.ScoreBoard.postScore();
		
		// Sets the submitScoreID to the one in your project
		submit_score.id = ScoreBoardID;
		
		// Sets the value to send to the server to the one in your game
		submit_score.value = score;
		
		// Calls the server with the core and makes these changes
		submit_score.callWith(NGcore);
	}

	// # Function to submit both scores
	public void SubmitBothScores() {
		Debug.Log("NewgroundsManager | Scores got submitted");
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
	
		SaveManager.DataSaver(scripterRef);

	}

	// # Function to get the #5 best scores from scoreboard
/* 	public int[] GetScoreBoardScores() {
		io.newgrounds.objects.score stringr = Object.getGlobalScores("A", 5, 0); 

		return scores;

		Debug.Log(stringr[0]);
	
		return scores;
	}
 */

	// # Function to get the #5 names from scoreboard
/* 	public string[] GetScoreBoardNames() {
		io.newgrounds.objects.score stringr = Object.getGlobalScores("A", 5, 0); 

		return scores;

		Debug.Log(stringr[0]);
	
		return names;
	}
*/
	#endregion
	
} // END OF MAIN