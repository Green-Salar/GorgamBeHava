using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    public Material gorgMaterial;
    public Material lambMaterial;

    public bool isGorg = false;
    private Renderer playerRenderer; 
    public Text TagText; 
    public Text ScoreText;
    public float gameScore;

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();

        UpdateMaterial();
    }
    public void Update()
    {
        if (TagText == null || ScoreText == null) return;
        if (isGorg)
        {
            gameScore -= Time.deltaTime;
            ScoreText.text = "Score: " + gameScore.ToString("F2");
        }
        else
        {
            gameScore += Time.deltaTime;
            ScoreText.text = "Score: " + gameScore.ToString("F2");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
       
        if (isGorg&&other.gameObject.CompareTag("Player"))
        {
            PlayerManager otherPlayer = other.gameObject.GetComponent<PlayerManager>();
             Debug.Log("OnTriggerEnter2");
           
                SwapRoles(otherPlayer);
            
        }
        if (other.gameObject.CompareTag("SpeedBoost"))
        {
            Debug.Log("SpeedBoost");
            //gameObject.GetComponent<PlayerMovement>().moveSpeed = 20f;
            gameObject.GetComponent<PlayerMove>().SpeedBoost();
            Destroy(other.gameObject);
        }
        // See if its in blue zone
        if (other.gameObject.CompareTag("BlueZone"))
        {
            Debug.Log("BlueZone");
            gameScore += 10;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BlueZone"))
        {
            Debug.Log("BlueZone");
            gameScore += 10;
        }
    }

    private void SwapRoles(PlayerManager otherPlayer)
    { 
        Debug.Log("SwapRoles"); 
        StartCoroutine(ChangeRole(otherPlayer)); 


    }

    private IEnumerator ChangeRole(PlayerManager otherPlayer)
    {

        isGorg = false;
        UpdateMaterial();
        yield return new WaitForSeconds(1f);

        otherPlayer.isGorg = true;

        otherPlayer.UpdateMaterial();

    }

    private void UpdateMaterial()
    {
        if (isGorg)
        {
            playerRenderer.material = gorgMaterial;
            if (TagText!=null&& ScoreText != null)
            {

                TagText.text = "Gorg";
                TagText.color = Color.red;
                ScoreText.color = Color.red;
            }
        }
        else
        {
            playerRenderer.material = lambMaterial;
            if (TagText != null && ScoreText != null)
            {
                TagText.text = "Lamb";
                TagText.color = Color.green;
                ScoreText.color = Color.green;
            }
        }
    }
}
