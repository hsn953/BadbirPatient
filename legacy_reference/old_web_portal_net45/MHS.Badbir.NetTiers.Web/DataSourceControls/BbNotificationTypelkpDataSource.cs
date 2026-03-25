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
	/// Represents the DataRepository.BbNotificationTypelkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbNotificationTypelkpDataSourceDesigner))]
	public class BbNotificationTypelkpDataSource : ProviderDataSource<BbNotificationTypelkp, BbNotificationTypelkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbNotificationTypelkpDataSource class.
		/// </summary>
		public BbNotificationTypelkpDataSource() : base(new BbNotificationTypelkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbNotificationTypelkpDataSourceView used by the BbNotificationTypelkpDataSource.
		/// </summary>
		protected BbNotificationTypelkpDataSourceView BbNotificationTypelkpView
		{
			get { return ( View as BbNotificationTypelkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbNotificationTypelkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbNotificationTypelkpSelectMethod SelectMethod
		{
			get
			{
				BbNotificationTypelkpSelectMethod selectMethod = BbNotificationTypelkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbNotificationTypelkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbNotificationTypelkpDataSourceView class that is to be
		/// used by the BbNotificationTypelkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbNotificationTypelkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbNotificationTypelkp, BbNotificationTypelkpKey> GetNewDataSourceView()
		{
			return new BbNotificationTypelkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbNotificationTypelkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbNotificationTypelkpDataSourceView : ProviderDataSourceView<BbNotificationTypelkp, BbNotificationTypelkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbNotificationTypelkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbNotificationTypelkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbNotificationTypelkpDataSourceView(BbNotificationTypelkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbNotificationTypelkpDataSource BbNotificationTypelkpOwner
		{
			get { return Owner as BbNotificationTypelkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbNotificationTypelkpSelectMethod SelectMethod
		{
			get { return BbNotificationTypelkpOwner.SelectMethod; }
			set { BbNotificationTypelkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbNotificationTypelkpService BbNotificationTypelkpProvider
		{
			get { return Provider as BbNotificationTypelkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbNotificationTypelkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbNotificationTypelkp> results = null;
			BbNotificationTypelkp item;
			count = 0;
			
			int _ntypeId;

			switch ( SelectMethod )
			{
				case BbNotificationTypelkpSelectMethod.Get:
					BbNotificationTypelkpKey entityKey  = new BbNotificationTypelkpKey();
					entityKey.Load(values);
					item = BbNotificationTypelkpProvider.Get(entityKey);
					results = new TList<BbNotificationTypelkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbNotificationTypelkpSelectMethod.GetAll:
                    results = BbNotificationTypelkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbNotificationTypelkpSelectMethod.GetPaged:
					results = BbNotificationTypelkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbNotificationTypelkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbNotificationTypelkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbNotificationTypelkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbNotificationTypelkpSelectMethod.GetByNtypeId:
					_ntypeId = ( values["NtypeId"] != null ) ? (int) EntityUtil.ChangeType(values["NtypeId"], typeof(int)) : (int)0;
					item = BbNotificationTypelkpProvider.GetByNtypeId(_ntypeId);
					results = new TList<BbNotificationTypelkp>();
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
			if ( SelectMethod == BbNotificationTypelkpSelectMethod.Get || SelectMethod == BbNotificationTypelkpSelectMethod.GetByNtypeId )
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
				BbNotificationTypelkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbNotificationTypelkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbNotificationTypelkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbNotificationTypelkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbNotificationTypelkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbNotificationTypelkpDataSource class.
	/// </summary>
	public class BbNotificationTypelkpDataSourceDesigner : ProviderDataSourceDesigner<BbNotificationTypelkp, BbNotificationTypelkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbNotificationTypelkpDataSourceDesigner class.
		/// </summary>
		public BbNotificationTypelkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbNotificationTypelkpSelectMethod SelectMethod
		{
			get { return ((BbNotificationTypelkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbNotificationTypelkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbNotificationTypelkpDataSourceActionList

	/// <summary>
	/// Supports the BbNotificationTypelkpDataSourceDesigner class.
	/// </summary>
	internal class BbNotificationTypelkpDataSourceActionList : DesignerActionList
	{
		private BbNotificationTypelkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbNotificationTypelkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbNotificationTypelkpDataSourceActionList(BbNotificationTypelkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbNotificationTypelkpSelectMethod SelectMethod
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

	#endregion BbNotificationTypelkpDataSourceActionList
	
	#endregion BbNotificationTypelkpDataSourceDesigner
	
	#region BbNotificationTypelkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbNotificationTypelkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbNotificationTypelkpSelectMethod
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
		/// Represents the GetByNtypeId method.
		/// </summary>
		GetByNtypeId
	}
	
	#endregion BbNotificationTypelkpSelectMethod

	#region BbNotificationTypelkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbNotificationTypelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbNotificationTypelkpFilter : SqlFilter<BbNotificationTypelkpColumn>
	{
	}
	
	#endregion BbNotificationTypelkpFilter

	#region BbNotificationTypelkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbNotificationTypelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbNotificationTypelkpExpressionBuilder : SqlExpressionBuilder<BbNotificationTypelkpColumn>
	{
	}
	
	#endregion BbNotificationTypelkpExpressionBuilder	

	#region BbNotificationTypelkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbNotificationTypelkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbNotificationTypelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbNotificationTypelkpProperty : ChildEntityProperty<BbNotificationTypelkpChildEntityTypes>
	{
	}
	
	#endregion BbNotificationTypelkpProperty
}

