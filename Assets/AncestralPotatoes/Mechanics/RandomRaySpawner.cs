using UnityEngine;

[ExecuteInEditMode]
public class RandomRaySpawner : MonoBehaviour
{
    public bool activated;
    public Vector3 point;

    private void Update()
    {
        if (!activated) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            activated = false;
            return;
        }
    }

    private void OnDrawGizmos()
    {
        if (!activated) return;
        Gizmos.DrawWireSphere(point, .5f);
    }
}
