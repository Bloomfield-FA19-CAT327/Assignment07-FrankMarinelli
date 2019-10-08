using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public Text uiText;
	public int scoreCount;
	public int lives;
	public float currentTime;
	public int level;
	public int steps;

	private void Awake() {
		Debug.Log("PDP: " + Application.persistentDataPath);
			if(PlayerPrefs.HasKey("score")) {
			RestorePlayerValues();
			Debug.Log("Current Level: " + level);
			Debug.Log("SceneManager.GetActiveScene().buildIndex: " + SceneManager.GetActiveScene().buildIndex);
			if(level!=0 && level != SceneManager.GetActiveScene().buildIndex) {
				SceneManager.LoadScene(level);
			}
		} else {
			NewGame();
		}
	}
	private void Start() {
		uiText = GameObject.Find("HudText").GetComponent<Text>();
	}
	private void Update() {
		if (Input.GetButtonDown("Clear")) {
			Debug.LogWarning("Erasing PlayerPrefs().");
			DeletePlayerValues();
		}
		string hudInfo = "";
		currentTime += Time.deltaTime;
		hudInfo += "Level: " + (level + 1) + "\n";
		hudInfo += "Score: " + scoreCount + "\n";
		hudInfo += "Lives: " + lives + "\n";
		hudInfo += "Your Time: " + currentTime.ToString("F2");
		hudInfo += "Steps: " + steps + "\n";
		uiText.text = hudInfo;
	}

	public void NewGame() {
		if (PlayerPrefs.HasKey("score")) {
			DeletePlayerValues();
		}
		scoreCount = 0;
		currentTime = 0;
		lives = 5;
		level = 0;
		steps = 12;
		StorePlayerValues();
	}
	public void LevelProgress() {
		level = SceneManager.GetActiveScene().buildIndex + 1;
		LoadNextLevel();
	}
	public void LevelNoProgress() {
		level = SceneManager.GetActiveScene().buildIndex - 1;
		LoadNextLevel();
	}
	public void LoadNextLevel() {
		StorePlayerValues();
		SceneManager.LoadScene(level);
	}
	public void Collectable() {
		scoreCount += 550;
	}
	void StorePlayerValues() {
		PlayerPrefs.SetInt("score", scoreCount);
		PlayerPrefs.SetInt("lives", lives);
		PlayerPrefs.SetFloat("currentTime", currentTime);
		PlayerPrefs.SetInt("level", level);
		PlayerPrefs.SetInt("steps", steps);
		PlayerPrefs.Save();
	}
	void RestorePlayerValues() {
		scoreCount = PlayerPrefs.GetInt("score");
		lives = PlayerPrefs.GetInt("lives");
		currentTime = PlayerPrefs.GetFloat("currentTime");
		level = PlayerPrefs.GetInt("level");
		steps = PlayerPrefs.GetInt("steps");
	}
	void DeletePlayerValues() {
		PlayerPrefs.DeleteKey("score");
		PlayerPrefs.DeleteKey("lives");
		PlayerPrefs.DeleteKey("currentTime");
		PlayerPrefs.DeleteKey("level");
		PlayerPrefs.DeleteKey("steps");
	}
}
