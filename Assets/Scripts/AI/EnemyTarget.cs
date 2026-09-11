using System.Collections;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
	public Transform TargetObj { get => targetObj; }
	
	Transform targetObj;

	public void Initialize(Transform targetObj)
	{
		this.targetObj = targetObj;
	}


}