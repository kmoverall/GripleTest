using UnityEngine;

public class AlbumListEntry : MonoBehaviour
{
    public AlbumDataModel Model { get; private set; }

    public void Initialize(AlbumDataModel model)
    {
        Model = model;
    }
}
