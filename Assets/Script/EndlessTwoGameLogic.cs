using UnityEngine;
using System.Collections;

public class EndlessTwoGameLogic : MonoBehaviour {

	private UILabel m_historyScore;
	private UILabel m_scorePanel;
	private UILabel m_OngameScorePanel;
	private UILabel m_OngameTime;
	private UILabel m_OngameLevel;
	private GameObject m_onGameBack;

	private Transform m_trans;
	private Vector3 m_UpPos;
	private Vector3 m_UpUpPos;
	private Vector3 m_midPos;
	private Vector3 m_midmidPos;
	public static float currentLife;
	private const int m_maxLife = 1000;
	private bool m_panelMoveFlag;
	private bool m_startFlag;
	private bool m_gameOverFlag;

	private int defaultTime = 4;
	private int timer;
	private BoxCollider m_boxColli;
	private bool ifFlash;

	private string tempL;
	private string tempR;
	void Awake()
	{
		m_boxColli = GetComponent<BoxCollider>();
		m_scorePanel = transform.FindChild("Score").GetComponent<UILabel>();
		m_OngameScorePanel = transform.FindChild("OnScore").GetComponent<UILabel>();
		m_OngameTime = m_OngameScorePanel.transform.FindChild("Time").GetComponent<UILabel>();
		m_OngameLevel = m_OngameScorePanel.transform.FindChild("Level").GetComponent<UILabel>();
		m_historyScore = transform.FindChild("HistoryScore").GetComponent<UILabel>();
		m_onGameBack = transform.FindChild("OnGameBack").gameObject;
	}

	void Start()
	{
		m_trans = transform;
		m_midPos = m_trans.position;
		m_midmidPos = m_midPos - new Vector3(0f, 1f, 0f);

		
		

		m_UpPos = m_trans.position + new Vector3(0f, 1.1f, 0f);
		m_UpUpPos = m_UpPos + new Vector3(0f, 1f, 0f);

	}

	void SetOnGameScore(bool flag)
	{
		m_onGameBack.SetActive(flag);
		m_OngameScorePanel.enabled = flag;
		m_OngameLevel.enabled = flag;
		m_OngameTime.enabled = flag;
	}

	public void OnGameBack()
	{
		m_gameOverFlag = true;
		m_boxColli.enabled = false;
		GameCtrl.ms_currentModel = (int)GameModel.Start;
	}

	
	// Update is called once per frame
	void Update () {

		if (GameCtrl.ms_currentModel == (int)GameModel.Endless)
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
					m_boxColli.enabled = true;

					SetOnGameScore(true);

					m_trans.position = m_UpPos;
					m_panelMoveFlag = false;
				}
				return;
			}



			if (timer < 0)
			{
				m_gameOverFlag = true;
				m_boxColli.enabled = false;
				GameCtrl.ms_currentModel = (int)GameModel.Start;
				return;
			}

			m_OngameTime.text = timer +"";
			m_OngameLevel.text = GameCtrl.curentLevel + "";

			if (GameCtrl.ms_currentCount == 0)
			{
				GameCtrl.curentLevel++;
				timer = defaultTime;
				ifFlash = true;
			}

			if (ifFlash)
			{
				ifFlash = false;
				tempL = "Rect" + Random.Range(1, 10);
				tempR = "Rect" + Random.Range(1, 10);
				Instantiate(Resources.Load(tempL), GameCtrl.ms_flashLeftPos, Quaternion.identity);
				Instantiate(Resources.Load(tempR), GameCtrl.ms_flashRightPos, Quaternion.identity);
			}
		}
		//游戏失败
		if (m_gameOverFlag)
		{
			m_scorePanel.text = "Pass Level : " + GameCtrl.curentLevel;

			int tempScore = PlayerPrefs.GetInt(HistoryRecords.ENDLESSTOW);
			if (GameCtrl.curentLevel > tempScore)
				PlayerPrefs.SetInt(HistoryRecords.ENDLESSTOW, GameCtrl.curentLevel);

			m_historyScore.text = "History Records : " + PlayerPrefs.GetInt(HistoryRecords.ENDLESSTOW);

			m_scorePanel.enabled = true;

			SetOnGameScore(false);

			m_trans.position = Vector3.Lerp(m_trans.position, m_midmidPos, 0.8f * Time.deltaTime);
			if (Mathf.Abs(m_trans.position.y - m_midPos.y) < 0.01f)
			{
				m_boxColli.enabled = true;
				m_trans.position = m_midPos;
				m_gameOverFlag = false;
				CancelInvoke("TimeCounter");

			}
			return;
		}
	}

	void TimeCounter()
	{
		timer--;
	}

	void OnEnable()
	{
		m_historyScore.text = "History Records : " + PlayerPrefs.GetInt(HistoryRecords.ENDLESSTOW);
		m_scorePanel.enabled = false;
	}

	void OnClick()
	{
		timer = defaultTime;
		GameCtrl.ms_score = 0;
		GameCtrl.curentLevel = 0;
		m_panelMoveFlag = true;
		m_boxColli.enabled = false;
		GameCtrl.ms_currentModel = (int)GameModel.Endless;
		m_startFlag = true;
		InvokeRepeating("TimeCounter", 0, 1);
		ifFlash = true;

		m_scorePanel.enabled = false;
	}

}