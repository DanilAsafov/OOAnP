namespace SpaceBattle.Lib.Models;

public class GameArea(int regionSize)
{
    private readonly int _regionSize = regionSize > 0 ? regionSize
        : throw new ArgumentException("Размер региона должен быть больше нуля.", nameof(regionSize));
    private readonly Dictionary<(int, int), List<string>> _regions = new();

    public void Register(string objectId, Vector position)
    {
        var key = GetRegionKey(position);
        if (!_regions.TryGetValue(key, out var list))
        {
            list = [];
            _regions[key] = list;
        }
        list.Add(objectId);
    }

    public void Unregister(string objectId, Vector position)
    {
        var key = GetRegionKey(position);
        if (_regions.TryGetValue(key, out var list))
        {
            list.Remove(objectId);
            if (list.Count == 0)
                _regions.Remove(key);
        }
    }

    public IEnumerable<string> GetNearbyObjects(Vector position)
    {
        var (cx, cy) = GetRegionKey(position);
        var result = new List<string>();

        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
            {
                var neighborKey = (cx + dx, cy + dy);
                if (_regions.TryGetValue(neighborKey, out var list))
                    result.AddRange(list);
            }

        return result;
    }

    private (int, int) GetRegionKey(Vector position) => (position[0] / _regionSize, position[1] / _regionSize);
}
