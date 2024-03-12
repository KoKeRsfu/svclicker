using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSeasonChange : MonoBehaviour
{
	
	[SerializeField] Storage storage;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    	
	public IEnumerator ChangeSeasonGlobal() 
	{
		this.transform.GetChild(1).GetComponent<Button>().interactable = false;
		
		yield return new WaitForSeconds(0.4f);
		
		this.transform.GetChild(1).GetComponent<Button>().interactable = true;
				
		switch (storage.clockTier) 
		{
			case 0:
				storage.season = 0;
				break;
			case 1:
				if (storage.season < 1) 
				{
					storage.season += 1;	
				}
				else 
				{
					storage.season = 0;	
				}
				storage.ChangeObjectsWithSeason();
				break;
			case 2:
				if (storage.season < 2) 
				{
					storage.season += 1;	
				}
				else 
				{
					storage.season = 0;	
				}
				storage.ChangeObjectsWithSeason();
				break;
			default:
				break;
		}		
	}
}
