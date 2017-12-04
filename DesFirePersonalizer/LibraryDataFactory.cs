using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.IO;

namespace DesFirePersonalizer
{
    internal class DataFactory
    {
        static string dbFile = string.Empty;
        static bool overwriteExisting = false;
        static bool showToStdOut = true;

        private static volatile ISessionFactory iSessionFactory;
        private static object syncRoot = new Object();

        public static ISession OpenSession
        {
            get
            {
                if (iSessionFactory == null || overwriteExisting == true)
                {
                    lock (syncRoot)
                    {
                        iSessionFactory = BuildSessionFactory();
                    }
                }
                return iSessionFactory.OpenSession();
            }
        }

        public DataFactory(string _dbFile, bool _overwriteExisting)
        {
            DataFactory.dbFile = _dbFile;
            DataFactory.overwriteExisting = _overwriteExisting;
        }

        public static ISessionFactory BuildSessionFactory()
        {
            return Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.UsingFile(dbFile).ShowSql())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            var se = new SchemaExport(config).SetOutputFile("MyDDL.sql");

            if (overwriteExisting)
            {
                if (File.Exists(dbFile))
                {
                    try
                    {
                        File.Delete(dbFile);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        throw ex;
                    }
                }

                se.Create(true, true);
            }
            else
                se.Create(false, false);

        }
    }
}
