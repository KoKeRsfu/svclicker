using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnderStorage : MonoBehaviour
{
	
	public int[] itemSlots = new int[36];
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    for (int i=0;i < transform.childCount; i++) 
	    {
	    	if (itemSlots[i] == -1) 
	    	{
	    		transform.GetChild(i).GetComponent<Image>().color = new Color(1,1,1,0);
		    	transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = "";
		    	continue;
	    	}
		    string objload = "Items/" + itemSlots[i];
	    	transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>(objload);
	    	transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = GetComponentInParent<Storage>().items[itemSlots[i]].ToString();
	    }
    }
}
