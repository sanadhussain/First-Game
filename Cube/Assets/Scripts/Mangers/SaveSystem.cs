using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
	// Method to save progress of user 
	public static void savePlayer(int highScore = 0, int coin = 0, int distance = 0, int mostCoins = 0, float music = 0, float vfx = 0, int sensitivity = 0, string mode = null)
	{
		

		BinaryFormatter formatter = new BinaryFormatter();

		string path = Application.persistentDataPath + "/playerData.fun";
		PlayerData data = new PlayerData(highScore,coin,distance,mostCoins,music,vfx,sensitivity,mode);
		
		FileStream stream = new FileStream(path, FileMode.Create);
		formatter.Serialize(stream, data);
		stream.Close();


	}   
	// Mthod to load progress of user
	public static PlayerData loadData()
	{

		string path = Application.persistentDataPath + "/playerData.fun";
		BinaryFormatter formatter = new BinaryFormatter();
		
		if (File.Exists(path))
		{
			
			FileStream stream = new FileStream(path, FileMode.Open);

			PlayerData data = (PlayerData)formatter.Deserialize(stream);
			stream.Close();
			return data;
		}
		else
		{
			PlayerData data = new PlayerData(0, 0, 0, 0, 0.5f , 0.5f, 10, "Button");

			FileStream stream = new FileStream(path, FileMode.Create);
			formatter.Serialize(stream, data);
			stream.Close();
			return data;
		}
	}
}
