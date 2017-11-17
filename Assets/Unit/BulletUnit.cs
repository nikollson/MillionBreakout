using System;
using UnityEngine;
using System.Collections;
using Stool.MilllionBullets;
using Stool.MilllionBullets.Sample;

public class BulletUnit : MonoBehaviour
{
    [SerializeField] private Transform _ballShotPosition;
    [SerializeField] private int _ballNum;
    [SerializeField] private float _timeSpan;
    [SerializeField] private float _firstTime;
    [SerializeField] private float _angleWidth;
    [SerializeField] private float _speed;

    private float _timeCount = 0;

    void Awake()
    {
        _timeCount = _firstTime;
    }

    void Update()
    {
        _timeCount += Time.deltaTime;
        if (_timeCount > _timeSpan)
        {
            ShotBalls();
            _timeCount -= _timeSpan;
        }
    }

    void ShotBalls()
    {
        var states = new BulletState[_ballNum];
        var options = new ColorBallOption[_ballNum];

        for (int i = 0; i < _ballNum; i++)
        {
            float angle = -_angleWidth + _angleWidth * 2 * i / (_ballNum - 1);
            angle *= Mathf.PI / 180;
            Vector3 velocity = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle))*_speed;
            float radius = MillionBulletsManager.Instance.ColorBallFunctions.GetRadius();

            states[i] = new BulletState(_ballShotPosition.position, velocity,radius);
            options[i] = new ColorBallOption(Color.white);
        }

        MillionBulletsManager.Instance.ColorBallBuffer.Adder.AddBullets(states, options);
    }
}
