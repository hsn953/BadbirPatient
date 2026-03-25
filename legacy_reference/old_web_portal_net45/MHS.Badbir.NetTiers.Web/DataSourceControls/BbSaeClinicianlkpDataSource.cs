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
	/// Represents the DataRepository.BbSaeClinicianlkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbSaeClinicianlkpDataSourceDesigner))]
	public class BbSaeClinicianlkpDataSource : ProviderDataSource<BbSaeClinicianlkp, BbSaeClinicianlkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbSaeClinicianlkpDataSource class.
		/// </summary>
		public BbSaeClinicianlkpDataSource() : base(new BbSaeClinicianlkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbSaeClinicianlkpDataSourceView used by the BbSaeClinicianlkpDataSource.
		/// </summary>
		protected BbSaeClinicianlkpDataSourceView BbSaeClinicianlkpView
		{
			get { return ( View as BbSaeClinicianlkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbSaeClinicianlkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbSaeClinicianlkpSelectMethod SelectMethod
		{
			get
			{
				BbSaeClinicianlkpSelectMethod selectMethod = BbSaeClinicianlkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbSaeClinicianlkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbSaeClinicianlkpDataSourceView class that is to be
		/// used by the BbSaeClinicianlkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbSaeClinicianlkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbSaeClinicianlkp, BbSaeClinicianlkpKey> GetNewDataSourceView()
		{
			return new BbSaeClinicianlkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbSaeClinicianlkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbSaeClinicianlkpDataSourceView : ProviderDataSourceView<BbSaeClinicianlkp, BbSaeClinicianlkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbSaeClinicianlkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbSaeClinicianlkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbSaeClinicianlkpDataSourceView(BbSaeClinicianlkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbSaeClinicianlkpDataSource BbSaeClinicianlkpOwner
		{
			get { return Owner as BbSaeClinicianlkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbSaeClinicianlkpSelectMethod SelectMethod
		{
			get { return BbSaeClinicianlkpOwner.SelectMethod; }
			set { BbSaeClinicianlkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbSaeClinicianlkpService BbSaeClinicianlkpProvider
		{
			get { return Provider as BbSaeClinicianlkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbSaeClinicianlkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbSaeClinicianlkp> results = null;
			BbSaeClinicianlkp item;
			count = 0;
			
			int _saeid;

			switch ( SelectMethod )
			{
				case BbSaeClinicianlkpSelectMethod.Get:
					BbSaeClinicianlkpKey entityKey  = new BbSaeClinicianlkpKey();
					entityKey.Load(values);
					item = BbSaeClinicianlkpProvider.Get(entityKey);
					results = new TList<BbSaeClinicianlkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbSaeClinicianlkpSelectMethod.GetAll:
                    results = BbSaeClinicianlkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbSaeClinicianlkpSelectMethod.GetPaged:
					results = BbSaeClinicianlkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbSaeClinicianlkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbSaeClinicianlkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbSaeClinicianlkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbSaeClinicianlkpSelectMethod.GetBySaeid:
					_saeid = ( values["Saeid"] != null ) ? (int) EntityUtil.ChangeType(values["Saeid"], typeof(int)) : (int)0;
					item = BbSaeClinicianlkpProvider.GetBySaeid(_saeid);
					results = new TList<BbSaeClinicianlkp>();
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
			if ( SelectMethod == BbSaeClinicianlkpSelectMethod.Get || SelectMethod == BbSaeClinicianlkpSelectMethod.GetBySaeid )
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
				BbSaeClinicianlkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbSaeClinicianlkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbSaeClinicianlkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbSaeClinicianlkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbSaeClinicianlkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbSaeClinicianlkpDataSource class.
	/// </summary>
	public class BbSaeClinicianlkpDataSourceDesigner : ProviderDataSourceDesigner<BbSaeClinicianlkp, BbSaeClinicianlkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbSaeClinicianlkpDataSourceDesigner class.
		/// </summary>
		public BbSaeClinicianlkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbSaeClinicianlkpSelectMethod SelectMethod
		{
			get { return ((BbSaeClinicianlkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbSaeClinicianlkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbSaeClinicianlkpDataSourceActionList

	/// <summary>
	/// Supports the BbSaeClinicianlkpDataSourceDesigner class.
	/// </summary>
	internal class BbSaeClinicianlkpDataSourceActionList : DesignerActionList
	{
		private BbSaeClinicianlkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbSaeClinicianlkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbSaeClinicianlkpDataSourceActionList(BbSaeClinicianlkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbSaeClinicianlkpSelectMethod SelectMethod
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

	#endregion BbSaeClinicianlkpDataSourceActionList
	
	#endregion BbSaeClinicianlkpDataSourceDesigner
	
	#region BbSaeClinicianlkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbSaeClinicianlkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbSaeClinicianlkpSelectMethod
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
		/// Represents the GetBySaeid method.
		/// </summary>
		GetBySaeid
	}
	
	#endregion BbSaeClinicianlkpSelectMethod

	#region BbSaeClinicianlkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbSaeClinicianlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbSaeClinicianlkpFilter : SqlFilter<BbSaeClinicianlkpColumn>
	{
	}
	
	#endregion BbSaeClinicianlkpFilter

	#region BbSaeClinicianlkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbSaeClinicianlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbSaeClinicianlkpExpressionBuilder : SqlExpressionBuilder<BbSaeClinicianlkpColumn>
	{
	}
	
	#endregion BbSaeClinicianlkpExpressionBuilder	

	#region BbSaeClinicianlkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbSaeClinicianlkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbSaeClinicianlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbSaeClinicianlkpProperty : ChildEntityProperty<BbSaeClinicianlkpChildEntityTypes>
	{
	}
	
	#endregion BbSaeClinicianlkpProperty
}

