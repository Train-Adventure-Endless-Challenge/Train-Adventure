using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds;

    [SerializeField] private Vector3 firstBackgroundPosition;
    [SerializeField] private float speed;

    private int backgroundIndex;

    private void Start()
    {
        firstBackgroundPosition = backgrounds[0].transform.localPosition;

        backgroundIndex = 0;
    }

    private void Update()
    {
        if (backgrounds[backgroundIndex].localPosition.z <= firstBackgroundPosition.z)
        {
            Vector3 nextPosition = backgrounds[backgroundIndex].localPosition;
            nextPosition = new Vector3(nextPosition.x, nextPosition.y, nextPosition.z + backgrounds[0].transform.localScale.z * backgrounds.Length);
            backgrounds[backgroundIndex].localPosition = nextPosition;
            backgroundIndex++;
            backgroundIndex %= backgrounds.Length;
        }

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].Translate(-Vector3.forward * speed * Time.deltaTime);
        }
    }
}
