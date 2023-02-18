using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject cameraShake;
	public GameObject player;
	public int score = 0;
	public float timeRemaining = 0;

	CameraShake camShakeScript;
	UiController uiControllerScript;
    private void Start()
    {
		camShakeScript = cameraShake.GetComponent<CameraShake>();
		var duration = PlayerPrefs.GetInt("duration");
		timeRemaining = duration;
		StartCoroutine(EndGameByTime(duration));
	}
    private void Update()
    {
		timeRemaining -= Time.deltaTime;
	}

	public void ShakeCamera(float intensity = 5f, float timing = 0.5f)
    {
		camShakeScript.startShake(intensity, timing);
	}

	public void NextLevel(string levelName)
	{
		StartCoroutine(NextLevelCoroutine(levelName));
	}
	IEnumerator NextLevelCoroutine(string levelName)
	{
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(levelName);
	}

	public void FinishGame()
	{
		StartCoroutine(EndGameCoroutine());
	}
	public void AddToScore(int value)
    {
		score += value;
    }

	IEnumerator EndGameCoroutine()
	{
        PlayerPrefs.SetInt("score", score);
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene("GameOver");
	}
	IEnumerator EndGameByTime(float duration)
	{
		yield return new WaitForSeconds(duration);
        PlayerPrefs.SetInt("score", score);
		SceneManager.LoadScene("GameOver");
	}
}
