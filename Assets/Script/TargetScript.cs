using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {

	public GameObject effect;
	void Start()
	{
	}
	public void BeCut()
	{
		if (GameCtrl.ms_currentModel == (int)GameModel.Endless)
		{
			EndlessGameLogic.currentLife = Mathf.Min(EndlessGameLogic.currentLife + 5, EndlessGameLogic.m_maxLife);
		}
		GameCtrl.ms_score += 5 + GameCtrl.curentLevel*2;
		BeBeCut();
		
	}

	void BeBeCut()
	{
		Instantiate(effect, transform.position, Quaternion.identity);
		GameCtrl.ms_currentCount--;
		Destroy(gameObject);
	}

	void Update()
	{
		if (GameCtrl.ms_currentModel == (int)GameModel.Start)
		{
			BeBeCut();
		}
		transform.Rotate(new Vector3(2f,3f, 5f));
	}
}
