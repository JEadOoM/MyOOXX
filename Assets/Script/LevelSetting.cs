using UnityEngine;
using System.Collections;

public class LevelSetting : MonoBehaviour
{
	public int noLevel;
	private GameCtrl gameCtrl;

	void Start()
	{
		gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameCtrl>();
	}

	void OnClick()
	{
		gameCtrl.StartNormalGame(noLevel);
	}
}
