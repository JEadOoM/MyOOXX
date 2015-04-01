using UnityEngine;
using System.Collections;

public class HandClick : MonoBehaviour {

	private Ray ray;
	private RaycastHit hit;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetMouseButton(0))
		//{
		//	ray = UICamera.mainCamera.ScreenPointToRay(Input.mousePosition);

		//	if (Physics.Raycast(ray, out hit))
		//	{
		//		if (hit.transform != null && hit.transform.tag.CompareTo("target") == 0 && GameCtrl.ms_currentModel != (int)GameModel.Start)
		//		{
		//			hit.transform.GetComponent<TargetScript>().BeCut();
		//		}
		//	}
		//}

		if (Input.touchCount > 2)
			return;
		for (int i = 0; i < Input.touchCount; i++)
		{
			ray = UICamera.mainCamera.ScreenPointToRay(Input.GetTouch(i).position);

			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform != null && hit.transform.tag.CompareTo("target") == 0 && GameCtrl.ms_currentModel != (int)GameModel.Start)
				{
					hit.transform.GetComponent<TargetScript>().BeCut();
				}
			}
		}
	}
}
