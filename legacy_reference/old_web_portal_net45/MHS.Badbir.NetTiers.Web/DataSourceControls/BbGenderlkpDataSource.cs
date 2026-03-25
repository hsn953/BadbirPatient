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
	/// Represents the DataRepository.BbGenderlkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbGenderlkpDataSourceDesigner))]
	public class BbGenderlkpDataSource : ProviderDataSource<BbGenderlkp, BbGenderlkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbGenderlkpDataSource class.
		/// </summary>
		public BbGenderlkpDataSource() : base(new BbGenderlkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbGenderlkpDataSourceView used by the BbGenderlkpDataSource.
		/// </summary>
		protected BbGenderlkpDataSourceView BbGenderlkpView
		{
			get { return ( View as BbGenderlkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbGenderlkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbGenderlkpSelectMethod SelectMethod
		{
			get
			{
				BbGenderlkpSelectMethod selectMethod = BbGenderlkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbGenderlkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbGenderlkpDataSourceView class that is to be
		/// used by the BbGenderlkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbGenderlkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbGenderlkp, BbGenderlkpKey> GetNewDataSourceView()
		{
			return new BbGenderlkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbGenderlkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbGenderlkpDataSourceView : ProviderDataSourceView<BbGenderlkp, BbGenderlkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbGenderlkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbGenderlkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbGenderlkpDataSourceView(BbGenderlkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbGenderlkpDataSource BbGenderlkpOwner
		{
			get { return Owner as BbGenderlkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbGenderlkpSelectMethod SelectMethod
		{
			get { return BbGenderlkpOwner.SelectMethod; }
			set { BbGenderlkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbGenderlkpService BbGenderlkpProvider
		{
			get { return Provider as BbGenderlkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbGenderlkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbGenderlkp> results = null;
			BbGenderlkp item;
			count = 0;
			
			int _genderid;

			switch ( SelectMethod )
			{
				case BbGenderlkpSelectMethod.Get:
					BbGenderlkpKey entityKey  = new BbGenderlkpKey();
					entityKey.Load(values);
					item = BbGenderlkpProvider.Get(entityKey);
					results = new TList<BbGenderlkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbGenderlkpSelectMethod.GetAll:
                    results = BbGenderlkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbGenderlkpSelectMethod.GetPaged:
					results = BbGenderlkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbGenderlkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbGenderlkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbGenderlkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbGenderlkpSelectMethod.GetByGenderid:
					_genderid = ( values["Genderid"] != null ) ? (int) EntityUtil.ChangeType(values["Genderid"], typeof(int)) : (int)0;
					item = BbGenderlkpProvider.GetByGenderid(_genderid);
					results = new TList<BbGenderlkp>();
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
			if ( SelectMethod == BbGenderlkpSelectMethod.Get || SelectMethod == BbGenderlkpSelectMethod.GetByGenderid )
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
				BbGenderlkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbGenderlkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbGenderlkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbGenderlkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbGenderlkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbGenderlkpDataSource class.
	/// </summary>
	public class BbGenderlkpDataSourceDesigner : ProviderDataSourceDesigner<BbGenderlkp, BbGenderlkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbGenderlkpDataSourceDesigner class.
		/// </summary>
		public BbGenderlkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbGenderlkpSelectMethod SelectMethod
		{
			get { return ((BbGenderlkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbGenderlkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbGenderlkpDataSourceActionList

	/// <summary>
	/// Supports the BbGenderlkpDataSourceDesigner class.
	/// </summary>
	internal class BbGenderlkpDataSourceActionList : DesignerActionList
	{
		private BbGenderlkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbGenderlkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbGenderlkpDataSourceActionList(BbGenderlkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbGenderlkpSelectMethod SelectMethod
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

	#endregion BbGenderlkpDataSourceActionList
	
	#endregion BbGenderlkpDataSourceDesigner
	
	#region BbGenderlkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbGenderlkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbGenderlkpSelectMethod
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
		/// Represents the GetByGenderid method.
		/// </summary>
		GetByGenderid
	}
	
	#endregion BbGenderlkpSelectMethod

	#region BbGenderlkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbGenderlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbGenderlkpFilter : SqlFilter<BbGenderlkpColumn>
	{
	}
	
	#endregion BbGenderlkpFilter

	#region BbGenderlkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbGenderlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbGenderlkpExpressionBuilder : SqlExpressionBuilder<BbGenderlkpColumn>
	{
	}
	
	#endregion BbGenderlkpExpressionBuilder	

	#region BbGenderlkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbGenderlkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbGenderlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbGenderlkpProperty : ChildEntityProperty<BbGenderlkpChildEntityTypes>
	{
	}
	
	#endregion BbGenderlkpProperty
}

