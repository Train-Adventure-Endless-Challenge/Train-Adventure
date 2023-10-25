using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[Serializable]
public class TutorialMission
{
    [Multiline]
    public string Description;
    public UnityEvent OnMissionStarted;
    public UnityEvent OnMissionCompleted;
}

public class TutorialBuilder : MonoBehaviour
{
    public TutorialMission[] Missions;

    private int _currentMissionIndex = 0;

    [SerializeField]
    private TextMeshProUGUI _missionText;

    private void Start()
    {
        // 튜토리얼 진행을 위한 기어 지급
        GearManager.Instance.AddGear(100);

        ActivateMission();
    }

    public void CompleteMission()
    {
        Missions[_currentMissionIndex].OnMissionCompleted?.Invoke();
    }

    private void ActivateMission()
    {
        if (Missions.Length > 0)
        {
            var currentMission = Missions[_currentMissionIndex];
            currentMission.OnMissionStarted?.Invoke();
            currentMission.OnMissionCompleted.AddListener(OnMissionCompleted);
            _missionText.text = currentMission.Description;
        }
    }

    private void OnMissionCompleted()
    {
        if (_currentMissionIndex < Missions.Length)
        {
            _currentMissionIndex++;
            ActivateMission();
        }
    }
}