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
	/// Represents the DataRepository.BbAdditionalUserDetailProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbAdditionalUserDetailDataSourceDesigner))]
	public class BbAdditionalUserDetailDataSource : ProviderDataSource<BbAdditionalUserDetail, BbAdditionalUserDetailKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbAdditionalUserDetailDataSource class.
		/// </summary>
		public BbAdditionalUserDetailDataSource() : base(new BbAdditionalUserDetailService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbAdditionalUserDetailDataSourceView used by the BbAdditionalUserDetailDataSource.
		/// </summary>
		protected BbAdditionalUserDetailDataSourceView BbAdditionalUserDetailView
		{
			get { return ( View as BbAdditionalUserDetailDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbAdditionalUserDetailDataSource control invokes to retrieve data.
		/// </summary>
		public BbAdditionalUserDetailSelectMethod SelectMethod
		{
			get
			{
				BbAdditionalUserDetailSelectMethod selectMethod = BbAdditionalUserDetailSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbAdditionalUserDetailSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbAdditionalUserDetailDataSourceView class that is to be
		/// used by the BbAdditionalUserDetailDataSource.
		/// </summary>
		/// <returns>An instance of the BbAdditionalUserDetailDataSourceView class.</returns>
		protected override BaseDataSourceView<BbAdditionalUserDetail, BbAdditionalUserDetailKey> GetNewDataSourceView()
		{
			return new BbAdditionalUserDetailDataSourceView(this, DefaultViewName);
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
	/// Supports the BbAdditionalUserDetailDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbAdditionalUserDetailDataSourceView : ProviderDataSourceView<BbAdditionalUserDetail, BbAdditionalUserDetailKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbAdditionalUserDetailDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbAdditionalUserDetailDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbAdditionalUserDetailDataSourceView(BbAdditionalUserDetailDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbAdditionalUserDetailDataSource BbAdditionalUserDetailOwner
		{
			get { return Owner as BbAdditionalUserDetailDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbAdditionalUserDetailSelectMethod SelectMethod
		{
			get { return BbAdditionalUserDetailOwner.SelectMethod; }
			set { BbAdditionalUserDetailOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbAdditionalUserDetailService BbAdditionalUserDetailProvider
		{
			get { return Provider as BbAdditionalUserDetailService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbAdditionalUserDetail> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbAdditionalUserDetail> results = null;
			BbAdditionalUserDetail item;
			count = 0;
			
			int _badbiRuserid;
			System.Guid _userid;
			int _centreid;
			System.Int32? sp60_Centreid;

			switch ( SelectMethod )
			{
				case BbAdditionalUserDetailSelectMethod.Get:
					BbAdditionalUserDetailKey entityKey  = new BbAdditionalUserDetailKey();
					entityKey.Load(values);
					item = BbAdditionalUserDetailProvider.Get(entityKey);
					results = new TList<BbAdditionalUserDetail>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbAdditionalUserDetailSelectMethod.GetAll:
                    results = BbAdditionalUserDetailProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbAdditionalUserDetailSelectMethod.GetPaged:
					results = BbAdditionalUserDetailProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbAdditionalUserDetailSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbAdditionalUserDetailProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbAdditionalUserDetailProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbAdditionalUserDetailSelectMethod.GetByUserid:
					_userid = ( values["Userid"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["Userid"], typeof(System.Guid)) : Guid.Empty;
					item = BbAdditionalUserDetailProvider.GetByUserid(_userid);
					results = new TList<BbAdditionalUserDetail>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case BbAdditionalUserDetailSelectMethod.GetByBadbiRuserid:
					_badbiRuserid = ( values["BadbiRuserid"] != null ) ? (int) EntityUtil.ChangeType(values["BadbiRuserid"], typeof(int)) : (int)0;
					results = BbAdditionalUserDetailProvider.GetByBadbiRuserid(_badbiRuserid, this.StartIndex, this.PageSize, out count);
					break;
				// FK
				// M:M
				case BbAdditionalUserDetailSelectMethod.GetByCentreidFromBbAdditionalUserAndCentre:
					_centreid = ( values["Centreid"] != null ) ? (int) EntityUtil.ChangeType(values["Centreid"], typeof(int)) : (int)0;
					results = BbAdditionalUserDetailProvider.GetByCentreidFromBbAdditionalUserAndCentre(_centreid, this.StartIndex, this.PageSize, out count);
					break;
				// Custom
				case BbAdditionalUserDetailSelectMethod.AdditionalUserDetail_getNonAdminUsersByCentreID:
					sp60_Centreid = (System.Int32?) EntityUtil.ChangeType(values["Centreid"], typeof(System.Int32?));
					results = BbAdditionalUserDetailProvider.AdditionalUserDetail_getNonAdminUsersByCentreID(sp60_Centreid, StartIndex, PageSize);
					break;
				case BbAdditionalUserDetailSelectMethod.AdditionalUserDetail_getValidUsers:
					results = BbAdditionalUserDetailProvider.AdditionalUserDetail_getValidUsers(StartIndex, PageSize);
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
			if ( SelectMethod == BbAdditionalUserDetailSelectMethod.Get || SelectMethod == BbAdditionalUserDetailSelectMethod.GetByUserid )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(BbAdditionalUserDetail entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.Userid == Guid.Empty )
				entity.Userid = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				BbAdditionalUserDetail entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbAdditionalUserDetailProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbAdditionalUserDetail> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbAdditionalUserDetailProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbAdditionalUserDetailDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbAdditionalUserDetailDataSource class.
	/// </summary>
	public class BbAdditionalUserDetailDataSourceDesigner : ProviderDataSourceDesigner<BbAdditionalUserDetail, BbAdditionalUserDetailKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbAdditionalUserDetailDataSourceDesigner class.
		/// </summary>
		public BbAdditionalUserDetailDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbAdditionalUserDetailSelectMethod SelectMethod
		{
			get { return ((BbAdditionalUserDetailDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbAdditionalUserDetailDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbAdditionalUserDetailDataSourceActionList

	/// <summary>
	/// Supports the BbAdditionalUserDetailDataSourceDesigner class.
	/// </summary>
	internal class BbAdditionalUserDetailDataSourceActionList : DesignerActionList
	{
		private BbAdditionalUserDetailDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbAdditionalUserDetailDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbAdditionalUserDetailDataSourceActionList(BbAdditionalUserDetailDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbAdditionalUserDetailSelectMethod SelectMethod
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

	#endregion BbAdditionalUserDetailDataSourceActionList
	
	#endregion BbAdditionalUserDetailDataSourceDesigner
	
	#region BbAdditionalUserDetailSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbAdditionalUserDetailDataSource.SelectMethod property.
	/// </summary>
	public enum BbAdditionalUserDetailSelectMethod
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
		/// Represents the GetByBadbiRuserid method.
		/// </summary>
		GetByBadbiRuserid,
		/// <summary>
		/// Represents the GetByUserid method.
		/// </summary>
		GetByUserid,
		/// <summary>
		/// Represents the GetByCentreidFromBbAdditionalUserAndCentre method.
		/// </summary>
		GetByCentreidFromBbAdditionalUserAndCentre,
		/// <summary>
		/// Represents the AdditionalUserDetail_getNonAdminUsersByCentreID method.
		/// </summary>
		AdditionalUserDetail_getNonAdminUsersByCentreID,
		/// <summary>
		/// Represents the AdditionalUserDetail_getValidUsers method.
		/// </summary>
		AdditionalUserDetail_getValidUsers
	}
	
	#endregion BbAdditionalUserDetailSelectMethod

	#region BbAdditionalUserDetailFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbAdditionalUserDetail"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbAdditionalUserDetailFilter : SqlFilter<BbAdditionalUserDetailColumn>
	{
	}
	
	#endregion BbAdditionalUserDetailFilter

	#region BbAdditionalUserDetailExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbAdditionalUserDetail"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbAdditionalUserDetailExpressionBuilder : SqlExpressionBuilder<BbAdditionalUserDetailColumn>
	{
	}
	
	#endregion BbAdditionalUserDetailExpressionBuilder	

	#region BbAdditionalUserDetailProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbAdditionalUserDetailChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbAdditionalUserDetail"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbAdditionalUserDetailProperty : ChildEntityProperty<BbAdditionalUserDetailChildEntityTypes>
	{
	}
	
	#endregion BbAdditionalUserDetailProperty
}

