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
	/// Represents the DataRepository.BbPatientStatusDetaillkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbPatientStatusDetaillkpDataSourceDesigner))]
	public class BbPatientStatusDetaillkpDataSource : ProviderDataSource<BbPatientStatusDetaillkp, BbPatientStatusDetaillkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientStatusDetaillkpDataSource class.
		/// </summary>
		public BbPatientStatusDetaillkpDataSource() : base(new BbPatientStatusDetaillkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbPatientStatusDetaillkpDataSourceView used by the BbPatientStatusDetaillkpDataSource.
		/// </summary>
		protected BbPatientStatusDetaillkpDataSourceView BbPatientStatusDetaillkpView
		{
			get { return ( View as BbPatientStatusDetaillkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbPatientStatusDetaillkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbPatientStatusDetaillkpSelectMethod SelectMethod
		{
			get
			{
				BbPatientStatusDetaillkpSelectMethod selectMethod = BbPatientStatusDetaillkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbPatientStatusDetaillkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbPatientStatusDetaillkpDataSourceView class that is to be
		/// used by the BbPatientStatusDetaillkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbPatientStatusDetaillkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbPatientStatusDetaillkp, BbPatientStatusDetaillkpKey> GetNewDataSourceView()
		{
			return new BbPatientStatusDetaillkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbPatientStatusDetaillkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbPatientStatusDetaillkpDataSourceView : ProviderDataSourceView<BbPatientStatusDetaillkp, BbPatientStatusDetaillkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientStatusDetaillkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbPatientStatusDetaillkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbPatientStatusDetaillkpDataSourceView(BbPatientStatusDetaillkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbPatientStatusDetaillkpDataSource BbPatientStatusDetaillkpOwner
		{
			get { return Owner as BbPatientStatusDetaillkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbPatientStatusDetaillkpSelectMethod SelectMethod
		{
			get { return BbPatientStatusDetaillkpOwner.SelectMethod; }
			set { BbPatientStatusDetaillkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbPatientStatusDetaillkpService BbPatientStatusDetaillkpProvider
		{
			get { return Provider as BbPatientStatusDetaillkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbPatientStatusDetaillkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbPatientStatusDetaillkp> results = null;
			BbPatientStatusDetaillkp item;
			count = 0;
			
			System.Int32 _pstatusdetailid;

			switch ( SelectMethod )
			{
				case BbPatientStatusDetaillkpSelectMethod.Get:
					BbPatientStatusDetaillkpKey entityKey  = new BbPatientStatusDetaillkpKey();
					entityKey.Load(values);
					item = BbPatientStatusDetaillkpProvider.Get(entityKey);
					results = new TList<BbPatientStatusDetaillkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPatientStatusDetaillkpSelectMethod.GetAll:
                    results = BbPatientStatusDetaillkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbPatientStatusDetaillkpSelectMethod.GetPaged:
					results = BbPatientStatusDetaillkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbPatientStatusDetaillkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbPatientStatusDetaillkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbPatientStatusDetaillkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbPatientStatusDetaillkpSelectMethod.GetByPstatusdetailid:
					_pstatusdetailid = ( values["Pstatusdetailid"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Pstatusdetailid"], typeof(System.Int32)) : (int)0;
					item = BbPatientStatusDetaillkpProvider.GetByPstatusdetailid(_pstatusdetailid);
					results = new TList<BbPatientStatusDetaillkp>();
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
			if ( SelectMethod == BbPatientStatusDetaillkpSelectMethod.Get || SelectMethod == BbPatientStatusDetaillkpSelectMethod.GetByPstatusdetailid )
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
				BbPatientStatusDetaillkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbPatientStatusDetaillkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbPatientStatusDetaillkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbPatientStatusDetaillkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbPatientStatusDetaillkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbPatientStatusDetaillkpDataSource class.
	/// </summary>
	public class BbPatientStatusDetaillkpDataSourceDesigner : ProviderDataSourceDesigner<BbPatientStatusDetaillkp, BbPatientStatusDetaillkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbPatientStatusDetaillkpDataSourceDesigner class.
		/// </summary>
		public BbPatientStatusDetaillkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientStatusDetaillkpSelectMethod SelectMethod
		{
			get { return ((BbPatientStatusDetaillkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbPatientStatusDetaillkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbPatientStatusDetaillkpDataSourceActionList

	/// <summary>
	/// Supports the BbPatientStatusDetaillkpDataSourceDesigner class.
	/// </summary>
	internal class BbPatientStatusDetaillkpDataSourceActionList : DesignerActionList
	{
		private BbPatientStatusDetaillkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbPatientStatusDetaillkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbPatientStatusDetaillkpDataSourceActionList(BbPatientStatusDetaillkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientStatusDetaillkpSelectMethod SelectMethod
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

	#endregion BbPatientStatusDetaillkpDataSourceActionList
	
	#endregion BbPatientStatusDetaillkpDataSourceDesigner
	
	#region BbPatientStatusDetaillkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbPatientStatusDetaillkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbPatientStatusDetaillkpSelectMethod
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
		/// Represents the GetByPstatusdetailid method.
		/// </summary>
		GetByPstatusdetailid
	}
	
	#endregion BbPatientStatusDetaillkpSelectMethod

	#region BbPatientStatusDetaillkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientStatusDetaillkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientStatusDetaillkpFilter : SqlFilter<BbPatientStatusDetaillkpColumn>
	{
	}
	
	#endregion BbPatientStatusDetaillkpFilter

	#region BbPatientStatusDetaillkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientStatusDetaillkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientStatusDetaillkpExpressionBuilder : SqlExpressionBuilder<BbPatientStatusDetaillkpColumn>
	{
	}
	
	#endregion BbPatientStatusDetaillkpExpressionBuilder	

	#region BbPatientStatusDetaillkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbPatientStatusDetaillkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientStatusDetaillkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientStatusDetaillkpProperty : ChildEntityProperty<BbPatientStatusDetaillkpChildEntityTypes>
	{
	}
	
	#endregion BbPatientStatusDetaillkpProperty
}

