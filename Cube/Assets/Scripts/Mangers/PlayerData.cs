// Class of user defined data type to store player progress and menu setiing in game to a binary file
// implementation of saving progress in SaveSystem class
[System.Serializable]
public class PlayerData
{
	public int highScore;
	public int coin;
	public int distance;
	public int mostCoins;
	public float music;
	public float vfx;
	public int sensitivity;
	public string mode;
	
	public PlayerData(int highScore, int coin, int distnace, int mostCoins, float music = 0, float vfx = 0, int sensitivity = 0, string mode = null )
	{
		this.highScore = highScore;
		this.coin = coin;
		this.distance = distnace;
		this.mostCoins = mostCoins;
		this.music = music;
		this.vfx = vfx;
		this.sensitivity = sensitivity;
		this.mode = mode;
	}
}
