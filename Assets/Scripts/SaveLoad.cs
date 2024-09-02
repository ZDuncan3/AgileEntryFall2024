using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class SaveLoad
{
	public static void Save(string fileName)
	{
		BinaryFormatter bf = new BinaryFormatter();
		string path = Application.persistentDataPath + "/" + fileName + ".sav";

		try
		{
			FileStream fs = new FileStream(path, FileMode.Create);
			GameData data = new GameData();
			bf.Serialize(fs, data);
			fs.Close();
			Debug.Log($"Saving to: {path}");
			PlayerPrefs.Save();
		}
		catch (Exception exc)
		{
			Debug.LogError($"Unable to save to: {path}\n{exc}");
		}
	}

	public static GameData Load(string fileName)
	{
		string path = Application.persistentDataPath + "/" + fileName + ".sav";

		if (File.Exists(path))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fs = new FileStream(path, FileMode.Open);
			GameData data = (GameData)bf.Deserialize(fs);
			GameObject tempCheckpointObj = new GameObject("tempCheckpoint");
			Checkpoint tempCheckpoint = tempCheckpointObj.AddComponent<Checkpoint>();
			tempCheckpoint.spawnPosition = data.lastCheckpointPosition.GetVector;
			tempCheckpoint.spawnRotation = data.lastCheckpointRotation.GetQuaternion;
			CheckpointManager.instance.currentCheckpoint = tempCheckpoint;
			//CheckpointManager.instance.currentCheckpoint = new Checkpoint(data.lastCheckpointPosition.GetVector, data.lastCheckpointRotation.GetQuaternion);
			fs.Close();

			Debug.Log($"Loading save from: {path}");

			return data;
		}

		return null;
	}

	public static void DeleteSave(string fileName)
	{
		string path = Application.persistentDataPath + "/" + fileName + ".sav";
		Debug.Log($"Deleting file: {path}");
		File.Delete(path);
		PlayerPrefs.Save();
	}
}