/* Script to control the main camera's movement
 * Programmer: Brandon Bunting
 * Date Created: 01/28/2022
 * Date Modified: 02/19/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //
    [SerializeField] Transform playerTrans;

    [Range(0f,10f)] public float _smoothFactor;

    //
    private void Awake()
    {
        playerTrans = playerTrans.GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveCamera();
    }

    //
    private void MoveCamera()
    {
        Vector3 targetPos = playerTrans.position;
        Vector3 smoothPos = Vector3.Lerp(transform.position, playerTrans.position, _smoothFactor*Time.fixedDeltaTime);
        smoothPos.z = -10f;
        transform.position = smoothPos;
    }
}
