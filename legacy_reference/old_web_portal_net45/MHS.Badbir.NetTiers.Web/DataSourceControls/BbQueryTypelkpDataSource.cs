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
	/// Represents the DataRepository.BbQueryTypelkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbQueryTypelkpDataSourceDesigner))]
	public class BbQueryTypelkpDataSource : ProviderDataSource<BbQueryTypelkp, BbQueryTypelkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryTypelkpDataSource class.
		/// </summary>
		public BbQueryTypelkpDataSource() : base(new BbQueryTypelkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbQueryTypelkpDataSourceView used by the BbQueryTypelkpDataSource.
		/// </summary>
		protected BbQueryTypelkpDataSourceView BbQueryTypelkpView
		{
			get { return ( View as BbQueryTypelkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbQueryTypelkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbQueryTypelkpSelectMethod SelectMethod
		{
			get
			{
				BbQueryTypelkpSelectMethod selectMethod = BbQueryTypelkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbQueryTypelkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbQueryTypelkpDataSourceView class that is to be
		/// used by the BbQueryTypelkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbQueryTypelkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbQueryTypelkp, BbQueryTypelkpKey> GetNewDataSourceView()
		{
			return new BbQueryTypelkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbQueryTypelkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbQueryTypelkpDataSourceView : ProviderDataSourceView<BbQueryTypelkp, BbQueryTypelkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryTypelkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbQueryTypelkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbQueryTypelkpDataSourceView(BbQueryTypelkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbQueryTypelkpDataSource BbQueryTypelkpOwner
		{
			get { return Owner as BbQueryTypelkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbQueryTypelkpSelectMethod SelectMethod
		{
			get { return BbQueryTypelkpOwner.SelectMethod; }
			set { BbQueryTypelkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbQueryTypelkpService BbQueryTypelkpProvider
		{
			get { return Provider as BbQueryTypelkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbQueryTypelkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbQueryTypelkp> results = null;
			BbQueryTypelkp item;
			count = 0;
			
			int _queryTypeId;

			switch ( SelectMethod )
			{
				case BbQueryTypelkpSelectMethod.Get:
					BbQueryTypelkpKey entityKey  = new BbQueryTypelkpKey();
					entityKey.Load(values);
					item = BbQueryTypelkpProvider.Get(entityKey);
					results = new TList<BbQueryTypelkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbQueryTypelkpSelectMethod.GetAll:
                    results = BbQueryTypelkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbQueryTypelkpSelectMethod.GetPaged:
					results = BbQueryTypelkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbQueryTypelkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbQueryTypelkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbQueryTypelkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbQueryTypelkpSelectMethod.GetByQueryTypeId:
					_queryTypeId = ( values["QueryTypeId"] != null ) ? (int) EntityUtil.ChangeType(values["QueryTypeId"], typeof(int)) : (int)0;
					item = BbQueryTypelkpProvider.GetByQueryTypeId(_queryTypeId);
					results = new TList<BbQueryTypelkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				// M:M
				// Custom
				case BbQueryTypelkpSelectMethod.QueryTypelkp_GetAllForPV:
					results = BbQueryTypelkpProvider.QueryTypelkp_GetAllForPV(StartIndex, PageSize);
					break;
				case BbQueryTypelkpSelectMethod.QueryTypelkp_GetAllForAdmin:
					results = BbQueryTypelkpProvider.QueryTypelkp_GetAllForAdmin(StartIndex, PageSize);
					break;
				case BbQueryTypelkpSelectMethod.QueryTypelkp_GetAllForCentre:
					results = BbQueryTypelkpProvider.QueryTypelkp_GetAllForCentre(StartIndex, PageSize);
					break;
				case BbQueryTypelkpSelectMethod.QueryTypelkp_GetAllForClinician:
					results = BbQueryTypelkpProvider.QueryTypelkp_GetAllForClinician(StartIndex, PageSize);
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
			if ( SelectMethod == BbQueryTypelkpSelectMethod.Get || SelectMethod == BbQueryTypelkpSelectMethod.GetByQueryTypeId )
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
				BbQueryTypelkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbQueryTypelkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbQueryTypelkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbQueryTypelkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbQueryTypelkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbQueryTypelkpDataSource class.
	/// </summary>
	public class BbQueryTypelkpDataSourceDesigner : ProviderDataSourceDesigner<BbQueryTypelkp, BbQueryTypelkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbQueryTypelkpDataSourceDesigner class.
		/// </summary>
		public BbQueryTypelkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbQueryTypelkpSelectMethod SelectMethod
		{
			get { return ((BbQueryTypelkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbQueryTypelkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbQueryTypelkpDataSourceActionList

	/// <summary>
	/// Supports the BbQueryTypelkpDataSourceDesigner class.
	/// </summary>
	internal class BbQueryTypelkpDataSourceActionList : DesignerActionList
	{
		private BbQueryTypelkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbQueryTypelkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbQueryTypelkpDataSourceActionList(BbQueryTypelkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbQueryTypelkpSelectMethod SelectMethod
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

	#endregion BbQueryTypelkpDataSourceActionList
	
	#endregion BbQueryTypelkpDataSourceDesigner
	
	#region BbQueryTypelkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbQueryTypelkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbQueryTypelkpSelectMethod
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
		/// Represents the GetByQueryTypeId method.
		/// </summary>
		GetByQueryTypeId,
		/// <summary>
		/// Represents the QueryTypelkp_GetAllForPV method.
		/// </summary>
		QueryTypelkp_GetAllForPV,
		/// <summary>
		/// Represents the QueryTypelkp_GetAllForAdmin method.
		/// </summary>
		QueryTypelkp_GetAllForAdmin,
		/// <summary>
		/// Represents the QueryTypelkp_GetAllForCentre method.
		/// </summary>
		QueryTypelkp_GetAllForCentre,
		/// <summary>
		/// Represents the QueryTypelkp_GetAllForClinician method.
		/// </summary>
		QueryTypelkp_GetAllForClinician
	}
	
	#endregion BbQueryTypelkpSelectMethod

	#region BbQueryTypelkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryTypelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryTypelkpFilter : SqlFilter<BbQueryTypelkpColumn>
	{
	}
	
	#endregion BbQueryTypelkpFilter

	#region BbQueryTypelkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryTypelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryTypelkpExpressionBuilder : SqlExpressionBuilder<BbQueryTypelkpColumn>
	{
	}
	
	#endregion BbQueryTypelkpExpressionBuilder	

	#region BbQueryTypelkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbQueryTypelkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryTypelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryTypelkpProperty : ChildEntityProperty<BbQueryTypelkpChildEntityTypes>
	{
	}
	
	#endregion BbQueryTypelkpProperty
}

