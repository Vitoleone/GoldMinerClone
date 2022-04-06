using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kiskac : MonoBehaviour
{
    float rotateZ;
    public float rotateSpeed,maxZ,minZ,moveSpeed;
    public bool isRight,isStopped,isFired;
    public LineRenderer lineRenderer;
    public Transform lineOrigin;
    float defaultGameObjectX, defaultGameObjectY, defaultGameObjectZ;
    float stoneSlow,goldSlow,diamondSlow;
    

    void Start()
    {
        maxZ = 90;
        minZ = -90;
        defaultGameObjectX = gameObject.transform.position.x;
        defaultGameObjectY = gameObject.transform.position.y;
        defaultGameObjectZ = gameObject.transform.position.z;
        stoneSlow = 1.25f;
        goldSlow = 1;
        diamondSlow = 0.60f;
    }

    // Update is called once per frame
    void Update()
    {
        AutoRotate();
        lineRenderer.SetPositions(new Vector3[] { gameObject.transform.position, lineOrigin.position });

        if (Input.GetMouseButtonDown(0) && isFired == true)//sol click basarsak
        {
            StopRotate();
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        void Move(float slow)
        {
            moveSpeed -= (collision.gameObject.GetComponent<RectTransform>().rect.width + collision.gameObject.GetComponent<RectTransform>().rect.height)*slow / 350f;
            MovingUp();
            
            collision.gameObject.transform.SetParent(gameObject.transform);
            
        }
        if (collision.gameObject.tag == "Gold")
        {
            Move(goldSlow);
            
        }
        else if (collision.gameObject.tag == "Diamond")
        {
            Move(diamondSlow);
        }
        else if (collision.gameObject.tag == "Stone")
        {
            Move(stoneSlow);
        }
        else if(collision.gameObject.tag == "EndPoint")
        {
            MovingUp();
        }

        if(collision.gameObject.tag == "StartPoint")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.transform.position = new Vector3(defaultGameObjectX,defaultGameObjectY,defaultGameObjectZ);
            isRight = true;
            rotateSpeed = 100;
            AutoRotate();
            isFired = true;
        }
    }

    




    void AutoRotate()
    {
        moveSpeed = 4;
        if (rotateZ >= maxZ)
        {    
            isRight = false;
        }
        
        else if (rotateZ <= minZ)
        {
            isRight = true;
        }

        if (isRight == true)
        { 
            rotateZ += Time.deltaTime * rotateSpeed;
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        }

        else
        {
            rotateZ -= Time.deltaTime * rotateSpeed;
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        }

    }

    void StopRotate()
    {
            isFired = false;
            rotateSpeed = 0;
            MovingDown();
    }

    void MovingDown()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = -gameObject.transform.up * moveSpeed;
    }
    void MovingUp()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.transform.up * moveSpeed;
    }
}