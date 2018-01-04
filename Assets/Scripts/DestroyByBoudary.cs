using UnityEngine;

public class DestroyByBoudary : MonoBehaviour {
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
