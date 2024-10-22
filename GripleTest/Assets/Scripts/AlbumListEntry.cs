using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlbumListEntry : MonoBehaviour
{
    public AlbumDataModel Model { get; private set; }

    [SerializeField]
    private TMP_Text _nameField;
    [SerializeField]
    private Button _loadButton;
    [SerializeField]
    private Button _deleteButton;

    private AlbumMenu _menu;

    public void Initialize(AlbumDataModel model, AlbumMenu menu)
    {
        Model = model;
        _menu = menu;
        _nameField.text = model.title;
        _deleteButton.onClick.AddListener(OnDelete);
    }

    private void OnDelete()
    {
        _menu.DeleteEntry(this);
    }
}
