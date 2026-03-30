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
	/// Represents the DataRepository.BbMailingListsProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbMailingListsDataSourceDesigner))]
	public class BbMailingListsDataSource : ProviderDataSource<BbMailingLists, BbMailingListsKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbMailingListsDataSource class.
		/// </summary>
		public BbMailingListsDataSource() : base(new BbMailingListsService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbMailingListsDataSourceView used by the BbMailingListsDataSource.
		/// </summary>
		protected BbMailingListsDataSourceView BbMailingListsView
		{
			get { return ( View as BbMailingListsDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbMailingListsDataSource control invokes to retrieve data.
		/// </summary>
		public BbMailingListsSelectMethod SelectMethod
		{
			get
			{
				BbMailingListsSelectMethod selectMethod = BbMailingListsSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbMailingListsSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbMailingListsDataSourceView class that is to be
		/// used by the BbMailingListsDataSource.
		/// </summary>
		/// <returns>An instance of the BbMailingListsDataSourceView class.</returns>
		protected override BaseDataSourceView<BbMailingLists, BbMailingListsKey> GetNewDataSourceView()
		{
			return new BbMailingListsDataSourceView(this, DefaultViewName);
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
	/// Supports the BbMailingListsDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbMailingListsDataSourceView : ProviderDataSourceView<BbMailingLists, BbMailingListsKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbMailingListsDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbMailingListsDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbMailingListsDataSourceView(BbMailingListsDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbMailingListsDataSource BbMailingListsOwner
		{
			get { return Owner as BbMailingListsDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbMailingListsSelectMethod SelectMethod
		{
			get { return BbMailingListsOwner.SelectMethod; }
			set { BbMailingListsOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbMailingListsService BbMailingListsProvider
		{
			get { return Provider as BbMailingListsService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbMailingLists> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbMailingLists> results = null;
			BbMailingLists item;
			count = 0;
			
			System.String _roleName_nullable;
			System.Int32 _bbMlid;

			switch ( SelectMethod )
			{
				case BbMailingListsSelectMethod.Get:
					BbMailingListsKey entityKey  = new BbMailingListsKey();
					entityKey.Load(values);
					item = BbMailingListsProvider.Get(entityKey);
					results = new TList<BbMailingLists>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbMailingListsSelectMethod.GetAll:
                    results = BbMailingListsProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbMailingListsSelectMethod.GetPaged:
					results = BbMailingListsProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbMailingListsSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbMailingListsProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbMailingListsProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbMailingListsSelectMethod.GetByBbMlid:
					_bbMlid = ( values["BbMlid"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["BbMlid"], typeof(System.Int32)) : (int)0;
					item = BbMailingListsProvider.GetByBbMlid(_bbMlid);
					results = new TList<BbMailingLists>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case BbMailingListsSelectMethod.GetByRoleName:
					_roleName_nullable = (System.String) EntityUtil.ChangeType(values["RoleName"], typeof(System.String));
					results = BbMailingListsProvider.GetByRoleName(_roleName_nullable, this.StartIndex, this.PageSize, out count);
					break;
				// FK
				// M:M
				// Custom
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
			if ( SelectMethod == BbMailingListsSelectMethod.Get || SelectMethod == BbMailingListsSelectMethod.GetByBbMlid )
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
				BbMailingLists entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbMailingListsProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbMailingLists> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbMailingListsProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbMailingListsDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbMailingListsDataSource class.
	/// </summary>
	public class BbMailingListsDataSourceDesigner : ProviderDataSourceDesigner<BbMailingLists, BbMailingListsKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbMailingListsDataSourceDesigner class.
		/// </summary>
		public BbMailingListsDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbMailingListsSelectMethod SelectMethod
		{
			get { return ((BbMailingListsDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbMailingListsDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbMailingListsDataSourceActionList

	/// <summary>
	/// Supports the BbMailingListsDataSourceDesigner class.
	/// </summary>
	internal class BbMailingListsDataSourceActionList : DesignerActionList
	{
		private BbMailingListsDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbMailingListsDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbMailingListsDataSourceActionList(BbMailingListsDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbMailingListsSelectMethod SelectMethod
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

	#endregion BbMailingListsDataSourceActionList
	
	#endregion BbMailingListsDataSourceDesigner
	
	#region BbMailingListsSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbMailingListsDataSource.SelectMethod property.
	/// </summary>
	public enum BbMailingListsSelectMethod
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
		/// Represents the GetByRoleName method.
		/// </summary>
		GetByRoleName,
		/// <summary>
		/// Represents the GetByBbMlid method.
		/// </summary>
		GetByBbMlid
	}
	
	#endregion BbMailingListsSelectMethod

	#region BbMailingListsFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbMailingLists"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbMailingListsFilter : SqlFilter<BbMailingListsColumn>
	{
	}
	
	#endregion BbMailingListsFilter

	#region BbMailingListsExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbMailingLists"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbMailingListsExpressionBuilder : SqlExpressionBuilder<BbMailingListsColumn>
	{
	}
	
	#endregion BbMailingListsExpressionBuilder	

	#region BbMailingListsProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbMailingListsChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbMailingLists"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbMailingListsProperty : ChildEntityProperty<BbMailingListsChildEntityTypes>
	{
	}
	
	#endregion BbMailingListsProperty
}

