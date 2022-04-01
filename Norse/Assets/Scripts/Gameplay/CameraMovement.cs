/* Script to control the main camera's movement
 * Programmer: Brandon Bunting
 * Date Created: 01/28/2022
 * Date Modified: 03/30/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //
    [SerializeField] Transform playerTrans;
    [Range(0f,10f)] public float _speed;

    //Constraints
    public Vector2 _topLeftLimit;
    public Vector2 _bottomRightLimit;
    public Vector3 _offset = new Vector3(0,0,0);

    Vector3 _velocity = new Vector3();

    //
    private void Awake()
    {
        playerTrans = playerTrans.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    // Transform-based camera movement
    private void MoveCamera()
    {
        //Vector3 smoothPos = Vector3.Lerp(transform.position, playerTrans.position, _speed * Time.fixedDeltaTime);
        //smoothPos.z = -10f;

        //if (smoothPos.x > _topLeftLimit.x && smoothPos.x < _bottomRightLimit.x)
        //{
        //    transform.position = new Vector3(smoothPos.x, transform.position.y, smoothPos.z);
        //}
        //if (smoothPos.y < _topLeftLimit.y && smoothPos.y > _bottomRightLimit.y)
        //{
        //    transform.position = new Vector3(transform.position.x, smoothPos.y, smoothPos.z);
        //}

        //Below code from "Muddy Wolf" on Youtube: https://www.youtube.com/watch?v=kAlo_pGF178
        //My own code works, but isn't quite what I wanted. Due to time constraints, this code was used.

        Vector3 targetPos = playerTrans.position + _offset;
        Vector3 clampedPos = new Vector3(Mathf.Clamp(targetPos.x, _topLeftLimit.x, _bottomRightLimit.x), targetPos.y, -10);
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, clampedPos, ref _velocity, _speed * Time.fixedDeltaTime);

        transform.position = smoothPos;
    }
}
