using Unity.VisualScripting;
using UnityEngine;

public class GlobalManager : DontDestroySingleton<GlobalManager>
{
    private bool _isStart = true;
    private bool _isUI = false;

    public bool IsStart { get { return _isStart; } }
    public bool IsUI { get { return _isUI; } }

    public void SetStart(bool value) => _isStart = value;
    public void SetOnUI(bool value) => _isUI = value;
}
