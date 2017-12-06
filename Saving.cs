using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveController : MonoBehaviour {
	//This system is good because you cannot find the file in the game folder 
	//and change variables from there to cheat

	public static SaveController saveController;

	//This is the variable from int the game,
	//Make a reference to the object with the 
	//script with the variables you want to
	//manage, or put all the variables you want to manage in here
	public int thisCanBeAnything;

	//Singleton manager
	private void Awake()
	{
		if(saveController == null)
		{
			DontDestroyOnLoad(gameObject);
			saveController = this;
		}
		else if (saveController != this)
		{
			Destroy(gameObject);
		}
	}

	public void Save()
	{
		// creates a binary formatter &  a file;
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");//Does not have to be .dat, but is what I went with
	
		// creates a object to save the data to
		PlayerData data = new PlayerData();
		
		data.thisCanBeAnything = thisCanBeAnything;

		// writes the object to the file and closes it
		bf.Serialize(file, data);
		file.Close();
	}

	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			thisCanBeAnything = data.thisCanBeAnything;
		}
	}

	public void Delete()
	{
		if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			File.Delete(Application.persistentDataPath + "/playerInfo.dat");
		}
	}

}


[Serializable]
class PlayerData
{
	//Variables that will be in the created file
	public int thisCanBeAnything;
}