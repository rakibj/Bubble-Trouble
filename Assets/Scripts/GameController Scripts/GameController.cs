using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class GameController : MonoBehaviour {

	public static GameController instance;
	private GameData data;
	public bool isGameStartedFirstTime;
	public bool isMusicOn;
	public int coins;
	public int highScore;



	// Use this for initialization
	void Awake () {
		MakeSingleton ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void MakeSingleton(){
		if (instance != null) {
			Destroy (gameObject);
		}else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

	}

	public void Save() {

		FileStream file = null;

		try {

			BinaryFormatter bf = new BinaryFormatter();

			file = File.Create(Application.persistentDataPath + "/GameData.dat");

			if(data != null) {

				data.setHighScore(highScore);
				data.setCoins(coins);
				data.setIsGameStartedFirstTime(isGameStartedFirstTime);
				data.setIsMusicOn(isMusicOn);

				bf.Serialize(file, data);

			}

		} catch(Exception e) {

		} finally {
			if(file != null) {
				file.Close();
			}
		}

	} // save data

	public void Load() {

		FileStream file = null;

		try {

			BinaryFormatter bf = new BinaryFormatter();

			file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);

			data = (GameData)bf.Deserialize(file);

		} catch(Exception e) {

		} finally {
			if(file != null) {
				file.Close();
			}
		}
	} // load data
}


class GameData {

	private bool isGameStartedFirstTime;

	private bool isMusicOn;

	private int coins;
	private int highScore;

	public void setIsGameStartedFirstTime(bool isGameStartedFirstTime) {
		this.isGameStartedFirstTime = isGameStartedFirstTime;
	}

	public bool getIsGameStartedFirstTime() {
		return this.isGameStartedFirstTime;
	}

	public void setIsMusicOn(bool isMusicOn) {
		this.isMusicOn = isMusicOn;
	}

	public bool getIsMusicOn() {
		return this.isMusicOn;
	}

	public void setCoins(int coins) {
		this.coins = coins;
	}

	public int getCoins() {
		return this.coins;
	}

	public void setHighScore(int highScore) {
		this.highScore = highScore;
	}

	public int getHighScore() {
		return this.highScore;
	}

} // game data

