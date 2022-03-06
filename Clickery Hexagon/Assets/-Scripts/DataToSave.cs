using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
	public int ClickScore;
	public int TimePlayed;
	public bool[] gotMedal = new bool[14];
	
	public SaveData(ScrScripter scripterInfo) {
		ClickScore = scripterInfo.ClickScore;
		TimePlayed = scripterInfo.TimePlayed;
				
		for (int i = 0; i < gotMedal.Length; i++) {
			gotMedal[i] = scripterInfo.gotMedal[i];
		}
	}
}