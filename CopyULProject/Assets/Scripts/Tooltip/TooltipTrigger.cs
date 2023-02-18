using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string content;
    
    
    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData)
    {
        
               TooltipSystem.Show(content);
        
          
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        
        TooltipSystem.Hide();
       
    }
}
