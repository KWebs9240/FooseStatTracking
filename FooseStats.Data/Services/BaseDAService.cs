﻿using FooseStats.Data.FooseStats.Data.Ef;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FooseStats.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FooseStats.Data.Services
{
    public class BaseDAService<T> where T : class, IUpdatable
    {
        private readonly Func<FooseStatsContext, DbSet<T>> _dbSetFunc;
        private readonly Func<T, T, bool> _pkCheckFunc;

        BaseDAService(Func<FooseStatsContext, DbSet<T>> dbSetFunc,
            Func<T, T, bool> pkCheckFunc)
        {
            _dbSetFunc = dbSetFunc;
            _pkCheckFunc = pkCheckFunc;
        }

        public int Delete(T toDelete)
        {
            using (var db = new FooseStatsContext())
            {
                _dbSetFunc(db).Remove(toDelete);

                return db.SaveChanges();
            }
        }

        public int DeleteEnum(IEnumerable<T> enumToDelete)
        {
            using (var db = new FooseStatsContext())
            {
                _dbSetFunc(db).RemoveRange(enumToDelete);

                return db.SaveChanges();
            }
        }

        public IEnumerable<T> Get(Func<T, bool> filterFunction = null)
        {
            using (var db = new FooseStatsContext())
            {
                if (filterFunction == null)
                {
                    return _dbSetFunc(db).ToList();
                }
                else
                {
                    return _dbSetFunc(db).Where(filterFunction).ToList();
                }
            }
        }

        public T SaveorUpdate(T toSave)
        {
            using (var db = new FooseStatsContext())
            {
                T updt = _dbSetFunc(db).FirstOrDefault(x => _pkCheckFunc(x, toSave));

                if (updt == null)
                {
                    toSave.CreatedDate = DateTime.Now;
                    _dbSetFunc(db).Add(toSave);
                    updt = toSave;
                }
                else
                {
                    Mapper.Map<T, T>(updt, toSave);
                }

                db.SaveChanges();

                return updt;
            }
        }

        public IEnumerable<T> SaveorUpdateEnum(IEnumerable<T> enumToSave)
        {
            enumToSave.ToList().ForEach(x => x = SaveorUpdate(x));

            return enumToSave;
        }
    }
}
