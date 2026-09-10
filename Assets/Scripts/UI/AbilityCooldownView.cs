using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownView : MonoBehaviour
{
	[SerializeField]
	Image fill;
	[SerializeField]
	Image iconImage;

	public void SetIcon(Sprite icon)
	{
		iconImage.sprite = icon;
	}

	public void SetState(float cooldownPercentage)
	{
		fill.fillAmount = cooldownPercentage;
	}

}