﻿namespace OfficeBaseApp.Repositories;
using OfficeBaseApp.Entities;
public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
    where T : class, IEntity
{
    public event EventHandler<T> ItemAdded;
    public event EventHandler<T> ItemRemoved;
    public abstract void Load();
}