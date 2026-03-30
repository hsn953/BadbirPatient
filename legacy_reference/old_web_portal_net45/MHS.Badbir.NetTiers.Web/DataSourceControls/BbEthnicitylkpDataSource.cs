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
	/// Represents the DataRepository.BbEthnicitylkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbEthnicitylkpDataSourceDesigner))]
	public class BbEthnicitylkpDataSource : ProviderDataSource<BbEthnicitylkp, BbEthnicitylkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbEthnicitylkpDataSource class.
		/// </summary>
		public BbEthnicitylkpDataSource() : base(new BbEthnicitylkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbEthnicitylkpDataSourceView used by the BbEthnicitylkpDataSource.
		/// </summary>
		protected BbEthnicitylkpDataSourceView BbEthnicitylkpView
		{
			get { return ( View as BbEthnicitylkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbEthnicitylkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbEthnicitylkpSelectMethod SelectMethod
		{
			get
			{
				BbEthnicitylkpSelectMethod selectMethod = BbEthnicitylkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbEthnicitylkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbEthnicitylkpDataSourceView class that is to be
		/// used by the BbEthnicitylkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbEthnicitylkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbEthnicitylkp, BbEthnicitylkpKey> GetNewDataSourceView()
		{
			return new BbEthnicitylkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbEthnicitylkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbEthnicitylkpDataSourceView : ProviderDataSourceView<BbEthnicitylkp, BbEthnicitylkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbEthnicitylkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbEthnicitylkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbEthnicitylkpDataSourceView(BbEthnicitylkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbEthnicitylkpDataSource BbEthnicitylkpOwner
		{
			get { return Owner as BbEthnicitylkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbEthnicitylkpSelectMethod SelectMethod
		{
			get { return BbEthnicitylkpOwner.SelectMethod; }
			set { BbEthnicitylkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbEthnicitylkpService BbEthnicitylkpProvider
		{
			get { return Provider as BbEthnicitylkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbEthnicitylkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbEthnicitylkp> results = null;
			BbEthnicitylkp item;
			count = 0;
			
			int _ethnicityid;

			switch ( SelectMethod )
			{
				case BbEthnicitylkpSelectMethod.Get:
					BbEthnicitylkpKey entityKey  = new BbEthnicitylkpKey();
					entityKey.Load(values);
					item = BbEthnicitylkpProvider.Get(entityKey);
					results = new TList<BbEthnicitylkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbEthnicitylkpSelectMethod.GetAll:
                    results = BbEthnicitylkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbEthnicitylkpSelectMethod.GetPaged:
					results = BbEthnicitylkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbEthnicitylkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbEthnicitylkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbEthnicitylkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbEthnicitylkpSelectMethod.GetByEthnicityid:
					_ethnicityid = ( values["Ethnicityid"] != null ) ? (int) EntityUtil.ChangeType(values["Ethnicityid"], typeof(int)) : (int)0;
					item = BbEthnicitylkpProvider.GetByEthnicityid(_ethnicityid);
					results = new TList<BbEthnicitylkp>();
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
			if ( SelectMethod == BbEthnicitylkpSelectMethod.Get || SelectMethod == BbEthnicitylkpSelectMethod.GetByEthnicityid )
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
				BbEthnicitylkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbEthnicitylkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbEthnicitylkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbEthnicitylkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbEthnicitylkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbEthnicitylkpDataSource class.
	/// </summary>
	public class BbEthnicitylkpDataSourceDesigner : ProviderDataSourceDesigner<BbEthnicitylkp, BbEthnicitylkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbEthnicitylkpDataSourceDesigner class.
		/// </summary>
		public BbEthnicitylkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbEthnicitylkpSelectMethod SelectMethod
		{
			get { return ((BbEthnicitylkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbEthnicitylkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbEthnicitylkpDataSourceActionList

	/// <summary>
	/// Supports the BbEthnicitylkpDataSourceDesigner class.
	/// </summary>
	internal class BbEthnicitylkpDataSourceActionList : DesignerActionList
	{
		private BbEthnicitylkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbEthnicitylkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbEthnicitylkpDataSourceActionList(BbEthnicitylkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbEthnicitylkpSelectMethod SelectMethod
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

	#endregion BbEthnicitylkpDataSourceActionList
	
	#endregion BbEthnicitylkpDataSourceDesigner
	
	#region BbEthnicitylkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbEthnicitylkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbEthnicitylkpSelectMethod
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
		/// Represents the GetByEthnicityid method.
		/// </summary>
		GetByEthnicityid
	}
	
	#endregion BbEthnicitylkpSelectMethod

	#region BbEthnicitylkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbEthnicitylkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbEthnicitylkpFilter : SqlFilter<BbEthnicitylkpColumn>
	{
	}
	
	#endregion BbEthnicitylkpFilter

	#region BbEthnicitylkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbEthnicitylkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbEthnicitylkpExpressionBuilder : SqlExpressionBuilder<BbEthnicitylkpColumn>
	{
	}
	
	#endregion BbEthnicitylkpExpressionBuilder	

	#region BbEthnicitylkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbEthnicitylkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbEthnicitylkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbEthnicitylkpProperty : ChildEntityProperty<BbEthnicitylkpChildEntityTypes>
	{
	}
	
	#endregion BbEthnicitylkpProperty
}

