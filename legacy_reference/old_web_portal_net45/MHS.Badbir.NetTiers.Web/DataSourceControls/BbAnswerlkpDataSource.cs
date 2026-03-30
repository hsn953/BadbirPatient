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
	/// Represents the DataRepository.BbAnswerlkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbAnswerlkpDataSourceDesigner))]
	public class BbAnswerlkpDataSource : ProviderDataSource<BbAnswerlkp, BbAnswerlkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbAnswerlkpDataSource class.
		/// </summary>
		public BbAnswerlkpDataSource() : base(new BbAnswerlkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbAnswerlkpDataSourceView used by the BbAnswerlkpDataSource.
		/// </summary>
		protected BbAnswerlkpDataSourceView BbAnswerlkpView
		{
			get { return ( View as BbAnswerlkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbAnswerlkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbAnswerlkpSelectMethod SelectMethod
		{
			get
			{
				BbAnswerlkpSelectMethod selectMethod = BbAnswerlkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbAnswerlkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbAnswerlkpDataSourceView class that is to be
		/// used by the BbAnswerlkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbAnswerlkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbAnswerlkp, BbAnswerlkpKey> GetNewDataSourceView()
		{
			return new BbAnswerlkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbAnswerlkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbAnswerlkpDataSourceView : ProviderDataSourceView<BbAnswerlkp, BbAnswerlkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbAnswerlkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbAnswerlkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbAnswerlkpDataSourceView(BbAnswerlkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbAnswerlkpDataSource BbAnswerlkpOwner
		{
			get { return Owner as BbAnswerlkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbAnswerlkpSelectMethod SelectMethod
		{
			get { return BbAnswerlkpOwner.SelectMethod; }
			set { BbAnswerlkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbAnswerlkpService BbAnswerlkpProvider
		{
			get { return Provider as BbAnswerlkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbAnswerlkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbAnswerlkp> results = null;
			BbAnswerlkp item;
			count = 0;
			
			int _answerid;

			switch ( SelectMethod )
			{
				case BbAnswerlkpSelectMethod.Get:
					BbAnswerlkpKey entityKey  = new BbAnswerlkpKey();
					entityKey.Load(values);
					item = BbAnswerlkpProvider.Get(entityKey);
					results = new TList<BbAnswerlkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbAnswerlkpSelectMethod.GetAll:
                    results = BbAnswerlkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbAnswerlkpSelectMethod.GetPaged:
					results = BbAnswerlkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbAnswerlkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbAnswerlkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbAnswerlkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbAnswerlkpSelectMethod.GetByAnswerid:
					_answerid = ( values["Answerid"] != null ) ? (int) EntityUtil.ChangeType(values["Answerid"], typeof(int)) : (int)0;
					item = BbAnswerlkpProvider.GetByAnswerid(_answerid);
					results = new TList<BbAnswerlkp>();
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
			if ( SelectMethod == BbAnswerlkpSelectMethod.Get || SelectMethod == BbAnswerlkpSelectMethod.GetByAnswerid )
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
				BbAnswerlkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbAnswerlkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbAnswerlkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbAnswerlkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbAnswerlkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbAnswerlkpDataSource class.
	/// </summary>
	public class BbAnswerlkpDataSourceDesigner : ProviderDataSourceDesigner<BbAnswerlkp, BbAnswerlkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbAnswerlkpDataSourceDesigner class.
		/// </summary>
		public BbAnswerlkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbAnswerlkpSelectMethod SelectMethod
		{
			get { return ((BbAnswerlkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbAnswerlkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbAnswerlkpDataSourceActionList

	/// <summary>
	/// Supports the BbAnswerlkpDataSourceDesigner class.
	/// </summary>
	internal class BbAnswerlkpDataSourceActionList : DesignerActionList
	{
		private BbAnswerlkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbAnswerlkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbAnswerlkpDataSourceActionList(BbAnswerlkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbAnswerlkpSelectMethod SelectMethod
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

	#endregion BbAnswerlkpDataSourceActionList
	
	#endregion BbAnswerlkpDataSourceDesigner
	
	#region BbAnswerlkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbAnswerlkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbAnswerlkpSelectMethod
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
		/// Represents the GetByAnswerid method.
		/// </summary>
		GetByAnswerid
	}
	
	#endregion BbAnswerlkpSelectMethod

	#region BbAnswerlkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbAnswerlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbAnswerlkpFilter : SqlFilter<BbAnswerlkpColumn>
	{
	}
	
	#endregion BbAnswerlkpFilter

	#region BbAnswerlkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbAnswerlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbAnswerlkpExpressionBuilder : SqlExpressionBuilder<BbAnswerlkpColumn>
	{
	}
	
	#endregion BbAnswerlkpExpressionBuilder	

	#region BbAnswerlkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbAnswerlkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbAnswerlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbAnswerlkpProperty : ChildEntityProperty<BbAnswerlkpChildEntityTypes>
	{
	}
	
	#endregion BbAnswerlkpProperty
}

