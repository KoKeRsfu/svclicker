using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	public List<int> availableCraftsPool;
	
	public Storage storage;
	
	[SerializeField] GameObject craftsContainer;
	
    // Start is called before the first frame update
    void Start()
    {
	    UpdatePool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void UpdatePool() 
	{
		for (int i = 0; i < craftsContainer.transform.childCount; i++) 
		{
			Destroy(craftsContainer.transform.GetChild(i).gameObject);
		}
		
		for (int i = 0; i < availableCraftsPool.Count; i++) 
		{
			GameObject currentCraft = Instantiate(Resources.Load<GameObject>("UI/Prefabs/Craft"), craftsContainer.transform);
			string objload = "Crafts/" + availableCraftsPool[i];
			currentCraft.GetComponent<Craft>().currentCraft = Resources.Load<CraftScriptableObject>(objload);
		}
	}
    
	public void UpdateCrafts() 
	{
		UpdatePool();
		for (int i =0;i<craftsContainer.transform.childCount;i++) 
		{
			craftsContainer.transform.GetChild(i).GetComponent<Craft>().UpdateCraft();
		}
	}
}
