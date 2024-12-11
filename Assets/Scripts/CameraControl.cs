using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform target;
    // This is the offset of the camera from the bird
    [SerializeField] Vector3 offset = new Vector3(+20, 0, -40);
    //[SerializeField] float followSpeed = 700f;

    //private Vector3 velocity = Vector3.Zero;
    private Vector3 velocity = new Vector3(0, 0, 0);

    private int counterZoomIn = 0;

    void Start()
    {
        StartCoroutine(CameraZoomInAtStart());
    }


    IEnumerator CameraZoomInAtStart() {
        // Wait a short time, to let the physics engine operate the spring and give some initial speed to the ball.
        while (counterZoomIn < 40)
        {
            yield return new WaitForSeconds(0.05f); 
            offset.x -= 0.5f;
            counterZoomIn++;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            //Vector3 targetPos = target.position + offset;
            //transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 1f/followSpeed);
            transform.position = target.position + offset;
        }
    }
}
