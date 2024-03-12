using System.Collections;
using System.Collections.Generic;
using THB.SClicker;
using UnityEngine;

[CreateAssetMenu(fileName = "Craft", menuName = "ScriptableObjects/Craft", order = 1)]
public class CraftScriptableObject : ScriptableObject
{
	public Sprite sprite;
	
	public bool oneTime;
	public bool unlocksOther;
	
	public List<int> craftingItemIdPool;
	public List<int> craftingQuantity;
	
	public int craftingId;
	
	public List<int> unlockingId;
}
