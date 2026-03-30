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
	/// Represents the DataRepository.BbTitlelkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbTitlelkpDataSourceDesigner))]
	public class BbTitlelkpDataSource : ProviderDataSource<BbTitlelkp, BbTitlelkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbTitlelkpDataSource class.
		/// </summary>
		public BbTitlelkpDataSource() : base(new BbTitlelkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbTitlelkpDataSourceView used by the BbTitlelkpDataSource.
		/// </summary>
		protected BbTitlelkpDataSourceView BbTitlelkpView
		{
			get { return ( View as BbTitlelkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbTitlelkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbTitlelkpSelectMethod SelectMethod
		{
			get
			{
				BbTitlelkpSelectMethod selectMethod = BbTitlelkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbTitlelkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbTitlelkpDataSourceView class that is to be
		/// used by the BbTitlelkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbTitlelkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbTitlelkp, BbTitlelkpKey> GetNewDataSourceView()
		{
			return new BbTitlelkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbTitlelkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbTitlelkpDataSourceView : ProviderDataSourceView<BbTitlelkp, BbTitlelkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbTitlelkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbTitlelkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbTitlelkpDataSourceView(BbTitlelkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbTitlelkpDataSource BbTitlelkpOwner
		{
			get { return Owner as BbTitlelkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbTitlelkpSelectMethod SelectMethod
		{
			get { return BbTitlelkpOwner.SelectMethod; }
			set { BbTitlelkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbTitlelkpService BbTitlelkpProvider
		{
			get { return Provider as BbTitlelkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbTitlelkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbTitlelkp> results = null;
			BbTitlelkp item;
			count = 0;
			
			int _titleid;

			switch ( SelectMethod )
			{
				case BbTitlelkpSelectMethod.Get:
					BbTitlelkpKey entityKey  = new BbTitlelkpKey();
					entityKey.Load(values);
					item = BbTitlelkpProvider.Get(entityKey);
					results = new TList<BbTitlelkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbTitlelkpSelectMethod.GetAll:
                    results = BbTitlelkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbTitlelkpSelectMethod.GetPaged:
					results = BbTitlelkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbTitlelkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbTitlelkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbTitlelkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbTitlelkpSelectMethod.GetByTitleid:
					_titleid = ( values["Titleid"] != null ) ? (int) EntityUtil.ChangeType(values["Titleid"], typeof(int)) : (int)0;
					item = BbTitlelkpProvider.GetByTitleid(_titleid);
					results = new TList<BbTitlelkp>();
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
			if ( SelectMethod == BbTitlelkpSelectMethod.Get || SelectMethod == BbTitlelkpSelectMethod.GetByTitleid )
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
				BbTitlelkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbTitlelkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbTitlelkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbTitlelkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbTitlelkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbTitlelkpDataSource class.
	/// </summary>
	public class BbTitlelkpDataSourceDesigner : ProviderDataSourceDesigner<BbTitlelkp, BbTitlelkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbTitlelkpDataSourceDesigner class.
		/// </summary>
		public BbTitlelkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbTitlelkpSelectMethod SelectMethod
		{
			get { return ((BbTitlelkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbTitlelkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbTitlelkpDataSourceActionList

	/// <summary>
	/// Supports the BbTitlelkpDataSourceDesigner class.
	/// </summary>
	internal class BbTitlelkpDataSourceActionList : DesignerActionList
	{
		private BbTitlelkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbTitlelkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbTitlelkpDataSourceActionList(BbTitlelkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbTitlelkpSelectMethod SelectMethod
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

	#endregion BbTitlelkpDataSourceActionList
	
	#endregion BbTitlelkpDataSourceDesigner
	
	#region BbTitlelkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbTitlelkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbTitlelkpSelectMethod
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
		/// Represents the GetByTitleid method.
		/// </summary>
		GetByTitleid
	}
	
	#endregion BbTitlelkpSelectMethod

	#region BbTitlelkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbTitlelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbTitlelkpFilter : SqlFilter<BbTitlelkpColumn>
	{
	}
	
	#endregion BbTitlelkpFilter

	#region BbTitlelkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbTitlelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbTitlelkpExpressionBuilder : SqlExpressionBuilder<BbTitlelkpColumn>
	{
	}
	
	#endregion BbTitlelkpExpressionBuilder	

	#region BbTitlelkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbTitlelkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbTitlelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbTitlelkpProperty : ChildEntityProperty<BbTitlelkpChildEntityTypes>
	{
	}
	
	#endregion BbTitlelkpProperty
}

