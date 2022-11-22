using System.Text.Json;

public class Repository<T>
    where T : class, IIdModel
{
    public List<T> Items { get; private set; } = new List<T>();
    public string FilePath { get; }

    public Repository(string filePath)
    {
        FilePath = filePath;
        using (var stream = File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            List<T>? items = null;
            try
            {
                items = JsonSerializer.Deserialize<List<T>>(stream);
            }
            catch (JsonException ex)
            {
                // Ignore, 'cause if file empty - it's thrown

                Console.WriteLine("File is empty.");
            }
            if (items != null)
            {
                Items = items;
            }
        }
    }

    public void Add(T item)
    {
        item.Id = Guid.NewGuid().ToString();
        Items.Add(item);
        Store();
    }

    public void Update(T item)
    {
        RemoveById(item.Id);
        Add(item);
        Store();
    }

    public T? GetById(string id)
    {
        return Items.FirstOrDefault(x => x.Id == id);
    }

    public void RemoveById(string id)
    {
        var item = GetById(id);
        if (item != null)
        {
            Remove(item);
            Store();
        }
    }
    public void Remove(T item)
    {
        Items.Remove(item);
        Store();
    }

    private void Store()
    {
        using (var stream = File.Open(FilePath, FileMode.Create, FileAccess.Write))
        {
            JsonSerializer.Serialize<List<T>>(stream, Items);
            stream.Flush();
        }
    }

}