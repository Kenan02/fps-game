using UnityEngine;

namespace kenan.daniel.spel{
[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public string Name;
    public float firerate;
    public GameObject prefab;
}
}
