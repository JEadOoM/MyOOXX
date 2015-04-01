using UnityEngine;
using System.Collections;

public class CreateLevel : MonoBehaviour {

	// Use this for initialization
	void Awake () {

		//CreateLvl();
	}

	public void CreateLvl()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
		GetComponent<UIGrid>().cellWidth *= (NGUITools.screenSize.x / 1280);

		for (int i = 1; i <= GameCtrl.levelCount; i++)
		{
			Transform obj = ((GameObject)Instantiate(Resources.Load("Level"), transform.position, Quaternion.identity)).transform;
			obj.localScale = new Vector3(0.005f, 0.005f, 0.005f);
			obj.GetComponent<LevelSetting>().noLevel = i;
			obj.SetParent(transform);
			obj.FindChild("NO").GetComponent<UILabel>().text = "Level : " + i;

			for (int j = 0; j < PlayerPrefs.GetInt(HistoryRecords.LEVELSTART + i); j++)
			{
				obj.FindChild("Starts").GetChild(j).GetComponent<UISprite>().enabled = true;
			}


		}
		transform.GetComponent<UIGrid>().enabled = true;
	}
}
