#region Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI;
using System.Web.UI.Design;

using MHS.Badbir.NetTiers.Entities;
using MHS.Badbir.NetTiers.Data;
using MHS.Badbir.NetTiers.Data.Bases;
using MHS.Badbir.NetTiers.Component;
#endregion

namespace MHS.Badbir.NetTiers.Web.Data
{
	/// <summary>
	/// Represents the DataRepository.BbLoginLogProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbLoginLogDataSourceDesigner))]
	public class BbLoginLogDataSource : ProviderDataSource<BbLoginLog, BbLoginLogKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbLoginLogDataSource class.
		/// </summary>
		public BbLoginLogDataSource() : base(new BbLoginLogService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbLoginLogDataSourceView used by the BbLoginLogDataSource.
		/// </summary>
		protected BbLoginLogDataSourceView BbLoginLogView
		{
			get { return ( View as BbLoginLogDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbLoginLogDataSource control invokes to retrieve data.
		/// </summary>
		public BbLoginLogSelectMethod SelectMethod
		{
			get
			{
				BbLoginLogSelectMethod selectMethod = BbLoginLogSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbLoginLogSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbLoginLogDataSourceView class that is to be
		/// used by the BbLoginLogDataSource.
		/// </summary>
		/// <returns>An instance of the BbLoginLogDataSourceView class.</returns>
		protected override BaseDataSourceView<BbLoginLog, BbLoginLogKey> GetNewDataSourceView()
		{
			return new BbLoginLogDataSourceView(this, DefaultViewName);
		}
		
		/// <summary>
        /// Creates a cache hashing key based on the startIndex, pageSize and the SelectMethod being used.
        /// </summary>
        /// <param name="startIndex">The current start row index.</param>
        /// <param name="pageSize">The current page size.</param>
        /// <returns>A string that can be used as a key for caching purposes.</returns>
		protected override string CacheHashKey(int startIndex, int pageSize)
        {
			return String.Format("{0}:{1}:{2}", SelectMethod, startIndex, pageSize);
        }
		
		#endregion Methods
	}
	
	/// <summary>
	/// Supports the BbLoginLogDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbLoginLogDataSourceView : ProviderDataSourceView<BbLoginLog, BbLoginLogKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbLoginLogDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbLoginLogDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbLoginLogDataSourceView(BbLoginLogDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbLoginLogDataSource BbLoginLogOwner
		{
			get { return Owner as BbLoginLogDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbLoginLogSelectMethod SelectMethod
		{
			get { return BbLoginLogOwner.SelectMethod; }
			set { BbLoginLogOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbLoginLogService BbLoginLogProvider
		{
			get { return Provider as BbLoginLogService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbLoginLog> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbLoginLog> results = null;
			BbLoginLog item;
			count = 0;
			
			bool _isOnline;
			string _sessionId;
			System.DateTime? _logoutTime_nullable;
			System.Int32? _badbirUserId_nullable;
			int _rowId;
			System.Int32? sp152_Badbiruserid;

			switch ( SelectMethod )
			{
				case BbLoginLogSelectMethod.Get:
					BbLoginLogKey entityKey  = new BbLoginLogKey();
					entityKey.Load(values);
					item = BbLoginLogProvider.Get(entityKey);
					results = new TList<BbLoginLog>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbLoginLogSelectMethod.GetAll:
                    results = BbLoginLogProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbLoginLogSelectMethod.GetPaged:
					results = BbLoginLogProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbLoginLogSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbLoginLogProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbLoginLogProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbLoginLogSelectMethod.GetByRowId:
					_rowId = ( values["RowId"] != null ) ? (int) EntityUtil.ChangeType(values["RowId"], typeof(int)) : (int)0;
					item = BbLoginLogProvider.GetByRowId(_rowId);
					results = new TList<BbLoginLog>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case BbLoginLogSelectMethod.GetByIsOnline:
					_isOnline = ( values["IsOnline"] != null ) ? (bool) EntityUtil.ChangeType(values["IsOnline"], typeof(bool)) : false;
					results = BbLoginLogProvider.GetByIsOnline(_isOnline, this.StartIndex, this.PageSize, out count);
					break;
				case BbLoginLogSelectMethod.GetBySessionIdLogoutTime:
					_sessionId = ( values["SessionId"] != null ) ? (string) EntityUtil.ChangeType(values["SessionId"], typeof(string)) : string.Empty;
					_logoutTime_nullable = (System.DateTime?) EntityUtil.ChangeType(values["LogoutTime"], typeof(System.DateTime?));
					results = BbLoginLogProvider.GetBySessionIdLogoutTime(_sessionId, _logoutTime_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbLoginLogSelectMethod.GetBySessionIdBadbirUserIdIsOnline:
					_sessionId = ( values["SessionId"] != null ) ? (string) EntityUtil.ChangeType(values["SessionId"], typeof(string)) : string.Empty;
					_badbirUserId_nullable = (System.Int32?) EntityUtil.ChangeType(values["BadbirUserId"], typeof(System.Int32?));
					_isOnline = ( values["IsOnline"] != null ) ? (bool) EntityUtil.ChangeType(values["IsOnline"], typeof(bool)) : false;
					results = BbLoginLogProvider.GetBySessionIdBadbirUserIdIsOnline(_sessionId, _badbirUserId_nullable, _isOnline, this.StartIndex, this.PageSize, out count);
					break;
				// FK
				// M:M
				// Custom
				case BbLoginLogSelectMethod.LoginLog_getLastTenLoginsByUser:
					sp152_Badbiruserid = (System.Int32?) EntityUtil.ChangeType(values["Badbiruserid"], typeof(System.Int32?));
					results = BbLoginLogProvider.LoginLog_getLastTenLoginsByUser(sp152_Badbiruserid, StartIndex, PageSize);
					break;
				default:
					break;
			}

			if ( results != null && count < 1 )
			{
				count = results.Count;

				if ( !String.IsNullOrEmpty(CustomMethodRecordCountParamName) )
				{
					object objCustomCount = EntityUtil.ChangeType(customOutput[CustomMethodRecordCountParamName], typeof(Int32));
					
					if ( objCustomCount != null )
					{
						count = (int) objCustomCount;
					}
				}
			}
			
			return results;
		}
		
		/// <summary>
		/// Gets the values of any supplied parameters for internal caching.
		/// </summary>
		/// <param name="values">An IDictionary object of name/value pairs.</param>
		protected override void GetSelectParameters(IDictionary values)
		{
			if ( SelectMethod == BbLoginLogSelectMethod.Get || SelectMethod == BbLoginLogSelectMethod.GetByRowId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				BbLoginLog entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbLoginLogProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
					// set loaded flag
					IsDeepLoaded = true;
				}
			}
		}

		/// <summary>
		/// Performs a DeepLoad operation on the specified entity collection.
		/// </summary>
		/// <param name="entityList"></param>
		/// <param name="properties"></param>
		internal override void DeepLoad(TList<BbLoginLog> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbLoginLogProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbLoginLogDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbLoginLogDataSource class.
	/// </summary>
	public class BbLoginLogDataSourceDesigner : ProviderDataSourceDesigner<BbLoginLog, BbLoginLogKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbLoginLogDataSourceDesigner class.
		/// </summary>
		public BbLoginLogDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbLoginLogSelectMethod SelectMethod
		{
			get { return ((BbLoginLogDataSource) DataSource).SelectMethod; }
			set { SetPropertyValue("SelectMethod", value); }
		}

		/// <summary>Gets the designer action list collection for this designer.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection"/>
		/// associated with this designer.</returns>
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection actions = new DesignerActionListCollection();
				actions.Add(new BbLoginLogDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbLoginLogDataSourceActionList

	/// <summary>
	/// Supports the BbLoginLogDataSourceDesigner class.
	/// </summary>
	internal class BbLoginLogDataSourceActionList : DesignerActionList
	{
		private BbLoginLogDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbLoginLogDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbLoginLogDataSourceActionList(BbLoginLogDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbLoginLogSelectMethod SelectMethod
		{
			get { return _designer.SelectMethod; }
			set { _designer.SelectMethod = value; }
		}

		/// <summary>
		/// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
		/// objects contained in the list.
		/// </summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
		/// array that contains the items in this list.</returns>
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection items = new DesignerActionItemCollection();
			items.Add(new DesignerActionPropertyItem("SelectMethod", "Select Method", "Methods"));
			return items;
		}
	}

	#endregion BbLoginLogDataSourceActionList
	
	#endregion BbLoginLogDataSourceDesigner
	
	#region BbLoginLogSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbLoginLogDataSource.SelectMethod property.
	/// </summary>
	public enum BbLoginLogSelectMethod
	{
		/// <summary>
		/// Represents the Get method.
		/// </summary>
		Get,
		/// <summary>
		/// Represents the GetAll method.
		/// </summary>
		GetAll,
		/// <summary>
		/// Represents the GetPaged method.
		/// </summary>
		GetPaged,
		/// <summary>
		/// Represents the Find method.
		/// </summary>
		Find,
		/// <summary>
		/// Represents the GetByIsOnline method.
		/// </summary>
		GetByIsOnline,
		/// <summary>
		/// Represents the GetBySessionIdLogoutTime method.
		/// </summary>
		GetBySessionIdLogoutTime,
		/// <summary>
		/// Represents the GetBySessionIdBadbirUserIdIsOnline method.
		/// </summary>
		GetBySessionIdBadbirUserIdIsOnline,
		/// <summary>
		/// Represents the GetByRowId method.
		/// </summary>
		GetByRowId,
		/// <summary>
		/// Represents the LoginLog_getLastTenLoginsByUser method.
		/// </summary>
		LoginLog_getLastTenLoginsByUser
	}
	
	#endregion BbLoginLogSelectMethod

	#region BbLoginLogFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbLoginLog"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbLoginLogFilter : SqlFilter<BbLoginLogColumn>
	{
	}
	
	#endregion BbLoginLogFilter

	#region BbLoginLogExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbLoginLog"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbLoginLogExpressionBuilder : SqlExpressionBuilder<BbLoginLogColumn>
	{
	}
	
	#endregion BbLoginLogExpressionBuilder	

	#region BbLoginLogProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbLoginLogChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbLoginLog"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbLoginLogProperty : ChildEntityProperty<BbLoginLogChildEntityTypes>
	{
	}
	
	#endregion BbLoginLogProperty
}

