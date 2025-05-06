using UnityEngine;
using System;

public class IndexRandomizer
{
    /// <summary>
    /// Randomly retrieves a new index. Does not take into account previous index.
    /// </summary>
    /// <param name="length"></param>
    public int GetNewIndex(int length)
    {
        return UnityEngine.Random.Range(0, length);
    }
    /// <summary>
    /// Recursively gets a new index different to the previous one.
    /// </summary>
    /// <param name="previousIndex"></param>
    public int GetNewIndex(int length, int previousIndex)
    {
        if (length <= 1)
        {
            Debug.Log("Length must be greater than 1 to get a different index.");
            return 0;
        }
            
            

        int newIndex = UnityEngine.Random.Range(0, length);

        if (newIndex == previousIndex)
            return GetNewIndex(length, previousIndex); // Recursive call if same as previous

        return newIndex;
    }
        
    
}
