using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DataManager : MonoBehaviour
{
    [SerializeField] TextAsset csvFile;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GridLayoutGroup gridLayout;

    [Serializable]
    public class PrimaryElement
    {
        public string id;
        public Texture2D image;
        public Texture2D ongoingTypeImage;
    }

    public List<PrimaryElement> primaryElements = new List<PrimaryElement>();

    [Serializable]
    public class TreeElement
    {
        public string id;
        public Texture2D treeImage;
        public Texture2D ongoingTypeImage;
    }

    public List<TreeElement> treeElements = new List<TreeElement>();

    private void Start()
    {
        // Verify all entries are valid
        foreach( var( idx, entry ) in primaryElements.Enumerate() )
        {
            if( entry.id.Length == 0 )
                Debug.LogError( string.Format( "DataManager - Primary element at index {0} contains an invalid entry: empty id / key / name", idx ) );
            else
            {
                if( entry.image == null )
                    Debug.LogError( string.Format( "DataManager - Primary element at index {0} contains an invalid entry: empty / invalid image", idx ) );
                if( entry.ongoingTypeImage == null )
                    Debug.LogError( string.Format( "DataManager - Primary element at index {0} contains an invalid entry: empty / invalid on-going type image", idx ) );
            }
        }

        foreach( var (idx, entry) in treeElements.Enumerate() )
        {
            if( entry.id.Length == 0 )
                Debug.LogError( string.Format( "DataManager - Tree element entry at index {0} contains an invalid entry: empty id / key / name", idx ) );
            else
            {
                if( entry.treeImage == null )
                    Debug.LogError( string.Format( "DataManager - Tree element entry at index {0} contains an invalid entry: empty / invalid tree image", idx ) );
                if( entry.ongoingTypeImage == null )
                    Debug.LogError( string.Format( "DataManager - Tree element entry at index {0} contains an invalid entry: empty / invalid on-going type image", idx ) );
            }
        }

        // Load card data from csv file
        var csvData = CSVReader.Read( csvFile );

        if( csvData == null || csvData.IsEmpty() )
            Debug.LogError( "DataManager - Failed to load/read card data from csv file" );

        // Generate cards
        foreach( var( idx, card ) in csvData.Enumerate() )
        {
            var numCopies = ( int )card["Copies"];
            if( numCopies > 0 )
            {
                for( int i = 0; i < numCopies; ++i )
                {
                    var newCard = Instantiate( cardPrefab );
                    newCard.GetComponent<CardGenerator>().Generate( this, card, idx );
                    newCard.transform.SetParent( gridLayout.transform, false );
                }
            }
            else
                Debug.LogError( string.Format( "DataManager - Failed to read number of copies for card index {0} with name {1} - copies is not a valid number: {2}", idx, card["Name"] as string, numCopies ) );
        }
    }
}
