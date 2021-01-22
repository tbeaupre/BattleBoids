using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
	public static void SaveTeamJson(TeamData data)
	{
		string jsonData = JsonUtility.ToJson(data, true);
		System.IO.File.WriteAllText(
			Application.persistentDataPath + "/enemy.json", jsonData);
	}

	public static EnemyTeamData LoadEnemyTeamJson(int level)
	{
		string path = Application.persistentDataPath + "/" + level + ".json";
		if (File.Exists(path)) {
			string jsonString = File.ReadAllText(path);
			EnemyTeamData data = JsonUtility.FromJson<EnemyTeamData>(jsonString);
			return data;
		} else {
			Debug.LogError("File not found at " + path);
			return null;
		}
	}

	public static void SaveTeam(TeamData data)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/boid.bin";
		FileStream stream = new FileStream(path, FileMode.Create);

		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static TeamData LoadTeam()
	{
		string path = Application.persistentDataPath + "/boid.bin";
		if (File.Exists(path)) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			TeamData data = formatter.Deserialize(stream) as TeamData;
			stream.Close();

			return data;
		} else {
			Debug.LogError("Save file not found in " + path);
			return null;
		}
	}
}
