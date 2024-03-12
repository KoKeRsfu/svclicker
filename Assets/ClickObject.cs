using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using THB.SClicker;
using UnityEngine.EventSystems;

public class ClickObject : MonoBehaviour
{
	[SerializeField] int[] toolTierIds = new int[4];
	
	public int toolTier = 0;
	
	[SerializeField] int[] junimoIds = new int[3];
	public float[] junimoQuantity = new float[3];
	
	public ClickScriptableObject obj;
	
	[SerializeField] GameObject workers;
	public List<GameObject> JunimosTier1;
	public List<GameObject> JunimosTier2;
	public List<GameObject> JunimosTier3;
	
	[SerializeField] SpriteRenderer Back;
	[SerializeField] Sprite[] backSeasons = new Sprite[3];
	
	private SpriteRenderer sprite;
	private Vector2 spriteSize;
	[SerializeField] Storage storage;
	
	public float hp;
	
	public int[] springPool = new int[5];
	public int[] springPoolChance = new int[5];
	
	public int[] summerPool = new int[5];
	public int[] summerPoolChance = new int[5];
	
	public int[] fallPool = new int[5];
	public int[] fallPoolChance = new int[5];
	
	public int[] basicPool = new int[5];
	public int[] basicPoolChance = new int[5];
	
	public int[] copperPool = new int[0];
	public int[] ironPool = new int[0];
	public int[] goldPool = new int[0];
	public int[] iridiumPool = new int[0];
	
	public List<int> ObjectPool;
	
    // Start is called before the first frame update
    void Start()
	{
    	
		sprite = GetComponentInChildren<SpriteRenderer>();
		spriteSize = sprite.transform.localScale;
	    
	    
		UpdatePool(storage.season);
		ChangeObject(ObjectPool[Random.Range(0, ObjectPool.Count)]);
	    
		StartCoroutine("JunimoTier1");
		StartCoroutine("JunimoTier2");
		StartCoroutine("JunimoTier3");
    }

    // Update is called once per frame
    void Update()
    {
	    Back.sprite = backSeasons[storage.season];
    }
    
	protected void OnMouseDown() 
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;
		
		Click(0);
	}
	
	public void ChangeObject(int id) 
	{
		string objload = "ClickableObjects/" + id;
		obj = Resources.Load<ClickScriptableObject>(objload);
		sprite.sprite = obj.sprite;
		hp = obj.health;
	}
	
	public void Click(int id) 
	{
		
		StartCoroutine("Size");
		
		if (id != 0) 
		{
			if (id == 1) 
			{
				hp -= 1;	
			}
			if (id == 2) 
			{
				hp -= 3;
			}
			if (id == 3) 
			{
				hp -= 5;
			}
			if (hp <= 0) 
			{
				Death();
			}
			return;
		}
		
		switch (obj.skillType) 
		{
		case SkillType.Watering:
			Instantiate(Resources.Load<GameObject>("UI/Prefabs/WateringParticle"), new Vector2(this.transform.position.x - 0.1f, this.transform.position.y - 1.1f), new Quaternion(0,0,0,0));
			hp -= 1;
			if (storage.items[103] == 1) hp -= 1;
			if (storage.items[110] == 1) hp -= 1;
			if (storage.items[117] == 1) hp -= 1;
			if (storage.items[124] == 1) hp -= 1;
			break;
		case SkillType.Gathering:
			hp -= 1;
			if (storage.items[102] == 1) hp -= 1;
			if (storage.items[109] == 1) hp -= 1;
			if (storage.items[116] == 1) hp -= 1;
			if (storage.items[123] == 1) hp -= 1;
			break;
		case SkillType.Cutting:
			hp -= 1;
			if (storage.items[100] == 1) hp -= 1;
			if (storage.items[107] == 1) hp -= 1;
			if (storage.items[114] == 1) hp -= 1;
			if (storage.items[121] == 1) hp -= 1;
			break;
		case SkillType.Digging:
			hp -= 1;
			if (storage.items[101] == 1) hp -= 1;
			if (storage.items[108] == 1) hp -= 1;
			if (storage.items[115] == 1) hp -= 1;
			if (storage.items[122] == 1) hp -= 1;
			break;
		case SkillType.Fishing:
			hp -= 1;
			if (storage.items[105] == 1) hp -= 1;
			if (storage.items[112] == 1) hp -= 1;
			if (storage.items[119] == 1) hp -= 1;
			if (storage.items[126] == 1) hp -= 1;
			break;
		case SkillType.Attacking:
			hp -= 1;
			if (storage.items[104] == 1) hp -= 1;
			if (storage.items[111] == 1) hp -= 1;
			if (storage.items[118] == 1) hp -= 1;
			if (storage.items[125] == 1) hp -= 1;
			break;
		default:
			hp -= 1;
			break;
		}
		if (hp <= 0) 
		{
			Death();
		}
	}
	
	public void Death() 
	{
		UpdatePool(storage.season);
		
		if (obj.clickType == ClickObjectType.givesItem) 
		{
			for (int i=0; i<obj.givingItemIdPool.Count; i++)
			{
				if (Random.Range(1, 101) <= obj.givingChance[i])
				{
					storage.AddItem(obj.givingItemIdPool[i],  Random.Range(obj.givingQuantityMin[i], obj.givingQuantityMax[i] + 1));
				}
			}
			ChangeObject(ObjectPool[Random.Range(0, ObjectPool.Count)]);
		}
		else 
		{
			ChangeObject(obj.changingClickId);
		}
	}
	
	public void UpdateSeason() 
	{
		UpdatePool(storage.season);
		ChangeObject(ObjectPool[Random.Range(0, ObjectPool.Count)]);
	}
	
	public void CheckToolTier() 
	{
		toolTier = 0;
		if (storage.items[toolTierIds[0]] == 1) toolTier = 1; 
		if (storage.items[toolTierIds[1]] == 1) toolTier = 2; 
		if (storage.items[toolTierIds[2]] == 1) toolTier = 3; 
		if (storage.items[toolTierIds[3]] == 1) toolTier = 4; 
	}
	
	public void UpdatePool(int season) 
	{
		ObjectPool.Clear();
		
		CheckToolTier();
		
		switch (season) 
		{ 
		case 0:
			for (int i = 0; i<springPool.Length; i++) 
			{
				for (int j = 0; j<springPoolChance[i]; j++) 
				{
					ObjectPool.Add(springPool[i]);
				}
			}
			break;
		case 1:
			for (int i = 0; i<summerPool.Length; i++) 
			{
				for (int j = 0; j<summerPoolChance[i]; j++) 
				{
					ObjectPool.Add(summerPool[i]);
				}
			}
			break;
		case 2:
			for (int i = 0; i<fallPool.Length; i++) 
			{
				for (int j = 0; j<fallPoolChance[i]; j++) 
				{
					ObjectPool.Add(fallPool[i]);
				}
			}
			break;
		case 4:
			break;
		}
		
		for (int i = 0; i<basicPool.Length; i++) 
		{
			for (int j = 0; j<basicPoolChance[i]; j++) 
			{
				ObjectPool.Add(basicPool[i]);
			}
		}
		
		if (toolTier < 1) 
		{
			for (int i = 0; i<copperPool.Length; i++) 
			{
				for (int j = ObjectPool.Count - 1; j>=0;j--) 
				{
					if (copperPool[i] == ObjectPool[j]) 
					{
						ObjectPool.RemoveAt(j);
					}
				}
			}
		}
		
		if (toolTier < 2) 
		{
			for (int i = 0; i<ironPool.Length; i++) 
			{
				for (int j = ObjectPool.Count - 1; j>=0;j--) 
				{
					if (ironPool[i] == ObjectPool[j]) 
					{
						ObjectPool.RemoveAt(j);
					}
				}
			}
		}
		
		if (toolTier < 3) 
		{		
			for (int i = 0; i<goldPool.Length; i++) 
			{
				for (int j = ObjectPool.Count - 1; j>=0;j--) 
				{
					if (goldPool[i] == ObjectPool[j]) 
					{
						ObjectPool.RemoveAt(j);
					}
				}
			}
		}
		
		if (toolTier < 4) 
		{
			for (int i = 0; i<iridiumPool.Length; i++) 
			{
				for (int j = ObjectPool.Count - 1; j>=0;j--) 
				{
					if (iridiumPool[i] == ObjectPool[j]) 
					{
						ObjectPool.RemoveAt(j);
					}
				}
			}
		}
	}
	
	public IEnumerator Size() 
	{
		sprite.transform.localScale = new Vector3(0.9f * spriteSize.x,0.9f * spriteSize.y,0.9f);
		yield return new WaitForSeconds(0.1f);
		sprite.transform.localScale = new Vector3(1f * spriteSize.x,1f * spriteSize.y,1f);
	}
	
	public void CheckJumimos() 
	{
		if (JunimosTier1.Count < junimoQuantity[0]) 
		{
			JunimosTier1.Add(Instantiate(Resources.Load<GameObject>("UI/Prefabs/JunimoFarm1"), new Vector3(transform.parent.transform.position.x + Random.Range(-6f, 6f), transform.parent.transform.position.y + Random.Range(-4f, 4f), 0), new Quaternion(0,0,0,0), workers.transform));
			CheckJumimos();
		}
	}
	
	public IEnumerator JunimoTier1() 
	{
		junimoQuantity[0] = storage.items[junimoIds[0]];  
		if (junimoQuantity[0] == 0) 
		{
			yield return new WaitForSeconds(5);
			StartCoroutine("JunimoTier1");
		}
		else 
		{
			yield return new WaitForSeconds(10/junimoQuantity[0]);
			CheckJumimos();
			Click(1);
			StartCoroutine("JunimoTier1");
		}
	}
	
	public IEnumerator JunimoTier2() 
	{  
		junimoQuantity[1] = storage.items[junimoIds[1]];
		if (junimoQuantity[1] == 0) 
		{
			yield return new WaitForSeconds(5);
			StartCoroutine("JunimoTier2");
		}
		else 
		{
			yield return new WaitForSeconds(10/junimoQuantity[1]);
			CheckJumimos();
			Click(2);
			StartCoroutine("JunimoTier2");
		}
	}
	
	public IEnumerator JunimoTier3() 
	{
		junimoQuantity[2] = storage.items[junimoIds[2]];
		if (junimoQuantity[2] == 0) 
		{
			yield return new WaitForSeconds(5);
			StartCoroutine("JunimoTier3");
		}
		else 
		{
			yield return new WaitForSeconds(10/junimoQuantity[2]);
			CheckJumimos();
			Click(3);
			StartCoroutine("JunimoTier3");
		}
	}
	
}
