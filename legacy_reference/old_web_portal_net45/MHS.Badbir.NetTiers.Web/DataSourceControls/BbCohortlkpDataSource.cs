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
	/// Represents the DataRepository.BbCohortlkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbCohortlkpDataSourceDesigner))]
	public class BbCohortlkpDataSource : ProviderDataSource<BbCohortlkp, BbCohortlkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCohortlkpDataSource class.
		/// </summary>
		public BbCohortlkpDataSource() : base(new BbCohortlkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbCohortlkpDataSourceView used by the BbCohortlkpDataSource.
		/// </summary>
		protected BbCohortlkpDataSourceView BbCohortlkpView
		{
			get { return ( View as BbCohortlkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbCohortlkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbCohortlkpSelectMethod SelectMethod
		{
			get
			{
				BbCohortlkpSelectMethod selectMethod = BbCohortlkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbCohortlkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbCohortlkpDataSourceView class that is to be
		/// used by the BbCohortlkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbCohortlkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbCohortlkp, BbCohortlkpKey> GetNewDataSourceView()
		{
			return new BbCohortlkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbCohortlkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbCohortlkpDataSourceView : ProviderDataSourceView<BbCohortlkp, BbCohortlkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCohortlkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbCohortlkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbCohortlkpDataSourceView(BbCohortlkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbCohortlkpDataSource BbCohortlkpOwner
		{
			get { return Owner as BbCohortlkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbCohortlkpSelectMethod SelectMethod
		{
			get { return BbCohortlkpOwner.SelectMethod; }
			set { BbCohortlkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbCohortlkpService BbCohortlkpProvider
		{
			get { return Provider as BbCohortlkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbCohortlkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbCohortlkp> results = null;
			BbCohortlkp item;
			count = 0;
			
			int _cohortid;

			switch ( SelectMethod )
			{
				case BbCohortlkpSelectMethod.Get:
					BbCohortlkpKey entityKey  = new BbCohortlkpKey();
					entityKey.Load(values);
					item = BbCohortlkpProvider.Get(entityKey);
					results = new TList<BbCohortlkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbCohortlkpSelectMethod.GetAll:
                    results = BbCohortlkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbCohortlkpSelectMethod.GetPaged:
					results = BbCohortlkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbCohortlkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbCohortlkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbCohortlkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbCohortlkpSelectMethod.GetByCohortid:
					_cohortid = ( values["Cohortid"] != null ) ? (int) EntityUtil.ChangeType(values["Cohortid"], typeof(int)) : (int)0;
					item = BbCohortlkpProvider.GetByCohortid(_cohortid);
					results = new TList<BbCohortlkp>();
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
			if ( SelectMethod == BbCohortlkpSelectMethod.Get || SelectMethod == BbCohortlkpSelectMethod.GetByCohortid )
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
				BbCohortlkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbCohortlkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbCohortlkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbCohortlkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbCohortlkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbCohortlkpDataSource class.
	/// </summary>
	public class BbCohortlkpDataSourceDesigner : ProviderDataSourceDesigner<BbCohortlkp, BbCohortlkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbCohortlkpDataSourceDesigner class.
		/// </summary>
		public BbCohortlkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbCohortlkpSelectMethod SelectMethod
		{
			get { return ((BbCohortlkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbCohortlkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbCohortlkpDataSourceActionList

	/// <summary>
	/// Supports the BbCohortlkpDataSourceDesigner class.
	/// </summary>
	internal class BbCohortlkpDataSourceActionList : DesignerActionList
	{
		private BbCohortlkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbCohortlkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbCohortlkpDataSourceActionList(BbCohortlkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbCohortlkpSelectMethod SelectMethod
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

	#endregion BbCohortlkpDataSourceActionList
	
	#endregion BbCohortlkpDataSourceDesigner
	
	#region BbCohortlkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbCohortlkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbCohortlkpSelectMethod
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
		/// Represents the GetByCohortid method.
		/// </summary>
		GetByCohortid
	}
	
	#endregion BbCohortlkpSelectMethod

	#region BbCohortlkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbCohortlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCohortlkpFilter : SqlFilter<BbCohortlkpColumn>
	{
	}
	
	#endregion BbCohortlkpFilter

	#region BbCohortlkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbCohortlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCohortlkpExpressionBuilder : SqlExpressionBuilder<BbCohortlkpColumn>
	{
	}
	
	#endregion BbCohortlkpExpressionBuilder	

	#region BbCohortlkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbCohortlkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbCohortlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCohortlkpProperty : ChildEntityProperty<BbCohortlkpChildEntityTypes>
	{
	}
	
	#endregion BbCohortlkpProperty
}

