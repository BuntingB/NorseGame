/* Script to control the main camera's movement
 * Programmer: Brandon Bunting
 * Date Created: 01/28/2022
 * Date Modified: 03/20/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //
    [SerializeField] Transform playerTrans;
    [Range(0f,10f)] public float _smoothFactor;

    //Constraints
    public Vector2 _topLeftLimit;
    public Vector2 _bottomRightLimit;

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

    // Transform-based camera movement
    private void MoveCamera()
    {
        Vector3 targetPos = playerTrans.position;
        Vector3 smoothPos = Vector3.Lerp(transform.position, playerTrans.position, _smoothFactor * Time.fixedDeltaTime);
        smoothPos.z = -10f;
        if (smoothPos.x > _topLeftLimit.x && smoothPos.x < _bottomRightLimit.x) 
        {
            transform.position = new Vector3(smoothPos.x, transform.position.y, smoothPos.z);
        }
        if (smoothPos.y < _topLeftLimit.y && smoothPos.y > _bottomRightLimit.y)
        {
            transform.position = new Vector3(transform.position.x, smoothPos.y, smoothPos.z);
        }
        
    }
}
