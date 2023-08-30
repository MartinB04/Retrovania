using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RelicController : MonoBehaviour
{
    [SerializeField] GameObject target;

    //GameObject relic1;
    //public GameObject obj;
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
            if(this.gameObject.name == "FinalRelic")
            {
                this.gameObject.SetActive(false);
                Debug.Log("Jugador toco reliquia final Bv");
                GameManager.sharedInstance.Win();
            }
            else if (this.target.name == "FlyPlatform") {
                this.gameObject.SetActive(false);
                this.target.SetActive(true);
                Debug.Log(this.gameObject.name + " y " + this.target.name + " desactivado");
            }
            else
            {
                this.gameObject.SetActive(false);
                this.target.SetActive(false);
                Debug.Log(this.gameObject.name + " y " + this.target.name + " desactivado");
            }

                
                    

            
            
            /*
            else if (gameObject.name == "Relic3" && obj.name == "WallDoor")
            {
                gameObject.SetActive(false);
                obj.SetActive(false);
            } else if(gameObject.name == "FinalRelic")
            {
                Debug.Log("Jugador toco reliquia final Bv");
                GameManager.sharedInstance.Win();
            }*/
        }
            

        
    }


}
