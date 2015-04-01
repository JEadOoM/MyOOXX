using UnityEngine;
using System.Collections;

public class EndlessGameLogic : MonoBehaviour {

	private UILabel m_historyScore;
	private UILabel m_scorePanel;
	private UILabel m_OngameScorePanel;
	private GameObject m_lifeSliderObj;
	private UISlider m_lifeSlider;

	private Transform m_trans;
	private Vector3 m_UpPos;
	private Vector3 m_UpUpPos;
	private Vector3 m_midPos;
	private Vector3 m_midmidPos;
	public static float currentLife;
	public const int m_maxLife = 1000;
	private bool m_panelMoveFlag;
	private bool m_startFlag;
	private bool m_gameOverFlag;

	private BoxCollider m_boxColli;
	private bool ifFlash;
	private string tempL;
	private string tempR;
	void Awake()
	{
		m_boxColli = GetComponent<BoxCollider>();
		m_scorePanel = transform.FindChild("Score").GetComponent<UILabel>();
		m_historyScore = transform.FindChild("HistoryScore").GetComponent<UILabel>();

		m_lifeSliderObj = transform.FindChild("LifeSlider").gameObject;
		m_lifeSlider = m_lifeSliderObj.GetComponent<UISlider>();
		m_OngameScorePanel = transform.FindChild("OnScore").GetComponent<UILabel>();
	}

	void Start()
	{
		m_trans = transform;
		m_midPos = m_trans.position;
		m_midmidPos = m_midPos - new Vector3(0f, 1f, 0f);




		m_UpPos = m_trans.position + new Vector3(0f, 1.13f, 0f);
		m_UpUpPos = m_UpPos + new Vector3(0f, 1f, 0f);

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
					m_lifeSliderObj.SetActive(true);
					m_OngameScorePanel.enabled = true;
					m_trans.position = m_UpPos;
					m_panelMoveFlag = false;
				}
				return;
			}



			if (currentLife < 0)
			{
				m_gameOverFlag = true;
				m_boxColli.enabled = false;
				GameCtrl.ms_currentModel = (int)GameModel.Start;
				return;
			}

			m_lifeSlider.value = (float)(currentLife / m_maxLife);
			m_OngameScorePanel.text = "Score : " + GameCtrl.ms_score;

			if (GameCtrl.ms_currentCount == 0)
			{
				GameCtrl.curentLevel++;
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
			m_scorePanel.text = "Score : " + GameCtrl.ms_score;

			int tempScore = PlayerPrefs.GetInt(HistoryRecords.ENDLESS);
			if (GameCtrl.ms_score > tempScore)
				PlayerPrefs.SetInt(HistoryRecords.ENDLESS, GameCtrl.ms_score);

			m_historyScore.text = "History Records : " + PlayerPrefs.GetInt(HistoryRecords.ENDLESS);

			m_scorePanel.enabled = true;
			m_lifeSliderObj.SetActive(false);
			m_OngameScorePanel.enabled = false;

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
		currentLife -= Mathf.Min(10 + GameCtrl.curentLevel * 2, 50);
	}

	void OnEnable()
	{
		m_scorePanel.enabled = false;
		m_historyScore.text = "History Score : " + PlayerPrefs.GetInt("EndlessGameHightScore");
	}

	void OnClick()
	{
		currentLife = m_maxLife;
		GameCtrl.ms_score = 0;
		GameCtrl.curentLevel = 1;
		m_panelMoveFlag = true;
		m_boxColli.enabled = false;
		GameCtrl.ms_currentModel = (int)GameModel.Endless;
		m_startFlag = true;
		InvokeRepeating("TimeCounter", 0, 0.5f);
		ifFlash = true;

		

		m_scorePanel.enabled = false;
	}

	void ReSet()
	{
		m_lifeSliderObj.SetActive(true);
		m_OngameScorePanel.enabled = false;
		m_scorePanel.enabled = false;
	}

	public void OnGameBack()
	{
		m_gameOverFlag = true;
		m_boxColli.enabled = false;
		GameCtrl.ms_currentModel = (int)GameModel.Start;
	}

}