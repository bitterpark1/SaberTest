using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{

	public enum State
	{
		Menu,
		Gameplay,
		GameOverScreen
	}

	private void Awake()
	{
		EventBus<NewGamePressedEventArgs>.Subscribe(OnNewGamePressed);
	}

	private void OnDestroy()
	{
		EventBus<NewGamePressedEventArgs>.Unsubscribe(OnNewGamePressed);
		EventBus<GameOverEventArgs>.Unsubscribe(OnGameOver);
		EventBus<ReturnToMainMenuEventArgs>.Unsubscribe(OnReturnToMainMenuPressed);
	}

	private void Start()
	{
		EventBus<GameStateChangedEventArgs>.Invoke(new GameStateChangedEventArgs { NewState = State.Menu });
	}


	/// <summary>
	/// Стартуем новую игру из меню
	/// </summary>
	/// <param name="obj"></param>
	private void OnNewGamePressed(NewGamePressedEventArgs obj)
	{
		EventBus<NewGamePressedEventArgs>.Unsubscribe(OnNewGamePressed);
		EventBus<GameOverEventArgs>.Subscribe(OnGameOver);
		EventBus<ReturnToMainMenuEventArgs>.Subscribe(OnReturnToMainMenuPressed);
		EventBus<GameStateChangedEventArgs>.Invoke(new GameStateChangedEventArgs { NewState = State.Gameplay });
	}


	/// <summary>
	/// Переходим к экрану результатов после победы или поражения
	/// </summary>
	/// <param name="obj"></param>
	private void OnGameOver(GameOverEventArgs obj)
	{
		EventBus<GameOverEventArgs>.Unsubscribe(OnGameOver);
		EventBus<GameStateChangedEventArgs>.Invoke(new GameStateChangedEventArgs { NewState = State.GameOverScreen });
	}

	/// <summary>
	/// Возвращаемся в меню после экрана результатов
	/// </summary>
	private void OnReturnToMainMenuPressed(ReturnToMainMenuEventArgs obj)
	{
		EventBus<ReturnToMainMenuEventArgs>.Unsubscribe(OnReturnToMainMenuPressed);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
