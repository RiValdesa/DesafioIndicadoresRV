using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Configuration;

namespace DesafioIndicadores
{
    public class NHibernateHelper
    {
        private const string CurrentSessionKey = "nhibernate.current_session";
        private static readonly ISessionFactory _sessionFactory;
        static NHibernateHelper()
        {
            _sessionFactory = FluentConfigure();
        }
        public static ISession GetCurrentSession()
        {
            return _sessionFactory.OpenSession();
        }
        public static void CloseSession()
        {
            _sessionFactory.Close();
        }
        public static void CloseSessionFactory()
        {
            if (_sessionFactory != null)
            {
                _sessionFactory.Close();
            }
        }

        public static ISessionFactory FluentConfigure()
        {
            return Fluently.Configure()
            .Database(MySQLConfiguration.Standard
            .ConnectionString(cs=>cs.FromConnectionStringWithKey("ConnectionString")))
            .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
            .BuildSessionFactory();
        }

    }
}