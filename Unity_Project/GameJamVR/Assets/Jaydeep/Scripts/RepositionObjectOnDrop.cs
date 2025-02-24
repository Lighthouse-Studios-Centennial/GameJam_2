using UnityEngine;

public class RepositionObjectOnDrop : MonoBehaviour
{
    [SerializeField] private bool snapToSpawn;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Quaternion targetRot;

    private void Awake()
    {
        if (!snapToSpawn) return;

        targetPos = transform.position;
        targetRot = transform.rotation;
    }

    public void Reposition()
    {
        transform.SetPositionAndRotation(targetPos, targetRot);
    }
}
