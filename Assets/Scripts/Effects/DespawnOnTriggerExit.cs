using UnityEngine;

public class DespawnOnTriggerExit : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision) => Destroy(this.gameObject);
}
