using UnityEngine;
using System.Collections;

public class PanelMove : MonoBehaviour {

	public bool m_moveUp;
	public bool m_moveDown;

	private Transform m_trans;
	private Vector3 m_upPos;
	private Vector3 m_downPos;
	// Use this for initialization
	void Start () {
		m_trans = transform;
		m_downPos = m_trans.position;
		m_upPos = m_trans.position + new Vector3(0f, Screen.height / 1.5f, 0f);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (m_moveUp)
		{
			MoveUp();
		}
		if (m_moveDown)
		{
			MoveDown();
		}
	}
	private void MoveUp()
	{
		m_trans.position = Vector3.Lerp(m_trans.position, m_upPos, 4f * Time.deltaTime);
		if (Mathf.Abs(m_trans.position.y - m_upPos.y) < 5)
		{
			m_trans.position = m_upPos;
			m_moveUp = false;
		}
	}

	private void MoveDown()
	{
		m_trans.position = Vector3.Lerp(m_trans.position, m_downPos, 4f * Time.deltaTime);
		if (Mathf.Abs(m_trans.position.y - m_downPos.y) < 5)
		{
			m_trans.position = m_downPos;
			m_moveDown = false;
		}
	}
}
