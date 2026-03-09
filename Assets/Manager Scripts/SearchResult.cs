/*
 * SearchResult.cs
 *
 * Represents a ranked search result returned by the search engine.
 * It contains the document and its calculated TF-IDF score.
 */

public class SearchResult
{
    public Document document;
    public float score;

    public SearchResult(Document document, float scoreValue)
    {
        this.document = document;
        this.score = scoreValue;
    }
}
