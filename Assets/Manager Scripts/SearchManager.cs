using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using UnityEngine;

/*
 * SearchManager.cs
 * 
 * This class acts as the central controller of the search engine.
 * 
 * Responsibilities:
 * 
 * - Load documents from disk
 * - Pass documents to the indexer
 * - Process search queries
 * - Return matching documents
 * 
 * The SearchManager communicates with both the DocumentIndexer
 * and the user interface controller.
 */

public class SearchManager : MonoBehaviour
{
    // singleton reference so other scripts can easily access the search system
    public static SearchManager Instance;

    // List storing all loaded documents
    List<Document> documents = new List<Document>();

    // Instance of the indexed system
    DocumentIndexer indexer = new DocumentIndexer();    

    /*
     * Awake()
     * 
     * Called when the object is first initialised
     * Used to assign the singleton instance
     */
    private void Awake()
    {
        Instance = this;
    }

    /*
     * Start()
     * 
     * Called when the scene begins.
     * This loads all documents and builds the search index.
     */
    private void Start()
    {
        LoadDocuments();
        indexer.IndexDocuments(documents);
    }

    /*
     * LoadDocuments()
     * 
     * Reads all .txt files from the documents folder
     * and converts them into document objects
     */
    void LoadDocuments()
    {
        // PAth to the documents folder
        string path = Application.dataPath + "/Documents";

        // Retieve all text files
        string[] files = Directory.GetFiles(path, "*.txt");

        int id = 0;

        // Loop through each file
        foreach (var file in files) 
        {
            // Read the file contents
            string content = File.ReadAllText(file);

            // Create a new document object
            documents.Add(new Document(id, Path.GetFileName(file), content));

            id++;
        }        
    }

    /*
     * Search()
     * 
     * Accepts a user query and returns matching documents.
     */
    public List<SearchResult> Search(string query)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Dictionary<int, float> scores = new Dictionary<int, float>();

        var tokens = query.ToLower().Split(' ');

        foreach (var token in tokens)
        {
            if (!indexer.invertedIndex.ContainsKey(token))
                continue;

            var docList = indexer.invertedIndex[token];

            // Calculate IDF
            float idf = Mathf.Log((float)documents.Count / docList.Count);

            foreach (var docID in docList)
            {
                var doc = documents[docID];

                // Count term frequency in the document
                int tf = CountWordOccurences(doc.content, token);

                float score = tf * idf;

                if (!scores.ContainsKey(docID))
                    scores[docID] = 0;

                scores[docID] += score;
            }
        }

        // sort results by score
        var sorted = scores.OrderByDescending(x => x.Value);

        List<SearchResult> results = new List<SearchResult>();

        foreach (var item in sorted)
            results.Add(new SearchResult(documents[item.Key], item.Value));

        stopwatch.Stop();

        UnityEngine.Debug.Log("Search completed in: " + stopwatch.ElapsedMilliseconds + " ms");

        return results;        
    }

    int CountWordOccurences(string text, string word)
    {
        int count = 0;

        var tokens = text.ToLower().Split(' ');

        foreach (var token in tokens)
        {
            if (token == word)
                count++;
        }        

        return count;
    }
}
