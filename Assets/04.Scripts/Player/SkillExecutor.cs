using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class SkillExecutor : MonoBehaviour
{
    public GameObject skillPrefab; // ��ų ������

    public void Use(Vector3 position) // �ܺο��� �� �޼��带 ȣ���ϸ� �ش� ��ġ�� ����Ʈ�� ����.
    {
        Instantiate(skillPrefab, position, Quaternion.identity); // ����Ʈ �������� ������ ��ġ�� ����.
    }
       
}
