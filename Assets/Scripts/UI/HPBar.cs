using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class HPBar : MonoBehaviour
{
	[SerializeField]
	GameObject owner;
	[SerializeField]
	Transform barFiller;
	[SerializeField]
	TextMeshProUGUI barText;

	private void Awake()
	{
		EventBus<HPUpdatedEventArgs>.Subscribe(OnHPUpdated);
		EventBus<GameStateChangedEventArgs>.Subscribe(OnGameStateChanged);
	}
	private void OnDestroy()
	{
		EventBus<HPUpdatedEventArgs>.Unsubscribe(OnHPUpdated);
		EventBus<GameStateChangedEventArgs>.Unsubscribe(OnGameStateChanged);
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

	private void OnHPUpdated(HPUpdatedEventArgs obj)
	{
		if (obj.Owner == owner)
		{
			if (obj.MaxHP == 0)
			{
				throw new Exception($"Максимальное здоровье {obj.Owner.name} не может быть 0!");
			}
			float ratio = obj.NewHP / (float)obj.MaxHP;
			barFiller.localScale = new Vector3(ratio, 1, 1);
			if (barText != null)
			{
				barText.text = $"{obj.NewHP}/{obj.MaxHP}";
			}
		}
	}
}
