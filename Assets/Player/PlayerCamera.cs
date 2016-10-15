using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {
    public float cameraDist;
    public float cameraRotation;

    Transform cameraPivot;
    Transform cameraHolder;


	void Start () {
        cameraPivot = new GameObject("CameraPivot").transform;
        cameraPivot.transform.parent = this.transform;
        cameraPivot.transform.localPosition = Vector3.zero;

        cameraHolder = new GameObject("CameraHolder").transform;
        cameraHolder.parent = cameraPivot;
        cameraHolder.localPosition = Vector3.back * cameraDist;
        cameraHolder.localRotation = Quaternion.identity;

        cameraPivot.transform.localRotation = Quaternion.Euler(cameraRotation, 0, 0);

        Camera.main.transform.position = cameraHolder.position;
        Camera.main.transform.rotation = cameraHolder.rotation;
    }

    void Update()
    {
        MoveRealCamera();
    }

    void MoveRealCamera()
    {
        float distance = Vector3.Distance(Camera.main.transform.position, cameraHolder.position);
        if (distance <= 0)
            return;
        float moveDistance = 5 * distance * Time.deltaTime;
        float fraction = moveDistance / distance;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraHolder.position, fraction);
    }
}
