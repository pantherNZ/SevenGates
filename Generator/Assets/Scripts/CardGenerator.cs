using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardGenerator : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Image foregroundImage;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI classText;
    [SerializeField] TextMeshProUGUI deckAndRankText;
    [SerializeField] TextMeshProUGUI effectsText;
    [SerializeField] GameObject onGoingTypeSection;
    [SerializeField] Image onGoingTypeImagePrimary;
    [SerializeField] Image onGoingTypeImageSecondary;
    [SerializeField] TextMeshProUGUI onGoingTypeText;
    [SerializeField] TextMeshProUGUI onGoingEffectText;
    [SerializeField] TextMeshProUGUI levelAndSlotText;

    public void Generate( DataManager dataManager, Dictionary<string, object> cardEntry, Int32 cardIndex )
    {
        // Keys
        var backgroundKey = cardEntry["Primary Element"] as string;
        var foregroundKey = cardEntry["Secondary Element"] as string;
        var classKey = cardEntry["Class"] as string;
        var deckKey = cardEntry["Deck"] as string;
        var rankKey = ( int )cardEntry["Rank"];
        var levelKey = ( int )cardEntry["Level"];
        var slotKey = cardEntry["Slot"] as string;
        var nameKey = cardEntry["Name"] as string;
        var effectsKey = cardEntry["Effects"] as string;
        var ongoingTypeKey = cardEntry["Ongoing Type"] as string;
        var ongoingEffectKey = cardEntry["Ongoing Effect"] as string;
        var numCopiesKey = cardEntry["Copies"] as string;

        // Load background image
        var primaryElement = dataManager.primaryElements.Find( ( x ) => x.id == backgroundKey );
        if( primaryElement != null )
        {
            backgroundImage.sprite = Utility.CreateSprite( primaryElement.image );
            onGoingTypeImagePrimary.sprite = Utility.CreateSprite( primaryElement.ongoingTypeImage );
        }
        else
            Debug.LogError( string.Format( "CardGenerator::Start - Card {0} has an invalid background of {1}, is it missing from the data manager?", cardIndex, backgroundKey ) );

        // Load tree foreground image & ongoing type image (if it is used)
        var secondaryElement = dataManager.treeElements.Find( ( x ) => x.id == foregroundKey );
        var destroyOnGoingType = secondaryElement != null && ( secondaryElement.ongoingTypeImage == null || ongoingTypeKey.Length == 0 );

        if( secondaryElement != null )
        {
            foregroundImage.sprite = Utility.CreateSprite( secondaryElement.treeImage );

            if( destroyOnGoingType )
            {
                onGoingTypeSection.Destroy();
                onGoingEffectText.gameObject.Destroy();
            }
            else
                onGoingTypeImageSecondary.sprite = Utility.CreateSprite( secondaryElement.ongoingTypeImage );
        }
        else
            Debug.LogError( string.Format( "CardGenerator::Start - Card {0} has an invalid foreground / tree image of {1}, is it missing from the data manager?", cardIndex, foregroundKey ) );

        // Set text fields
        titleText.text = nameKey;
        classText.text = classKey;
        deckAndRankText.text = string.Format( "{0} Rank {1}", deckKey, rankKey );
        effectsText.text = effectsKey;
        effectsText.ForceMeshUpdate();
        effectsText.text += new string( '\n', Mathf.Max( 0, 2 - effectsText.textInfo.lineCount ) );

        if( destroyOnGoingType )
            effectsText.text = new string( '\n', Mathf.Max( 0, 2 ) ) + effectsText.text;

        if( onGoingTypeText != null )
            onGoingTypeText.text = ongoingTypeKey;
        if( onGoingEffectText != null )
            onGoingEffectText.text = ongoingEffectKey;
        levelAndSlotText.text = string.Format( "Level {0} - Slot {1}", levelKey, slotKey );
    }
}
