using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * SearchUIController.cs
 * 
 * This script manages all user interface interactions.
 * 
 * Responsibilities:
 * 
 * - Receive search input from the user
 * - Send the query to the SearchManager
 * - Display search results in the scroll view
 */

public class SearchUIController : MonoBehaviour
{
    // Input field where the user types a search query
    public TMP_InputField inputField;

    // PArent container where search result in Ui elements will appear
    public Transform resultsParent;

    // Prefab used to create a results item
    public GameObject resultPrefab;

    /*
     * SearchClicked()
     * 
     * Called when the user presses the search button.
     */

    public void SearchClicked()
    {
        // Remove any previous results from previous searches
        ClearResults();

        // Send query to the search engine
        List<SearchResult> results = SearchManager.Instance.Search(inputField.text);

        // Create UI elements for each result
        foreach (var result in results) 
        {
            GameObject item = Instantiate(resultPrefab, resultsParent);

            // Retrieve the text component
            var text = item.GetComponentInChildren<TextMeshProUGUI>();

            // Display the document name
            text.text = result.document.fileName + " Score: " + result.score.ToString("F2");
        }
    }

    /*
    * ClearResults()
    * 
    * Removes all existing search results from the UI.
    */
    void ClearResults()
    {
        foreach (Transform child in resultsParent)
            Destroy(child.gameObject);
    }
}


