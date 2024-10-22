using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using System.Text;

public class AlbumMenu : MonoBehaviour
{
    [SerializeField]
    private Button _loadButton;
    [SerializeField]
    private Button _printButton;
    [SerializeField]
    private Transform _listRoot;
    [SerializeField]
    private AlbumListEntry _entryPrefab;

    private List<AlbumListEntry> _listEntries;

    private void Awake()
    {
        _loadButton.onClick.AddListener(OnLoad);
        _printButton.onClick.AddListener(OnPrint);
        _printButton.interactable = false;
        _loadButton.interactable = true;
    }

    private void OnLoad() => LoadAlbums();

    private void OnPrint()
    {
        List<AlbumDataModel> albums = new();
        
        foreach (var album in _listEntries)
        {
            albums.Add(album.Model);
        }
        Debug.Log(JsonConvert.SerializeObject(albums));
    }

    private async void LoadAlbums()
    {
        HttpClient httpClient = new();
        var result = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/albums");


        List<AlbumDataModel> albums = new();
        if (result.IsSuccessStatusCode)
        {
            var jsonText = await result.Content.ReadAsStringAsync();
            albums = JsonConvert.DeserializeObject<List<AlbumDataModel>>(jsonText);
        }
        else
        {
            Debug.LogError(result.StatusCode);
            return;
        }

        _listEntries = new();
        foreach (var album in albums)
        {
            var entry = Instantiate(_entryPrefab, _listRoot);
            entry.Initialize(album, this);
            _listEntries.Add(entry);
        }
        _printButton.interactable = true;
        _loadButton.interactable = false;
    }

    public void DeleteEntry(AlbumListEntry entry)
    {
        _listEntries.Remove(entry);
        Destroy(entry.gameObject);
    }
}
