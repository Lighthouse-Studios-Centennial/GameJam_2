using UnityEngine;

[CreateAssetMenu(fileName = "DayNightObjectHandler", menuName = "Scriptable Objects/DayNightObjectHandler")]
public class DayNightObjectHandler : ScriptableObject
{
    public string cycleName;
    public Color lightColor;
    public string objectToEnable;
    public AudioClip cycleSound;    
}
