using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
	public static void SaveTeam(Boid[] boids)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/boid.bin";
		FileStream stream = new FileStream(path, FileMode.Create);

		TeamData data = new TeamData(boids);

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
