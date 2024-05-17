﻿namespace OfficeBaseApp.Repositories;
using OfficeBaseApp.Entities;
public interface IWriteRepository<in T> where T : class, IEntity
{
    void Add(T item);
    void Remove(T item);
    void Save();
}