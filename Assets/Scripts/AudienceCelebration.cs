using UnityEngine;

public class AudienceCelebration : MonoBehaviour
{
    Vector3 _startPos;
    [SerializeField] Vector3 _movementVector;
    [SerializeField] float periods;
    float _offset;

    float moveFactor;
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _offset = Random.Range(0f, 180f);
        periods = Random.Range(0.3f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (periods > 0)
        {
            float cycles = Time.time / periods; // continuoslly growing over time
            const float tau = Mathf.PI * 2; // constant value of 6.283
            float rawSinwave = Mathf.Sin((cycles * tau) +_offset); // going from -1 to 1
            moveFactor = (rawSinwave + 1f) / 2f; // recalculated to go from 0 to 1 so its 
            Vector3 offset = moveFactor * _movementVector;
            transform.position = _startPos + offset;
        }

    }
}
