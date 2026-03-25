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
	/// Represents the DataRepository.BbMailingListSubscriptionsProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbMailingListSubscriptionsDataSourceDesigner))]
	public class BbMailingListSubscriptionsDataSource : ProviderDataSource<BbMailingListSubscriptions, BbMailingListSubscriptionsKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbMailingListSubscriptionsDataSource class.
		/// </summary>
		public BbMailingListSubscriptionsDataSource() : base(new BbMailingListSubscriptionsService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbMailingListSubscriptionsDataSourceView used by the BbMailingListSubscriptionsDataSource.
		/// </summary>
		protected BbMailingListSubscriptionsDataSourceView BbMailingListSubscriptionsView
		{
			get { return ( View as BbMailingListSubscriptionsDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbMailingListSubscriptionsDataSource control invokes to retrieve data.
		/// </summary>
		public BbMailingListSubscriptionsSelectMethod SelectMethod
		{
			get
			{
				BbMailingListSubscriptionsSelectMethod selectMethod = BbMailingListSubscriptionsSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbMailingListSubscriptionsSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbMailingListSubscriptionsDataSourceView class that is to be
		/// used by the BbMailingListSubscriptionsDataSource.
		/// </summary>
		/// <returns>An instance of the BbMailingListSubscriptionsDataSourceView class.</returns>
		protected override BaseDataSourceView<BbMailingListSubscriptions, BbMailingListSubscriptionsKey> GetNewDataSourceView()
		{
			return new BbMailingListSubscriptionsDataSourceView(this, DefaultViewName);
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
	/// Supports the BbMailingListSubscriptionsDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbMailingListSubscriptionsDataSourceView : ProviderDataSourceView<BbMailingListSubscriptions, BbMailingListSubscriptionsKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbMailingListSubscriptionsDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbMailingListSubscriptionsDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbMailingListSubscriptionsDataSourceView(BbMailingListSubscriptionsDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbMailingListSubscriptionsDataSource BbMailingListSubscriptionsOwner
		{
			get { return Owner as BbMailingListSubscriptionsDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbMailingListSubscriptionsSelectMethod SelectMethod
		{
			get { return BbMailingListSubscriptionsOwner.SelectMethod; }
			set { BbMailingListSubscriptionsOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbMailingListSubscriptionsService BbMailingListSubscriptionsProvider
		{
			get { return Provider as BbMailingListSubscriptionsService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbMailingListSubscriptions> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbMailingListSubscriptions> results = null;
			BbMailingListSubscriptions item;
			count = 0;
			
			System.Int32? _subsCentreid_nullable;
			System.String _userName_nullable;
			System.Int32 _bbMlSid;
			System.Int32 _bbMlid;

			switch ( SelectMethod )
			{
				case BbMailingListSubscriptionsSelectMethod.Get:
					BbMailingListSubscriptionsKey entityKey  = new BbMailingListSubscriptionsKey();
					entityKey.Load(values);
					item = BbMailingListSubscriptionsProvider.Get(entityKey);
					results = new TList<BbMailingListSubscriptions>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbMailingListSubscriptionsSelectMethod.GetAll:
                    results = BbMailingListSubscriptionsProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbMailingListSubscriptionsSelectMethod.GetPaged:
					results = BbMailingListSubscriptionsProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbMailingListSubscriptionsSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbMailingListSubscriptionsProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbMailingListSubscriptionsProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbMailingListSubscriptionsSelectMethod.GetByBbMlSid:
					_bbMlSid = ( values["BbMlSid"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["BbMlSid"], typeof(System.Int32)) : (int)0;
					item = BbMailingListSubscriptionsProvider.GetByBbMlSid(_bbMlSid);
					results = new TList<BbMailingListSubscriptions>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case BbMailingListSubscriptionsSelectMethod.GetBySubsCentreid:
					_subsCentreid_nullable = (System.Int32?) EntityUtil.ChangeType(values["SubsCentreid"], typeof(System.Int32?));
					results = BbMailingListSubscriptionsProvider.GetBySubsCentreid(_subsCentreid_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbMailingListSubscriptionsSelectMethod.GetByUserName:
					_userName_nullable = (System.String) EntityUtil.ChangeType(values["UserName"], typeof(System.String));
					results = BbMailingListSubscriptionsProvider.GetByUserName(_userName_nullable, this.StartIndex, this.PageSize, out count);
					break;
				// FK
				case BbMailingListSubscriptionsSelectMethod.GetByBbMlid:
					_bbMlid = ( values["BbMlid"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["BbMlid"], typeof(System.Int32)) : (int)0;
					results = BbMailingListSubscriptionsProvider.GetByBbMlid(_bbMlid, this.StartIndex, this.PageSize, out count);
					break;
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
			if ( SelectMethod == BbMailingListSubscriptionsSelectMethod.Get || SelectMethod == BbMailingListSubscriptionsSelectMethod.GetByBbMlSid )
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
				BbMailingListSubscriptions entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbMailingListSubscriptionsProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbMailingListSubscriptions> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbMailingListSubscriptionsProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbMailingListSubscriptionsDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbMailingListSubscriptionsDataSource class.
	/// </summary>
	public class BbMailingListSubscriptionsDataSourceDesigner : ProviderDataSourceDesigner<BbMailingListSubscriptions, BbMailingListSubscriptionsKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbMailingListSubscriptionsDataSourceDesigner class.
		/// </summary>
		public BbMailingListSubscriptionsDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbMailingListSubscriptionsSelectMethod SelectMethod
		{
			get { return ((BbMailingListSubscriptionsDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbMailingListSubscriptionsDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbMailingListSubscriptionsDataSourceActionList

	/// <summary>
	/// Supports the BbMailingListSubscriptionsDataSourceDesigner class.
	/// </summary>
	internal class BbMailingListSubscriptionsDataSourceActionList : DesignerActionList
	{
		private BbMailingListSubscriptionsDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbMailingListSubscriptionsDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbMailingListSubscriptionsDataSourceActionList(BbMailingListSubscriptionsDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbMailingListSubscriptionsSelectMethod SelectMethod
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

	#endregion BbMailingListSubscriptionsDataSourceActionList
	
	#endregion BbMailingListSubscriptionsDataSourceDesigner
	
	#region BbMailingListSubscriptionsSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbMailingListSubscriptionsDataSource.SelectMethod property.
	/// </summary>
	public enum BbMailingListSubscriptionsSelectMethod
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
		/// Represents the GetBySubsCentreid method.
		/// </summary>
		GetBySubsCentreid,
		/// <summary>
		/// Represents the GetByUserName method.
		/// </summary>
		GetByUserName,
		/// <summary>
		/// Represents the GetByBbMlSid method.
		/// </summary>
		GetByBbMlSid,
		/// <summary>
		/// Represents the GetByBbMlid method.
		/// </summary>
		GetByBbMlid
	}
	
	#endregion BbMailingListSubscriptionsSelectMethod

	#region BbMailingListSubscriptionsFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbMailingListSubscriptions"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbMailingListSubscriptionsFilter : SqlFilter<BbMailingListSubscriptionsColumn>
	{
	}
	
	#endregion BbMailingListSubscriptionsFilter

	#region BbMailingListSubscriptionsExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbMailingListSubscriptions"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbMailingListSubscriptionsExpressionBuilder : SqlExpressionBuilder<BbMailingListSubscriptionsColumn>
	{
	}
	
	#endregion BbMailingListSubscriptionsExpressionBuilder	

	#region BbMailingListSubscriptionsProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbMailingListSubscriptionsChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbMailingListSubscriptions"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbMailingListSubscriptionsProperty : ChildEntityProperty<BbMailingListSubscriptionsChildEntityTypes>
	{
	}
	
	#endregion BbMailingListSubscriptionsProperty
}

