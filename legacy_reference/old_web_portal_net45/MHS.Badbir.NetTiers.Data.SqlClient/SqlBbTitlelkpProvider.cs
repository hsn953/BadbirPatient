#region Using directives

using System;
using System.Data;
using System.Collections;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.ComponentModel;

using MHS.Badbir.NetTiers.Entities;
using MHS.Badbir.NetTiers.Data;

#endregion

namespace MHS.Badbir.NetTiers.Data.SqlClient
{
	///<summary>
	/// This class is the SqlClient Data Access Logic Component implementation for the <see cref="BbTitlelkp"/> entity.
	///</summary>
	[DataObject]
	[CLSCompliant(true)]
	public partial class SqlBbTitlelkpProvider: SqlBbTitlelkpProviderBase
	{
		/// <summary>
		/// Creates a new <see cref="SqlBbTitlelkpProvider"/> instance.
		/// Uses connection string to connect to datasource.
		/// </summary>
		/// <param name="connectionString">The connection string to the database.</param>
		/// <param name="useStoredProcedure">A boolean value that indicates if we use the stored procedures or embedded queries.</param>
		/// <param name="providerInvariantName">Name of the invariant provider use by the DbProviderFactory.</param>
		public SqlBbTitlelkpProvider(string connectionString, bool useStoredProcedure, string providerInvariantName): base(connectionString, useStoredProcedure, providerInvariantName){}
	}
}