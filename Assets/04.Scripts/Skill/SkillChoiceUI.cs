using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillChoiceUI : MonoBehaviour
{
    public static SkillChoiceUI Instance;

    public GameObject panel; // 스킬 선택 패널
    public Button[] buttons; // 스킬 선택 버튼들
    public Text[] nameTexts; // 스킬 이름 텍스트
    private List<Skill> currentchoices; // 현재 선택 가능한 스킬 목록

    void Awake()
    {
        Instance = this; // 싱글톤 인스턴스 설정
        Debug.Log("SkillChoiceUI Awake가 호출됨");
        panel.SetActive(false); // 초기에는 패널을 숨김
    }

    public void ShowChoices(List<Skill> skills)
    {
        currentchoices = skills; // 현재 선택 가능한 스킬 목록 설정
        panel.SetActive(true); // 패널을 활성화
        Time.timeScale = 0f; // 게임 일시 정지

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < skills.Count)
            {
                buttons[i].gameObject.SetActive(true); // 버튼 활성화
                nameTexts[i].text = skills[i].skillName; // 스킬 이름 설정
                int idx = i; // 로컬 변수로 인덱스 저장
                buttons[i].onClick.RemoveAllListeners(); // 기존 리스너 제거
                buttons[i].onClick.AddListener(() => OnClickSelect(idx)); // 클릭 리스너 추가
            }
            else
            {
                buttons[i].gameObject.SetActive(false); // 버튼 비활성화
            }
        }
    }

    void OnClickSelect(int idx)
    {
        SkillManager.Instance.AcquireSkill(currentchoices[idx]); // 선택한 스킬 획득
        panel.SetActive(false); // 패널 숨김
        Time.timeScale = 1f; // 게임 재개
    }
}
