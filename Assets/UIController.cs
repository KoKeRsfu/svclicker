using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField] GameObject camera;
	
	[SerializeField] GameObject storage;
	[SerializeField] GameObject toolMenu;
	
	public List<GameObject> US_Buttons;
	public List<GameObject> US_Storages;
	
	[SerializeField] Image tool1;
	[SerializeField] Image tool2;
	
    // Start is called before the first frame update
    void Start()
    {
	    UpdateButtons(0);
    }

    // Update is called once per frame
    void Update()
    {
	    storage.GetComponent<Storage>().UpdateCounters();
    }
    
	public void OpenMenu(int id) 
	{
		switch (id) 
		{
		case 0:
			storage.SetActive(!storage.active);
			break;
		case 1:		
			toolMenu.SetActive(!toolMenu.active);
			break;
		
		}
		for (int i = 0; i<transform.childCount; i++) 
		{
			transform.GetChild(i).GetComponent<Button>().interactable = !(transform.GetChild(i).GetComponent<Button>().IsInteractable());
		}
		
		transform.GetChild(id).GetComponent<Button>().interactable = true;
	}
	
	public void MoveLocation(int a) 
	{
		int id;
		string objload = "";
		switch (a) 
		{
		case 0: //Ферма
			camera.transform.position = new Vector3(0,0,-10);
			
			id=0;
			if (storage.GetComponent<Storage>().items[103] == 1) id += 1;
			if (storage.GetComponent<Storage>().items[110] == 1) id += 1;
			if (storage.GetComponent<Storage>().items[117] == 1) id += 1;
			if (storage.GetComponent<Storage>().items[124] == 1) id += 1;
			objload = "UI/Tools/tool_icons_" + id;
			tool1.sprite = Resources.Load<Sprite>(objload);
			
			id=5;
			if (storage.GetComponent<Storage>().items[102] == 1) id += 1;
			if (storage.GetComponent<Storage>().items[109] == 1) id += 1;
			if (storage.GetComponent<Storage>().items[116] == 1) id += 1;
			if (storage.GetComponent<Storage>().items[123] == 1) id += 1;
			
			objload = "UI/Tools/tool_icons_" + id;
			tool2.sprite = Resources.Load<Sprite>(objload);
			
			tool1.color = new Color(1,1,1,1);
			tool2.color = new Color(1,1,1,1);
			break;
		case 1: //Лес 
			camera.transform.position = new Vector3(0,12,-10);
			
			id=10;
			if (storage.GetComponent<Storage>().items[100] == 1) id += 1;
			if (storage.GetComponent<Storage>().items[107] == 1) id += 1;
			if (storage.GetComponent<Storage>().items[114] == 1) id += 1;
			if (storage.GetComponent<Storage>().items[121] == 1) id += 1;
			objload = "UI/Tools/tool_icons_" + id;
			tool1.sprite = Resources.Load<Sprite>(objload);
			
			tool1.color = new Color(1,1,1,1);
			tool2.color = new Color(1,1,1,0);
			break;
		case 2: //Шахта
			camera.transform.position = new Vector3(0,-12,-10);
			
			id=15;
			if (storage.GetComponent<Storage>().items[101] == 1)id += 1;
			if (storage.GetComponent<Storage>().items[108] == 1)id += 1;
			if (storage.GetComponent<Storage>().items[115] == 1)id += 1;
			if (storage.GetComponent<Storage>().items[122] == 1)id += 1;
			objload = "UI/Tools/tool_icons_" + id;
			tool1.sprite = Resources.Load<Sprite>(objload);
			
			id=25;
			if (storage.GetComponent<Storage>().items[104] == 1) id += 1;
			if (storage.GetComponent<Storage>().items[111] == 1) id += 1;
			if (storage.GetComponent<Storage>().items[118] == 1) id += 1;
			if (storage.GetComponent<Storage>().items[125] == 1) id += 1;
			objload = "UI/Tools/tool_icons_" + id;
			tool2.sprite = Resources.Load<Sprite>(objload);
			
			tool1.color = new Color(1,1,1,1);
			tool2.color = new Color(1,1,1,1);
			break;
		case 3: //Озеро
			camera.transform.position = new Vector3(-20, 0,-10);
			
			id=20;
			if (storage.GetComponent<Storage>().items[105] == 1)id += 1;
			if (storage.GetComponent<Storage>().items[112] == 1)id += 1;
			if (storage.GetComponent<Storage>().items[119] == 1)id += 1;
			if (storage.GetComponent<Storage>().items[126] == 1)id += 1;
			objload = "UI/Tools/tool_icons_" + id;
			tool1.sprite = Resources.Load<Sprite>(objload);
			
			tool1.color = new Color(1,1,1,1);
			tool2.color = new Color(1,1,1,0);
			break;
		case 4: //Город
			camera.transform.position = new Vector3(20, 0,-10);
			
			tool1.color = new Color(1,1,1,0);
			tool2.color = new Color(1,1,1,0);
			break;
		case 5:
			break;
		case 6:
			break;
		default:
			break;
		}
		Debug.Log(objload);
	}

	public void UpdateButtons(int a) 
	{
		for (int i = 0; i < US_Buttons.Count; i++) 
		{
			US_Buttons[i].transform.localPosition = new Vector2(US_Buttons[i].transform.localPosition.x, 132);
			US_Storages[i].SetActive(false);
		}
		
		US_Buttons[a].transform.localPosition = new Vector2(US_Buttons[a].transform.localPosition.x, 126);
		US_Storages[a].SetActive(true);
	}
}
