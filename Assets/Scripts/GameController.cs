using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] public Transform selectedObj;
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] public bool isTank;
    [SerializeField] public bool isStriker;
    [HideInInspector] Vector3 targetPos;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
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
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] hitColliders = Physics2D.OverlapPointAll(mousePos);

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
        if (Input.GetMouseButton(1))
        {
            targetPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
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
        selectedObj.position = Vector2.MoveTowards(selectedObj.position,
                                                   RightClick(),
                                                   speed * Time.deltaTime);
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
