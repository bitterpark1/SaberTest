using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

	[SerializeField]
	GameObject mainPanel;

	[SerializeField]
	Button saveGame;
	[SerializeField]
	Button loadGame;

	[SerializeField]
	Button startNewGame;
	[SerializeField]
	Button returnToStartMenu;
	[SerializeField]
	Button quitGame;

	private void Awake()
	{
		startNewGame.onClick.AddListener(OnNewGamePressed);
		returnToStartMenu.onClick.AddListener(OnReturnToMainMenuPressed);
		quitGame.onClick.AddListener(OnQuitGamePressed);
		saveGame.onClick.AddListener(OnSaveGamePressed);
		loadGame.onClick.AddListener(OnLoadGamePressed);
		EventBus<GameStateChangedEventArgs>.Subscribe(OnNewGameState);
		gameObject.SetActive(false);	
	}

	private void OnSaveGamePressed()
	{
		EventBus<SaveGamePressedEventArgs>.Invoke(new SaveGamePressedEventArgs());
	}
	private void OnLoadGamePressed()
	{
		EventBus<LoadGamePressedEventArgs>.Invoke(new LoadGamePressedEventArgs());
	}

	

	private void OnDestroy()
	{
		EventBus<GameStateChangedEventArgs>.Unsubscribe(OnNewGameState);
	}

	private void OnNewGameState(GameStateChangedEventArgs obj)
	{
		if (obj.NewState != GameStateManager.State.GameOverScreen)
		{
			gameObject.SetActive(true);
		} else
		{
			gameObject.SetActive(false);
		}
		if (obj.NewState == GameStateManager.State.Gameplay)
		{
			startNewGame.gameObject.SetActive(false);
			returnToStartMenu.gameObject.SetActive(true);
		} else
		{
			startNewGame.gameObject.SetActive(true);
			returnToStartMenu.gameObject.SetActive(false);
		}
		mainPanel.SetActive(obj.NewState == GameStateManager.State.Menu);
;	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			mainPanel.SetActive(!mainPanel.activeSelf);
		}
	}

	private void OnNewGamePressed()
	{
		EventBus<NewGamePressedEventArgs>.Invoke(new NewGamePressedEventArgs());
	}
	private void OnReturnToMainMenuPressed()
	{
		EventBus<ReturnToMainMenuEventArgs>.Invoke(new ReturnToMainMenuEventArgs());
	}

	private void OnQuitGamePressed()
	{
		Application.Quit();
	}

	

}
