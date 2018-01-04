using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'Game Controller' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Boundary"))
        {
            if (other.CompareTag("Player"))
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
            }
            Instantiate(explosion, transform.position, transform.rotation);
            gameController.AddScore(scoreValue);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
