using System.Collections.Generic;
using Engine.Core;

public class World
{
    private List<Chunk> chunks = new List<Chunk>();

    public void AddChunk(Chunk chunk)
    {
        chunks.Add(chunk);
    }

    public void Render(Shader shader)
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            chunks[i].Render(shader);
        }
    }
}