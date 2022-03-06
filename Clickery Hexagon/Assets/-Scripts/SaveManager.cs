using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public static class SaveManager
{
	
	static string dataPath = Application.persistentDataPath + "/data.save";

	public static void DataSaver(ScrScripter scriptData) {
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream filestream = new FileStream(dataPath, FileMode.Create);
		
		SaveData scripterThing = new SaveData(scriptData);
		
		formatter.Serialize(filestream, scripterThing);
		filestream.Close();

		Debug.Log("SCRIPTER DATA | Data got saved");
	}

	public static SaveData LoadData() {
		if (File.Exists(dataPath)) {
			
			Debug.Log("SCRIPTER DATA | Data got loaded");

			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream filestream = new FileStream(dataPath, FileMode.Open);
			
			SaveData savedData = binaryFormatter.Deserialize(filestream) as SaveData;
			filestream.Close();
			
			return savedData;
		}

		else {
			Debug.Log("SCRIPTER DATA | There was no saved data found");
			return null;
		}
	}

}