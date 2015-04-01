using UnityEngine;
using System.Collections;

public class NormalGameLogic : MonoBehaviour {

	private UILabel m_timelabel;
	private GameObject m_onGameBack;

	private Transform m_trans;
	private Vector3 m_UpPos;
	private Vector3 m_UpUpPos;
	private Vector3 m_midPos;
	private Vector3 m_midmidPos;
	private int timer;
	private bool m_panelMoveFlag;
	private bool m_startFlag;
	private bool m_gameOverFlag;
	private int defaultTime = 6;
	private bool ifFlash;
	void Awake()
	{
		m_timelabel = transform.FindChild("TimeLabel").GetComponent<UILabel>();
		m_onGameBack = transform.FindChild("OnGameBack").gameObject;
	}

	void Start () {
		m_trans = transform;
		m_midPos = m_trans.position;
		m_midmidPos = m_midPos - new Vector3(0f, 1f, 0f);

		ifFlash = true;
		timer = 0;

		m_UpPos = m_trans.position + new Vector3(0f, 1.13f, 0f);
		m_UpUpPos = m_UpPos + new Vector3(0f, 1f, 0f);
		
	}
	
	// Update is called once per frame
	void Update () {
		//游戏失败
		if (m_gameOverFlag)
		{

			//判断得到的星星
			int tempStar;
			int tempHistoryStat;
			if (GameCtrl.ms_currentCount == 0)
			{
				tempStar = 3;
			}
			else if (GameCtrl.ms_currentCount < 5)
			{
				tempStar = 2;
			}
			else if (GameCtrl.ms_currentCount < 10)
			{
				tempStar = 1;
			}
			else
			{
				tempStar = 0;
			}
			tempHistoryStat = PlayerPrefs.GetInt(HistoryRecords.LEVELSTART + GameCtrl.curentLevel);

			if (tempStar > tempHistoryStat)
				PlayerPrefs.SetInt(HistoryRecords.LEVELSTART + GameCtrl.curentLevel, tempStar);

			ShowStar();

			
			m_timelabel.enabled = false;
			m_onGameBack.SetActive(false);

			m_trans.position = Vector3.Lerp(m_trans.position, m_midmidPos, 0.8f * Time.deltaTime);
			if (Mathf.Abs(m_trans.position.y - m_midPos.y) < 0.01f)
			{
				GameCtrl.ms_currentModel = (int)GameModel.Start;
				m_trans.position = m_midPos;
				m_gameOverFlag = false;
				CancelInvoke("TimeCounter");

			}
			return;
		}

		if (GameCtrl.ms_currentModel == (int)GameModel.Normal)
		{
			if (!m_startFlag)
			{
				return;
			}
			
			//开始游戏
			if (m_panelMoveFlag)
			{

				m_trans.position = Vector3.Lerp(m_trans.position, m_UpUpPos, 0.8f * Time.deltaTime);
				if (Mathf.Abs(m_trans.position.y - m_UpPos.y) < 0.01f)
				{
					m_timelabel.enabled = true;
					m_onGameBack.SetActive(true);
					m_trans.position = m_UpPos;
					m_panelMoveFlag = false;
				}
				else
					return;
			}
			if (ifFlash)
			{
				ifFlash = false;
				Instantiate(Resources.Load("Rect" + HistoryRecords.LEVELINFOLEFT[GameCtrl.curentLevel - 1]), GameCtrl.ms_flashLeftPos, Quaternion.identity);
				Instantiate(Resources.Load("Rect" + HistoryRecords.LEVELINFORIGHT[GameCtrl.curentLevel - 1]), GameCtrl.ms_flashRightPos, Quaternion.identity);
			}

			if (timer < 0)
			{
				timer = 0;
				m_gameOverFlag = true;
				return;
			}
			else
			{
				if (GameCtrl.ms_currentCount == 0 && !m_panelMoveFlag)
				{
					m_gameOverFlag = true;
					return;
				}
			}
			m_timelabel.text = "Current Count : " + GameCtrl.ms_currentCount + "  Remaining Time : " + timer;
						
		}
		
	}

	void TimeCounter()
	{
		timer--;
	}

	void OnEnable()
	{
		ShowStar();
	}

	void OnDisable()
	{

		for (int j = 0; j < 3; j++)
		{
			transform.FindChild("Starts").GetChild(j).GetComponent<UISprite>().enabled = false;
		}
	}

	void ShowStar()
	{
		for (int j = 0; j < PlayerPrefs.GetInt(HistoryRecords.LEVELSTART + GameCtrl.curentLevel); j++)
		{
			transform.FindChild("Starts").GetChild(j).GetComponent<UISprite>().enabled = true;
		}
	}

	void OnClick()
	{
		GameCtrl.ms_score = 0;
		m_panelMoveFlag = true;
		GameCtrl.ms_currentModel = (int)GameModel.Normal;
		m_startFlag = true;

		timer = defaultTime;
		InvokeRepeating("TimeCounter", 0, 1);
		ifFlash = true;
		m_timelabel.enabled = true;
		m_onGameBack.SetActive(true);
	}

	public void OnGameBack()
	{
		m_gameOverFlag = true;
	}

}
