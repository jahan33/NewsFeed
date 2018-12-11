
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.DAL
{
    public class DbFactory : IDbFactory
    {
        private NewsContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public NewsContext Get()
        {
            Dispose(true);
            return _context ?? (_context = new NewsContext());
        }

        #region Dispose

        private bool _disposed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (_context != null)
                //{
                //    _context.Dispose();
                //    _context = null;
                //}
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
        ~DbFactory()
        {
            Dispose();
        }
    }
}
