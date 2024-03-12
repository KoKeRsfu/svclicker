using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CraftsCreator : MonoBehaviour
{
	
	[SerializeField] TextAsset crafts;
	
	
	public Sprite sprite;
	public bool oneTime;
	public bool unlocksOther;
	public List<int> craftingItemIdPool;
	public List<int> craftingQuantity;
	public int craftingId;
	public List<int> unlockingId;
	
	public List<CraftScriptableObject> craftss;
	
    // Start is called before the first frame update
    void Start()
	{
		
		string craftsList = crafts.text;
		string current = "";
		string lineEnd = "\n";
		string tab = "\t";
		string yn = "yn";
		int icraft = 0;
		int phase = 0;
		
		for (int i = 0; i<craftsList.Length; i++) 
		{
			if (craftsList[i] == lineEnd[0]) 
			{
				if (current[0] == yn[0]) unlocksOther = true;	
				if (current[0] == yn[1]) unlocksOther = false;	
				
				string objload = "craftSprites/" + icraft;
				sprite = Resources.Load<Sprite>(objload);
				
				craftss.Add(ScriptableObject.CreateInstance<CraftScriptableObject>());
				
				craftss[icraft].sprite = sprite;
				craftss[icraft].oneTime = oneTime;
				craftss[icraft].unlocksOther = unlocksOther;
				craftss[icraft].craftingItemIdPool = craftingItemIdPool;
				craftss[icraft].craftingQuantity = craftingQuantity;
				craftss[icraft].craftingId = craftingId;
				craftss[icraft].unlockingId = unlockingId;
				
				string path = "Assets/Resources/Crafts/" + icraft + ".asset";
				
				AssetDatabase.CreateAsset(craftss[icraft], path);    
				
				sprite = null;
				oneTime = false;
				unlocksOther = false;
				craftingItemIdPool.Clear();
				craftingQuantity.Clear();
				craftingId = 0;
				unlockingId.Clear();
				
				icraft += 1;
				current = "";
				phase = 0;
			}
			else if (craftsList[i] == '*') 
			{
				if (phase == 0) //materials
				{
					craftingItemIdPool.Add(int.Parse(current));
					current = "";
				}
			}
			else if (craftsList[i] == ',') 
			{
				if (phase == 0) 
				{
					craftingQuantity.Add(int.Parse(current));
					current = "";
				}
				if (phase == 2)
				{
					unlockingId.Add(int.Parse(current));
					current = "";
				}
			}
			else if (craftsList[i] == tab[0]) 
			{
				if (phase == 0) 
				{
					craftingQuantity.Add(int.Parse(current));
					current = "";
				}
				if (phase == 1)
				{
					craftingId = int.Parse(current);
					current = "";
				}
				if (phase == 2)
				{
					unlockingId.Add(int.Parse(current));
					current = "";
				}
				if (phase == 3) 	
				{
					if (current[0] == yn[0]) oneTime = true;	
					if (current[0] == yn[1]) oneTime = false;	
					current = "";
				}
				phase += 1;
			}
			else 
			{
				current = current + craftsList[i];	
			}	
			//Debug.Log(current);
		}
		
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
