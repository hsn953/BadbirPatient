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
	/// Represents the DataRepository.BbPatientStatuslkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbPatientStatuslkpDataSourceDesigner))]
	public class BbPatientStatuslkpDataSource : ProviderDataSource<BbPatientStatuslkp, BbPatientStatuslkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientStatuslkpDataSource class.
		/// </summary>
		public BbPatientStatuslkpDataSource() : base(new BbPatientStatuslkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbPatientStatuslkpDataSourceView used by the BbPatientStatuslkpDataSource.
		/// </summary>
		protected BbPatientStatuslkpDataSourceView BbPatientStatuslkpView
		{
			get { return ( View as BbPatientStatuslkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbPatientStatuslkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbPatientStatuslkpSelectMethod SelectMethod
		{
			get
			{
				BbPatientStatuslkpSelectMethod selectMethod = BbPatientStatuslkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbPatientStatuslkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbPatientStatuslkpDataSourceView class that is to be
		/// used by the BbPatientStatuslkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbPatientStatuslkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbPatientStatuslkp, BbPatientStatuslkpKey> GetNewDataSourceView()
		{
			return new BbPatientStatuslkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbPatientStatuslkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbPatientStatuslkpDataSourceView : ProviderDataSourceView<BbPatientStatuslkp, BbPatientStatuslkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientStatuslkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbPatientStatuslkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbPatientStatuslkpDataSourceView(BbPatientStatuslkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbPatientStatuslkpDataSource BbPatientStatuslkpOwner
		{
			get { return Owner as BbPatientStatuslkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbPatientStatuslkpSelectMethod SelectMethod
		{
			get { return BbPatientStatuslkpOwner.SelectMethod; }
			set { BbPatientStatuslkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbPatientStatuslkpService BbPatientStatuslkpProvider
		{
			get { return Provider as BbPatientStatuslkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbPatientStatuslkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbPatientStatuslkp> results = null;
			BbPatientStatuslkp item;
			count = 0;
			
			System.Int32 _pstatusid;

			switch ( SelectMethod )
			{
				case BbPatientStatuslkpSelectMethod.Get:
					BbPatientStatuslkpKey entityKey  = new BbPatientStatuslkpKey();
					entityKey.Load(values);
					item = BbPatientStatuslkpProvider.Get(entityKey);
					results = new TList<BbPatientStatuslkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPatientStatuslkpSelectMethod.GetAll:
                    results = BbPatientStatuslkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbPatientStatuslkpSelectMethod.GetPaged:
					results = BbPatientStatuslkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbPatientStatuslkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbPatientStatuslkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbPatientStatuslkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbPatientStatuslkpSelectMethod.GetByPstatusid:
					_pstatusid = ( values["Pstatusid"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Pstatusid"], typeof(System.Int32)) : (int)0;
					item = BbPatientStatuslkpProvider.GetByPstatusid(_pstatusid);
					results = new TList<BbPatientStatuslkp>();
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
			if ( SelectMethod == BbPatientStatuslkpSelectMethod.Get || SelectMethod == BbPatientStatuslkpSelectMethod.GetByPstatusid )
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
				BbPatientStatuslkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbPatientStatuslkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbPatientStatuslkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbPatientStatuslkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbPatientStatuslkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbPatientStatuslkpDataSource class.
	/// </summary>
	public class BbPatientStatuslkpDataSourceDesigner : ProviderDataSourceDesigner<BbPatientStatuslkp, BbPatientStatuslkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbPatientStatuslkpDataSourceDesigner class.
		/// </summary>
		public BbPatientStatuslkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientStatuslkpSelectMethod SelectMethod
		{
			get { return ((BbPatientStatuslkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbPatientStatuslkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbPatientStatuslkpDataSourceActionList

	/// <summary>
	/// Supports the BbPatientStatuslkpDataSourceDesigner class.
	/// </summary>
	internal class BbPatientStatuslkpDataSourceActionList : DesignerActionList
	{
		private BbPatientStatuslkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbPatientStatuslkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbPatientStatuslkpDataSourceActionList(BbPatientStatuslkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientStatuslkpSelectMethod SelectMethod
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

	#endregion BbPatientStatuslkpDataSourceActionList
	
	#endregion BbPatientStatuslkpDataSourceDesigner
	
	#region BbPatientStatuslkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbPatientStatuslkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbPatientStatuslkpSelectMethod
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
		/// Represents the GetByPstatusid method.
		/// </summary>
		GetByPstatusid
	}
	
	#endregion BbPatientStatuslkpSelectMethod

	#region BbPatientStatuslkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientStatuslkpFilter : SqlFilter<BbPatientStatuslkpColumn>
	{
	}
	
	#endregion BbPatientStatuslkpFilter

	#region BbPatientStatuslkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientStatuslkpExpressionBuilder : SqlExpressionBuilder<BbPatientStatuslkpColumn>
	{
	}
	
	#endregion BbPatientStatuslkpExpressionBuilder	

	#region BbPatientStatuslkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbPatientStatuslkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientStatuslkpProperty : ChildEntityProperty<BbPatientStatuslkpChildEntityTypes>
	{
	}
	
	#endregion BbPatientStatuslkpProperty
}

