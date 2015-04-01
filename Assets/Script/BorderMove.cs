using UnityEngine;
using System.Collections;

public class BorderMove : MonoBehaviour {

	private Vector3 m_gamePos;
	private Vector3 m_notGamePos;
	// Use this for initialization
	void Start () {
		m_gamePos = new Vector3(0, 0, -0.25f);
		m_notGamePos = new Vector3(0, -2f, -0.25f);
		transform.position = m_notGamePos;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameCtrl.ms_currentModel == (int)GameModel.Normal || GameCtrl.ms_currentModel == (int)GameModel.Endless)
		{
			transform.position = Vector3.Lerp(transform.position, m_gamePos, 2.5f * Time.deltaTime);
			if (Mathf.Abs(transform.position.y - m_gamePos.y) < 0.05f)
			{
				transform.position = m_gamePos;
				return;
			}

		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, m_notGamePos, 2.5f * Time.deltaTime);
			if (Mathf.Abs(transform.position.y - m_notGamePos.y) < 0.05f)
			{
				transform.position = m_notGamePos;
				return;
			}
		}
	}
}
