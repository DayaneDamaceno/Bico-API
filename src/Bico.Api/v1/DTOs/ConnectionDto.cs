namespace Bico.Api.v1.DTOs;

public class ConnectionDto<T>
{
    private readonly Dictionary<T, HashSet<string>> _connections = [];

    public int Count
    {
        get
        {
            return _connections.Count;
        }
    }

    public void Add(T key, string connectionId)
    {
        lock (_connections)
        {
            if (!_connections.TryGetValue(key, out var connections))
            {
                connections = [];
                _connections[key] = connections;
            }

            connections.Add(connectionId);
        }
    }

    public void Remove(T key, string connectionId)
    {
        lock (_connections)
        {
            if (_connections.TryGetValue(key, out var connections))
            {
                connections.Remove(connectionId);

                if (connections.Count == 0)
                {
                    _connections.Remove(key);
                }
            }
        }
    }

    public IEnumerable<string> GetConnections(T key)
    {
        lock (_connections)
        {
            if (_connections.TryGetValue(key, out var connections))
            {
                return connections;
            }

            return [];
        }
    }
}

