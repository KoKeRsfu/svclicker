using System.Collections;
using System.Collections.Generic;
using THB.SClicker;
using UnityEngine;

[CreateAssetMenu(fileName = "ClickObject", menuName = "ScriptableObjects/ClickObject", order = 1)]
public class ClickScriptableObject : ScriptableObject
{
	public Sprite sprite;
	public float health;
	
	public ClickObjectType clickType;
	public SkillType skillType;
	
	public List<int> givingItemIdPool;
	public List<int> givingChance;
	public List<int> givingQuantityMin;
	public List<int> givingQuantityMax;
	
	public int changingClickId;
}
