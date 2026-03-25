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
	/// Represents the DataRepository.BbPatientLifestyleProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbPatientLifestyleDataSourceDesigner))]
	public class BbPatientLifestyleDataSource : ProviderDataSource<BbPatientLifestyle, BbPatientLifestyleKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientLifestyleDataSource class.
		/// </summary>
		public BbPatientLifestyleDataSource() : base(new BbPatientLifestyleService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbPatientLifestyleDataSourceView used by the BbPatientLifestyleDataSource.
		/// </summary>
		protected BbPatientLifestyleDataSourceView BbPatientLifestyleView
		{
			get { return ( View as BbPatientLifestyleDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbPatientLifestyleDataSource control invokes to retrieve data.
		/// </summary>
		public BbPatientLifestyleSelectMethod SelectMethod
		{
			get
			{
				BbPatientLifestyleSelectMethod selectMethod = BbPatientLifestyleSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbPatientLifestyleSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbPatientLifestyleDataSourceView class that is to be
		/// used by the BbPatientLifestyleDataSource.
		/// </summary>
		/// <returns>An instance of the BbPatientLifestyleDataSourceView class.</returns>
		protected override BaseDataSourceView<BbPatientLifestyle, BbPatientLifestyleKey> GetNewDataSourceView()
		{
			return new BbPatientLifestyleDataSourceView(this, DefaultViewName);
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
	/// Supports the BbPatientLifestyleDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbPatientLifestyleDataSourceView : ProviderDataSourceView<BbPatientLifestyle, BbPatientLifestyleKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientLifestyleDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbPatientLifestyleDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbPatientLifestyleDataSourceView(BbPatientLifestyleDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbPatientLifestyleDataSource BbPatientLifestyleOwner
		{
			get { return Owner as BbPatientLifestyleDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbPatientLifestyleSelectMethod SelectMethod
		{
			get { return BbPatientLifestyleOwner.SelectMethod; }
			set { BbPatientLifestyleOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbPatientLifestyleService BbPatientLifestyleProvider
		{
			get { return Provider as BbPatientLifestyleService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbPatientLifestyle> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbPatientLifestyle> results = null;
			BbPatientLifestyle item;
			count = 0;
			
			System.Int32 _fupId;
			System.Int32? _ethnicityid_nullable;
			System.Int32? _workstatusid_nullable;

			switch ( SelectMethod )
			{
				case BbPatientLifestyleSelectMethod.Get:
					BbPatientLifestyleKey entityKey  = new BbPatientLifestyleKey();
					entityKey.Load(values);
					item = BbPatientLifestyleProvider.Get(entityKey);
					results = new TList<BbPatientLifestyle>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPatientLifestyleSelectMethod.GetAll:
                    results = BbPatientLifestyleProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbPatientLifestyleSelectMethod.GetPaged:
					results = BbPatientLifestyleProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbPatientLifestyleSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbPatientLifestyleProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbPatientLifestyleProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbPatientLifestyleSelectMethod.GetByFupId:
					_fupId = ( values["FupId"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["FupId"], typeof(System.Int32)) : (int)0;
					item = BbPatientLifestyleProvider.GetByFupId(_fupId);
					results = new TList<BbPatientLifestyle>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case BbPatientLifestyleSelectMethod.GetByEthnicityid:
					_ethnicityid_nullable = (System.Int32?) EntityUtil.ChangeType(values["Ethnicityid"], typeof(System.Int32?));
					results = BbPatientLifestyleProvider.GetByEthnicityid(_ethnicityid_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbPatientLifestyleSelectMethod.GetByWorkstatusid:
					_workstatusid_nullable = (System.Int32?) EntityUtil.ChangeType(values["Workstatusid"], typeof(System.Int32?));
					results = BbPatientLifestyleProvider.GetByWorkstatusid(_workstatusid_nullable, this.StartIndex, this.PageSize, out count);
					break;
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
			if ( SelectMethod == BbPatientLifestyleSelectMethod.Get || SelectMethod == BbPatientLifestyleSelectMethod.GetByFupId )
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
				BbPatientLifestyle entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbPatientLifestyleProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbPatientLifestyle> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbPatientLifestyleProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbPatientLifestyleDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbPatientLifestyleDataSource class.
	/// </summary>
	public class BbPatientLifestyleDataSourceDesigner : ProviderDataSourceDesigner<BbPatientLifestyle, BbPatientLifestyleKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbPatientLifestyleDataSourceDesigner class.
		/// </summary>
		public BbPatientLifestyleDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientLifestyleSelectMethod SelectMethod
		{
			get { return ((BbPatientLifestyleDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbPatientLifestyleDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbPatientLifestyleDataSourceActionList

	/// <summary>
	/// Supports the BbPatientLifestyleDataSourceDesigner class.
	/// </summary>
	internal class BbPatientLifestyleDataSourceActionList : DesignerActionList
	{
		private BbPatientLifestyleDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbPatientLifestyleDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbPatientLifestyleDataSourceActionList(BbPatientLifestyleDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientLifestyleSelectMethod SelectMethod
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

	#endregion BbPatientLifestyleDataSourceActionList
	
	#endregion BbPatientLifestyleDataSourceDesigner
	
	#region BbPatientLifestyleSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbPatientLifestyleDataSource.SelectMethod property.
	/// </summary>
	public enum BbPatientLifestyleSelectMethod
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
		/// Represents the GetByFupId method.
		/// </summary>
		GetByFupId,
		/// <summary>
		/// Represents the GetByEthnicityid method.
		/// </summary>
		GetByEthnicityid,
		/// <summary>
		/// Represents the GetByWorkstatusid method.
		/// </summary>
		GetByWorkstatusid
	}
	
	#endregion BbPatientLifestyleSelectMethod

	#region BbPatientLifestyleFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientLifestyle"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientLifestyleFilter : SqlFilter<BbPatientLifestyleColumn>
	{
	}
	
	#endregion BbPatientLifestyleFilter

	#region BbPatientLifestyleExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientLifestyle"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientLifestyleExpressionBuilder : SqlExpressionBuilder<BbPatientLifestyleColumn>
	{
	}
	
	#endregion BbPatientLifestyleExpressionBuilder	

	#region BbPatientLifestyleProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbPatientLifestyleChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientLifestyle"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientLifestyleProperty : ChildEntityProperty<BbPatientLifestyleChildEntityTypes>
	{
	}
	
	#endregion BbPatientLifestyleProperty
}

