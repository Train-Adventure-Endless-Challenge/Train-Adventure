using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[Serializable]
public class TutorialMission
{
    [Multiline]
    public string Description;
    public UnityEvent OnMissionCompleted;
}

public class TutorialBuilder : SceneSingleton<TutorialBuilder>
{
    public TutorialMission[] Missions;

    private int _currentMissionIndex = 0;

    [SerializeField]
    private TextMeshProUGUI _missionText;

    private void Start()
    {
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