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
	/// Represents the DataRepository.BbPappPatientLifestyleProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbPappPatientLifestyleDataSourceDesigner))]
	public class BbPappPatientLifestyleDataSource : ProviderDataSource<BbPappPatientLifestyle, BbPappPatientLifestyleKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientLifestyleDataSource class.
		/// </summary>
		public BbPappPatientLifestyleDataSource() : base(new BbPappPatientLifestyleService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbPappPatientLifestyleDataSourceView used by the BbPappPatientLifestyleDataSource.
		/// </summary>
		protected BbPappPatientLifestyleDataSourceView BbPappPatientLifestyleView
		{
			get { return ( View as BbPappPatientLifestyleDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbPappPatientLifestyleDataSource control invokes to retrieve data.
		/// </summary>
		public BbPappPatientLifestyleSelectMethod SelectMethod
		{
			get
			{
				BbPappPatientLifestyleSelectMethod selectMethod = BbPappPatientLifestyleSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbPappPatientLifestyleSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbPappPatientLifestyleDataSourceView class that is to be
		/// used by the BbPappPatientLifestyleDataSource.
		/// </summary>
		/// <returns>An instance of the BbPappPatientLifestyleDataSourceView class.</returns>
		protected override BaseDataSourceView<BbPappPatientLifestyle, BbPappPatientLifestyleKey> GetNewDataSourceView()
		{
			return new BbPappPatientLifestyleDataSourceView(this, DefaultViewName);
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
	/// Supports the BbPappPatientLifestyleDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbPappPatientLifestyleDataSourceView : ProviderDataSourceView<BbPappPatientLifestyle, BbPappPatientLifestyleKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientLifestyleDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbPappPatientLifestyleDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbPappPatientLifestyleDataSourceView(BbPappPatientLifestyleDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbPappPatientLifestyleDataSource BbPappPatientLifestyleOwner
		{
			get { return Owner as BbPappPatientLifestyleDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbPappPatientLifestyleSelectMethod SelectMethod
		{
			get { return BbPappPatientLifestyleOwner.SelectMethod; }
			set { BbPappPatientLifestyleOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbPappPatientLifestyleService BbPappPatientLifestyleProvider
		{
			get { return Provider as BbPappPatientLifestyleService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbPappPatientLifestyle> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbPappPatientLifestyle> results = null;
			BbPappPatientLifestyle item;
			count = 0;
			
			int _formId;
			System.Int32? _workstatusid_nullable;
			int _chid;

			switch ( SelectMethod )
			{
				case BbPappPatientLifestyleSelectMethod.Get:
					BbPappPatientLifestyleKey entityKey  = new BbPappPatientLifestyleKey();
					entityKey.Load(values);
					item = BbPappPatientLifestyleProvider.Get(entityKey);
					results = new TList<BbPappPatientLifestyle>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPappPatientLifestyleSelectMethod.GetAll:
                    results = BbPappPatientLifestyleProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbPappPatientLifestyleSelectMethod.GetPaged:
					results = BbPappPatientLifestyleProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbPappPatientLifestyleSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbPappPatientLifestyleProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbPappPatientLifestyleProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbPappPatientLifestyleSelectMethod.GetByFormId:
					_formId = ( values["FormId"] != null ) ? (int) EntityUtil.ChangeType(values["FormId"], typeof(int)) : (int)0;
					item = BbPappPatientLifestyleProvider.GetByFormId(_formId);
					results = new TList<BbPappPatientLifestyle>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case BbPappPatientLifestyleSelectMethod.GetByWorkstatusid:
					_workstatusid_nullable = (System.Int32?) EntityUtil.ChangeType(values["Workstatusid"], typeof(System.Int32?));
					results = BbPappPatientLifestyleProvider.GetByWorkstatusid(_workstatusid_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbPappPatientLifestyleSelectMethod.GetByChid:
					_chid = ( values["Chid"] != null ) ? (int) EntityUtil.ChangeType(values["Chid"], typeof(int)) : (int)0;
					results = BbPappPatientLifestyleProvider.GetByChid(_chid, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == BbPappPatientLifestyleSelectMethod.Get || SelectMethod == BbPappPatientLifestyleSelectMethod.GetByFormId )
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
				BbPappPatientLifestyle entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbPappPatientLifestyleProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbPappPatientLifestyle> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbPappPatientLifestyleProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbPappPatientLifestyleDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbPappPatientLifestyleDataSource class.
	/// </summary>
	public class BbPappPatientLifestyleDataSourceDesigner : ProviderDataSourceDesigner<BbPappPatientLifestyle, BbPappPatientLifestyleKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbPappPatientLifestyleDataSourceDesigner class.
		/// </summary>
		public BbPappPatientLifestyleDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPappPatientLifestyleSelectMethod SelectMethod
		{
			get { return ((BbPappPatientLifestyleDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbPappPatientLifestyleDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbPappPatientLifestyleDataSourceActionList

	/// <summary>
	/// Supports the BbPappPatientLifestyleDataSourceDesigner class.
	/// </summary>
	internal class BbPappPatientLifestyleDataSourceActionList : DesignerActionList
	{
		private BbPappPatientLifestyleDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbPappPatientLifestyleDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbPappPatientLifestyleDataSourceActionList(BbPappPatientLifestyleDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPappPatientLifestyleSelectMethod SelectMethod
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

	#endregion BbPappPatientLifestyleDataSourceActionList
	
	#endregion BbPappPatientLifestyleDataSourceDesigner
	
	#region BbPappPatientLifestyleSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbPappPatientLifestyleDataSource.SelectMethod property.
	/// </summary>
	public enum BbPappPatientLifestyleSelectMethod
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
		/// Represents the GetByFormId method.
		/// </summary>
		GetByFormId,
		/// <summary>
		/// Represents the GetByWorkstatusid method.
		/// </summary>
		GetByWorkstatusid,
		/// <summary>
		/// Represents the GetByChid method.
		/// </summary>
		GetByChid
	}
	
	#endregion BbPappPatientLifestyleSelectMethod

	#region BbPappPatientLifestyleFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientLifestyle"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientLifestyleFilter : SqlFilter<BbPappPatientLifestyleColumn>
	{
	}
	
	#endregion BbPappPatientLifestyleFilter

	#region BbPappPatientLifestyleExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientLifestyle"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientLifestyleExpressionBuilder : SqlExpressionBuilder<BbPappPatientLifestyleColumn>
	{
	}
	
	#endregion BbPappPatientLifestyleExpressionBuilder	

	#region BbPappPatientLifestyleProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbPappPatientLifestyleChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientLifestyle"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientLifestyleProperty : ChildEntityProperty<BbPappPatientLifestyleChildEntityTypes>
	{
	}
	
	#endregion BbPappPatientLifestyleProperty
}

