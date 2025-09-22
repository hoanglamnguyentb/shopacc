using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Hinet.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        System.Data.Entity.DbContext GetContext();

        IDbSet<T> DBSet();

        IEnumerable<T> GetAll();

        IQueryable<T> GetQueryable();

        IQueryable<T> GetAllAsQueryable();

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        void UpdateRange(IEnumerable<T> entities);

        T Add(T entity);

        T Delete(T entity);

        void SoftDelete(T entity);

        void Edit(T entity);

        void Save();

        T GetById(object id);

        T GetEmptyIfNullById(object id);

        T FindEmptyIfNullByExp(Expression<Func<T, bool>> predicate);

        void DeleteRange(IEnumerable<T> entities);

        void InsertRange(IEnumerable<T> entities);

        List<SelectListItem> GetDropdown(string displayMember, string valueMember, object selected = null);

        List<SelectListItem> GetDropDownMultiple(string displayMember, string valueMember, List<object> selected = null);

        List<object> GetListFieldValue(string fieldName);

        List<SelectListItem> GetDropdownFields(object selected = null);

        List<T> GetEntitiesByFieldValue(string fieldName, object value);

        List<T> GetEntitiesByMultipleFieldValue(params KeyValuePair<string, object>[] groupKeyValue);
    }
}