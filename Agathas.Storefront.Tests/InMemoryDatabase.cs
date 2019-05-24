using System;
using System.Reflection;

using NHibernate;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Environment = NHibernate.Cfg.Environment;


public class InMemoryDatabaseTest : IDisposable {
	private static Configuration Configuration;
	private static ISessionFactory SessionFactory;
	protected ISession session;

	public InMemoryDatabaseTest(Assembly assemblyContainingMapping) {
		if (Configuration == null) {
			Configuration = new Configuration()
				.SetProperty(Environment.ReleaseConnections,"on_close")
				.SetProperty(Environment.Dialect, typeof (SQLiteDialect).AssemblyQualifiedName)
				.SetProperty(Environment.ConnectionDriver, typeof(SQLite20Driver).AssemblyQualifiedName)
				.SetProperty(Environment.ConnectionString, "data source=:memory:")
				.AddAssembly(assemblyContainingMapping);

			SessionFactory = Configuration.BuildSessionFactory();
		}

		session = SessionFactory.OpenSession();

		new SchemaExport(Configuration).Execute(false, true, false);
	}

	public void Dispose() { session.Dispose(); }
}