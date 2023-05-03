using UnityEngine;

public class PlayerRolling : MonoBehaviour
{
    #region Variable
    [Header("Key")]
    [SerializeField] private KeyCode _rollingKey;

    [Header("Attribute")]
    [SerializeField] private float rollingRange;

    private Animator animator;
    #endregion
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Rolling()
    {
        // 콜라이더 빼기
        // 애니메이션 
        // 이동
    }
}
