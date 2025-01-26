using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DepthIndicator : MonoBehaviour
{
    public TMP_Text depthNumberText;
    public Slider depthSlider;
    
    private float _minY = 64f;  
    private float _maxY = -64f;
    private int _maxDepth = 1280;

    private Transform _playerTransform;

    public void Initialize()
    {
        _playerTransform = App.Instance.Player.transform;
        UpdateDepth();
    }

    private void Update()
    {
        UpdateDepth();
    }

    private void UpdateDepth()
    {
        var depth = Mathf.Clamp(Depth, 0, _maxDepth);
        float sliderValue = (float)depth / _maxDepth;

        depthSlider.value = sliderValue;
        depthNumberText.text = "-" + depth + "m";
    }

    
    private int Depth => Mathf.RoundToInt(Mathf.Lerp(0, 1280, Mathf.InverseLerp(_minY, _maxY, _playerTransform.position.y)));

}