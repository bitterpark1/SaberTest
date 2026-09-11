using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
	[SerializeField]
	GameObject panel;
	[SerializeField]
	TextMeshProUGUI gameOverText;
	[SerializeField]
	TextMeshProUGUI damageDealt;
	[SerializeField]
	TextMeshProUGUI damageTaken;
	[SerializeField]
	TextMeshProUGUI enemiesKilled;
	[SerializeField]
	TextMeshProUGUI time;
	[SerializeField]
	TextMeshProUGUI shotsFired;
	[SerializeField]
	TextMeshProUGUI accuracy;

	[SerializeField]
	Button returnToMenu;

	private void Awake()
	{
		EventBus<ShowResultsEventArgs>.Subscribe(OnShowResults);
		returnToMenu.onClick.AddListener(ReturnToMenuPressed);
	}

	private void OnDestroy()
	{
		EventBus<ShowResultsEventArgs>.Unsubscribe(OnShowResults);
	}

	private void ReturnToMenuPressed()
	{
		EventBus<ReturnToMainMenuEventArgs>.Invoke(new ReturnToMainMenuEventArgs());
	}

	private void OnShowResults(ShowResultsEventArgs obj)
	{
		ShowResults(obj.PlayerWon, obj.Stats);
	}

	private void ShowResults(bool playerWon, SessionStats stats)
	{
		panel.SetActive(true);

		gameOverText.text = playerWon ? "YOU WIN!": "YOU DIED";

		damageDealt.text = $"Damage dealt: {stats.TotalDamageDealt }";
		damageTaken.text = $"Damage taken: {stats.TotalDamageTaken }";
		enemiesKilled.text = $"Enemies killed: {stats.EnemiesKilled}";
		time.text = $"Time survived: {TimeSpan.FromSeconds(stats.FinishTime - stats.StartTime).ToString(@"hh\:mm\:ss")}";
		shotsFired.text = $"Shots fired: {stats.ShotsFired }";
		if (stats.ShotsFired > 0)
		{
			var acc = Mathf.RoundToInt((float)stats.ShotsHit / stats.ShotsFired * 100);
			accuracy.text = $"Accuracy: {acc}%";
		}
		
	}

}
