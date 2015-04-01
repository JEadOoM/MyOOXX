using UnityEngine;
using System.Collections;

public class CreateFruit : MonoBehaviour {
	public bool circleFlag;
	public float radius;
	public bool drawType;
	private int m_pointCount;
	private Transform m_trans;
	private Transform m_startPoint;
	private Transform m_endPoint;
	void Awake()
	{
		transform.Rotate(new Vector3(0f, 180f, Random.Range(0, 90)));
		m_trans = transform;
		m_pointCount = m_trans.childCount;
		Destroy(gameObject, 0.5f);
		Spawn();
	}

	void Spawn()
	{
		string fruit = FruitType.Fruit[Random.Range(0, 3)];
		if (circleFlag)
		{
			for (int i = 0; i < 24; i++)
			{
				Instantiate(Resources.Load(fruit), transform.position + new Vector3(radius * Mathf.Cos(i * 15), radius * Mathf.Sin(i * 15), 0), Quaternion.identity);
				GameCtrl.ms_currentCount++;
			}
			return;
		}
		for (int i = 0; i < m_pointCount; i++)
		{
			m_startPoint = m_trans.GetChild(i);
			if (drawType)
			{

				if (i + 1 == m_pointCount)
				{
					m_endPoint = m_trans.GetChild(0);
				}
				else
				{
					m_endPoint = m_trans.GetChild(i + 1);
				}
				for (int j = 0; j < 5; j++)
				{
					Vector3 pos = Vector3.Lerp(m_startPoint.position, m_endPoint.position, 0.25f * j);
					Instantiate(Resources.Load(fruit), pos, Quaternion.identity);
					GameCtrl.ms_currentCount++;
				}
			}
			else
			{
				Instantiate(Resources.Load(fruit), m_startPoint.position, Quaternion.identity);
				GameCtrl.ms_currentCount++;
			}
		}
	}


}
