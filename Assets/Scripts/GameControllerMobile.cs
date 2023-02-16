using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerMobile : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] public Transform selectedObj;
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] public bool isTank;
    [SerializeField] public bool isStriker;
    [HideInInspector] public Vector3 targetPos;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        LeftClick();
        if (selectedObj != null)
        {
            RightClick();
            if (RightClick() != Vector3.zero)
            {
                Move();
            }
        }
    }
    private void LeftClick()
    {
        if (Input.touchCount > 0)
        {
            Vector2 touchPos = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            Collider2D[] hitColliders = Physics2D.OverlapPointAll(touchPos);

            bool foundSelected = false;
            for (int i = 0; i < hitColliders.Length; i++)
            {
                Collider2D hitCollider = hitColliders[i];
                if (hitCollider.CompareTag("Allies"))
                {
                    Transform hitTransform = hitCollider.transform;

                    if (selectedObj != null)
                    {
                        Transform childObj = selectedObj.GetChild(1);
                        if (childObj != null)
                        {
                            childObj.gameObject.SetActive(false);
                        }
                    }

                    selectedObj = hitTransform;
                    selectedObj.GetChild(1).gameObject.SetActive(true);

                    if (selectedObj.name.Equals("Striker Bike"))
                    {
                        isStriker = true;
                        isTank = false;
                    }
                    else if (selectedObj.name.Equals("Heavy Tank"))
                    {
                        isStriker = false;
                        isTank = true;
                    }
                    targetPos = selectedObj.position;

                    foundSelected = true;
                    break;
                }
            }

            if (!foundSelected)
            {
                if (selectedObj != null)
                {
                    Transform childObj = selectedObj.GetChild(1);
                    if (childObj != null)
                    {
                        childObj.gameObject.SetActive(false);
                    }
                }

                selectedObj = null;
                isStriker = false;
                isTank = false;
            }
        }

    }

    private Vector3 RightClick()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            targetPos = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            selectedObj.rotation = Quaternion.Euler(0, 0, RotationUnit(selectedObj.position.x,
                                                                       selectedObj.position.y,
                                                                       targetPos.x, targetPos.y));
        }
        return targetPos;
    }



    private void Move()
    {
        if (isTank)
            MoveSpeedChoose(moveSpeed);
        else if (isStriker)
            MoveSpeedChoose(moveSpeed * 2);
    }

    private void MoveSpeedChoose(float speed)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                selectedObj.position = Vector2.MoveTowards(selectedObj.position,
                                                           mainCamera.ScreenToWorldPoint(touch.position),
                                                           speed * Time.deltaTime);
            }
        }
    }


    private float RotationUnit(float objX, float objY, float targetX, float targetY)
    {
        float distanceX = targetY - objY;
        float distanceY = targetX - objX;
        float degree = Mathf.Atan2(distanceY, distanceX) * Mathf.Rad2Deg * -1;//açı ters hesaplandığı için eksiyle çarpıldı
        if (degree < 0)
        {
            degree += 360;
        }
        return degree;
    }
}
