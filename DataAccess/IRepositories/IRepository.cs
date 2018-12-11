using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    // <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        void Delete(Func<T, Boolean> predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> where);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        IEnumerable<T> GetAll();


        // IQueryable<T> GetAllTemp();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<T> Get(Expression<Func<T, bool>> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        //IQueryable<T> GetManyAsIQueryable(Expression<Func<T, bool>> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<dynamic> ExcuteSpAnonmious<T>(string query, SqlParameter[] Parameters);

        DataSet ExcuteSp(string query, List<SqlParameter> Parameters = null, int dataTableCount = 1);
        string GetConnectionString();
        dynamic ExecuteScalar(string query, SqlParameter[] Parameters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>

        int ExcuteStoreProc(string query, SqlParameter[] Parameters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>



        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>



    }
}
