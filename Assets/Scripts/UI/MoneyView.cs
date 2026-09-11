using System;
using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI moneyText;

	private void Awake()
	{
		EventBus<GameStateChangedEventArgs>.Subscribe(OnGameStateChanged);
	}

	private void OnGameStateChanged(GameStateChangedEventArgs obj)
	{
		if (obj.NewState == GameStateManager.State.Gameplay)
		{
			gameObject.SetActive(true);
		} else
		{
			gameObject.SetActive(false);
		}
	}

	private void OnDestroy()
	{
		EventBus<GameStateChangedEventArgs>.Unsubscribe(OnGameStateChanged);
	}

	private void OnEnable()
	{
		EventBus<MoneyChangedEventArgs>.Subscribe(OnMoneyChanged);
	}
	private void OnDisable()
	{
		EventBus<MoneyChangedEventArgs>.Unsubscribe(OnMoneyChanged);
	}

	private void OnMoneyChanged(MoneyChangedEventArgs obj)
	{
		moneyText.text = $"${obj.NewAmount}";
	}
}
