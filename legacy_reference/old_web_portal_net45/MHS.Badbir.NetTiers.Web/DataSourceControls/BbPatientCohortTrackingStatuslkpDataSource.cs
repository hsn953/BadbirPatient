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
	/// Represents the DataRepository.BbPatientCohortTrackingStatuslkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbPatientCohortTrackingStatuslkpDataSourceDesigner))]
	public class BbPatientCohortTrackingStatuslkpDataSource : ProviderDataSource<BbPatientCohortTrackingStatuslkp, BbPatientCohortTrackingStatuslkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingStatuslkpDataSource class.
		/// </summary>
		public BbPatientCohortTrackingStatuslkpDataSource() : base(new BbPatientCohortTrackingStatuslkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbPatientCohortTrackingStatuslkpDataSourceView used by the BbPatientCohortTrackingStatuslkpDataSource.
		/// </summary>
		protected BbPatientCohortTrackingStatuslkpDataSourceView BbPatientCohortTrackingStatuslkpView
		{
			get { return ( View as BbPatientCohortTrackingStatuslkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbPatientCohortTrackingStatuslkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbPatientCohortTrackingStatuslkpSelectMethod SelectMethod
		{
			get
			{
				BbPatientCohortTrackingStatuslkpSelectMethod selectMethod = BbPatientCohortTrackingStatuslkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbPatientCohortTrackingStatuslkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbPatientCohortTrackingStatuslkpDataSourceView class that is to be
		/// used by the BbPatientCohortTrackingStatuslkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbPatientCohortTrackingStatuslkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbPatientCohortTrackingStatuslkp, BbPatientCohortTrackingStatuslkpKey> GetNewDataSourceView()
		{
			return new BbPatientCohortTrackingStatuslkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbPatientCohortTrackingStatuslkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbPatientCohortTrackingStatuslkpDataSourceView : ProviderDataSourceView<BbPatientCohortTrackingStatuslkp, BbPatientCohortTrackingStatuslkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingStatuslkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbPatientCohortTrackingStatuslkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbPatientCohortTrackingStatuslkpDataSourceView(BbPatientCohortTrackingStatuslkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbPatientCohortTrackingStatuslkpDataSource BbPatientCohortTrackingStatuslkpOwner
		{
			get { return Owner as BbPatientCohortTrackingStatuslkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbPatientCohortTrackingStatuslkpSelectMethod SelectMethod
		{
			get { return BbPatientCohortTrackingStatuslkpOwner.SelectMethod; }
			set { BbPatientCohortTrackingStatuslkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbPatientCohortTrackingStatuslkpService BbPatientCohortTrackingStatuslkpProvider
		{
			get { return Provider as BbPatientCohortTrackingStatuslkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbPatientCohortTrackingStatuslkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbPatientCohortTrackingStatuslkp> results = null;
			BbPatientCohortTrackingStatuslkp item;
			count = 0;
			
			int _ptrackingstatusid;

			switch ( SelectMethod )
			{
				case BbPatientCohortTrackingStatuslkpSelectMethod.Get:
					BbPatientCohortTrackingStatuslkpKey entityKey  = new BbPatientCohortTrackingStatuslkpKey();
					entityKey.Load(values);
					item = BbPatientCohortTrackingStatuslkpProvider.Get(entityKey);
					results = new TList<BbPatientCohortTrackingStatuslkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPatientCohortTrackingStatuslkpSelectMethod.GetAll:
                    results = BbPatientCohortTrackingStatuslkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbPatientCohortTrackingStatuslkpSelectMethod.GetPaged:
					results = BbPatientCohortTrackingStatuslkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbPatientCohortTrackingStatuslkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbPatientCohortTrackingStatuslkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbPatientCohortTrackingStatuslkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbPatientCohortTrackingStatuslkpSelectMethod.GetByPtrackingstatusid:
					_ptrackingstatusid = ( values["Ptrackingstatusid"] != null ) ? (int) EntityUtil.ChangeType(values["Ptrackingstatusid"], typeof(int)) : (int)0;
					item = BbPatientCohortTrackingStatuslkpProvider.GetByPtrackingstatusid(_ptrackingstatusid);
					results = new TList<BbPatientCohortTrackingStatuslkp>();
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
			if ( SelectMethod == BbPatientCohortTrackingStatuslkpSelectMethod.Get || SelectMethod == BbPatientCohortTrackingStatuslkpSelectMethod.GetByPtrackingstatusid )
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
				BbPatientCohortTrackingStatuslkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbPatientCohortTrackingStatuslkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbPatientCohortTrackingStatuslkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbPatientCohortTrackingStatuslkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbPatientCohortTrackingStatuslkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbPatientCohortTrackingStatuslkpDataSource class.
	/// </summary>
	public class BbPatientCohortTrackingStatuslkpDataSourceDesigner : ProviderDataSourceDesigner<BbPatientCohortTrackingStatuslkp, BbPatientCohortTrackingStatuslkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingStatuslkpDataSourceDesigner class.
		/// </summary>
		public BbPatientCohortTrackingStatuslkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientCohortTrackingStatuslkpSelectMethod SelectMethod
		{
			get { return ((BbPatientCohortTrackingStatuslkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbPatientCohortTrackingStatuslkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbPatientCohortTrackingStatuslkpDataSourceActionList

	/// <summary>
	/// Supports the BbPatientCohortTrackingStatuslkpDataSourceDesigner class.
	/// </summary>
	internal class BbPatientCohortTrackingStatuslkpDataSourceActionList : DesignerActionList
	{
		private BbPatientCohortTrackingStatuslkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingStatuslkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbPatientCohortTrackingStatuslkpDataSourceActionList(BbPatientCohortTrackingStatuslkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientCohortTrackingStatuslkpSelectMethod SelectMethod
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

	#endregion BbPatientCohortTrackingStatuslkpDataSourceActionList
	
	#endregion BbPatientCohortTrackingStatuslkpDataSourceDesigner
	
	#region BbPatientCohortTrackingStatuslkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbPatientCohortTrackingStatuslkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbPatientCohortTrackingStatuslkpSelectMethod
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
		/// Represents the GetByPtrackingstatusid method.
		/// </summary>
		GetByPtrackingstatusid
	}
	
	#endregion BbPatientCohortTrackingStatuslkpSelectMethod

	#region BbPatientCohortTrackingStatuslkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortTrackingStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortTrackingStatuslkpFilter : SqlFilter<BbPatientCohortTrackingStatuslkpColumn>
	{
	}
	
	#endregion BbPatientCohortTrackingStatuslkpFilter

	#region BbPatientCohortTrackingStatuslkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortTrackingStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortTrackingStatuslkpExpressionBuilder : SqlExpressionBuilder<BbPatientCohortTrackingStatuslkpColumn>
	{
	}
	
	#endregion BbPatientCohortTrackingStatuslkpExpressionBuilder	

	#region BbPatientCohortTrackingStatuslkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbPatientCohortTrackingStatuslkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortTrackingStatuslkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortTrackingStatuslkpProperty : ChildEntityProperty<BbPatientCohortTrackingStatuslkpChildEntityTypes>
	{
	}
	
	#endregion BbPatientCohortTrackingStatuslkpProperty
}

