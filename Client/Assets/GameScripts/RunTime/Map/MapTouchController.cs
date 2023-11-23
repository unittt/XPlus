using System;
using HT.Framework;
using UnityEngine;

public sealed class MapTouchController : MonoBehaviour
{
    private Camera _cam;
    /// <summary>
    /// 用于筛选点击事件响应的层
    /// </summary>
    public LayerMask LayerMask;
    public bool IsEnable = true;
    public event Action<Vector2, GameObject> OnMouseDown;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (!IsEnable)
        {
            return;
        }

        //如果点击在ui上跳出
        if (UIToolkit.IsStayUI)
        {
            return;
        }

        //当鼠标点下时
        if (_cam is null || !Input.GetMouseButtonDown(0)) return;
        var mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        // 执行射线投射检测点击
        var hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, LayerMask);
        OnMouseDown?.Invoke(mousePosition, hit.collider?.gameObject);
    }
}
