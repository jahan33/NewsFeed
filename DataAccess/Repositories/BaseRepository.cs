using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Linq;
using DataAccess.DAL;
using System.Collections;
using System.Data.Common;
using System.Dynamic;
using System.Linq.Expressions;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.IRepositories;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Abstract Entity Framework repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        private NewsContext _context;
        private  DbSet<T> _dbset;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbFactory"></param>
        protected BaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbset = Context.Set<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }


        /// <summary>
        /// 
        /// </summary>
        protected DbContext Context
        {
            get
            {
                return _context ?? (_context = DbFactory.Get());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
         //   _dbset.Add(entity);
            _context.Entry(entity).State = EntityState.Added;
        //    _context.Entry(entity).CurrentValues.SetValues(EntityState.Added);
         
            _context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            //_context.Entry(entity).CurrentValues.SetValues(EntityState.Modified);    

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        public void Delete(Func<T, Boolean> where)
        {
            IEnumerable<T> objects = _dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbset.Remove(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(long id)
        {
            return _dbset.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {

            return _dbset.ToList();

        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public virtual IQueryable<T> GetAllTemp()
        //{
        //    return _dbset.AsQueryable();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> where)
        {
            return _dbset.AsQueryable().Where(where);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="where"></param>
        ///// <returns></returns>
        //public virtual IQueryable<T> GetManyAsIQueryable(Expression<Func<T, bool>> where)
        //{
        //    return _dbset.Where(where);
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T GetSingle(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault<T>();
        }


        

        public List<dynamic> ExcuteSpAnonmious<T>(string query, SqlParameter[] Parameters)
        {
            List<dynamic> returnList = new List<dynamic>();



            var dbCommand = _context.Database.GetDbConnection().CreateCommand();
            dbCommand.Connection.Open();

            dbCommand.CommandText = query;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;


            var parameter1 = dbCommand.CreateParameter();
            parameter1.ParameterName = Parameters[0].ParameterName;
            parameter1.Value = Parameters[0].Value;
            dbCommand.Parameters.Add(parameter1);

            var parameter2 = dbCommand.CreateParameter();
            parameter2.ParameterName = Parameters[1].ParameterName;
            parameter2.Value = Parameters[1].Value;
            dbCommand.Parameters.Add(parameter2);

            var parameter3 = dbCommand.CreateParameter();
            parameter3.ParameterName = Parameters[2].ParameterName;
            parameter3.Value = Parameters[2].Value;
            dbCommand.Parameters.Add(parameter3);

            DbDataReader reader = dbCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                returnList.Add(SqlDataReaderToExpando(reader));
            }

            return returnList;



        }


        public DataSet ExcuteSp(string query, List<SqlParameter> Parameters=null, int dataTableCount=1)
        {
            DataSet returnList = new DataSet();



            var dbCommand = _context.Database.GetDbConnection().CreateCommand();
            try {
                returnList = new DataSet();
                dbCommand.Connection.Open();

                dbCommand.CommandText = query;
                dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
                if (Parameters != null)
                {
                    foreach (var param in Parameters)
                    {
                        var parameter1 = dbCommand.CreateParameter();
                        parameter1.ParameterName = param.ParameterName;
                        if (param.Value is string)
                        {
                            parameter1.Value = param.Value.ToString().Replace("'", "''");
                        }
                        else
                        {
                            parameter1.Value = param.Value;
                        }

                        dbCommand.Parameters.Add(parameter1);
                    }
                }

                LoadOption lOption = new LoadOption();
                List<DataTable> dt = new List<DataTable>();
                for (int i = 0; i < dataTableCount; i++)
                {
                    DataTable dt1 = new DataTable();
                    dt1.TableName = "table" + i;
                    dt.Add(dt1);
                    returnList.Tables.Add(dt1);
                }

                returnList.Load(dbCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection), lOption, dt.ToArray());

            }
            catch(Exception ex)
            {
                returnList = new DataSet();
                dbCommand.Connection.Open();

                dbCommand.CommandText = query;
                dbCommand.CommandType = System.Data.CommandType.Text;
                if (Parameters != null)
                {
                    foreach (var param in Parameters)
                    {
                        var parameter1 = dbCommand.CreateParameter();
                        parameter1.ParameterName = param.ParameterName;
                        if (param.Value is string)
                        {
                            parameter1.Value = param.Value.ToString().Replace("'", "''");
                        }
                        else
                        {
                            parameter1.Value = param.Value;
                        }

                        dbCommand.Parameters.Add(parameter1);
                    }
                }

                LoadOption lOption = new LoadOption();
                List<DataTable> dt = new List<DataTable>();
                for (int i = 0; i < dataTableCount; i++)
                {
                    DataTable dt1 = new DataTable();
                    dt1.TableName = "table" + i;
                    dt.Add(dt1);
                    returnList.Tables.Add(dt1);
                }

                returnList.Load(dbCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection), lOption, dt.ToArray());
            }
            finally
            {
                dbCommand.Connection.Close();

                dbCommand.Connection.Dispose();
                dbCommand.Dispose();
            }

            return returnList;



        }

        public string GetConnectionString()
        {
            


        return  _context.Database.GetDbConnection().ConnectionString;
            
        }

        public dynamic ExecuteScalar(string query, SqlParameter[] Parameters)
        {
            dynamic result;
            var dbCommand = _context.Database.GetDbConnection().CreateCommand();
            try {

              //  var dbCommand = _context.Database.Connection.CreateCommand();
                dbCommand.Connection.Open();

                dbCommand.CommandText = query;
                dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var param in Parameters)
                {
                    var parameter1 = dbCommand.CreateParameter();
                    parameter1.ParameterName = param.ParameterName;
                    parameter1.Value = param.Value.ToString().Replace("'", "''");
                    dbCommand.Parameters.Add(parameter1);
                }
                result= dbCommand.ExecuteScalar();
            }
            catch(Exception ex)
            {
               

                dbCommand.CommandText = query;
                dbCommand.CommandType = System.Data.CommandType.Text;
                foreach (var param in Parameters)
                {
                    var parameter1 = dbCommand.CreateParameter();
                    parameter1.ParameterName = param.ParameterName;
                    parameter1.Value = param.Value.ToString().Replace("'", "''");
                    dbCommand.Parameters.Add(parameter1);
                }
                try
                {
                    result = dbCommand.ExecuteScalar();
                }catch(Exception ex2)
                {
                    result = dbCommand.ExecuteNonQuery();
                }
                
            }
            finally
            {
                dbCommand.Connection.Close();
           
                //dbCommand.Connection.Dispose();
                dbCommand.Dispose();
            }

            return result;
        }


        private dynamic SqlDataReaderToExpando(DbDataReader reader)
        {
            var expandoObject = new ExpandoObject() as IDictionary<string, object>;

            for (var i = 0; i < reader.FieldCount; i++)
                expandoObject.Add(reader.GetName(i), reader[i]);

            return expandoObject;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
      

        public int ExcuteStoreProc(string query, SqlParameter[] Parameters)
        {
            return _context.Database.ExecuteSqlCommand(query, Parameters);

        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
      

        public void Dispose()
        {
            //Dispose(true);
            //GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (_context != null)
                //{
                //    _context.Dispose();
                //    _context = null;
                //}
                //if (_dbset != null)
                //{

                //    _dbset = null;
                //}

            }
        }
       
        //~BaseRepository()
        //{
        //    Dispose();
        //}





    }
}
