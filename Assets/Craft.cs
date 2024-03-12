using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Craft : MonoBehaviour
{
	private Shop Shop;
	
	public Storage storage;
	public CraftScriptableObject currentCraft;
	
	[SerializeField] Image craftSprite;
	[SerializeField] TextMeshProUGUI Name;
	[SerializeField] TextMeshProUGUI Description;
	
	[SerializeField] TextMeshProUGUI[] materialsText = new TextMeshProUGUI[8];

	[SerializeField] Image[] matSprites = new Image[8];
	public int[] matIds = new int[8];
	public int[] matQuantities = new int[8];
	
	
	
    void Start()
    {
	    Shop = transform.parent.parent.parent.parent.parent.GetComponent<Shop>();
	    storage = Shop.storage;
	    UpdateCraft();
    }

	protected void OnEnable() 
	{
		UpdateCraft();
	}

	public void TryCraft() 
	{
		for (int i=0;i<currentCraft.craftingItemIdPool.Count;i++) 
		{
			if (storage.items[matIds[i]] < matQuantities[i]) return;
		}
		
		for (int i=0;i<currentCraft.craftingItemIdPool.Count;i++) 
		{
			storage.items[matIds[i]] -= matQuantities[i];
		}
		
		storage.items[currentCraft.craftingId] += 1;
		
		if (currentCraft.oneTime) 
		{
			Shop.availableCraftsPool.Remove(int.Parse(currentCraft.name));
		}
		
		if (currentCraft.unlocksOther) 
		{
			for (int i=0; i<currentCraft.unlockingId.Count; i++) 
			{
				Shop.availableCraftsPool.Add(currentCraft.unlockingId[i]);
			}
		}
		
		Shop.UpdateCrafts();
		
	}

	public void UpdateCraft() 
	{
		for (int i=0;i<currentCraft.craftingItemIdPool.Count;i++) 
		{
			matIds[i] = currentCraft.craftingItemIdPool[i];
			matQuantities[i] = currentCraft.craftingQuantity[i];
			materialsText[i].text = storage.items[matIds[i]].ToString() + "/" + matQuantities[i].ToString();
			if (storage.items[matIds[i]] >= matQuantities[i]) 
			{
				materialsText[i].color = new Color(0,0.44f,0.06f,1);
			}
			else 
			{
				materialsText[i].color = new Color(0.56f,0.03f,0,1);
			}
			string objload = "Items/" + matIds[i];
			matSprites[i].sprite = Resources.Load<Sprite>(objload);
		}
	    
		for (int i=0;i<matIds.Length;i++) 
		{
			if (matIds[i] == -1) 
			{
				matSprites[i].color = new Color(1,1,1,0);
				materialsText[i].color = new Color(1,1,1,0);
			}
		}
		
		string objload2 = "craftSprites/" + currentCraft.name;
		craftSprite.sprite = Resources.Load<Sprite>(objload2);
	}

    void Update()
    {
        
    }
}
