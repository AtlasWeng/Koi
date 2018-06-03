using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class DoubleClick : MonoBehaviour, IPointerClickHandler {

    public virtual void OnPointerClick(PointerEventData data)
    {
    	Debug.Log("one clicked");
        if (data.clickCount == 2)
        {
            Debug.Log("double click");
        }
    }
  
}