using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Hinet.Service
{
    public interface IEntityService<T> : IService
     where T : class
    {
        void Create(T entity);

        void Delete(T entity);

        void SoftDelete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        void InsertRange(IEnumerable<T> entities);

        void UpdateRange(IEnumerable<T> entities);

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetAll();

        void Update(T entity);

        T GetById(object id);

        T GetEmptyIfNullById(object id);

        void Save(T entity);

        List<SelectListItem> GetDropdown(string displayMember, string valueMember, object selected = null);

        List<SelectListItem> GetDropDownMultiple(string displayMember, string valueMember, List<object> selected = null);

        List<object> GetListFieldValue(string fieldName);

        List<SelectListItem> GetDropdownFields(object selected = null);

        List<T> GetEntitiesByFieldValue(string fieldName, object value);

        List<T> GetEntitiesByMultipleFieldValue(params KeyValuePair<string, object>[] groupKeyValue);

        //List<QLLinhVucDto> GetAllQLLinhVucToList(string list);
        //List<DepartmentDTO> GetAllLinhVucToList(string list);
        //List<QLLinhVucDto> GetAllLinhVucToList(string list);
        List<T> GetByConditionRoleRequest(int? numberToTake);

        IEnumerable<T> GetByCreatedIDConditionList(long id);

        IEnumerable<T> GetByConditionList(int number);

        IEnumerable<T> GetByConditionList2(int number);

        IEnumerable<Dictionary<string, object>> GetByConditionList3(List<string> strings);

        List<T> GetListBySqlQuery(string sqlQuery);

        void DeleteRange(Expression<Func<T, bool>> predicate);
    }
}