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
	/// Represents the DataRepository.BbNotificationProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbNotificationDataSourceDesigner))]
	public class BbNotificationDataSource : ProviderDataSource<BbNotification, BbNotificationKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbNotificationDataSource class.
		/// </summary>
		public BbNotificationDataSource() : base(new BbNotificationService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbNotificationDataSourceView used by the BbNotificationDataSource.
		/// </summary>
		protected BbNotificationDataSourceView BbNotificationView
		{
			get { return ( View as BbNotificationDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbNotificationDataSource control invokes to retrieve data.
		/// </summary>
		public BbNotificationSelectMethod SelectMethod
		{
			get
			{
				BbNotificationSelectMethod selectMethod = BbNotificationSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbNotificationSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbNotificationDataSourceView class that is to be
		/// used by the BbNotificationDataSource.
		/// </summary>
		/// <returns>An instance of the BbNotificationDataSourceView class.</returns>
		protected override BaseDataSourceView<BbNotification, BbNotificationKey> GetNewDataSourceView()
		{
			return new BbNotificationDataSourceView(this, DefaultViewName);
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
	/// Supports the BbNotificationDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbNotificationDataSourceView : ProviderDataSourceView<BbNotification, BbNotificationKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbNotificationDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbNotificationDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbNotificationDataSourceView(BbNotificationDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbNotificationDataSource BbNotificationOwner
		{
			get { return Owner as BbNotificationDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbNotificationSelectMethod SelectMethod
		{
			get { return BbNotificationOwner.SelectMethod; }
			set { BbNotificationOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbNotificationService BbNotificationProvider
		{
			get { return Provider as BbNotificationService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbNotification> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbNotification> results = null;
			BbNotification item;
			count = 0;
			
			int _nid;
			int _typeId;
			System.Int32? sp157_ShowAdminInbox;
			System.Int32? sp157_ShowPvInbox;
			System.Int32? sp157_ShowSuperInbox;
			System.String sp157_TypeList;
			System.Int32? sp157_StudyId;
			System.DateTime? sp157_DateFrom;
			System.DateTime? sp157_DateTo;
			System.Int32? sp156_InboxId;

			switch ( SelectMethod )
			{
				case BbNotificationSelectMethod.Get:
					BbNotificationKey entityKey  = new BbNotificationKey();
					entityKey.Load(values);
					item = BbNotificationProvider.Get(entityKey);
					results = new TList<BbNotification>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbNotificationSelectMethod.GetAll:
                    results = BbNotificationProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbNotificationSelectMethod.GetPaged:
					results = BbNotificationProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbNotificationSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbNotificationProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbNotificationProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbNotificationSelectMethod.GetByNid:
					_nid = ( values["Nid"] != null ) ? (int) EntityUtil.ChangeType(values["Nid"], typeof(int)) : (int)0;
					item = BbNotificationProvider.GetByNid(_nid);
					results = new TList<BbNotification>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case BbNotificationSelectMethod.GetByTypeId:
					_typeId = ( values["TypeId"] != null ) ? (int) EntityUtil.ChangeType(values["TypeId"], typeof(int)) : (int)0;
					results = BbNotificationProvider.GetByTypeId(_typeId, this.StartIndex, this.PageSize, out count);
					break;
				// M:M
				// Custom
				case BbNotificationSelectMethod.Notification_GetSolved_Custom:
					sp157_ShowAdminInbox = (System.Int32?) EntityUtil.ChangeType(values["ShowAdminInbox"], typeof(System.Int32?));
					sp157_ShowPvInbox = (System.Int32?) EntityUtil.ChangeType(values["ShowPvInbox"], typeof(System.Int32?));
					sp157_ShowSuperInbox = (System.Int32?) EntityUtil.ChangeType(values["ShowSuperInbox"], typeof(System.Int32?));
					sp157_TypeList = (System.String) EntityUtil.ChangeType(values["TypeList"], typeof(System.String));
					sp157_StudyId = (System.Int32?) EntityUtil.ChangeType(values["StudyId"], typeof(System.Int32?));
					sp157_DateFrom = (System.DateTime?) EntityUtil.ChangeType(values["DateFrom"], typeof(System.DateTime?));
					sp157_DateTo = (System.DateTime?) EntityUtil.ChangeType(values["DateTo"], typeof(System.DateTime?));
					results = BbNotificationProvider.Notification_GetSolved_Custom(sp157_ShowAdminInbox, sp157_ShowPvInbox, sp157_ShowSuperInbox, sp157_TypeList, sp157_StudyId, sp157_DateFrom, sp157_DateTo, StartIndex, PageSize);
					break;
				case BbNotificationSelectMethod.Notification_GetSolved_ByInboxID:
					sp156_InboxId = (System.Int32?) EntityUtil.ChangeType(values["InboxId"], typeof(System.Int32?));
					results = BbNotificationProvider.Notification_GetSolved_ByInboxID(sp156_InboxId, StartIndex, PageSize);
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
			if ( SelectMethod == BbNotificationSelectMethod.Get || SelectMethod == BbNotificationSelectMethod.GetByNid )
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
				BbNotification entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbNotificationProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbNotification> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbNotificationProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbNotificationDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbNotificationDataSource class.
	/// </summary>
	public class BbNotificationDataSourceDesigner : ProviderDataSourceDesigner<BbNotification, BbNotificationKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbNotificationDataSourceDesigner class.
		/// </summary>
		public BbNotificationDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbNotificationSelectMethod SelectMethod
		{
			get { return ((BbNotificationDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbNotificationDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbNotificationDataSourceActionList

	/// <summary>
	/// Supports the BbNotificationDataSourceDesigner class.
	/// </summary>
	internal class BbNotificationDataSourceActionList : DesignerActionList
	{
		private BbNotificationDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbNotificationDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbNotificationDataSourceActionList(BbNotificationDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbNotificationSelectMethod SelectMethod
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

	#endregion BbNotificationDataSourceActionList
	
	#endregion BbNotificationDataSourceDesigner
	
	#region BbNotificationSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbNotificationDataSource.SelectMethod property.
	/// </summary>
	public enum BbNotificationSelectMethod
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
		/// Represents the GetByNid method.
		/// </summary>
		GetByNid,
		/// <summary>
		/// Represents the GetByTypeId method.
		/// </summary>
		GetByTypeId,
		/// <summary>
		/// Represents the Notification_GetSolved_Custom method.
		/// </summary>
		Notification_GetSolved_Custom,
		/// <summary>
		/// Represents the Notification_GetSolved_ByInboxID method.
		/// </summary>
		Notification_GetSolved_ByInboxID
	}
	
	#endregion BbNotificationSelectMethod

	#region BbNotificationFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbNotification"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbNotificationFilter : SqlFilter<BbNotificationColumn>
	{
	}
	
	#endregion BbNotificationFilter

	#region BbNotificationExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbNotification"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbNotificationExpressionBuilder : SqlExpressionBuilder<BbNotificationColumn>
	{
	}
	
	#endregion BbNotificationExpressionBuilder	

	#region BbNotificationProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbNotificationChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbNotification"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbNotificationProperty : ChildEntityProperty<BbNotificationChildEntityTypes>
	{
	}
	
	#endregion BbNotificationProperty
}

