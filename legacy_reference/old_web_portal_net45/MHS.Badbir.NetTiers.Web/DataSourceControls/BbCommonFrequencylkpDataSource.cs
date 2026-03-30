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
	/// Represents the DataRepository.BbCommonFrequencylkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbCommonFrequencylkpDataSourceDesigner))]
	public class BbCommonFrequencylkpDataSource : ProviderDataSource<BbCommonFrequencylkp, BbCommonFrequencylkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCommonFrequencylkpDataSource class.
		/// </summary>
		public BbCommonFrequencylkpDataSource() : base(new BbCommonFrequencylkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbCommonFrequencylkpDataSourceView used by the BbCommonFrequencylkpDataSource.
		/// </summary>
		protected BbCommonFrequencylkpDataSourceView BbCommonFrequencylkpView
		{
			get { return ( View as BbCommonFrequencylkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbCommonFrequencylkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbCommonFrequencylkpSelectMethod SelectMethod
		{
			get
			{
				BbCommonFrequencylkpSelectMethod selectMethod = BbCommonFrequencylkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbCommonFrequencylkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbCommonFrequencylkpDataSourceView class that is to be
		/// used by the BbCommonFrequencylkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbCommonFrequencylkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbCommonFrequencylkp, BbCommonFrequencylkpKey> GetNewDataSourceView()
		{
			return new BbCommonFrequencylkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbCommonFrequencylkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbCommonFrequencylkpDataSourceView : ProviderDataSourceView<BbCommonFrequencylkp, BbCommonFrequencylkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCommonFrequencylkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbCommonFrequencylkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbCommonFrequencylkpDataSourceView(BbCommonFrequencylkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbCommonFrequencylkpDataSource BbCommonFrequencylkpOwner
		{
			get { return Owner as BbCommonFrequencylkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbCommonFrequencylkpSelectMethod SelectMethod
		{
			get { return BbCommonFrequencylkpOwner.SelectMethod; }
			set { BbCommonFrequencylkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbCommonFrequencylkpService BbCommonFrequencylkpProvider
		{
			get { return Provider as BbCommonFrequencylkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbCommonFrequencylkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbCommonFrequencylkp> results = null;
			BbCommonFrequencylkp item;
			count = 0;
			
			int _commonfrequencyid;

			switch ( SelectMethod )
			{
				case BbCommonFrequencylkpSelectMethod.Get:
					BbCommonFrequencylkpKey entityKey  = new BbCommonFrequencylkpKey();
					entityKey.Load(values);
					item = BbCommonFrequencylkpProvider.Get(entityKey);
					results = new TList<BbCommonFrequencylkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbCommonFrequencylkpSelectMethod.GetAll:
                    results = BbCommonFrequencylkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbCommonFrequencylkpSelectMethod.GetPaged:
					results = BbCommonFrequencylkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbCommonFrequencylkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbCommonFrequencylkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbCommonFrequencylkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbCommonFrequencylkpSelectMethod.GetByCommonfrequencyid:
					_commonfrequencyid = ( values["Commonfrequencyid"] != null ) ? (int) EntityUtil.ChangeType(values["Commonfrequencyid"], typeof(int)) : (int)0;
					item = BbCommonFrequencylkpProvider.GetByCommonfrequencyid(_commonfrequencyid);
					results = new TList<BbCommonFrequencylkp>();
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
			if ( SelectMethod == BbCommonFrequencylkpSelectMethod.Get || SelectMethod == BbCommonFrequencylkpSelectMethod.GetByCommonfrequencyid )
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
				BbCommonFrequencylkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbCommonFrequencylkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbCommonFrequencylkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbCommonFrequencylkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbCommonFrequencylkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbCommonFrequencylkpDataSource class.
	/// </summary>
	public class BbCommonFrequencylkpDataSourceDesigner : ProviderDataSourceDesigner<BbCommonFrequencylkp, BbCommonFrequencylkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbCommonFrequencylkpDataSourceDesigner class.
		/// </summary>
		public BbCommonFrequencylkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbCommonFrequencylkpSelectMethod SelectMethod
		{
			get { return ((BbCommonFrequencylkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbCommonFrequencylkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbCommonFrequencylkpDataSourceActionList

	/// <summary>
	/// Supports the BbCommonFrequencylkpDataSourceDesigner class.
	/// </summary>
	internal class BbCommonFrequencylkpDataSourceActionList : DesignerActionList
	{
		private BbCommonFrequencylkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbCommonFrequencylkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbCommonFrequencylkpDataSourceActionList(BbCommonFrequencylkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbCommonFrequencylkpSelectMethod SelectMethod
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

	#endregion BbCommonFrequencylkpDataSourceActionList
	
	#endregion BbCommonFrequencylkpDataSourceDesigner
	
	#region BbCommonFrequencylkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbCommonFrequencylkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbCommonFrequencylkpSelectMethod
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
		/// Represents the GetByCommonfrequencyid method.
		/// </summary>
		GetByCommonfrequencyid
	}
	
	#endregion BbCommonFrequencylkpSelectMethod

	#region BbCommonFrequencylkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbCommonFrequencylkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCommonFrequencylkpFilter : SqlFilter<BbCommonFrequencylkpColumn>
	{
	}
	
	#endregion BbCommonFrequencylkpFilter

	#region BbCommonFrequencylkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbCommonFrequencylkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCommonFrequencylkpExpressionBuilder : SqlExpressionBuilder<BbCommonFrequencylkpColumn>
	{
	}
	
	#endregion BbCommonFrequencylkpExpressionBuilder	

	#region BbCommonFrequencylkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbCommonFrequencylkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbCommonFrequencylkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCommonFrequencylkpProperty : ChildEntityProperty<BbCommonFrequencylkpChildEntityTypes>
	{
	}
	
	#endregion BbCommonFrequencylkpProperty
}

