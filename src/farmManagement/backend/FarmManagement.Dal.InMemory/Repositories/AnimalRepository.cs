using System.Collections.Concurrent;
using FarmManagement.Dal.Entities;
using FarmManagement.Dal.Exceptions;
using FarmManagement.Dal.Repositories;
using LazyCache;

namespace FarmManagement.Dal.InMemory.Repositories;

public sealed class AnimalRepository: IAnimalRepository
{
    private readonly IAppCache _appCache;
    private readonly ConcurrentDictionary<string, AnimalEntity> _animalEntities = new();
    private readonly ReaderWriterLockSlim _lock = new(LockRecursionPolicy.SupportsRecursion);

    public AnimalRepository(IAppCache appCache)
    {
        _appCache = appCache ?? throw new ArgumentNullException(nameof(appCache));
    }

    private const string AnimalCache = "animal_list_cache";

    public Task<AnimalEntity> AddAsync(AnimalEntity entity)
    {
        _lock.EnterWriteLock();

        try
        {
            if (_animalEntities.TryAdd(entity.Name, entity))
            {
                _appCache.Remove(AnimalCache);
                _appCache.GetOrAdd(AnimalCache, () => _animalEntities.Values.ToList());

                return Task.FromResult(entity);
            }

            throw new EntityUniqueException(entity.Name);
        }
        finally
        {
            if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
        }
    }
        
    public Task<IReadOnlyList<AnimalEntity>> GetAsync(Func<AnimalEntity, bool>? predicate = null)
    {
        var entities = _appCache.GetOrAdd<IReadOnlyList<AnimalEntity>>(AnimalCache, () => _animalEntities.Values.ToList());

        entities = predicate != null ? entities.Where(predicate).ToList() : entities;

        return Task.FromResult(entities);
    }

    public Task<AnimalEntity?> GetByNameAsync(string name)
    {
        var entities = _appCache.GetOrAdd<IReadOnlyList<AnimalEntity>>(AnimalCache, () => _animalEntities.Values.ToList());

        var entity = entities.FirstOrDefault(e =>
            string.Equals(e.Name, name, StringComparison.InvariantCultureIgnoreCase));

        return Task.FromResult(entity);
    }

    public Task<AnimalEntity> UpdateAsync(AnimalEntity entity, string name)
    {
        _lock.EnterWriteLock();

        try
        {
            if (_animalEntities.ContainsKey(entity.Name))
            {
                throw new EntityUniqueException(entity.Name);
            }

            if (_animalEntities.ContainsKey(name))
            {
                _animalEntities.Remove(name, out _);

                _animalEntities[entity.Name] = entity;

                _appCache.Remove(AnimalCache);
                _appCache.GetOrAdd(AnimalCache, () => _animalEntities.Values.ToList());

                return Task.FromResult(entity);
            }

            throw new EntityNotFoundException(entity.Name);
        }
        finally
        {
            if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
        }
    }

    public Task<AnimalEntity> RemoveAsync(AnimalEntity entity)
    {
        _lock.EnterWriteLock();

        try
        {
            if (_animalEntities.TryRemove(entity.Name, out var result))
            {
                _appCache.Remove(AnimalCache);
                _appCache.GetOrAdd(AnimalCache, () => _animalEntities.Values.ToList());

                return Task.FromResult(result);
            }

            throw new EntityNotFoundException(entity.Name);
        }
        finally
        {
            if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
        }
    }
}