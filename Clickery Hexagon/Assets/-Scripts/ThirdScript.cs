using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// # Script i made to sort some stuff i couldn't figure uot where to put #
public class ThirdScript : MonoBehaviour
{

	[SerializeField] ScrScripter scripterRef;

	// # Loader of the save thing
	public void SaveLoader() {
		SaveData saveData = SaveManager.LoadData(); 

		scripterRef.ClickScore = saveData.ClickScore;
		scripterRef.TimePlayed = saveData.TimePlayed;
		
		for (int i = 0; i > scripterRef.gotMedal.Length; i++) {
			scripterRef.gotMedal[i] = saveData.gotMedal[i];
		} 
	}

	/* CHECKED: Check if the Start funcions are called AFTER the splash, if yes then delete this IEnumerators, if not then keep them
	yes, they are indeed called after the start
	still will keep them because im lazy to change them */

	// # Function that gets called when the game starts AND after the splash anim 
	public IEnumerator Start_Scripter() {
		
		// then open the black thingy upwards

		// then start doing the important stuff (which you dumb idiot) 

		yield return 0;

	}

	// # Coroutine that checks and sets the ng stuff after my splash is done
	public IEnumerator Start_NG() {

		// # Checks when the NGcore is ready
		scripterRef.ngMan.NGcore.onReady(() => {
			scripterRef.ngMan.ChecksLogin();			
		});

		yield return 0;
	}

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
		submit_score.callWith(scripterRef.ngMan.NGcore);
	}

} // END OF MAIN