using UnityEngine;
using UnityEngine.EventSystems;
 
public class HandlePanelVisibility : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public bool inContext;
	GameObject myGO;
 
	private void Awake()
	{
		myGO = gameObject;
	}
 
	void Update()
	{
		if (Input.GetMouseButtonUp(0) && !inContext) myGO.SetActive(false);
	}
 
	public void OnPointerEnter(PointerEventData eventData)
	{
		inContext = true;
	}
 
	public void OnPointerExit(PointerEventData eventData)
	{
		inContext = false;
	}
	
}