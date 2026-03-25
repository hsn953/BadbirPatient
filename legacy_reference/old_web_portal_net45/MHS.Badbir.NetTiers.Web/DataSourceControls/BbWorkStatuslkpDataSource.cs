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
	/// Represents the DataRepository.BbWorkStatuslkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbWorkStatuslkpDataSourceDesigner))]
	public class BbWorkStatuslkpDataSource : ProviderDataSource<BbWorkStatuslkp, BbWorkStatuslkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbWorkStatuslkpDataSource class.
		/// </summary>
		public BbWorkStatuslkpDataSource() : base(new BbWorkStatuslkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbWorkStatuslkpDataSourceView used by the BbWorkStatuslkpDataSource.
		/// </summary>
		protected BbWorkStatuslkpDataSourceView BbWorkStatuslkpView
		{
			get { return ( View as BbWorkStatuslkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbWorkStatuslkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbWorkStatuslkpSelectMethod SelectMethod
		{
			get
			{
				BbWorkStatuslkpSelectMethod selectMethod = BbWorkStatuslkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbWorkStatuslkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbWorkStatuslkpDataSourceView class that is to be
		/// used by the BbWorkStatuslkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbWorkStatuslkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbWorkStatuslkp, BbWorkStatuslkpKey> GetNewDataSourceView()
		{
			return new BbWorkStatuslkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbWorkStatuslkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbWorkStatuslkpDataSourceView : ProviderDataSourceView<BbWorkStatuslkp, BbWorkStatuslkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbWorkStatuslkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbWorkStatuslkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbWorkStatuslkpDataSourceView(BbWorkStatuslkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbWorkStatuslkpDataSource BbWorkStatuslkpOwner
		{
			get { return Owner as BbWorkStatuslkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbWorkStatuslkpSelectMethod SelectMethod
		{
			get { return BbWorkStatuslkpOwner.SelectMethod; }
			set { BbWorkStatuslkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbWorkStatuslkpService BbWorkStatuslkpProvider
		{
			get { return Provider as BbWorkStatuslkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbWorkStatuslkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbWorkStatuslkp> results = null;
			BbWorkStatuslkp item;
			count = 0;
			
			int _worstatuskid;

			switch ( SelectMethod )
			{
				case BbWorkStatuslkpSelectMethod.Get:
					BbWorkStatuslkpKey entityKey  = new BbWorkStatuslkpKey();
					entityKey.Load(values);
					item = BbWorkStatuslkpProvider.Get(entityKey);
					results = new TList<BbWorkStatuslkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbWorkStatuslkpSelectMethod.GetAll:
                    results = BbWorkStatuslkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbWorkStatuslkpSelectMethod.GetPaged:
					results = BbWorkStatuslkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbWorkStatuslkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbWorkStatuslkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbWorkStatuslkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbWorkStatuslkpSelectMethod.GetByWorstatuskid:
					_worstatuskid = ( values["Worstatuskid"] != null ) ? (int) EntityUtil.ChangeType(values["Worstatuskid"], typeof(int)) : (int)0;
					item = BbWorkStatuslkpProvider.GetByWorstatuskid(_worstatuskid);
					results = new TList<BbWorkStatuslkp>();
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
			if ( SelectMethod == BbWorkStatuslkpSelectMethod.Get || SelectMethod == BbWorkStatuslkpSelectMethod.GetByWorstatuskid )
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
				BbWorkStatuslkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbWorkStatuslkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbWorkStatuslkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbWorkStatuslkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbWorkStatuslkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbWorkStatuslkpDataSource class.
	/// </summary>
	public class BbWorkStatuslkpDataSourceDesigner : ProviderDataSourceDesigner<BbWorkStatuslkp, BbWorkStatuslkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbWorkStatuslkpDataSourceDesigner class.
		/// </summary>
		public BbWorkStatuslkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbWorkStatuslkpSelectMethod SelectMethod
		{
			get { return ((BbWorkStatuslkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbWorkStatuslkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbWorkStatuslkpDataSourceActionList

	/// <summary>
	/// Supports the BbWorkStatuslkpDataSourceDesigner class.
	/// </summary>
	internal class BbWorkStatuslkpDataSourceActionList : DesignerActionList
	{
		private BbWorkStatuslkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbWorkStatuslkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbWorkStatuslkpDataSourceActionList(BbWorkStatuslkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbWorkStatuslkpSelectMethod SelectMethod
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

	#endregion BbWorkStatuslkpDataSourceActionList
	
	#endregion BbWorkStatuslkpDataSourceDesigner
	
	#region BbWorkStatuslkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbWorkStatuslkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbWorkStatuslkpSelectMethod
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
		/// Represents the GetByWorstatuskid method.
		/// </summary>
		GetByWorstatuskid
	}
	
	#endregion BbWorkStatuslkpSelectMethod

	#region BbWorkStatuslkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbWorkStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbWorkStatuslkpFilter : SqlFilter<BbWorkStatuslkpColumn>
	{
	}
	
	#endregion BbWorkStatuslkpFilter

	#region BbWorkStatuslkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbWorkStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbWorkStatuslkpExpressionBuilder : SqlExpressionBuilder<BbWorkStatuslkpColumn>
	{
	}
	
	#endregion BbWorkStatuslkpExpressionBuilder	

	#region BbWorkStatuslkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbWorkStatuslkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbWorkStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbWorkStatuslkpProperty : ChildEntityProperty<BbWorkStatuslkpChildEntityTypes>
	{
	}
	
	#endregion BbWorkStatuslkpProperty
}

