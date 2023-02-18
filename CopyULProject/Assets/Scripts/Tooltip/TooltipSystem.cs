using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;
    //public TextMeshProUGUI header;
    public Text My_Text;
    public GameObject Panel;
    public RectTransform rectTransfrom;
     

    // Start is called before the first frame update
    public void Awake()
    {
        current = this;
        rectTransfrom = GetComponent<RectTransform>();
    }
    

    public static void Show(string content="")
    {
        current.SetText(content);
        current.gameObject.SetActive(true);
        //Panel.SetActive(true);

    }
    public static void Hide()
    {
       
        current.gameObject.SetActive(false);
       // Panel.SetActive(false);

    }
    public void SetText(string content = "")
    {
        if (string.IsNullOrEmpty(content))
        {
            //My_Text.enabled = false;
            
            My_Text.gameObject.SetActive(false);
            //header.gameObject.SetActive(false);
        }
        else
        {
            
            My_Text.gameObject.SetActive(true);
            // header.gameObject.SetActive(true);
            My_Text.text=content;
           // header.text = content;
        }
    }
    private void Update()
    {
     Vector3 position = Input.mousePosition  ;
        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;
        //float pivotX = x / Screen.width;
        //float pivotY = y / Screen.height;
        rectTransfrom.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;  
    }
}
