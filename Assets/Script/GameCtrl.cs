using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameModel
{ 
	Start,
	Switch,
	Normal,
	Endless
}
public enum FruitPos
{ 
	Right,
	Left
}

public static class HistoryRecords
{
	public static string ENDLESSTOW = "EndlessGameTwoHightScore";
	public static string ENDLESS = "EndlessGameHightScore";
	public static string LEVELSTART = "LevelStart";//后面跟数字为指定关卡
	public static int[] LEVELINFOLEFT = { 1, 2, 3, 1, 1, 1, 1, 1, 1, 4, 5, 6, 7, 8, 9, 3, 3, 3, 3, 3, 3};
	public static int[] LEVELINFORIGHT = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 2, 2, 2, 2, 2, 2, 4, 5, 6, 7, 8, 9};
}

public static class FruitType
{
	public static string[] Fruit = { "apple_a", "lemon_a", "watermelon_a"};
}

public class GameCtrl : MonoBehaviour {
	private Transform m_mainCube;
	public static int ms_currentModel;
	private Animator m_mainCubeAnim;
	private GameObject m_switchPanel;

	public static int curentLevel = 1;

	public static Vector3 ms_flashLeftPos = new Vector3(-4f, 0f, 0f);
	public static Vector3 ms_flashRightPos = new Vector3(4f, 0f, 0f);
	public static int ms_currentCount;
	public static int ms_score;
	public static int levelCount;

	private float timer;
	void Awake () {
		m_mainCube = GameObject.Find("MainCube").transform;
		ms_currentModel = (int)GameModel.Start;
		m_mainCubeAnim = m_mainCube.GetComponent<Animator>();
		m_switchPanel = GameObject.Find("SwitchPanel");
		levelCount = HistoryRecords.LEVELINFOLEFT.Length;

		m_switchPanel.SetActive(false);
		ms_currentCount = 0;
		ms_score = 0;

		//PlayerPrefs.SetInt("LevelStart1", 3);
		/* * 
		 * 
		 * */
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 3)
		{
			Application.Quit();
		}
		//for (int i = 1; i < 11; i++)
		//{
		//	PlayerPrefs.SetInt(HistoryRecords.LEVELSTART + i, 0);
		//	//print(i + "    " + PlayerPrefs.GetInt(HistoryRecords.LEVELSTART + i));
		//}

	}

	public void StartNormalGame(int noLevel)
	{
		m_mainCubeAnim.SetTrigger("LeftToUp");
		m_switchPanel.SetActive(false);
		GameCtrl.curentLevel = noLevel;
	}

	public void NormalBackSwithBTN()
	{
		m_mainCubeAnim.SetTrigger("UpToLeft");
		ms_currentModel = (int)GameModel.Switch;
		m_switchPanel.SetActive(true);
		m_switchPanel.GetComponentInChildren<CreateLevel>().CreateLvl();
	}


	public void StartEndlessGame()
	{ 
	
	}

	public void LevelSwitch()
	{
		m_mainCubeAnim.SetTrigger("TurnLeft");
		ms_currentModel = (int)GameModel.Switch;
		m_switchPanel.SetActive(true);
		m_switchPanel.GetComponentInChildren<CreateLevel>().CreateLvl();
	}

	public void SwitchReturnBTN()
	{
		m_mainCubeAnim.SetTrigger("LeftReturn");
		ms_currentModel = (int)GameModel.Start;
		m_switchPanel.SetActive(false);
	}


	public void EndlessModel()
	{
		m_mainCubeAnim.SetTrigger("TurnRight");
	}

	public void EndlessModelTwo()
	{
		m_mainCubeAnim.SetTrigger("TurnUp");
	}

	public void NormalReturnBTN()
	{
		m_mainCubeAnim.SetTrigger("UnReturn");
		ms_currentModel = (int)GameModel.Start;
	}

	public void EndlessReturnBTN()
	{
		m_mainCubeAnim.SetTrigger("RightReturn");
		ms_currentModel = (int)GameModel.Start;
	}

	public void EndlessTwoReturnBTN()
	{
		m_mainCubeAnim.SetTrigger("UnReturn");
		ms_currentModel = (int)GameModel.Start;
	}

	public void QuitGame()
	{
		m_mainCubeAnim.SetTrigger("QuitTrigger");
		timer = 0;
		InvokeRepeating("QuitSelfTimer", 0, 0.5f);
	}

	void QuitSelfTimer()
	{
		timer++;
	}
}
