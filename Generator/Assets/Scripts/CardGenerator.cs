using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    void Start()
    {
        List<Dictionary<string, object>> data = CSVReader.Read( "Data/CardData" );

        for( var i = 0; i < data.Count; i++ )
        {
            Debug.Log( i.ToString() );

            foreach( var (key, value) in data[i] )
                Debug.Log( string.Format( "\t{0}: {1}", key, value.ToString() ) );
        }
    }
}
