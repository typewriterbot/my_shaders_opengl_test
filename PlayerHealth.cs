using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// this script keeps track of our player's hit points and will destroy if 0 
public class PlayerHealth : MonoBehaviour
{
    private float health = 0f;
    [SerializeField] private float maxHealth = 100f; // fox weak r.i.p.
    // FOR GAME OVER :
    public int iLevelToLoad = 6; //scene where game over exists
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth; 

    }
    // Update is called once per frame
    public void UpdateHealth(float mod)
    {
        health += mod;
        if (health > maxHealth) {
            health = maxHealth; 
        }
        else if (health <= 0f) {
            health = 0f;
            Debug.Log("Player Respawn");
            Destroy(this.gameObject); // kill player?? debug process
            //Change SCENCE to GAME OVER!!~:
            SceneManager.LoadScene(iLevelToLoad);
        }
    }
}
