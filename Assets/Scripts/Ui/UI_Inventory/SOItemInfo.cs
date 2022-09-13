using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOItemInfo", menuName = "Inventory/New Item SO")]
public class SOItemInfo : ScriptableObject
{
    [SerializeField]
    public string item_name;
    [SerializeField]
    public string item_description;
    [SerializeField]
    public Sprite default_sprite;
    [SerializeField]
    public Sprite on_hover_sprite;
    [SerializeField]
    public GameObject path_to_3d_version_prefab;



}
