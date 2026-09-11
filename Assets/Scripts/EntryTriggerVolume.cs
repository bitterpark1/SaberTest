using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class EntryTriggerVolume : MonoBehaviour
	{
		const string PLAYER_LAYER_NAME = "Player";

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer(PLAYER_LAYER_NAME))
			{
				EventBus<ArenaEnteredEventArgs>.Invoke(new ArenaEnteredEventArgs());
				gameObject.SetActive(false);
			}
		}

	}
}