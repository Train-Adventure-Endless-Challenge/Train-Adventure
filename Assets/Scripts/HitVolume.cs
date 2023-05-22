using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HitVolume : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerData _playerData;

    private Volume _volume;
    private Vignette _vignette;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _vignette.smoothness.value = 1 - _player.Hp / _playerData.Hp;
    }

    private void Init()
    {
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out _vignette);
    }
}