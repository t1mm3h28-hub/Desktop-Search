using System.Collections.Generic;
using System.Xml;

/*
 * DocumentIndexer.cs
 * 
 * This class is responsible for building the search engine's
 * INVERTED INDEX.
 * 
 * An inverted index maps words documents that contain those words.
 * 
 * Example:
 * 
 * fox  > [doc1, doc3]
 * dog  > [doc2, doc3]
 * car  > [doc4]
 * 
 * This structure allows the search engine to quickly retrieve
 * documents containing a specific word without scanning every file.
 */

public class DocumentIndexer
{
    /*
     * Dictionary used to store the inverted index
     * 
     * Key = word
     * Value = list of document IDs containing that word
     */
    public Dictionary<string, List<int>> invertedIndex = new Dictionary<string, List<int>>();

    /*
     * Index documents()
     * This method processes every document and builds the index
     * 
     */
    public void IndexDocuments(List<Document> documents)
    {
        // Loop through each document
        foreach (var doc in documents)
        {
            // Break the document text into searchable tokens
            var tokens = Tokenize(doc.content);

            // process each word
            foreach (var word in tokens)
            {
                // If the word does not exist in the index yet, create it
                if (!invertedIndex.ContainsKey(word))
                    invertedIndex[word] = new List<int>();

                // Add the document ID if not already stored
                if (!invertedIndex[word].Contains(doc.id))
                    invertedIndex[word].Add(doc.id);
            }
        }
    }

    /*
     * Tokenize()
     * 
     * Converts raw document text into individual searchable words
     * 
     * Steps:
     * 1. Convert text to lowercase
     * 2. Split text into words
     * 3. Remove punctuation
     */
    string[] Tokenize(string text)
    {
        // Convert all text to lowercasae
        text = text.ToLower();

        // Define characters that separate words
        char[] separators = { ' ', ',', '.', '!', '?', ';', ':' };

        // Split thetext into individual tokens
        return text.Split(separators);
    }

}
