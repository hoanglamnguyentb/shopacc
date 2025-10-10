using Hinet.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace Hinet.Service
{
	public abstract class EntityService<T> : IEntityService<T> where T : class
	{
		private IUnitOfWork _unitOfWork;
		private IGenericRepository<T> _repository;
		private IUnitOfWork unitOfWork;

		public EntityService(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
		{
			//var timeOutDate = new DateTime(2025, 12, 1);
			//if (DateTime.Now > timeOutDate)
			//{
			//    throw new Exception("timeOutDate");
			//}
			_unitOfWork = unitOfWork;
			_repository = repository;
		}

		protected EntityService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		//protected EntityService(IUnitOfWork _unitOfWork, IQLLinhVucRepository qLLinhVucRepository)
		//{
		//    this._unitOfWork = _unitOfWork;
		//    this.qLLinhVucRepository = qLLinhVucRepository;
		//}

		public virtual void Create(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			_repository.Add(entity);
			_unitOfWork.Commit();
		}

		public virtual void UpdateRange(IEnumerable<T> entities)
		{
			_repository.UpdateRange(entities);
			_unitOfWork.Commit();
		}

		public virtual void Update(T entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			_repository.Edit(entity);
			_unitOfWork.Commit();
		}

		public virtual void Delete(T entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			_repository.Delete(entity);
			_unitOfWork.Commit();
		}

		public virtual void SoftDelete(T entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			_repository.SoftDelete(entity);
			_unitOfWork.Commit();
		}

		public virtual IEnumerable<T> GetAll()
		{
			return _repository.GetAll();
		}

		public virtual T GetById(object id)
		{
			return _repository.GetById(id);
		}

		public virtual void DeleteRange(IEnumerable<T> entities)
		{
			_repository.DeleteRange(entities);
			_unitOfWork.Commit();
		}

		public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
		{
			return _repository.GetAllAsQueryable().Where(predicate);
		}

		public List<SelectListItem> GetDropdown(string displayMember, string valueMember, object selected = null)
		{
			return _repository.GetDropdown(displayMember, valueMember, selected);
		}

		public List<SelectListItem> GetDropDownMultiple(string displayMember, string valueMember, List<object> selected = null)
		{
			return _repository.GetDropDownMultiple(displayMember, valueMember, selected);
		}

		public List<object> GetListFieldValue(string fieldName)
		{
			return _repository.GetListFieldValue(fieldName);
		}

		public List<SelectListItem> GetDropdownFields(object selected = null)
		{
			return _repository.GetDropdownFields(selected);
		}

		public List<T> GetEntitiesByFieldValue(string fieldName, object value)
		{
			return _repository.GetEntitiesByFieldValue(fieldName, value);
		}

		public List<T> GetEntitiesByMultipleFieldValue(params KeyValuePair<string, object>[] groupKeyValue)
		{
			return _repository.GetEntitiesByMultipleFieldValue(groupKeyValue);
		}

		public T GetEmptyIfNullById(object id)
		{
			return _repository.GetEmptyIfNullById(id);
		}

		public void Save(T entity)
		{
			var id = typeof(T).GetProperty("Id").GetValue(entity);
			if (id.ToString().Equals("0"))
			{
				_repository.Add(entity);
			}
			else
			{
				_repository.Edit(entity);
			}
			_unitOfWork.Commit();
		}

		public virtual void InsertRange(IEnumerable<T> entities)
		{
			_repository.InsertRange(entities);
			_unitOfWork.Commit();
		}

		public List<T> GetByConditionRoleRequest(int? numberToTake)
		{
			if (numberToTake <= 0)
			{
				return _repository.GetAll().ToList();
			}
			return _repository.GetAll().Take(numberToTake.Value).ToList();
		}

		public IEnumerable<T> GetByCreatedIDConditionList(long id)
		{
			return _repository.GetAll().Where(x => (long)x.GetType().GetProperty("CreatedID").GetValue(x, null) == id).ToList();
		}

		public IEnumerable<T> GetByConditionList(int number)
		{
			Type type = typeof(T);
			PropertyInfo[] properties = type.GetProperties();

			PropertyInfo[] first3Properties = properties.Take(3).ToArray();
			return _repository.GetAll().ToList();
		}

		public IEnumerable<T> GetByConditionList2(int number)
		{
			Type type = typeof(T);
			PropertyInfo[] properties = type.GetProperties();
			PropertyInfo[] firstNProperties = properties.Take(number).ToArray();
			var result = _repository.GetAll().Select(d =>
			{
				T obj = Activator.CreateInstance<T>();
				foreach (PropertyInfo property in firstNProperties)
				{
					object value = property.GetValue(d);
					property.SetValue(obj, value);
				}
				return obj;
			});
			return result;
		}

		public IEnumerable<Dictionary<string, object>> GetByConditionList3(List<string> strings)
		{
			Type type = typeof(T);
			PropertyInfo[] properties = type.GetProperties();
			PropertyInfo[] firstNProperties = properties.Where(x => strings.Contains(x.Name)).ToArray();
			var result = _repository.GetAll().Select(d =>
			{
				var dict = new Dictionary<string, object>();
				foreach (PropertyInfo property in firstNProperties)
				{
					object value = property.GetValue(d);
					if (value != null)
					{
						dict[property.Name] = value;
					}
				}
				return dict;
			});
			return result;
		}

		public List<T> GetListBySqlQuery(string sqlQuery)
		{
			var result = new List<T>();
			var trace = string.Empty;
			try
			{
				result = _repository.GetContext().Database.SqlQuery<T>(sqlQuery).ToList();
			}
			catch (SqlException ex)
			{
				trace = ex.ToString();
			}
			return result;
		}

		public void DeleteRange(Expression<Func<T, bool>> predicate)
		{
			var data = _repository.FindBy(predicate);
			_repository.DeleteRange(data);
		}
	}
}