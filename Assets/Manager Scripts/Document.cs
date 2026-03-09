/*
 * Document.cs
 * 
 * This class represents a single document that will be indexed and searched
 * by the desktop search engine. Each document contains:
 * 
 * A unique ID used internally by the system
 * The file name of the document
 * The full text content of the document
 * 
 * The ID allows the search engine to reference documents efficiently when
 * building and querying the inverted index.
 */

public class Document
{
    // Unique identifier for the document
    public int id;

    // Name of the file (e.g. animals.txt)
    public string fileName;

    // The full text content extracted from the document
    public string content;

    /*
     * Constructor
     * 
     * Called when a new Document object is created.
     * This assigns the ID, filename, and content.
     */
    public Document(int id, string fileName, string content)
    {
        this.id = id;
        this.fileName = fileName;
        this.content = content;
    }
}
