using System;
using System.Collections;
using UnityEngine;


public class DestroyOnGameStateChanged : MonoBehaviour
{
	[SerializeField]
	GameStateManager.State destroyOnState;
	
	private void Awake()
	{
		EventBus<GameStateChangedEventArgs>.Subscribe(OnGameStateChange);
	}
	private void OnDestroy()
	{
		EventBus<GameStateChangedEventArgs>.Unsubscribe(OnGameStateChange);
	}

	private void OnGameStateChange(GameStateChangedEventArgs obj)
	{
		if (obj.NewState == destroyOnState)
		{
			Destroy(gameObject);
		}
	}
}
