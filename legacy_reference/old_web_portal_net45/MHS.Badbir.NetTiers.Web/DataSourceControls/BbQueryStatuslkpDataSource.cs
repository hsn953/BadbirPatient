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
	/// Represents the DataRepository.BbQueryStatuslkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbQueryStatuslkpDataSourceDesigner))]
	public class BbQueryStatuslkpDataSource : ProviderDataSource<BbQueryStatuslkp, BbQueryStatuslkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryStatuslkpDataSource class.
		/// </summary>
		public BbQueryStatuslkpDataSource() : base(new BbQueryStatuslkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbQueryStatuslkpDataSourceView used by the BbQueryStatuslkpDataSource.
		/// </summary>
		protected BbQueryStatuslkpDataSourceView BbQueryStatuslkpView
		{
			get { return ( View as BbQueryStatuslkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbQueryStatuslkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbQueryStatuslkpSelectMethod SelectMethod
		{
			get
			{
				BbQueryStatuslkpSelectMethod selectMethod = BbQueryStatuslkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbQueryStatuslkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbQueryStatuslkpDataSourceView class that is to be
		/// used by the BbQueryStatuslkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbQueryStatuslkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbQueryStatuslkp, BbQueryStatuslkpKey> GetNewDataSourceView()
		{
			return new BbQueryStatuslkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbQueryStatuslkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbQueryStatuslkpDataSourceView : ProviderDataSourceView<BbQueryStatuslkp, BbQueryStatuslkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryStatuslkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbQueryStatuslkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbQueryStatuslkpDataSourceView(BbQueryStatuslkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbQueryStatuslkpDataSource BbQueryStatuslkpOwner
		{
			get { return Owner as BbQueryStatuslkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbQueryStatuslkpSelectMethod SelectMethod
		{
			get { return BbQueryStatuslkpOwner.SelectMethod; }
			set { BbQueryStatuslkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbQueryStatuslkpService BbQueryStatuslkpProvider
		{
			get { return Provider as BbQueryStatuslkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbQueryStatuslkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbQueryStatuslkp> results = null;
			BbQueryStatuslkp item;
			count = 0;
			
			int _queryStatusId;

			switch ( SelectMethod )
			{
				case BbQueryStatuslkpSelectMethod.Get:
					BbQueryStatuslkpKey entityKey  = new BbQueryStatuslkpKey();
					entityKey.Load(values);
					item = BbQueryStatuslkpProvider.Get(entityKey);
					results = new TList<BbQueryStatuslkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbQueryStatuslkpSelectMethod.GetAll:
                    results = BbQueryStatuslkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbQueryStatuslkpSelectMethod.GetPaged:
					results = BbQueryStatuslkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbQueryStatuslkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbQueryStatuslkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbQueryStatuslkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbQueryStatuslkpSelectMethod.GetByQueryStatusId:
					_queryStatusId = ( values["QueryStatusId"] != null ) ? (int) EntityUtil.ChangeType(values["QueryStatusId"], typeof(int)) : (int)0;
					item = BbQueryStatuslkpProvider.GetByQueryStatusId(_queryStatusId);
					results = new TList<BbQueryStatuslkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
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
			if ( SelectMethod == BbQueryStatuslkpSelectMethod.Get || SelectMethod == BbQueryStatuslkpSelectMethod.GetByQueryStatusId )
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
				BbQueryStatuslkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbQueryStatuslkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbQueryStatuslkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbQueryStatuslkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbQueryStatuslkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbQueryStatuslkpDataSource class.
	/// </summary>
	public class BbQueryStatuslkpDataSourceDesigner : ProviderDataSourceDesigner<BbQueryStatuslkp, BbQueryStatuslkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbQueryStatuslkpDataSourceDesigner class.
		/// </summary>
		public BbQueryStatuslkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbQueryStatuslkpSelectMethod SelectMethod
		{
			get { return ((BbQueryStatuslkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbQueryStatuslkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbQueryStatuslkpDataSourceActionList

	/// <summary>
	/// Supports the BbQueryStatuslkpDataSourceDesigner class.
	/// </summary>
	internal class BbQueryStatuslkpDataSourceActionList : DesignerActionList
	{
		private BbQueryStatuslkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbQueryStatuslkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbQueryStatuslkpDataSourceActionList(BbQueryStatuslkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbQueryStatuslkpSelectMethod SelectMethod
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

	#endregion BbQueryStatuslkpDataSourceActionList
	
	#endregion BbQueryStatuslkpDataSourceDesigner
	
	#region BbQueryStatuslkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbQueryStatuslkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbQueryStatuslkpSelectMethod
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
		/// Represents the GetByQueryStatusId method.
		/// </summary>
		GetByQueryStatusId
	}
	
	#endregion BbQueryStatuslkpSelectMethod

	#region BbQueryStatuslkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryStatuslkpFilter : SqlFilter<BbQueryStatuslkpColumn>
	{
	}
	
	#endregion BbQueryStatuslkpFilter

	#region BbQueryStatuslkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryStatuslkpExpressionBuilder : SqlExpressionBuilder<BbQueryStatuslkpColumn>
	{
	}
	
	#endregion BbQueryStatuslkpExpressionBuilder	

	#region BbQueryStatuslkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbQueryStatuslkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryStatuslkpProperty : ChildEntityProperty<BbQueryStatuslkpChildEntityTypes>
	{
	}
	
	#endregion BbQueryStatuslkpProperty
}

