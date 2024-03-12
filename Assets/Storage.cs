using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Storage : MonoBehaviour
{
	public ClickObject[] clickObjects = new ClickObject[3];
	
	public int season;
	public int clockTier = 0;
	[SerializeField] Animator ClockAnimator;
	
	
	[SerializeField] TextMeshProUGUI moneyCount;
	[SerializeField] TextMeshProUGUI relationshipCount;
	
	[SerializeField] TextAsset text;
	[SerializeField] GameObject LowerCanvas;
	[SerializeField] GameObject SellMenu;
	
	public int[] items = new int[80];
	public int[] cost = new int[80];
	
	private int currentSellingItemId;
    // Start is called before the first frame update
    void Start()
	{
		string a = text.text;
		string b = "";
		string c = "\n";
		int icost = 0;
		for (int i = 0; i<a.Length; i++) 
		{
			if (a[i] == c[0]) 
			{
				cost[icost] = int.Parse(b);
				b = "";
				icost += 1;
			}
			else 
			{
				b = b + a[i];
			}
		}
    }

    // Update is called once per frame
    void Update()
	{
		/*
	    for (int i=0;i < transform.childCount; i++) 
	    {
		    string objload = "Items/" + i;
	    	transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>(objload);
	    	transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = items[i].ToString();
		} */
	}
	
	public void ChangeSeason() 
	{
		ClockAnimator.SetInteger("Season", season);
		ClockAnimator.SetInteger("Tier", clockTier);
		ClockAnimator.SetTrigger("SeasonChange");
		ClockAnimator.GetComponent<GlobalSeasonChange>().StartCoroutine("ChangeSeasonGlobal");
	}
	
	public void ChangeObjectsWithSeason() 
	{
		for (int i = 0; i<clickObjects.Length; i++) 
		{
			clickObjects[i].UpdateSeason();	
		}
	}
	
	public void UpdateCounters() 
	{
		moneyCount.text = items[98].ToString();
		relationshipCount.text = items[99].ToString();
		
	}
    
	public void AddItem(int id, int quantity) 
	{
		items[id] += quantity;
		GameObject addParticle = Instantiate(Resources.Load<GameObject>("UI/Prefabs/AddItemParticle"), new Vector3(Camera.main.transform.position.x + Random.Range(-1.5f, 1.5f), Camera.main.transform.position.y + Random.Range(0.7f, 1.5f), 0), new Quaternion(0,0,0,0), LowerCanvas.transform);
		string objload = "Items/" + id;
		addParticle.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(objload);
		addParticle.GetComponentInChildren<TextMeshProUGUI>().text = "+" + quantity.ToString();
	}
	
	public void AddItemWoParticle(int id, int quantity) 
	{
		items[id] += quantity;
	}
	
	public void TrySell(int id) 
	{
		UnderStorage a = CheckWhichUsPanelActive();
		
		currentSellingItemId = a.itemSlots[id];
		
		if (currentSellingItemId == -1) return;
		if (items[currentSellingItemId] < 1) return;
		
		SellMenu.SetActive(true);
		string objload = "Items/" + currentSellingItemId;
		SellMenu.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(objload);
		SellMenu.transform.GetChild(3).GetComponent<Slider>().minValue = 0;
		SellMenu.transform.GetChild(3).GetComponent<Slider>().maxValue = items[currentSellingItemId];
		
		SellCountUpdater();
	}
	
	public void SellCountUpdater() 
	{
		int value = Mathf.FloorToInt(SellMenu.transform.GetChild(3).GetComponent<Slider>().value);
		SellMenu.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = value.ToString();
		SellMenu.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = (value * cost[currentSellingItemId]).ToString();
	}
	
	public void SellConfirm()
	{
		int c = Mathf.FloorToInt(SellMenu.transform.GetChild(3).GetComponent<Slider>().value);
		int d = c * cost[currentSellingItemId];
		
		items[currentSellingItemId] -= c;
		items[98] += d;
		
		SellMenu.SetActive(false);
	}
	
	public void SellDeny() 
	{
		SellMenu.SetActive(false);
	}
	
	private UnderStorage CheckWhichUsPanelActive() 
	{
		for (int i = 0; i < transform.GetChild(2).childCount; i++) 
		{
			if (transform.GetChild(2).GetChild(i).gameObject.active)
			{
				return transform.GetChild(2).GetChild(i).GetComponent<UnderStorage>();
			}
		}
		return null;
	}
}
