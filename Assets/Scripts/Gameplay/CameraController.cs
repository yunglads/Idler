using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public float moveSpeed = 2f;
    public bool isMoving = false;

    private void Awake()
    {
        Instance = this;
    }

    public void MoveToTarget(Transform target)
    {
        if (!isMoving)
            StartCoroutine(MoveCamera(target));
    }

    private IEnumerator MoveCamera(Transform target)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, target.position) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }
}
