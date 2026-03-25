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
	/// Represents the DataRepository.BbUkcrNregionlkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbUkcrNregionlkpDataSourceDesigner))]
	public class BbUkcrNregionlkpDataSource : ProviderDataSource<BbUkcrNregionlkp, BbUkcrNregionlkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbUkcrNregionlkpDataSource class.
		/// </summary>
		public BbUkcrNregionlkpDataSource() : base(new BbUkcrNregionlkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbUkcrNregionlkpDataSourceView used by the BbUkcrNregionlkpDataSource.
		/// </summary>
		protected BbUkcrNregionlkpDataSourceView BbUkcrNregionlkpView
		{
			get { return ( View as BbUkcrNregionlkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbUkcrNregionlkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbUkcrNregionlkpSelectMethod SelectMethod
		{
			get
			{
				BbUkcrNregionlkpSelectMethod selectMethod = BbUkcrNregionlkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbUkcrNregionlkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbUkcrNregionlkpDataSourceView class that is to be
		/// used by the BbUkcrNregionlkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbUkcrNregionlkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbUkcrNregionlkp, BbUkcrNregionlkpKey> GetNewDataSourceView()
		{
			return new BbUkcrNregionlkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbUkcrNregionlkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbUkcrNregionlkpDataSourceView : ProviderDataSourceView<BbUkcrNregionlkp, BbUkcrNregionlkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbUkcrNregionlkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbUkcrNregionlkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbUkcrNregionlkpDataSourceView(BbUkcrNregionlkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbUkcrNregionlkpDataSource BbUkcrNregionlkpOwner
		{
			get { return Owner as BbUkcrNregionlkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbUkcrNregionlkpSelectMethod SelectMethod
		{
			get { return BbUkcrNregionlkpOwner.SelectMethod; }
			set { BbUkcrNregionlkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbUkcrNregionlkpService BbUkcrNregionlkpProvider
		{
			get { return Provider as BbUkcrNregionlkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbUkcrNregionlkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbUkcrNregionlkp> results = null;
			BbUkcrNregionlkp item;
			count = 0;
			
			System.Int32 _ukcrNregid;

			switch ( SelectMethod )
			{
				case BbUkcrNregionlkpSelectMethod.Get:
					BbUkcrNregionlkpKey entityKey  = new BbUkcrNregionlkpKey();
					entityKey.Load(values);
					item = BbUkcrNregionlkpProvider.Get(entityKey);
					results = new TList<BbUkcrNregionlkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbUkcrNregionlkpSelectMethod.GetAll:
                    results = BbUkcrNregionlkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbUkcrNregionlkpSelectMethod.GetPaged:
					results = BbUkcrNregionlkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbUkcrNregionlkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbUkcrNregionlkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbUkcrNregionlkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbUkcrNregionlkpSelectMethod.GetByUkcrNregid:
					_ukcrNregid = ( values["UkcrNregid"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["UkcrNregid"], typeof(System.Int32)) : (int)0;
					item = BbUkcrNregionlkpProvider.GetByUkcrNregid(_ukcrNregid);
					results = new TList<BbUkcrNregionlkp>();
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
			if ( SelectMethod == BbUkcrNregionlkpSelectMethod.Get || SelectMethod == BbUkcrNregionlkpSelectMethod.GetByUkcrNregid )
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
				BbUkcrNregionlkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbUkcrNregionlkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbUkcrNregionlkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbUkcrNregionlkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbUkcrNregionlkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbUkcrNregionlkpDataSource class.
	/// </summary>
	public class BbUkcrNregionlkpDataSourceDesigner : ProviderDataSourceDesigner<BbUkcrNregionlkp, BbUkcrNregionlkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbUkcrNregionlkpDataSourceDesigner class.
		/// </summary>
		public BbUkcrNregionlkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbUkcrNregionlkpSelectMethod SelectMethod
		{
			get { return ((BbUkcrNregionlkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbUkcrNregionlkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbUkcrNregionlkpDataSourceActionList

	/// <summary>
	/// Supports the BbUkcrNregionlkpDataSourceDesigner class.
	/// </summary>
	internal class BbUkcrNregionlkpDataSourceActionList : DesignerActionList
	{
		private BbUkcrNregionlkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbUkcrNregionlkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbUkcrNregionlkpDataSourceActionList(BbUkcrNregionlkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbUkcrNregionlkpSelectMethod SelectMethod
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

	#endregion BbUkcrNregionlkpDataSourceActionList
	
	#endregion BbUkcrNregionlkpDataSourceDesigner
	
	#region BbUkcrNregionlkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbUkcrNregionlkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbUkcrNregionlkpSelectMethod
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
		/// Represents the GetByUkcrNregid method.
		/// </summary>
		GetByUkcrNregid
	}
	
	#endregion BbUkcrNregionlkpSelectMethod

	#region BbUkcrNregionlkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbUkcrNregionlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbUkcrNregionlkpFilter : SqlFilter<BbUkcrNregionlkpColumn>
	{
	}
	
	#endregion BbUkcrNregionlkpFilter

	#region BbUkcrNregionlkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbUkcrNregionlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbUkcrNregionlkpExpressionBuilder : SqlExpressionBuilder<BbUkcrNregionlkpColumn>
	{
	}
	
	#endregion BbUkcrNregionlkpExpressionBuilder	

	#region BbUkcrNregionlkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbUkcrNregionlkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbUkcrNregionlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbUkcrNregionlkpProperty : ChildEntityProperty<BbUkcrNregionlkpChildEntityTypes>
	{
	}
	
	#endregion BbUkcrNregionlkpProperty
}

