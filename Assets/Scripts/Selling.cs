using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Selling : MonoBehaviour
{
    int money;
    public TextMeshProUGUI text;
    public float width,height;
    public bool isHit, isSelled;
    public GameObject goldPopUp;
    TextMesh goldPopUpText;
    float goldValue, stoneValue, diamondValue;
    

    //timer
    public TextMeshProUGUI timeText;
    public float time;
    public GameObject winPanel, losePanel;


    public TextMeshProUGUI mainText;
    
    public int targetPrice;

    void Start()
    {
        Time.timeScale = 1f;
        mainText.text = targetPrice.ToString();
        goldPopUpText = goldPopUp.GetComponent<TextMesh>();

        winPanel.SetActive(false);
        losePanel.SetActive(false);

        goldValue = 1.50f;
        stoneValue = 0.25f;
        diamondValue = 5.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            timeText.text = ((int)(time -= Time.deltaTime)).ToString();

        }

        if (isHit == true)// alınan puanı bastırma
        {

            text.text = money.ToString();
            isHit = false;
            
        }
       

        if(time <= 0 && money < targetPrice )
        {
            losePanel.SetActive(true);
            Time.timeScale = 0;


        }
        else if(time <= 0 && money >= targetPrice)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else if(money >= targetPrice)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        void Sell(float value)
        {

            width = collision.gameObject.GetComponent<RectTransform>().rect.width;//nesnenin genişliğini ve uzunluğunu alıyoruz
            height = collision.gameObject.GetComponent<RectTransform>().rect.height;

            

            money += (int)((width + height)*value);

            //alınan puanı gösterme
            goldPopUp.SetActive(false);
            goldPopUpText.text = "+" + ((int)((width + height))*value).ToString();
            var clone = Instantiate(goldPopUp, transform.position, Quaternion.identity);
            clone.SetActive(true);
            Destroy(clone, 1f);

            isHit = true;
            isSelled = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Gold")
        {
            
            Sell(goldValue);
            
            
        }
        else if (collision.gameObject.tag == "Diamond")
        {
            Sell(diamondValue);

        }
        else if(collision.gameObject.tag == "Stone")
        {
            Sell(stoneValue);
        }
    }

    
}
