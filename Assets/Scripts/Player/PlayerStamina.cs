using UnityEngine;
using System.Collections;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private float _waitTime = 0.5f;
    [SerializeField] private float _recoveryTime = 1.0f;
    [SerializeField] private int _recoveryValue = 20;
    [SerializeField] private int _maxValue = 100;

    private IEnumerator _recoverCor;

    private Player _player;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _player = GetComponent<Player>();
    }

    public void Recover()
    {
        if (_recoverCor == null)
        {
            StartCoroutine(RecoverCor());
        }
    }

    public void Stop()
    {
        if (_recoverCor != null)
        {
            StopAllCoroutines();
            _recoverCor = null;
        }
    }

    private IEnumerator RecoverCor()
    {
        _recoverCor = RecoverCor();
        yield return new WaitForSeconds(_waitTime);
        while (true)
        {
            if (_player.Stamina + _recoveryValue > _maxValue)
            {
                _player.Stamina = _maxValue;
            }
            else
            {
                _player.Stamina += _recoveryValue;
            }
            yield return new WaitForSeconds(_recoveryTime);
        }
    }
}
