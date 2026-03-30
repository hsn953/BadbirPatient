#region Using directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Web;
using MHS.Badbir.NetTiers.Entities;
using MHS.Badbir.NetTiers.Data;
using MHS.Badbir.NetTiers.Data.Bases;

#endregion

namespace MHS.Badbir.NetTiers.Data
{
	/// <summary>
	/// This class represents the Data source repository and gives access to all the underlying providers.
	/// </summary>
	[CLSCompliant(true)]
	public sealed class DataRepository 
	{
		private static volatile NetTiersProvider _provider = null;
        private static volatile NetTiersProviderCollection _providers = null;
		private static volatile NetTiersServiceSection _section = null;
		private static volatile Configuration _config = null;
        
        private static object SyncRoot = new object();
				
		private DataRepository()
		{
		}
		
		#region Public LoadProvider
		/// <summary>
        /// Enables the DataRepository to programatically create and 
        /// pass in a <c>NetTiersProvider</c> during runtime.
        /// </summary>
        /// <param name="provider">An instatiated NetTiersProvider.</param>
        public static void LoadProvider(NetTiersProvider provider)
        {
			LoadProvider(provider, false);
        }
		
		/// <summary>
        /// Enables the DataRepository to programatically create and 
        /// pass in a <c>NetTiersProvider</c> during runtime.
        /// </summary>
        /// <param name="provider">An instatiated NetTiersProvider.</param>
        /// <param name="setAsDefault">ability to set any valid provider as the default provider for the DataRepository.</param>
		public static void LoadProvider(NetTiersProvider provider, bool setAsDefault)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            if (_providers == null)
			{
				lock(SyncRoot)
				{
            		if (_providers == null)
						_providers = new NetTiersProviderCollection();
				}
			}
			
            if (_providers[provider.Name] == null)
            {
                lock (_providers.SyncRoot)
                {
                    _providers.Add(provider);
                }
            }

            if (_provider == null || setAsDefault)
            {
                lock (SyncRoot)
                {
                    if(_provider == null || setAsDefault)
                         _provider = provider;
                }
            }
        }
		#endregion 
		
		///<summary>
		/// Configuration based provider loading, will load the providers on first call.
		///</summary>
		private static void LoadProviders()
        {
            // Avoid claiming lock if providers are already loaded
            if (_provider == null)
            {
                lock (SyncRoot)
                {
                    // Do this again to make sure _provider is still null
                    if (_provider == null)
                    {
                        // Load registered providers and point _provider to the default provider
                        _providers = new NetTiersProviderCollection();

                        ProvidersHelper.InstantiateProviders(NetTiersSection.Providers, _providers, typeof(NetTiersProvider));
						_provider = _providers[NetTiersSection.DefaultProvider];

                        if (_provider == null)
                        {
                            throw new ProviderException("Unable to load default NetTiersProvider");
                        }
                    }
                }
            }
        }

		/// <summary>
        /// Gets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public static NetTiersProvider Provider
        {
            get { LoadProviders(); return _provider; }
        }

		/// <summary>
        /// Gets the provider collection.
        /// </summary>
        /// <value>The providers.</value>
        public static NetTiersProviderCollection Providers
        {
            get { LoadProviders(); return _providers; }
        }
		
		/// <summary>
		/// Creates a new <see cref="TransactionManager"/> instance from the current datasource.
		/// </summary>
		/// <returns></returns>
		public TransactionManager CreateTransaction()
		{
			return _provider.CreateTransaction();
		}

		#region Configuration

		/// <summary>
		/// Gets a reference to the configured NetTiersServiceSection object.
		/// </summary>
		public static NetTiersServiceSection NetTiersSection
		{
			get
			{
				// Try to get a reference to the default <netTiersService> section
				_section = WebConfigurationManager.GetSection("netTiersService") as NetTiersServiceSection;

				if ( _section == null )
				{
					// otherwise look for section based on the assembly name
					_section = WebConfigurationManager.GetSection("MHS.Badbir.NetTiers.Data") as NetTiersServiceSection;
				}

				#region Design-Time Support

				if ( _section == null )
				{
					// lastly, try to find the specific NetTiersServiceSection for this assembly
					foreach ( ConfigurationSection temp in Configuration.Sections )
					{
						if ( temp is NetTiersServiceSection )
						{
							_section = temp as NetTiersServiceSection;
							break;
						}
					}
				}

				#endregion Design-Time Support
				
				if ( _section == null )
				{
					throw new ProviderException("Unable to load NetTiersServiceSection");
				}

				return _section;
			}
		}

		#region Design-Time Support

		/// <summary>
		/// Gets a reference to the application configuration object.
		/// </summary>
		public static Configuration Configuration
		{
			get
			{
				if ( _config == null )
				{
					// load specific config file
					if ( HttpContext.Current != null )
					{
						_config = WebConfigurationManager.OpenWebConfiguration("~");
					}
					else
					{
						String configFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".config", "").Replace(".temp", "");

						// check for design mode
						if ( configFile.ToLower().Contains("devenv.exe") )
						{
							_config = GetDesignTimeConfig();
						}
						else
						{
							_config = ConfigurationManager.OpenExeConfiguration(configFile);
						}
					}
				}

				return _config;
			}
		}

		private static Configuration GetDesignTimeConfig()
		{
			ExeConfigurationFileMap configMap = null;
			Configuration config = null;
			String path = null;

			// Get an instance of the currently running Visual Studio IDE.
			EnvDTE80.DTE2 dte = (EnvDTE80.DTE2) System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.12.0");
			
			if ( dte != null )
			{
				dte.SuppressUI = true;

				EnvDTE.ProjectItem item = dte.Solution.FindProjectItem("web.config");
				if ( item != null )
				{
					if (!item.ContainingProject.FullName.ToLower().StartsWith("http:"))
               {
                  System.IO.FileInfo info = new System.IO.FileInfo(item.ContainingProject.FullName);
                  path = String.Format("{0}\\{1}", info.Directory.FullName, item.Name);
                  configMap = new ExeConfigurationFileMap();
                  configMap.ExeConfigFilename = path;
               }
               else
               {
                  configMap = new ExeConfigurationFileMap();
                  configMap.ExeConfigFilename = item.get_FileNames(0);
               }}

				/*
				Array projects = (Array) dte2.ActiveSolutionProjects;
				EnvDTE.Project project = (EnvDTE.Project) projects.GetValue(0);
				System.IO.FileInfo info;

				foreach ( EnvDTE.ProjectItem item in project.ProjectItems )
				{
					if ( String.Compare(item.Name, "web.config", true) == 0 )
					{
						info = new System.IO.FileInfo(project.FullName);
						path = String.Format("{0}\\{1}", info.Directory.FullName, item.Name);
						configMap = new ExeConfigurationFileMap();
						configMap.ExeConfigFilename = path;
						break;
					}
				}
				*/
			}

			config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
			return config;
		}

		#endregion Design-Time Support

		#endregion Configuration

		#region Connections

		/// <summary>
		/// Gets a reference to the ConnectionStringSettings collection.
		/// </summary>
		public static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
					// use default ConnectionStrings if _section has already been discovered
					if ( _config == null && _section != null )
					{
						return WebConfigurationManager.ConnectionStrings;
					}
					
					return Configuration.ConnectionStrings.ConnectionStrings;
			}
		}

		// dictionary of connection providers
		private static Dictionary<String, ConnectionProvider> _connections;

		/// <summary>
		/// Gets the dictionary of connection providers.
		/// </summary>
		public static Dictionary<String, ConnectionProvider> Connections
		{
			get
			{
				if ( _connections == null )
				{
					lock (SyncRoot)
                	{
						if (_connections == null)
						{
							_connections = new Dictionary<String, ConnectionProvider>();
		
							// add a connection provider for each configured connection string
							foreach ( ConnectionStringSettings conn in ConnectionStrings )
							{
								_connections.Add(conn.Name, new ConnectionProvider(conn.Name, conn.ConnectionString));
							}
						}
					}
				}

				return _connections;
			}
		}

		/// <summary>
		/// Adds the specified connection string to the map of connection strings.
		/// </summary>
		/// <param name="connectionStringName">The connection string name.</param>
		/// <param name="connectionString">The provider specific connection information.</param>
		public static void AddConnection(String connectionStringName, String connectionString)
		{
			lock (SyncRoot)
            {
				Connections.Remove(connectionStringName);
				ConnectionProvider connection = new ConnectionProvider(connectionStringName, connectionString);
				Connections.Add(connectionStringName, connection);
			}
		}

		/// <summary>
		/// Provides ability to switch connection string at runtime.
		/// </summary>
		public sealed class ConnectionProvider
		{
			private NetTiersProvider _provider;
			private NetTiersProviderCollection _providers;
			private String _connectionStringName;
			private String _connectionString;


			/// <summary>
			/// Initializes a new instance of the ConnectionProvider class.
			/// </summary>
			/// <param name="connectionStringName">The connection string name.</param>
			/// <param name="connectionString">The provider specific connection information.</param>
			public ConnectionProvider(String connectionStringName, String connectionString)
			{
				_connectionString = connectionString;
				_connectionStringName = connectionStringName;
			}

			/// <summary>
			/// Gets the provider.
			/// </summary>
			public NetTiersProvider Provider
			{
				get { LoadProviders(); return _provider; }
			}

			/// <summary>
			/// Gets the provider collection.
			/// </summary>
			public NetTiersProviderCollection Providers
			{
				get { LoadProviders(); return _providers; }
			}

			/// <summary>
			/// Instantiates the configured providers based on the supplied connection string.
			/// </summary>
			private void LoadProviders()
			{
				DataRepository.LoadProviders();

				// Avoid claiming lock if providers are already loaded
				if ( _providers == null )
				{
					lock ( SyncRoot )
					{
						// Do this again to make sure _provider is still null
						if ( _providers == null )
						{
							// apply connection information to each provider
							for ( int i = 0; i < NetTiersSection.Providers.Count; i++ )
							{
								NetTiersSection.Providers[i].Parameters["connectionStringName"] = _connectionStringName;
								// remove previous connection string, if any
								NetTiersSection.Providers[i].Parameters.Remove("connectionString");

								if ( !String.IsNullOrEmpty(_connectionString) )
								{
									NetTiersSection.Providers[i].Parameters["connectionString"] = _connectionString;
								}
							}

							// Load registered providers and point _provider to the default provider
							_providers = new NetTiersProviderCollection();

							ProvidersHelper.InstantiateProviders(NetTiersSection.Providers, _providers, typeof(NetTiersProvider));
							_provider = _providers[NetTiersSection.DefaultProvider];
						}
					}
				}
			}
		}

		#endregion Connections

		#region Static properties
		
		#region BbPatientCohortTrackingStatuslkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbPatientCohortTrackingStatuslkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbPatientCohortTrackingStatuslkpProviderBase BbPatientCohortTrackingStatuslkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbPatientCohortTrackingStatuslkpProvider;
			}
		}
		
		#endregion
		
		#region BbPatientProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbPatient"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbPatientProviderBase BbPatientProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbPatientProvider;
			}
		}
		
		#endregion
		
		#region BbCentreRegionlkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbCentreRegionlkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbCentreRegionlkpProviderBase BbCentreRegionlkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbCentreRegionlkpProvider;
			}
		}
		
		#endregion
		
		#region BbCentrestatusProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbCentrestatus"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbCentrestatusProviderBase BbCentrestatusProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbCentrestatusProvider;
			}
		}
		
		#endregion
		
		#region BbUkcrNregionlkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbUkcrNregionlkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbUkcrNregionlkpProviderBase BbUkcrNregionlkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbUkcrNregionlkpProvider;
			}
		}
		
		#endregion
		
		#region BbCentreProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbCentre"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbCentreProviderBase BbCentreProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbCentreProvider;
			}
		}
		
		#endregion
		
		#region BbPatientdrugProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbPatientdrug"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbPatientdrugProviderBase BbPatientdrugProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbPatientdrugProvider;
			}
		}
		
		#endregion
		
		#region BbQueryTypelkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbQueryTypelkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbQueryTypelkpProviderBase BbQueryTypelkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbQueryTypelkpProvider;
			}
		}
		
		#endregion
		
		#region BbQueryStatuslkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbQueryStatuslkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbQueryStatuslkpProviderBase BbQueryStatuslkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbQueryStatuslkpProvider;
			}
		}
		
		#endregion
		
		#region BbPatientStatusDetaillkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbPatientStatusDetaillkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbPatientStatusDetaillkpProviderBase BbPatientStatusDetaillkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbPatientStatusDetaillkpProvider;
			}
		}
		
		#endregion
		
		#region BbPatientdrugdoseProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbPatientdrugdose"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbPatientdrugdoseProviderBase BbPatientdrugdoseProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbPatientdrugdoseProvider;
			}
		}
		
		#endregion
		
		#region BbPatientLifestyleProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbPatientLifestyle"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbPatientLifestyleProviderBase BbPatientLifestyleProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbPatientLifestyleProvider;
			}
		}
		
		#endregion
		
		#region BbQueryForCentreProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbQueryForCentre"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbQueryForCentreProviderBase BbQueryForCentreProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbQueryForCentreProvider;
			}
		}
		
		#endregion
		
		#region BbPatientStatuslkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbPatientStatuslkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbPatientStatuslkpProviderBase BbPatientStatuslkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbPatientStatuslkpProvider;
			}
		}
		
		#endregion
		
		#region BbQueryForCentreMessageProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbQueryForCentreMessage"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbQueryForCentreMessageProviderBase BbQueryForCentreMessageProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbQueryForCentreMessageProvider;
			}
		}
		
		#endregion
		
		#region BbGenderlkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbGenderlkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbGenderlkpProviderBase BbGenderlkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbGenderlkpProvider;
			}
		}
		
		#endregion
		
		#region BbPatientCohortHistoryProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbPatientCohortHistory"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbPatientCohortHistoryProviderBase BbPatientCohortHistoryProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbPatientCohortHistoryProvider;
			}
		}
		
		#endregion
		
		#region BbQueryProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbQuery"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbQueryProviderBase BbQueryProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbQueryProvider;
			}
		}
		
		#endregion
		
		#region BbQueryMessageProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbQueryMessage"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbQueryMessageProviderBase BbQueryMessageProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbQueryMessageProvider;
			}
		}
		
		#endregion
		
		#region BbSaeClinicianlkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbSaeClinicianlkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbSaeClinicianlkpProviderBase BbSaeClinicianlkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbSaeClinicianlkpProvider;
			}
		}
		
		#endregion
		
		#region BbCohortlkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbCohortlkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbCohortlkpProviderBase BbCohortlkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbCohortlkpProvider;
			}
		}
		
		#endregion
		
		#region BbTitlelkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbTitlelkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbTitlelkpProviderBase BbTitlelkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbTitlelkpProvider;
			}
		}
		
		#endregion
		
		#region BbPositionRolelkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbPositionRolelkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbPositionRolelkpProviderBase BbPositionRolelkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbPositionRolelkpProvider;
			}
		}
		
		#endregion
		
		#region BbWorkStatuslkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbWorkStatuslkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbWorkStatuslkpProviderBase BbWorkStatuslkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbWorkStatuslkpProvider;
			}
		}
		
		#endregion
		
		#region BbPappPatientMedProblemFupProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbPappPatientMedProblemFup"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbPappPatientMedProblemFupProviderBase BbPappPatientMedProblemFupProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbPappPatientMedProblemFupProvider;
			}
		}
		
		#endregion
		
		#region BbAdditionalUserDetailProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbAdditionalUserDetail"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbAdditionalUserDetailProviderBase BbAdditionalUserDetailProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbAdditionalUserDetailProvider;
			}
		}
		
		#endregion
		
		#region BbAnswerlkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbAnswerlkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbAnswerlkpProviderBase BbAnswerlkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbAnswerlkpProvider;
			}
		}
		
		#endregion
		
		#region BbCommonFrequencylkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbCommonFrequencylkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbCommonFrequencylkpProviderBase BbCommonFrequencylkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbCommonFrequencylkpProvider;
			}
		}
		
		#endregion
		
		#region BbConfigFactoryProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbConfigFactory"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbConfigFactoryProviderBase BbConfigFactoryProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbConfigFactoryProvider;
			}
		}
		
		#endregion
		
		#region BbEthnicitylkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbEthnicitylkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbEthnicitylkpProviderBase BbEthnicitylkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbEthnicitylkpProvider;
			}
		}
		
		#endregion
		
		#region BbFileStorageProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbFileStorage"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbFileStorageProviderBase BbFileStorageProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbFileStorageProvider;
			}
		}
		
		#endregion
		
		#region BbLesionlkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbLesionlkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbLesionlkpProviderBase BbLesionlkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbLesionlkpProvider;
			}
		}
		
		#endregion
		
		#region BbPatientCohortTrackingClinicAttendanceLkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbPatientCohortTrackingClinicAttendanceLkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbPatientCohortTrackingClinicAttendanceLkpProviderBase BbPatientCohortTrackingClinicAttendanceLkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbPatientCohortTrackingClinicAttendanceLkpProvider;
			}
		}
		
		#endregion
		
		#region BbLoginLogProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbLoginLog"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbLoginLogProviderBase BbLoginLogProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbLoginLogProvider;
			}
		}
		
		#endregion
		
		#region BbMailingListSubscriptionsProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbMailingListSubscriptions"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbMailingListSubscriptionsProviderBase BbMailingListSubscriptionsProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbMailingListSubscriptionsProvider;
			}
		}
		
		#endregion
		
		#region BbNotificationTypelkpProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbNotificationTypelkp"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbNotificationTypelkpProviderBase BbNotificationTypelkpProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbNotificationTypelkpProvider;
			}
		}
		
		#endregion
		
		#region BbNotificationProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbNotification"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbNotificationProviderBase BbNotificationProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbNotificationProvider;
			}
		}
		
		#endregion
		
		#region BbPappPatientCohortTrackingProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbPappPatientCohortTracking"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbPappPatientCohortTrackingProviderBase BbPappPatientCohortTrackingProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbPappPatientCohortTrackingProvider;
			}
		}
		
		#endregion
		
		#region BbPappPatientDlqiProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbPappPatientDlqi"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbPappPatientDlqiProviderBase BbPappPatientDlqiProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbPappPatientDlqiProvider;
			}
		}
		
		#endregion
		
		#region BbPappPatientLifestyleProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbPappPatientLifestyle"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbPappPatientLifestyleProviderBase BbPappPatientLifestyleProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbPappPatientLifestyleProvider;
			}
		}
		
		#endregion
		
		#region BbMailingListsProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbMailingLists"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbMailingListsProviderBase BbMailingListsProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbMailingListsProvider;
			}
		}
		
		#endregion
		
		#region BbPatientCohortTrackingProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BbPatientCohortTracking"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BbPatientCohortTrackingProviderBase BbPatientCohortTrackingProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BbPatientCohortTrackingProvider;
			}
		}
		
		#endregion
		
		
		#endregion
	}
	
	#region Query/Filters
		
	#region BbPatientCohortTrackingStatuslkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortTrackingStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortTrackingStatuslkpFilters : BbPatientCohortTrackingStatuslkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingStatuslkpFilters class.
		/// </summary>
		public BbPatientCohortTrackingStatuslkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingStatuslkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientCohortTrackingStatuslkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingStatuslkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientCohortTrackingStatuslkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientCohortTrackingStatuslkpFilters
	
	#region BbPatientCohortTrackingStatuslkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbPatientCohortTrackingStatuslkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortTrackingStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortTrackingStatuslkpQuery : BbPatientCohortTrackingStatuslkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingStatuslkpQuery class.
		/// </summary>
		public BbPatientCohortTrackingStatuslkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingStatuslkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientCohortTrackingStatuslkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingStatuslkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientCohortTrackingStatuslkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientCohortTrackingStatuslkpQuery
		
	#region BbPatientFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientFilters : BbPatientFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientFilters class.
		/// </summary>
		public BbPatientFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientFilters
	
	#region BbPatientQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbPatientParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbPatient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientQuery : BbPatientParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientQuery class.
		/// </summary>
		public BbPatientQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientQuery
		
	#region BbCentreRegionlkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbCentreRegionlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCentreRegionlkpFilters : BbCentreRegionlkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCentreRegionlkpFilters class.
		/// </summary>
		public BbCentreRegionlkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbCentreRegionlkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbCentreRegionlkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbCentreRegionlkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbCentreRegionlkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbCentreRegionlkpFilters
	
	#region BbCentreRegionlkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbCentreRegionlkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbCentreRegionlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCentreRegionlkpQuery : BbCentreRegionlkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCentreRegionlkpQuery class.
		/// </summary>
		public BbCentreRegionlkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbCentreRegionlkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbCentreRegionlkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbCentreRegionlkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbCentreRegionlkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbCentreRegionlkpQuery
		
	#region BbCentrestatusFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbCentrestatus"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCentrestatusFilters : BbCentrestatusFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCentrestatusFilters class.
		/// </summary>
		public BbCentrestatusFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbCentrestatusFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbCentrestatusFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbCentrestatusFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbCentrestatusFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbCentrestatusFilters
	
	#region BbCentrestatusQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbCentrestatusParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbCentrestatus"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCentrestatusQuery : BbCentrestatusParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCentrestatusQuery class.
		/// </summary>
		public BbCentrestatusQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbCentrestatusQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbCentrestatusQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbCentrestatusQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbCentrestatusQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbCentrestatusQuery
		
	#region BbUkcrNregionlkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbUkcrNregionlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbUkcrNregionlkpFilters : BbUkcrNregionlkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbUkcrNregionlkpFilters class.
		/// </summary>
		public BbUkcrNregionlkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbUkcrNregionlkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbUkcrNregionlkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbUkcrNregionlkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbUkcrNregionlkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbUkcrNregionlkpFilters
	
	#region BbUkcrNregionlkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbUkcrNregionlkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbUkcrNregionlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbUkcrNregionlkpQuery : BbUkcrNregionlkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbUkcrNregionlkpQuery class.
		/// </summary>
		public BbUkcrNregionlkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbUkcrNregionlkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbUkcrNregionlkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbUkcrNregionlkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbUkcrNregionlkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbUkcrNregionlkpQuery
		
	#region BbCentreFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbCentre"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCentreFilters : BbCentreFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCentreFilters class.
		/// </summary>
		public BbCentreFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbCentreFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbCentreFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbCentreFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbCentreFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbCentreFilters
	
	#region BbCentreQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbCentreParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbCentre"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCentreQuery : BbCentreParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCentreQuery class.
		/// </summary>
		public BbCentreQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbCentreQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbCentreQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbCentreQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbCentreQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbCentreQuery
		
	#region BbPatientdrugFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientdrug"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientdrugFilters : BbPatientdrugFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugFilters class.
		/// </summary>
		public BbPatientdrugFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientdrugFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientdrugFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientdrugFilters
	
	#region BbPatientdrugQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbPatientdrugParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbPatientdrug"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientdrugQuery : BbPatientdrugParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugQuery class.
		/// </summary>
		public BbPatientdrugQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientdrugQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientdrugQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientdrugQuery
		
	#region BbQueryTypelkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryTypelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryTypelkpFilters : BbQueryTypelkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryTypelkpFilters class.
		/// </summary>
		public BbQueryTypelkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbQueryTypelkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbQueryTypelkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbQueryTypelkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbQueryTypelkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbQueryTypelkpFilters
	
	#region BbQueryTypelkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbQueryTypelkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbQueryTypelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryTypelkpQuery : BbQueryTypelkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryTypelkpQuery class.
		/// </summary>
		public BbQueryTypelkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbQueryTypelkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbQueryTypelkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbQueryTypelkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbQueryTypelkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbQueryTypelkpQuery
		
	#region BbQueryStatuslkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryStatuslkpFilters : BbQueryStatuslkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryStatuslkpFilters class.
		/// </summary>
		public BbQueryStatuslkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbQueryStatuslkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbQueryStatuslkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbQueryStatuslkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbQueryStatuslkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbQueryStatuslkpFilters
	
	#region BbQueryStatuslkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbQueryStatuslkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbQueryStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryStatuslkpQuery : BbQueryStatuslkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryStatuslkpQuery class.
		/// </summary>
		public BbQueryStatuslkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbQueryStatuslkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbQueryStatuslkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbQueryStatuslkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbQueryStatuslkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbQueryStatuslkpQuery
		
	#region BbPatientStatusDetaillkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientStatusDetaillkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientStatusDetaillkpFilters : BbPatientStatusDetaillkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientStatusDetaillkpFilters class.
		/// </summary>
		public BbPatientStatusDetaillkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientStatusDetaillkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientStatusDetaillkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientStatusDetaillkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientStatusDetaillkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientStatusDetaillkpFilters
	
	#region BbPatientStatusDetaillkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbPatientStatusDetaillkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbPatientStatusDetaillkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientStatusDetaillkpQuery : BbPatientStatusDetaillkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientStatusDetaillkpQuery class.
		/// </summary>
		public BbPatientStatusDetaillkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientStatusDetaillkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientStatusDetaillkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientStatusDetaillkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientStatusDetaillkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientStatusDetaillkpQuery
		
	#region BbPatientdrugdoseFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientdrugdose"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientdrugdoseFilters : BbPatientdrugdoseFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugdoseFilters class.
		/// </summary>
		public BbPatientdrugdoseFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugdoseFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientdrugdoseFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugdoseFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientdrugdoseFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientdrugdoseFilters
	
	#region BbPatientdrugdoseQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbPatientdrugdoseParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbPatientdrugdose"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientdrugdoseQuery : BbPatientdrugdoseParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugdoseQuery class.
		/// </summary>
		public BbPatientdrugdoseQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugdoseQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientdrugdoseQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugdoseQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientdrugdoseQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientdrugdoseQuery
		
	#region BbPatientLifestyleFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientLifestyle"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientLifestyleFilters : BbPatientLifestyleFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientLifestyleFilters class.
		/// </summary>
		public BbPatientLifestyleFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientLifestyleFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientLifestyleFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientLifestyleFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientLifestyleFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientLifestyleFilters
	
	#region BbPatientLifestyleQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbPatientLifestyleParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbPatientLifestyle"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientLifestyleQuery : BbPatientLifestyleParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientLifestyleQuery class.
		/// </summary>
		public BbPatientLifestyleQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientLifestyleQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientLifestyleQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientLifestyleQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientLifestyleQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientLifestyleQuery
		
	#region BbQueryForCentreFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryForCentre"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryForCentreFilters : BbQueryForCentreFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreFilters class.
		/// </summary>
		public BbQueryForCentreFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbQueryForCentreFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbQueryForCentreFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbQueryForCentreFilters
	
	#region BbQueryForCentreQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbQueryForCentreParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbQueryForCentre"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryForCentreQuery : BbQueryForCentreParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreQuery class.
		/// </summary>
		public BbQueryForCentreQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbQueryForCentreQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbQueryForCentreQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbQueryForCentreQuery
		
	#region BbPatientStatuslkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientStatuslkpFilters : BbPatientStatuslkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientStatuslkpFilters class.
		/// </summary>
		public BbPatientStatuslkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientStatuslkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientStatuslkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientStatuslkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientStatuslkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientStatuslkpFilters
	
	#region BbPatientStatuslkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbPatientStatuslkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbPatientStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientStatuslkpQuery : BbPatientStatuslkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientStatuslkpQuery class.
		/// </summary>
		public BbPatientStatuslkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientStatuslkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientStatuslkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientStatuslkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientStatuslkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientStatuslkpQuery
		
	#region BbQueryForCentreMessageFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryForCentreMessage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryForCentreMessageFilters : BbQueryForCentreMessageFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreMessageFilters class.
		/// </summary>
		public BbQueryForCentreMessageFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreMessageFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbQueryForCentreMessageFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreMessageFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbQueryForCentreMessageFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbQueryForCentreMessageFilters
	
	#region BbQueryForCentreMessageQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbQueryForCentreMessageParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbQueryForCentreMessage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryForCentreMessageQuery : BbQueryForCentreMessageParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreMessageQuery class.
		/// </summary>
		public BbQueryForCentreMessageQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreMessageQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbQueryForCentreMessageQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreMessageQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbQueryForCentreMessageQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbQueryForCentreMessageQuery
		
	#region BbGenderlkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbGenderlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbGenderlkpFilters : BbGenderlkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbGenderlkpFilters class.
		/// </summary>
		public BbGenderlkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbGenderlkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbGenderlkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbGenderlkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbGenderlkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbGenderlkpFilters
	
	#region BbGenderlkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbGenderlkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbGenderlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbGenderlkpQuery : BbGenderlkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbGenderlkpQuery class.
		/// </summary>
		public BbGenderlkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbGenderlkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbGenderlkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbGenderlkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbGenderlkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbGenderlkpQuery
		
	#region BbPatientCohortHistoryFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortHistoryFilters : BbPatientCohortHistoryFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortHistoryFilters class.
		/// </summary>
		public BbPatientCohortHistoryFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortHistoryFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientCohortHistoryFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortHistoryFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientCohortHistoryFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientCohortHistoryFilters
	
	#region BbPatientCohortHistoryQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbPatientCohortHistoryParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortHistoryQuery : BbPatientCohortHistoryParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortHistoryQuery class.
		/// </summary>
		public BbPatientCohortHistoryQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortHistoryQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientCohortHistoryQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortHistoryQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientCohortHistoryQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientCohortHistoryQuery
		
	#region BbQueryFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQuery"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryFilters : BbQueryFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryFilters class.
		/// </summary>
		public BbQueryFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbQueryFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbQueryFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbQueryFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbQueryFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbQueryFilters
	
	#region BbQueryQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbQueryParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbQuery"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryQuery : BbQueryParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryQuery class.
		/// </summary>
		public BbQueryQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbQueryQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbQueryQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbQueryQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbQueryQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbQueryQuery
		
	#region BbQueryMessageFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryMessage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryMessageFilters : BbQueryMessageFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryMessageFilters class.
		/// </summary>
		public BbQueryMessageFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbQueryMessageFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbQueryMessageFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbQueryMessageFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbQueryMessageFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbQueryMessageFilters
	
	#region BbQueryMessageQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbQueryMessageParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbQueryMessage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryMessageQuery : BbQueryMessageParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryMessageQuery class.
		/// </summary>
		public BbQueryMessageQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbQueryMessageQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbQueryMessageQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbQueryMessageQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbQueryMessageQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbQueryMessageQuery
		
	#region BbSaeClinicianlkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbSaeClinicianlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbSaeClinicianlkpFilters : BbSaeClinicianlkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbSaeClinicianlkpFilters class.
		/// </summary>
		public BbSaeClinicianlkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbSaeClinicianlkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbSaeClinicianlkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbSaeClinicianlkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbSaeClinicianlkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbSaeClinicianlkpFilters
	
	#region BbSaeClinicianlkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbSaeClinicianlkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbSaeClinicianlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbSaeClinicianlkpQuery : BbSaeClinicianlkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbSaeClinicianlkpQuery class.
		/// </summary>
		public BbSaeClinicianlkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbSaeClinicianlkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbSaeClinicianlkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbSaeClinicianlkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbSaeClinicianlkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbSaeClinicianlkpQuery
		
	#region BbCohortlkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbCohortlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCohortlkpFilters : BbCohortlkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCohortlkpFilters class.
		/// </summary>
		public BbCohortlkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbCohortlkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbCohortlkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbCohortlkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbCohortlkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbCohortlkpFilters
	
	#region BbCohortlkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbCohortlkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbCohortlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCohortlkpQuery : BbCohortlkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCohortlkpQuery class.
		/// </summary>
		public BbCohortlkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbCohortlkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbCohortlkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbCohortlkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbCohortlkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbCohortlkpQuery
		
	#region BbTitlelkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbTitlelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbTitlelkpFilters : BbTitlelkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbTitlelkpFilters class.
		/// </summary>
		public BbTitlelkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbTitlelkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbTitlelkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbTitlelkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbTitlelkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbTitlelkpFilters
	
	#region BbTitlelkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbTitlelkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbTitlelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbTitlelkpQuery : BbTitlelkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbTitlelkpQuery class.
		/// </summary>
		public BbTitlelkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbTitlelkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbTitlelkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbTitlelkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbTitlelkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbTitlelkpQuery
		
	#region BbPositionRolelkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPositionRolelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPositionRolelkpFilters : BbPositionRolelkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPositionRolelkpFilters class.
		/// </summary>
		public BbPositionRolelkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPositionRolelkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPositionRolelkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPositionRolelkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPositionRolelkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPositionRolelkpFilters
	
	#region BbPositionRolelkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbPositionRolelkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbPositionRolelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPositionRolelkpQuery : BbPositionRolelkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPositionRolelkpQuery class.
		/// </summary>
		public BbPositionRolelkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPositionRolelkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPositionRolelkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPositionRolelkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPositionRolelkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPositionRolelkpQuery
		
	#region BbWorkStatuslkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbWorkStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbWorkStatuslkpFilters : BbWorkStatuslkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbWorkStatuslkpFilters class.
		/// </summary>
		public BbWorkStatuslkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbWorkStatuslkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbWorkStatuslkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbWorkStatuslkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbWorkStatuslkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbWorkStatuslkpFilters
	
	#region BbWorkStatuslkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbWorkStatuslkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbWorkStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbWorkStatuslkpQuery : BbWorkStatuslkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbWorkStatuslkpQuery class.
		/// </summary>
		public BbWorkStatuslkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbWorkStatuslkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbWorkStatuslkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbWorkStatuslkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbWorkStatuslkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbWorkStatuslkpQuery
		
	#region BbPappPatientMedProblemFupFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientMedProblemFup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientMedProblemFupFilters : BbPappPatientMedProblemFupFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientMedProblemFupFilters class.
		/// </summary>
		public BbPappPatientMedProblemFupFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientMedProblemFupFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPappPatientMedProblemFupFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientMedProblemFupFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPappPatientMedProblemFupFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPappPatientMedProblemFupFilters
	
	#region BbPappPatientMedProblemFupQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbPappPatientMedProblemFupParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbPappPatientMedProblemFup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientMedProblemFupQuery : BbPappPatientMedProblemFupParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientMedProblemFupQuery class.
		/// </summary>
		public BbPappPatientMedProblemFupQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientMedProblemFupQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPappPatientMedProblemFupQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientMedProblemFupQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPappPatientMedProblemFupQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPappPatientMedProblemFupQuery
		
	#region BbAdditionalUserDetailFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbAdditionalUserDetail"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbAdditionalUserDetailFilters : BbAdditionalUserDetailFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbAdditionalUserDetailFilters class.
		/// </summary>
		public BbAdditionalUserDetailFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbAdditionalUserDetailFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbAdditionalUserDetailFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbAdditionalUserDetailFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbAdditionalUserDetailFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbAdditionalUserDetailFilters
	
	#region BbAdditionalUserDetailQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbAdditionalUserDetailParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbAdditionalUserDetail"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbAdditionalUserDetailQuery : BbAdditionalUserDetailParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbAdditionalUserDetailQuery class.
		/// </summary>
		public BbAdditionalUserDetailQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbAdditionalUserDetailQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbAdditionalUserDetailQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbAdditionalUserDetailQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbAdditionalUserDetailQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbAdditionalUserDetailQuery
		
	#region BbAnswerlkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbAnswerlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbAnswerlkpFilters : BbAnswerlkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbAnswerlkpFilters class.
		/// </summary>
		public BbAnswerlkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbAnswerlkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbAnswerlkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbAnswerlkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbAnswerlkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbAnswerlkpFilters
	
	#region BbAnswerlkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbAnswerlkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbAnswerlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbAnswerlkpQuery : BbAnswerlkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbAnswerlkpQuery class.
		/// </summary>
		public BbAnswerlkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbAnswerlkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbAnswerlkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbAnswerlkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbAnswerlkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbAnswerlkpQuery
		
	#region BbCommonFrequencylkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbCommonFrequencylkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCommonFrequencylkpFilters : BbCommonFrequencylkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCommonFrequencylkpFilters class.
		/// </summary>
		public BbCommonFrequencylkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbCommonFrequencylkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbCommonFrequencylkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbCommonFrequencylkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbCommonFrequencylkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbCommonFrequencylkpFilters
	
	#region BbCommonFrequencylkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbCommonFrequencylkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbCommonFrequencylkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCommonFrequencylkpQuery : BbCommonFrequencylkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCommonFrequencylkpQuery class.
		/// </summary>
		public BbCommonFrequencylkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbCommonFrequencylkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbCommonFrequencylkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbCommonFrequencylkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbCommonFrequencylkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbCommonFrequencylkpQuery
		
	#region BbConfigFactoryFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbConfigFactory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbConfigFactoryFilters : BbConfigFactoryFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbConfigFactoryFilters class.
		/// </summary>
		public BbConfigFactoryFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbConfigFactoryFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbConfigFactoryFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbConfigFactoryFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbConfigFactoryFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbConfigFactoryFilters
	
	#region BbConfigFactoryQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbConfigFactoryParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbConfigFactory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbConfigFactoryQuery : BbConfigFactoryParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbConfigFactoryQuery class.
		/// </summary>
		public BbConfigFactoryQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbConfigFactoryQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbConfigFactoryQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbConfigFactoryQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbConfigFactoryQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbConfigFactoryQuery
		
	#region BbEthnicitylkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbEthnicitylkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbEthnicitylkpFilters : BbEthnicitylkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbEthnicitylkpFilters class.
		/// </summary>
		public BbEthnicitylkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbEthnicitylkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbEthnicitylkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbEthnicitylkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbEthnicitylkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbEthnicitylkpFilters
	
	#region BbEthnicitylkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbEthnicitylkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbEthnicitylkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbEthnicitylkpQuery : BbEthnicitylkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbEthnicitylkpQuery class.
		/// </summary>
		public BbEthnicitylkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbEthnicitylkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbEthnicitylkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbEthnicitylkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbEthnicitylkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbEthnicitylkpQuery
		
	#region BbFileStorageFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbFileStorage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbFileStorageFilters : BbFileStorageFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbFileStorageFilters class.
		/// </summary>
		public BbFileStorageFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbFileStorageFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbFileStorageFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbFileStorageFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbFileStorageFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbFileStorageFilters
	
	#region BbFileStorageQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbFileStorageParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbFileStorage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbFileStorageQuery : BbFileStorageParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbFileStorageQuery class.
		/// </summary>
		public BbFileStorageQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbFileStorageQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbFileStorageQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbFileStorageQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbFileStorageQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbFileStorageQuery
		
	#region BbLesionlkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbLesionlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbLesionlkpFilters : BbLesionlkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbLesionlkpFilters class.
		/// </summary>
		public BbLesionlkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbLesionlkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbLesionlkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbLesionlkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbLesionlkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbLesionlkpFilters
	
	#region BbLesionlkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbLesionlkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbLesionlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbLesionlkpQuery : BbLesionlkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbLesionlkpQuery class.
		/// </summary>
		public BbLesionlkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbLesionlkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbLesionlkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbLesionlkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbLesionlkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbLesionlkpQuery
		
	#region BbPatientCohortTrackingClinicAttendanceLkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortTrackingClinicAttendanceLkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortTrackingClinicAttendanceLkpFilters : BbPatientCohortTrackingClinicAttendanceLkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingClinicAttendanceLkpFilters class.
		/// </summary>
		public BbPatientCohortTrackingClinicAttendanceLkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingClinicAttendanceLkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientCohortTrackingClinicAttendanceLkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingClinicAttendanceLkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientCohortTrackingClinicAttendanceLkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientCohortTrackingClinicAttendanceLkpFilters
	
	#region BbPatientCohortTrackingClinicAttendanceLkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbPatientCohortTrackingClinicAttendanceLkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortTrackingClinicAttendanceLkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortTrackingClinicAttendanceLkpQuery : BbPatientCohortTrackingClinicAttendanceLkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingClinicAttendanceLkpQuery class.
		/// </summary>
		public BbPatientCohortTrackingClinicAttendanceLkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingClinicAttendanceLkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientCohortTrackingClinicAttendanceLkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingClinicAttendanceLkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientCohortTrackingClinicAttendanceLkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientCohortTrackingClinicAttendanceLkpQuery
		
	#region BbLoginLogFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbLoginLog"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbLoginLogFilters : BbLoginLogFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbLoginLogFilters class.
		/// </summary>
		public BbLoginLogFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbLoginLogFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbLoginLogFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbLoginLogFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbLoginLogFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbLoginLogFilters
	
	#region BbLoginLogQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbLoginLogParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbLoginLog"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbLoginLogQuery : BbLoginLogParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbLoginLogQuery class.
		/// </summary>
		public BbLoginLogQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbLoginLogQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbLoginLogQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbLoginLogQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbLoginLogQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbLoginLogQuery
		
	#region BbMailingListSubscriptionsFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbMailingListSubscriptions"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbMailingListSubscriptionsFilters : BbMailingListSubscriptionsFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbMailingListSubscriptionsFilters class.
		/// </summary>
		public BbMailingListSubscriptionsFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbMailingListSubscriptionsFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbMailingListSubscriptionsFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbMailingListSubscriptionsFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbMailingListSubscriptionsFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbMailingListSubscriptionsFilters
	
	#region BbMailingListSubscriptionsQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbMailingListSubscriptionsParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbMailingListSubscriptions"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbMailingListSubscriptionsQuery : BbMailingListSubscriptionsParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbMailingListSubscriptionsQuery class.
		/// </summary>
		public BbMailingListSubscriptionsQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbMailingListSubscriptionsQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbMailingListSubscriptionsQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbMailingListSubscriptionsQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbMailingListSubscriptionsQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbMailingListSubscriptionsQuery
		
	#region BbNotificationTypelkpFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbNotificationTypelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbNotificationTypelkpFilters : BbNotificationTypelkpFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbNotificationTypelkpFilters class.
		/// </summary>
		public BbNotificationTypelkpFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbNotificationTypelkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbNotificationTypelkpFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbNotificationTypelkpFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbNotificationTypelkpFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbNotificationTypelkpFilters
	
	#region BbNotificationTypelkpQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbNotificationTypelkpParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbNotificationTypelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbNotificationTypelkpQuery : BbNotificationTypelkpParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbNotificationTypelkpQuery class.
		/// </summary>
		public BbNotificationTypelkpQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbNotificationTypelkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbNotificationTypelkpQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbNotificationTypelkpQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbNotificationTypelkpQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbNotificationTypelkpQuery
		
	#region BbNotificationFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbNotification"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbNotificationFilters : BbNotificationFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbNotificationFilters class.
		/// </summary>
		public BbNotificationFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbNotificationFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbNotificationFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbNotificationFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbNotificationFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbNotificationFilters
	
	#region BbNotificationQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbNotificationParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbNotification"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbNotificationQuery : BbNotificationParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbNotificationQuery class.
		/// </summary>
		public BbNotificationQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbNotificationQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbNotificationQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbNotificationQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbNotificationQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbNotificationQuery
		
	#region BbPappPatientCohortTrackingFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientCohortTracking"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientCohortTrackingFilters : BbPappPatientCohortTrackingFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientCohortTrackingFilters class.
		/// </summary>
		public BbPappPatientCohortTrackingFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientCohortTrackingFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPappPatientCohortTrackingFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientCohortTrackingFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPappPatientCohortTrackingFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPappPatientCohortTrackingFilters
	
	#region BbPappPatientCohortTrackingQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbPappPatientCohortTrackingParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbPappPatientCohortTracking"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientCohortTrackingQuery : BbPappPatientCohortTrackingParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientCohortTrackingQuery class.
		/// </summary>
		public BbPappPatientCohortTrackingQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientCohortTrackingQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPappPatientCohortTrackingQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientCohortTrackingQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPappPatientCohortTrackingQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPappPatientCohortTrackingQuery
		
	#region BbPappPatientDlqiFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientDlqi"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientDlqiFilters : BbPappPatientDlqiFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientDlqiFilters class.
		/// </summary>
		public BbPappPatientDlqiFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientDlqiFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPappPatientDlqiFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientDlqiFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPappPatientDlqiFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPappPatientDlqiFilters
	
	#region BbPappPatientDlqiQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbPappPatientDlqiParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbPappPatientDlqi"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientDlqiQuery : BbPappPatientDlqiParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientDlqiQuery class.
		/// </summary>
		public BbPappPatientDlqiQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientDlqiQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPappPatientDlqiQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientDlqiQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPappPatientDlqiQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPappPatientDlqiQuery
		
	#region BbPappPatientLifestyleFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientLifestyle"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientLifestyleFilters : BbPappPatientLifestyleFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientLifestyleFilters class.
		/// </summary>
		public BbPappPatientLifestyleFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientLifestyleFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPappPatientLifestyleFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientLifestyleFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPappPatientLifestyleFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPappPatientLifestyleFilters
	
	#region BbPappPatientLifestyleQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbPappPatientLifestyleParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbPappPatientLifestyle"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientLifestyleQuery : BbPappPatientLifestyleParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientLifestyleQuery class.
		/// </summary>
		public BbPappPatientLifestyleQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientLifestyleQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPappPatientLifestyleQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPappPatientLifestyleQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPappPatientLifestyleQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPappPatientLifestyleQuery
		
	#region BbMailingListsFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbMailingLists"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbMailingListsFilters : BbMailingListsFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbMailingListsFilters class.
		/// </summary>
		public BbMailingListsFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbMailingListsFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbMailingListsFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbMailingListsFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbMailingListsFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbMailingListsFilters
	
	#region BbMailingListsQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbMailingListsParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbMailingLists"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbMailingListsQuery : BbMailingListsParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbMailingListsQuery class.
		/// </summary>
		public BbMailingListsQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbMailingListsQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbMailingListsQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbMailingListsQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbMailingListsQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbMailingListsQuery
		
	#region BbPatientCohortTrackingFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortTracking"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortTrackingFilters : BbPatientCohortTrackingFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingFilters class.
		/// </summary>
		public BbPatientCohortTrackingFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientCohortTrackingFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientCohortTrackingFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientCohortTrackingFilters
	
	#region BbPatientCohortTrackingQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BbPatientCohortTrackingParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortTracking"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortTrackingQuery : BbPatientCohortTrackingParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingQuery class.
		/// </summary>
		public BbPatientCohortTrackingQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientCohortTrackingQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientCohortTrackingQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientCohortTrackingQuery
	#endregion

	
}
