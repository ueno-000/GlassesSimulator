using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrassMovement : MonoBehaviour
{

    [SerializeField]
    float _jointSpeed = 1.0f;
    [SerializeField]
    float _angleRMax;
    [SerializeField]
    float _angleRMin;
    [SerializeField]
    float _angleLMax;
    [SerializeField]
    float _angleLMin;
    [SerializeField]
    Transform _jointR;
    [SerializeField]
    Transform _jointL;
    [SerializeField, Range(0, 1)]
    float _angleR;
    [SerializeField, Range(0, 1)]
    float _angleL;


    private void Start()
    {
        SetAngleR(1);
            Debug.Log($"{GetAngleR()}, ");
        SetAngleL(1);
        StartCoroutine(Bend());
    }


    IEnumerator Bend()
    {
        while (true)
        {
            var r = Mathf.Sign(_angleR - GetAngleR());
            var l = Mathf.Sign(GetAngleL() - _angleL);
            SetAngleR(GetAngleR() + _jointSpeed * r);
            SetAngleL(GetAngleL() + _jointSpeed * l);
            yield return null;
        }
    }

    void SetAngleR(float rate)
    {
        rate = Mathf.Clamp01(rate);
        if (_jointR)
        {
            var angle = _jointR.eulerAngles;
            angle.y = Mathf.Lerp(_angleRMin, _angleRMax, rate);
            _jointR.eulerAngles = angle;
        }
    }

    void SetAngleL(float rate)
    {
        rate = Mathf.Clamp01(rate);
        if (_jointL)
        {
            var angle = _jointL.eulerAngles;
            angle.y = Mathf.Lerp(_angleLMin, _angleLMax, rate);
            _jointL.eulerAngles = angle;
        }
    }

    float GetAngleR()
    {
        return Mathf.InverseLerp(_angleRMax, _angleRMin, _jointR.eulerAngles.y);
    }

    float GetAngleL()
    {
        return Mathf.InverseLerp(_angleLMin, _angleLMax, _jointL.eulerAngles.y);
    }
}
