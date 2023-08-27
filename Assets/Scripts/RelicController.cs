using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicController : MonoBehaviour
{
    //GameObject relic1;
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jugador toco reliquia");
            if (gameObject.name == "Relic1" && obj.name == "Brigde")
            {
                gameObject.SetActive(false);
                obj.SetActive(false);
            } else if (gameObject.name == "Relic2" && obj.name == "FlyPlatform")
            {
                gameObject.SetActive(false);
                obj.SetActive(true);
            }
            else if (gameObject.name == "Relic3" && obj.name == "WallDoor")
            {
                gameObject.SetActive(false);
                obj.SetActive(false);
            } else if(gameObject.name == "FinalRelic")
            {
                Debug.Log("Jugador toco reliquia final Bv");
                GameManager.sharedInstance.Win();
            }

        }
    }
}
