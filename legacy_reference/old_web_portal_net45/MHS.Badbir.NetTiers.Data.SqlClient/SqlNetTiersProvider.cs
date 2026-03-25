#region Using directives

using System;
using System.Collections;
using System.Collections.Specialized;


using System.Web.Configuration;
using System.Data;
using System.Data.Common;
using System.Configuration.Provider;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using MHS.Badbir.NetTiers.Entities;
using MHS.Badbir.NetTiers.Data;
using MHS.Badbir.NetTiers.Data.Bases;

#endregion

namespace MHS.Badbir.NetTiers.Data.SqlClient
{
	/// <summary>
	/// This class is the Sql implementation of the NetTiersProvider.
	/// </summary>
	public sealed class SqlNetTiersProvider : MHS.Badbir.NetTiers.Data.Bases.NetTiersProvider
	{
		private static object syncRoot = new Object();
		private string _applicationName;
        private string _connectionString;
        private bool _useStoredProcedure;
        string _providerInvariantName;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlNetTiersProvider"/> class.
		///</summary>
		public SqlNetTiersProvider()
		{	
		}		
		
		/// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        /// <exception cref="T:System.ArgumentNullException">The name of the provider is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)"></see> on a provider after the provider has already been initialized.</exception>
        /// <exception cref="T:System.ArgumentException">The name of the provider has a length of zero.</exception>
		public override void Initialize(string name, NameValueCollection config)
        {
            // Verify that config isn't null
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            // Assign the provider a default name if it doesn't have one
            if (String.IsNullOrEmpty(name))
            {
                name = "SqlNetTiersProvider";
            }

            // Add a default "description" attribute to config if the
            // attribute doesn't exist or is empty
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "NetTiers Sql provider");
            }

            // Call the base class's Initialize method
            base.Initialize(name, config);

            // Initialize _applicationName
            _applicationName = config["applicationName"];

            if (string.IsNullOrEmpty(_applicationName))
            {
                _applicationName = "/";
            }
            config.Remove("applicationName");


            #region "Initialize UseStoredProcedure"
            string storedProcedure  = config["useStoredProcedure"];
           	if (string.IsNullOrEmpty(storedProcedure))
            {
                throw new ProviderException("Empty or missing useStoredProcedure");
            }
            this._useStoredProcedure = Convert.ToBoolean(config["useStoredProcedure"]);
            config.Remove("useStoredProcedure");
            #endregion

			#region ConnectionString

			// Initialize _connectionString
			_connectionString = config["connectionString"];
			config.Remove("connectionString");

			string connect = config["connectionStringName"];
			config.Remove("connectionStringName");

			if ( String.IsNullOrEmpty(_connectionString) )
			{
				if ( String.IsNullOrEmpty(connect) )
				{
					throw new ProviderException("Empty or missing connectionStringName");
				}

				if ( DataRepository.ConnectionStrings[connect] == null )
				{
					throw new ProviderException("Missing connection string");
				}

				_connectionString = DataRepository.ConnectionStrings[connect].ConnectionString;
			}

            if ( String.IsNullOrEmpty(_connectionString) )
            {
                throw new ProviderException("Empty connection string");
			}

			#endregion
            
             #region "_providerInvariantName"

            // initialize _providerInvariantName
            this._providerInvariantName = config["providerInvariantName"];

            if (String.IsNullOrEmpty(_providerInvariantName))
            {
                throw new ProviderException("Empty or missing providerInvariantName");
            }
            config.Remove("providerInvariantName");

            #endregion

        }
		
		/// <summary>
		/// Creates a new <see cref="TransactionManager"/> instance from the current datasource.
		/// </summary>
		/// <returns></returns>
		public override TransactionManager CreateTransaction()
		{
			return new TransactionManager(this._connectionString);
		}
		
		/// <summary>
		/// Gets a value indicating whether to use stored procedure or not.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this repository use stored procedures; otherwise, <c>false</c>.
		/// </value>
		public bool UseStoredProcedure
		{
			get {return this._useStoredProcedure;}
			set {this._useStoredProcedure = value;}
		}
		
		 /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
		public string ConnectionString
		{
			get {return this._connectionString;}
			set {this._connectionString = value;}
		}
		
		/// <summary>
	    /// Gets or sets the invariant provider name listed in the DbProviderFactories machine.config section.
	    /// </summary>
	    /// <value>The name of the provider invariant.</value>
	    public string ProviderInvariantName
	    {
	        get { return this._providerInvariantName; }
	        set { this._providerInvariantName = value; }
	    }		
		
		///<summary>
		/// Indicates if the current <see cref="NetTiersProvider"/> implementation supports Transacton.
		///</summary>
		public override bool IsTransactionSupported
		{
			get
			{
				return true;
			}
		}

		
		#region "BbPatientCohortTrackingStatuslkpProvider"
			
		private SqlBbPatientCohortTrackingStatuslkpProvider innerSqlBbPatientCohortTrackingStatuslkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbPatientCohortTrackingStatuslkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbPatientCohortTrackingStatuslkpProviderBase BbPatientCohortTrackingStatuslkpProvider
		{
			get
			{
				if (innerSqlBbPatientCohortTrackingStatuslkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbPatientCohortTrackingStatuslkpProvider == null)
						{
							this.innerSqlBbPatientCohortTrackingStatuslkpProvider = new SqlBbPatientCohortTrackingStatuslkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbPatientCohortTrackingStatuslkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbPatientCohortTrackingStatuslkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbPatientCohortTrackingStatuslkpProvider SqlBbPatientCohortTrackingStatuslkpProvider
		{
			get {return BbPatientCohortTrackingStatuslkpProvider as SqlBbPatientCohortTrackingStatuslkpProvider;}
		}
		
		#endregion
		
		
		#region "BbPatientProvider"
			
		private SqlBbPatientProvider innerSqlBbPatientProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbPatient"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbPatientProviderBase BbPatientProvider
		{
			get
			{
				if (innerSqlBbPatientProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbPatientProvider == null)
						{
							this.innerSqlBbPatientProvider = new SqlBbPatientProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbPatientProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbPatientProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbPatientProvider SqlBbPatientProvider
		{
			get {return BbPatientProvider as SqlBbPatientProvider;}
		}
		
		#endregion
		
		
		#region "BbCentreRegionlkpProvider"
			
		private SqlBbCentreRegionlkpProvider innerSqlBbCentreRegionlkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbCentreRegionlkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbCentreRegionlkpProviderBase BbCentreRegionlkpProvider
		{
			get
			{
				if (innerSqlBbCentreRegionlkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbCentreRegionlkpProvider == null)
						{
							this.innerSqlBbCentreRegionlkpProvider = new SqlBbCentreRegionlkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbCentreRegionlkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbCentreRegionlkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbCentreRegionlkpProvider SqlBbCentreRegionlkpProvider
		{
			get {return BbCentreRegionlkpProvider as SqlBbCentreRegionlkpProvider;}
		}
		
		#endregion
		
		
		#region "BbCentrestatusProvider"
			
		private SqlBbCentrestatusProvider innerSqlBbCentrestatusProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbCentrestatus"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbCentrestatusProviderBase BbCentrestatusProvider
		{
			get
			{
				if (innerSqlBbCentrestatusProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbCentrestatusProvider == null)
						{
							this.innerSqlBbCentrestatusProvider = new SqlBbCentrestatusProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbCentrestatusProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbCentrestatusProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbCentrestatusProvider SqlBbCentrestatusProvider
		{
			get {return BbCentrestatusProvider as SqlBbCentrestatusProvider;}
		}
		
		#endregion
		
		
		#region "BbUkcrNregionlkpProvider"
			
		private SqlBbUkcrNregionlkpProvider innerSqlBbUkcrNregionlkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbUkcrNregionlkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbUkcrNregionlkpProviderBase BbUkcrNregionlkpProvider
		{
			get
			{
				if (innerSqlBbUkcrNregionlkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbUkcrNregionlkpProvider == null)
						{
							this.innerSqlBbUkcrNregionlkpProvider = new SqlBbUkcrNregionlkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbUkcrNregionlkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbUkcrNregionlkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbUkcrNregionlkpProvider SqlBbUkcrNregionlkpProvider
		{
			get {return BbUkcrNregionlkpProvider as SqlBbUkcrNregionlkpProvider;}
		}
		
		#endregion
		
		
		#region "BbCentreProvider"
			
		private SqlBbCentreProvider innerSqlBbCentreProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbCentre"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbCentreProviderBase BbCentreProvider
		{
			get
			{
				if (innerSqlBbCentreProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbCentreProvider == null)
						{
							this.innerSqlBbCentreProvider = new SqlBbCentreProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbCentreProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbCentreProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbCentreProvider SqlBbCentreProvider
		{
			get {return BbCentreProvider as SqlBbCentreProvider;}
		}
		
		#endregion
		
		
		#region "BbPatientdrugProvider"
			
		private SqlBbPatientdrugProvider innerSqlBbPatientdrugProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbPatientdrug"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbPatientdrugProviderBase BbPatientdrugProvider
		{
			get
			{
				if (innerSqlBbPatientdrugProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbPatientdrugProvider == null)
						{
							this.innerSqlBbPatientdrugProvider = new SqlBbPatientdrugProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbPatientdrugProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbPatientdrugProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbPatientdrugProvider SqlBbPatientdrugProvider
		{
			get {return BbPatientdrugProvider as SqlBbPatientdrugProvider;}
		}
		
		#endregion
		
		
		#region "BbQueryTypelkpProvider"
			
		private SqlBbQueryTypelkpProvider innerSqlBbQueryTypelkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbQueryTypelkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbQueryTypelkpProviderBase BbQueryTypelkpProvider
		{
			get
			{
				if (innerSqlBbQueryTypelkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbQueryTypelkpProvider == null)
						{
							this.innerSqlBbQueryTypelkpProvider = new SqlBbQueryTypelkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbQueryTypelkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbQueryTypelkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbQueryTypelkpProvider SqlBbQueryTypelkpProvider
		{
			get {return BbQueryTypelkpProvider as SqlBbQueryTypelkpProvider;}
		}
		
		#endregion
		
		
		#region "BbQueryStatuslkpProvider"
			
		private SqlBbQueryStatuslkpProvider innerSqlBbQueryStatuslkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbQueryStatuslkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbQueryStatuslkpProviderBase BbQueryStatuslkpProvider
		{
			get
			{
				if (innerSqlBbQueryStatuslkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbQueryStatuslkpProvider == null)
						{
							this.innerSqlBbQueryStatuslkpProvider = new SqlBbQueryStatuslkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbQueryStatuslkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbQueryStatuslkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbQueryStatuslkpProvider SqlBbQueryStatuslkpProvider
		{
			get {return BbQueryStatuslkpProvider as SqlBbQueryStatuslkpProvider;}
		}
		
		#endregion
		
		
		#region "BbPatientStatusDetaillkpProvider"
			
		private SqlBbPatientStatusDetaillkpProvider innerSqlBbPatientStatusDetaillkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbPatientStatusDetaillkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbPatientStatusDetaillkpProviderBase BbPatientStatusDetaillkpProvider
		{
			get
			{
				if (innerSqlBbPatientStatusDetaillkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbPatientStatusDetaillkpProvider == null)
						{
							this.innerSqlBbPatientStatusDetaillkpProvider = new SqlBbPatientStatusDetaillkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbPatientStatusDetaillkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbPatientStatusDetaillkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbPatientStatusDetaillkpProvider SqlBbPatientStatusDetaillkpProvider
		{
			get {return BbPatientStatusDetaillkpProvider as SqlBbPatientStatusDetaillkpProvider;}
		}
		
		#endregion
		
		
		#region "BbPatientdrugdoseProvider"
			
		private SqlBbPatientdrugdoseProvider innerSqlBbPatientdrugdoseProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbPatientdrugdose"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbPatientdrugdoseProviderBase BbPatientdrugdoseProvider
		{
			get
			{
				if (innerSqlBbPatientdrugdoseProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbPatientdrugdoseProvider == null)
						{
							this.innerSqlBbPatientdrugdoseProvider = new SqlBbPatientdrugdoseProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbPatientdrugdoseProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbPatientdrugdoseProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbPatientdrugdoseProvider SqlBbPatientdrugdoseProvider
		{
			get {return BbPatientdrugdoseProvider as SqlBbPatientdrugdoseProvider;}
		}
		
		#endregion
		
		
		#region "BbPatientLifestyleProvider"
			
		private SqlBbPatientLifestyleProvider innerSqlBbPatientLifestyleProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbPatientLifestyle"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbPatientLifestyleProviderBase BbPatientLifestyleProvider
		{
			get
			{
				if (innerSqlBbPatientLifestyleProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbPatientLifestyleProvider == null)
						{
							this.innerSqlBbPatientLifestyleProvider = new SqlBbPatientLifestyleProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbPatientLifestyleProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbPatientLifestyleProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbPatientLifestyleProvider SqlBbPatientLifestyleProvider
		{
			get {return BbPatientLifestyleProvider as SqlBbPatientLifestyleProvider;}
		}
		
		#endregion
		
		
		#region "BbQueryForCentreProvider"
			
		private SqlBbQueryForCentreProvider innerSqlBbQueryForCentreProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbQueryForCentre"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbQueryForCentreProviderBase BbQueryForCentreProvider
		{
			get
			{
				if (innerSqlBbQueryForCentreProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbQueryForCentreProvider == null)
						{
							this.innerSqlBbQueryForCentreProvider = new SqlBbQueryForCentreProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbQueryForCentreProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbQueryForCentreProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbQueryForCentreProvider SqlBbQueryForCentreProvider
		{
			get {return BbQueryForCentreProvider as SqlBbQueryForCentreProvider;}
		}
		
		#endregion
		
		
		#region "BbPatientStatuslkpProvider"
			
		private SqlBbPatientStatuslkpProvider innerSqlBbPatientStatuslkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbPatientStatuslkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbPatientStatuslkpProviderBase BbPatientStatuslkpProvider
		{
			get
			{
				if (innerSqlBbPatientStatuslkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbPatientStatuslkpProvider == null)
						{
							this.innerSqlBbPatientStatuslkpProvider = new SqlBbPatientStatuslkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbPatientStatuslkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbPatientStatuslkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbPatientStatuslkpProvider SqlBbPatientStatuslkpProvider
		{
			get {return BbPatientStatuslkpProvider as SqlBbPatientStatuslkpProvider;}
		}
		
		#endregion
		
		
		#region "BbQueryForCentreMessageProvider"
			
		private SqlBbQueryForCentreMessageProvider innerSqlBbQueryForCentreMessageProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbQueryForCentreMessage"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbQueryForCentreMessageProviderBase BbQueryForCentreMessageProvider
		{
			get
			{
				if (innerSqlBbQueryForCentreMessageProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbQueryForCentreMessageProvider == null)
						{
							this.innerSqlBbQueryForCentreMessageProvider = new SqlBbQueryForCentreMessageProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbQueryForCentreMessageProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbQueryForCentreMessageProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbQueryForCentreMessageProvider SqlBbQueryForCentreMessageProvider
		{
			get {return BbQueryForCentreMessageProvider as SqlBbQueryForCentreMessageProvider;}
		}
		
		#endregion
		
		
		#region "BbGenderlkpProvider"
			
		private SqlBbGenderlkpProvider innerSqlBbGenderlkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbGenderlkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbGenderlkpProviderBase BbGenderlkpProvider
		{
			get
			{
				if (innerSqlBbGenderlkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbGenderlkpProvider == null)
						{
							this.innerSqlBbGenderlkpProvider = new SqlBbGenderlkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbGenderlkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbGenderlkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbGenderlkpProvider SqlBbGenderlkpProvider
		{
			get {return BbGenderlkpProvider as SqlBbGenderlkpProvider;}
		}
		
		#endregion
		
		
		#region "BbPatientCohortHistoryProvider"
			
		private SqlBbPatientCohortHistoryProvider innerSqlBbPatientCohortHistoryProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbPatientCohortHistory"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbPatientCohortHistoryProviderBase BbPatientCohortHistoryProvider
		{
			get
			{
				if (innerSqlBbPatientCohortHistoryProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbPatientCohortHistoryProvider == null)
						{
							this.innerSqlBbPatientCohortHistoryProvider = new SqlBbPatientCohortHistoryProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbPatientCohortHistoryProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbPatientCohortHistoryProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbPatientCohortHistoryProvider SqlBbPatientCohortHistoryProvider
		{
			get {return BbPatientCohortHistoryProvider as SqlBbPatientCohortHistoryProvider;}
		}
		
		#endregion
		
		
		#region "BbQueryProvider"
			
		private SqlBbQueryProvider innerSqlBbQueryProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbQuery"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbQueryProviderBase BbQueryProvider
		{
			get
			{
				if (innerSqlBbQueryProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbQueryProvider == null)
						{
							this.innerSqlBbQueryProvider = new SqlBbQueryProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbQueryProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbQueryProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbQueryProvider SqlBbQueryProvider
		{
			get {return BbQueryProvider as SqlBbQueryProvider;}
		}
		
		#endregion
		
		
		#region "BbQueryMessageProvider"
			
		private SqlBbQueryMessageProvider innerSqlBbQueryMessageProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbQueryMessage"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbQueryMessageProviderBase BbQueryMessageProvider
		{
			get
			{
				if (innerSqlBbQueryMessageProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbQueryMessageProvider == null)
						{
							this.innerSqlBbQueryMessageProvider = new SqlBbQueryMessageProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbQueryMessageProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbQueryMessageProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbQueryMessageProvider SqlBbQueryMessageProvider
		{
			get {return BbQueryMessageProvider as SqlBbQueryMessageProvider;}
		}
		
		#endregion
		
		
		#region "BbSaeClinicianlkpProvider"
			
		private SqlBbSaeClinicianlkpProvider innerSqlBbSaeClinicianlkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbSaeClinicianlkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbSaeClinicianlkpProviderBase BbSaeClinicianlkpProvider
		{
			get
			{
				if (innerSqlBbSaeClinicianlkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbSaeClinicianlkpProvider == null)
						{
							this.innerSqlBbSaeClinicianlkpProvider = new SqlBbSaeClinicianlkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbSaeClinicianlkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbSaeClinicianlkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbSaeClinicianlkpProvider SqlBbSaeClinicianlkpProvider
		{
			get {return BbSaeClinicianlkpProvider as SqlBbSaeClinicianlkpProvider;}
		}
		
		#endregion
		
		
		#region "BbCohortlkpProvider"
			
		private SqlBbCohortlkpProvider innerSqlBbCohortlkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbCohortlkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbCohortlkpProviderBase BbCohortlkpProvider
		{
			get
			{
				if (innerSqlBbCohortlkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbCohortlkpProvider == null)
						{
							this.innerSqlBbCohortlkpProvider = new SqlBbCohortlkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbCohortlkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbCohortlkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbCohortlkpProvider SqlBbCohortlkpProvider
		{
			get {return BbCohortlkpProvider as SqlBbCohortlkpProvider;}
		}
		
		#endregion
		
		
		#region "BbTitlelkpProvider"
			
		private SqlBbTitlelkpProvider innerSqlBbTitlelkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbTitlelkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbTitlelkpProviderBase BbTitlelkpProvider
		{
			get
			{
				if (innerSqlBbTitlelkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbTitlelkpProvider == null)
						{
							this.innerSqlBbTitlelkpProvider = new SqlBbTitlelkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbTitlelkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbTitlelkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbTitlelkpProvider SqlBbTitlelkpProvider
		{
			get {return BbTitlelkpProvider as SqlBbTitlelkpProvider;}
		}
		
		#endregion
		
		
		#region "BbPositionRolelkpProvider"
			
		private SqlBbPositionRolelkpProvider innerSqlBbPositionRolelkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbPositionRolelkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbPositionRolelkpProviderBase BbPositionRolelkpProvider
		{
			get
			{
				if (innerSqlBbPositionRolelkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbPositionRolelkpProvider == null)
						{
							this.innerSqlBbPositionRolelkpProvider = new SqlBbPositionRolelkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbPositionRolelkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbPositionRolelkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbPositionRolelkpProvider SqlBbPositionRolelkpProvider
		{
			get {return BbPositionRolelkpProvider as SqlBbPositionRolelkpProvider;}
		}
		
		#endregion
		
		
		#region "BbWorkStatuslkpProvider"
			
		private SqlBbWorkStatuslkpProvider innerSqlBbWorkStatuslkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbWorkStatuslkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbWorkStatuslkpProviderBase BbWorkStatuslkpProvider
		{
			get
			{
				if (innerSqlBbWorkStatuslkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbWorkStatuslkpProvider == null)
						{
							this.innerSqlBbWorkStatuslkpProvider = new SqlBbWorkStatuslkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbWorkStatuslkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbWorkStatuslkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbWorkStatuslkpProvider SqlBbWorkStatuslkpProvider
		{
			get {return BbWorkStatuslkpProvider as SqlBbWorkStatuslkpProvider;}
		}
		
		#endregion
		
		
		#region "BbPappPatientMedProblemFupProvider"
			
		private SqlBbPappPatientMedProblemFupProvider innerSqlBbPappPatientMedProblemFupProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbPappPatientMedProblemFup"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbPappPatientMedProblemFupProviderBase BbPappPatientMedProblemFupProvider
		{
			get
			{
				if (innerSqlBbPappPatientMedProblemFupProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbPappPatientMedProblemFupProvider == null)
						{
							this.innerSqlBbPappPatientMedProblemFupProvider = new SqlBbPappPatientMedProblemFupProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbPappPatientMedProblemFupProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbPappPatientMedProblemFupProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbPappPatientMedProblemFupProvider SqlBbPappPatientMedProblemFupProvider
		{
			get {return BbPappPatientMedProblemFupProvider as SqlBbPappPatientMedProblemFupProvider;}
		}
		
		#endregion
		
		
		#region "BbAdditionalUserDetailProvider"
			
		private SqlBbAdditionalUserDetailProvider innerSqlBbAdditionalUserDetailProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbAdditionalUserDetail"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbAdditionalUserDetailProviderBase BbAdditionalUserDetailProvider
		{
			get
			{
				if (innerSqlBbAdditionalUserDetailProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbAdditionalUserDetailProvider == null)
						{
							this.innerSqlBbAdditionalUserDetailProvider = new SqlBbAdditionalUserDetailProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbAdditionalUserDetailProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbAdditionalUserDetailProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbAdditionalUserDetailProvider SqlBbAdditionalUserDetailProvider
		{
			get {return BbAdditionalUserDetailProvider as SqlBbAdditionalUserDetailProvider;}
		}
		
		#endregion
		
		
		#region "BbAnswerlkpProvider"
			
		private SqlBbAnswerlkpProvider innerSqlBbAnswerlkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbAnswerlkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbAnswerlkpProviderBase BbAnswerlkpProvider
		{
			get
			{
				if (innerSqlBbAnswerlkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbAnswerlkpProvider == null)
						{
							this.innerSqlBbAnswerlkpProvider = new SqlBbAnswerlkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbAnswerlkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbAnswerlkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbAnswerlkpProvider SqlBbAnswerlkpProvider
		{
			get {return BbAnswerlkpProvider as SqlBbAnswerlkpProvider;}
		}
		
		#endregion
		
		
		#region "BbCommonFrequencylkpProvider"
			
		private SqlBbCommonFrequencylkpProvider innerSqlBbCommonFrequencylkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbCommonFrequencylkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbCommonFrequencylkpProviderBase BbCommonFrequencylkpProvider
		{
			get
			{
				if (innerSqlBbCommonFrequencylkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbCommonFrequencylkpProvider == null)
						{
							this.innerSqlBbCommonFrequencylkpProvider = new SqlBbCommonFrequencylkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbCommonFrequencylkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbCommonFrequencylkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbCommonFrequencylkpProvider SqlBbCommonFrequencylkpProvider
		{
			get {return BbCommonFrequencylkpProvider as SqlBbCommonFrequencylkpProvider;}
		}
		
		#endregion
		
		
		#region "BbConfigFactoryProvider"
			
		private SqlBbConfigFactoryProvider innerSqlBbConfigFactoryProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbConfigFactory"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbConfigFactoryProviderBase BbConfigFactoryProvider
		{
			get
			{
				if (innerSqlBbConfigFactoryProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbConfigFactoryProvider == null)
						{
							this.innerSqlBbConfigFactoryProvider = new SqlBbConfigFactoryProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbConfigFactoryProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbConfigFactoryProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbConfigFactoryProvider SqlBbConfigFactoryProvider
		{
			get {return BbConfigFactoryProvider as SqlBbConfigFactoryProvider;}
		}
		
		#endregion
		
		
		#region "BbEthnicitylkpProvider"
			
		private SqlBbEthnicitylkpProvider innerSqlBbEthnicitylkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbEthnicitylkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbEthnicitylkpProviderBase BbEthnicitylkpProvider
		{
			get
			{
				if (innerSqlBbEthnicitylkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbEthnicitylkpProvider == null)
						{
							this.innerSqlBbEthnicitylkpProvider = new SqlBbEthnicitylkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbEthnicitylkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbEthnicitylkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbEthnicitylkpProvider SqlBbEthnicitylkpProvider
		{
			get {return BbEthnicitylkpProvider as SqlBbEthnicitylkpProvider;}
		}
		
		#endregion
		
		
		#region "BbFileStorageProvider"
			
		private SqlBbFileStorageProvider innerSqlBbFileStorageProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbFileStorage"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbFileStorageProviderBase BbFileStorageProvider
		{
			get
			{
				if (innerSqlBbFileStorageProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbFileStorageProvider == null)
						{
							this.innerSqlBbFileStorageProvider = new SqlBbFileStorageProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbFileStorageProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbFileStorageProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbFileStorageProvider SqlBbFileStorageProvider
		{
			get {return BbFileStorageProvider as SqlBbFileStorageProvider;}
		}
		
		#endregion
		
		
		#region "BbLesionlkpProvider"
			
		private SqlBbLesionlkpProvider innerSqlBbLesionlkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbLesionlkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbLesionlkpProviderBase BbLesionlkpProvider
		{
			get
			{
				if (innerSqlBbLesionlkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbLesionlkpProvider == null)
						{
							this.innerSqlBbLesionlkpProvider = new SqlBbLesionlkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbLesionlkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbLesionlkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbLesionlkpProvider SqlBbLesionlkpProvider
		{
			get {return BbLesionlkpProvider as SqlBbLesionlkpProvider;}
		}
		
		#endregion
		
		
		#region "BbPatientCohortTrackingClinicAttendanceLkpProvider"
			
		private SqlBbPatientCohortTrackingClinicAttendanceLkpProvider innerSqlBbPatientCohortTrackingClinicAttendanceLkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbPatientCohortTrackingClinicAttendanceLkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbPatientCohortTrackingClinicAttendanceLkpProviderBase BbPatientCohortTrackingClinicAttendanceLkpProvider
		{
			get
			{
				if (innerSqlBbPatientCohortTrackingClinicAttendanceLkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbPatientCohortTrackingClinicAttendanceLkpProvider == null)
						{
							this.innerSqlBbPatientCohortTrackingClinicAttendanceLkpProvider = new SqlBbPatientCohortTrackingClinicAttendanceLkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbPatientCohortTrackingClinicAttendanceLkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbPatientCohortTrackingClinicAttendanceLkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbPatientCohortTrackingClinicAttendanceLkpProvider SqlBbPatientCohortTrackingClinicAttendanceLkpProvider
		{
			get {return BbPatientCohortTrackingClinicAttendanceLkpProvider as SqlBbPatientCohortTrackingClinicAttendanceLkpProvider;}
		}
		
		#endregion
		
		
		#region "BbLoginLogProvider"
			
		private SqlBbLoginLogProvider innerSqlBbLoginLogProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbLoginLog"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbLoginLogProviderBase BbLoginLogProvider
		{
			get
			{
				if (innerSqlBbLoginLogProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbLoginLogProvider == null)
						{
							this.innerSqlBbLoginLogProvider = new SqlBbLoginLogProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbLoginLogProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbLoginLogProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbLoginLogProvider SqlBbLoginLogProvider
		{
			get {return BbLoginLogProvider as SqlBbLoginLogProvider;}
		}
		
		#endregion
		
		
		#region "BbMailingListSubscriptionsProvider"
			
		private SqlBbMailingListSubscriptionsProvider innerSqlBbMailingListSubscriptionsProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbMailingListSubscriptions"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbMailingListSubscriptionsProviderBase BbMailingListSubscriptionsProvider
		{
			get
			{
				if (innerSqlBbMailingListSubscriptionsProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbMailingListSubscriptionsProvider == null)
						{
							this.innerSqlBbMailingListSubscriptionsProvider = new SqlBbMailingListSubscriptionsProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbMailingListSubscriptionsProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbMailingListSubscriptionsProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbMailingListSubscriptionsProvider SqlBbMailingListSubscriptionsProvider
		{
			get {return BbMailingListSubscriptionsProvider as SqlBbMailingListSubscriptionsProvider;}
		}
		
		#endregion
		
		
		#region "BbNotificationTypelkpProvider"
			
		private SqlBbNotificationTypelkpProvider innerSqlBbNotificationTypelkpProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbNotificationTypelkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbNotificationTypelkpProviderBase BbNotificationTypelkpProvider
		{
			get
			{
				if (innerSqlBbNotificationTypelkpProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbNotificationTypelkpProvider == null)
						{
							this.innerSqlBbNotificationTypelkpProvider = new SqlBbNotificationTypelkpProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbNotificationTypelkpProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbNotificationTypelkpProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbNotificationTypelkpProvider SqlBbNotificationTypelkpProvider
		{
			get {return BbNotificationTypelkpProvider as SqlBbNotificationTypelkpProvider;}
		}
		
		#endregion
		
		
		#region "BbNotificationProvider"
			
		private SqlBbNotificationProvider innerSqlBbNotificationProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbNotification"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbNotificationProviderBase BbNotificationProvider
		{
			get
			{
				if (innerSqlBbNotificationProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbNotificationProvider == null)
						{
							this.innerSqlBbNotificationProvider = new SqlBbNotificationProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbNotificationProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbNotificationProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbNotificationProvider SqlBbNotificationProvider
		{
			get {return BbNotificationProvider as SqlBbNotificationProvider;}
		}
		
		#endregion
		
		
		#region "BbPappPatientCohortTrackingProvider"
			
		private SqlBbPappPatientCohortTrackingProvider innerSqlBbPappPatientCohortTrackingProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbPappPatientCohortTracking"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbPappPatientCohortTrackingProviderBase BbPappPatientCohortTrackingProvider
		{
			get
			{
				if (innerSqlBbPappPatientCohortTrackingProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbPappPatientCohortTrackingProvider == null)
						{
							this.innerSqlBbPappPatientCohortTrackingProvider = new SqlBbPappPatientCohortTrackingProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbPappPatientCohortTrackingProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbPappPatientCohortTrackingProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbPappPatientCohortTrackingProvider SqlBbPappPatientCohortTrackingProvider
		{
			get {return BbPappPatientCohortTrackingProvider as SqlBbPappPatientCohortTrackingProvider;}
		}
		
		#endregion
		
		
		#region "BbPappPatientDlqiProvider"
			
		private SqlBbPappPatientDlqiProvider innerSqlBbPappPatientDlqiProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbPappPatientDlqi"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbPappPatientDlqiProviderBase BbPappPatientDlqiProvider
		{
			get
			{
				if (innerSqlBbPappPatientDlqiProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbPappPatientDlqiProvider == null)
						{
							this.innerSqlBbPappPatientDlqiProvider = new SqlBbPappPatientDlqiProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbPappPatientDlqiProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbPappPatientDlqiProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbPappPatientDlqiProvider SqlBbPappPatientDlqiProvider
		{
			get {return BbPappPatientDlqiProvider as SqlBbPappPatientDlqiProvider;}
		}
		
		#endregion
		
		
		#region "BbPappPatientLifestyleProvider"
			
		private SqlBbPappPatientLifestyleProvider innerSqlBbPappPatientLifestyleProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbPappPatientLifestyle"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbPappPatientLifestyleProviderBase BbPappPatientLifestyleProvider
		{
			get
			{
				if (innerSqlBbPappPatientLifestyleProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbPappPatientLifestyleProvider == null)
						{
							this.innerSqlBbPappPatientLifestyleProvider = new SqlBbPappPatientLifestyleProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbPappPatientLifestyleProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbPappPatientLifestyleProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbPappPatientLifestyleProvider SqlBbPappPatientLifestyleProvider
		{
			get {return BbPappPatientLifestyleProvider as SqlBbPappPatientLifestyleProvider;}
		}
		
		#endregion
		
		
		#region "BbMailingListsProvider"
			
		private SqlBbMailingListsProvider innerSqlBbMailingListsProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbMailingLists"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbMailingListsProviderBase BbMailingListsProvider
		{
			get
			{
				if (innerSqlBbMailingListsProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbMailingListsProvider == null)
						{
							this.innerSqlBbMailingListsProvider = new SqlBbMailingListsProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbMailingListsProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbMailingListsProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbMailingListsProvider SqlBbMailingListsProvider
		{
			get {return BbMailingListsProvider as SqlBbMailingListsProvider;}
		}
		
		#endregion
		
		
		#region "BbPatientCohortTrackingProvider"
			
		private SqlBbPatientCohortTrackingProvider innerSqlBbPatientCohortTrackingProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="BbPatientCohortTracking"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override BbPatientCohortTrackingProviderBase BbPatientCohortTrackingProvider
		{
			get
			{
				if (innerSqlBbPatientCohortTrackingProvider == null) 
				{
					lock (syncRoot) 
					{
						if (innerSqlBbPatientCohortTrackingProvider == null)
						{
							this.innerSqlBbPatientCohortTrackingProvider = new SqlBbPatientCohortTrackingProvider(_connectionString, _useStoredProcedure, _providerInvariantName);
						}
					}
				}
				return innerSqlBbPatientCohortTrackingProvider;
			}
		}
		
		/// <summary>
		/// Gets the current <see cref="SqlBbPatientCohortTrackingProvider"/>.
		/// </summary>
		/// <value></value>
		public SqlBbPatientCohortTrackingProvider SqlBbPatientCohortTrackingProvider
		{
			get {return BbPatientCohortTrackingProvider as SqlBbPatientCohortTrackingProvider;}
		}
		
		#endregion
		
		
		
		#region "General data access methods"

		#region "ExecuteNonQuery"
		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(string storedProcedureName, params object[] parameterValues)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteNonQuery(storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteNonQuery(transactionManager.TransactionObject, storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		public override void ExecuteNonQuery(DbCommand commandWrapper)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			database.ExecuteNonQuery(commandWrapper);	
			
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		public override void ExecuteNonQuery(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			database.ExecuteNonQuery(commandWrapper, transactionManager.TransactionObject);	
		}


		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(CommandType commandType, string commandText)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteNonQuery(commandType, commandText);	
		}
		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			Database database = transactionManager.Database;			
			return database.ExecuteNonQuery(transactionManager.TransactionObject , commandType, commandText);				
		}
		#endregion

		#region "ExecuteDataReader"
		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(string storedProcedureName, params object[] parameterValues)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteReader(storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			Database database = transactionManager.Database;
			return database.ExecuteReader(transactionManager.TransactionObject, storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(DbCommand commandWrapper)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteReader(commandWrapper);	
		}

		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			Database database = transactionManager.Database;
			return database.ExecuteReader(commandWrapper, transactionManager.TransactionObject);	
		}


		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(CommandType commandType, string commandText)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteReader(commandType, commandText);	
		}
		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			Database database = transactionManager.Database;			
			return database.ExecuteReader(transactionManager.TransactionObject , commandType, commandText);				
		}
		#endregion

		#region "ExecuteDataSet"
		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(string storedProcedureName, params object[] parameterValues)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteDataSet(storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			Database database = transactionManager.Database;
			return database.ExecuteDataSet(transactionManager.TransactionObject, storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(DbCommand commandWrapper)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteDataSet(commandWrapper);	
		}

		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			Database database = transactionManager.Database;
			return database.ExecuteDataSet(commandWrapper, transactionManager.TransactionObject);	
		}


		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(CommandType commandType, string commandText)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteDataSet(commandType, commandText);	
		}
		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			Database database = transactionManager.Database;			
			return database.ExecuteDataSet(transactionManager.TransactionObject , commandType, commandText);				
		}
		#endregion

		#region "ExecuteScalar"
		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override object ExecuteScalar(string storedProcedureName, params object[] parameterValues)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteScalar(storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override object ExecuteScalar(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			Database database = transactionManager.Database;
			return database.ExecuteScalar(transactionManager.TransactionObject, storedProcedureName, parameterValues);	
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override object ExecuteScalar(DbCommand commandWrapper)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);			
			return database.ExecuteScalar(commandWrapper);	
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override object ExecuteScalar(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			Database database = transactionManager.Database;
			return database.ExecuteScalar(commandWrapper, transactionManager.TransactionObject);	
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override object ExecuteScalar(CommandType commandType, string commandText)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			return database.ExecuteScalar(commandType, commandText);	
		}
		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override object ExecuteScalar(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			Database database = transactionManager.Database;			
			return database.ExecuteScalar(transactionManager.TransactionObject , commandType, commandText);				
		}
		#endregion

		#endregion


	}
}
