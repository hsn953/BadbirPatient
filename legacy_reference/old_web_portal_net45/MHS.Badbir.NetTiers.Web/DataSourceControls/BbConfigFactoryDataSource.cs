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
	/// Represents the DataRepository.BbConfigFactoryProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbConfigFactoryDataSourceDesigner))]
	public class BbConfigFactoryDataSource : ProviderDataSource<BbConfigFactory, BbConfigFactoryKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbConfigFactoryDataSource class.
		/// </summary>
		public BbConfigFactoryDataSource() : base(new BbConfigFactoryService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbConfigFactoryDataSourceView used by the BbConfigFactoryDataSource.
		/// </summary>
		protected BbConfigFactoryDataSourceView BbConfigFactoryView
		{
			get { return ( View as BbConfigFactoryDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbConfigFactoryDataSource control invokes to retrieve data.
		/// </summary>
		public BbConfigFactorySelectMethod SelectMethod
		{
			get
			{
				BbConfigFactorySelectMethod selectMethod = BbConfigFactorySelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbConfigFactorySelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbConfigFactoryDataSourceView class that is to be
		/// used by the BbConfigFactoryDataSource.
		/// </summary>
		/// <returns>An instance of the BbConfigFactoryDataSourceView class.</returns>
		protected override BaseDataSourceView<BbConfigFactory, BbConfigFactoryKey> GetNewDataSourceView()
		{
			return new BbConfigFactoryDataSourceView(this, DefaultViewName);
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
	/// Supports the BbConfigFactoryDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbConfigFactoryDataSourceView : ProviderDataSourceView<BbConfigFactory, BbConfigFactoryKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbConfigFactoryDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbConfigFactoryDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbConfigFactoryDataSourceView(BbConfigFactoryDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbConfigFactoryDataSource BbConfigFactoryOwner
		{
			get { return Owner as BbConfigFactoryDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbConfigFactorySelectMethod SelectMethod
		{
			get { return BbConfigFactoryOwner.SelectMethod; }
			set { BbConfigFactoryOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbConfigFactoryService BbConfigFactoryProvider
		{
			get { return Provider as BbConfigFactoryService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbConfigFactory> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbConfigFactory> results = null;
			BbConfigFactory item;
			count = 0;
			
			bool _inuse;
			byte _typeId;
			int _configid;

			switch ( SelectMethod )
			{
				case BbConfigFactorySelectMethod.Get:
					BbConfigFactoryKey entityKey  = new BbConfigFactoryKey();
					entityKey.Load(values);
					item = BbConfigFactoryProvider.Get(entityKey);
					results = new TList<BbConfigFactory>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbConfigFactorySelectMethod.GetAll:
                    results = BbConfigFactoryProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbConfigFactorySelectMethod.GetPaged:
					results = BbConfigFactoryProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbConfigFactorySelectMethod.Find:
					if ( FilterParameters != null )
						results = BbConfigFactoryProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbConfigFactoryProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbConfigFactorySelectMethod.GetByConfigid:
					_configid = ( values["Configid"] != null ) ? (int) EntityUtil.ChangeType(values["Configid"], typeof(int)) : (int)0;
					item = BbConfigFactoryProvider.GetByConfigid(_configid);
					results = new TList<BbConfigFactory>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case BbConfigFactorySelectMethod.GetByInuse:
					_inuse = ( values["Inuse"] != null ) ? (bool) EntityUtil.ChangeType(values["Inuse"], typeof(bool)) : true;
					results = BbConfigFactoryProvider.GetByInuse(_inuse, this.StartIndex, this.PageSize, out count);
					break;
				case BbConfigFactorySelectMethod.GetByTypeIdInuse:
					_typeId = ( values["TypeId"] != null ) ? (byte) EntityUtil.ChangeType(values["TypeId"], typeof(byte)) : (byte)0;
					_inuse = ( values["Inuse"] != null ) ? (bool) EntityUtil.ChangeType(values["Inuse"], typeof(bool)) : true;
					results = BbConfigFactoryProvider.GetByTypeIdInuse(_typeId, _inuse, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == BbConfigFactorySelectMethod.Get || SelectMethod == BbConfigFactorySelectMethod.GetByConfigid )
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
				BbConfigFactory entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbConfigFactoryProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbConfigFactory> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbConfigFactoryProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbConfigFactoryDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbConfigFactoryDataSource class.
	/// </summary>
	public class BbConfigFactoryDataSourceDesigner : ProviderDataSourceDesigner<BbConfigFactory, BbConfigFactoryKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbConfigFactoryDataSourceDesigner class.
		/// </summary>
		public BbConfigFactoryDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbConfigFactorySelectMethod SelectMethod
		{
			get { return ((BbConfigFactoryDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbConfigFactoryDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbConfigFactoryDataSourceActionList

	/// <summary>
	/// Supports the BbConfigFactoryDataSourceDesigner class.
	/// </summary>
	internal class BbConfigFactoryDataSourceActionList : DesignerActionList
	{
		private BbConfigFactoryDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbConfigFactoryDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbConfigFactoryDataSourceActionList(BbConfigFactoryDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbConfigFactorySelectMethod SelectMethod
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

	#endregion BbConfigFactoryDataSourceActionList
	
	#endregion BbConfigFactoryDataSourceDesigner
	
	#region BbConfigFactorySelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbConfigFactoryDataSource.SelectMethod property.
	/// </summary>
	public enum BbConfigFactorySelectMethod
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
		/// Represents the GetByInuse method.
		/// </summary>
		GetByInuse,
		/// <summary>
		/// Represents the GetByTypeIdInuse method.
		/// </summary>
		GetByTypeIdInuse,
		/// <summary>
		/// Represents the GetByConfigid method.
		/// </summary>
		GetByConfigid
	}
	
	#endregion BbConfigFactorySelectMethod

	#region BbConfigFactoryFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbConfigFactory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbConfigFactoryFilter : SqlFilter<BbConfigFactoryColumn>
	{
	}
	
	#endregion BbConfigFactoryFilter

	#region BbConfigFactoryExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbConfigFactory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbConfigFactoryExpressionBuilder : SqlExpressionBuilder<BbConfigFactoryColumn>
	{
	}
	
	#endregion BbConfigFactoryExpressionBuilder	

	#region BbConfigFactoryProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbConfigFactoryChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbConfigFactory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbConfigFactoryProperty : ChildEntityProperty<BbConfigFactoryChildEntityTypes>
	{
	}
	
	#endregion BbConfigFactoryProperty
}

