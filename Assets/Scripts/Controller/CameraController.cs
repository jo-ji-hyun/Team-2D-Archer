using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // === inspectorâ�� Ÿ���� ���� ===
    public Transform target;
    float _offsetX;
    float _offsetY;

    void Start()
    {
        if (target == null)
            return;

        // === Ÿ����� x, y �Ÿ� ===
        _offsetX = transform.position.x - target.position.x;
        _offsetY = transform.position.y - target.position.y;
    }

    void Update()
    {
        if (target == null)
            return;

        Vector3 pos = transform.position;

        // === 1. ������ ī�޶� ��ġ�� x, y �Ÿ��� ���� ===
        pos.x = target.position.x + _offsetX;
        pos.y = target.position.y + _offsetY;

        // === 2. �� ��ġ ��ȯ ===
        transform.position = pos;
    }
}
