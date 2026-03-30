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
	/// Represents the DataRepository.BbLesionlkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbLesionlkpDataSourceDesigner))]
	public class BbLesionlkpDataSource : ProviderDataSource<BbLesionlkp, BbLesionlkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbLesionlkpDataSource class.
		/// </summary>
		public BbLesionlkpDataSource() : base(new BbLesionlkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbLesionlkpDataSourceView used by the BbLesionlkpDataSource.
		/// </summary>
		protected BbLesionlkpDataSourceView BbLesionlkpView
		{
			get { return ( View as BbLesionlkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbLesionlkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbLesionlkpSelectMethod SelectMethod
		{
			get
			{
				BbLesionlkpSelectMethod selectMethod = BbLesionlkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbLesionlkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbLesionlkpDataSourceView class that is to be
		/// used by the BbLesionlkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbLesionlkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbLesionlkp, BbLesionlkpKey> GetNewDataSourceView()
		{
			return new BbLesionlkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbLesionlkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbLesionlkpDataSourceView : ProviderDataSourceView<BbLesionlkp, BbLesionlkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbLesionlkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbLesionlkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbLesionlkpDataSourceView(BbLesionlkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbLesionlkpDataSource BbLesionlkpOwner
		{
			get { return Owner as BbLesionlkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbLesionlkpSelectMethod SelectMethod
		{
			get { return BbLesionlkpOwner.SelectMethod; }
			set { BbLesionlkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbLesionlkpService BbLesionlkpProvider
		{
			get { return Provider as BbLesionlkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbLesionlkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbLesionlkp> results = null;
			BbLesionlkp item;
			count = 0;
			
			int _lesionid;

			switch ( SelectMethod )
			{
				case BbLesionlkpSelectMethod.Get:
					BbLesionlkpKey entityKey  = new BbLesionlkpKey();
					entityKey.Load(values);
					item = BbLesionlkpProvider.Get(entityKey);
					results = new TList<BbLesionlkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbLesionlkpSelectMethod.GetAll:
                    results = BbLesionlkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbLesionlkpSelectMethod.GetPaged:
					results = BbLesionlkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbLesionlkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbLesionlkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbLesionlkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbLesionlkpSelectMethod.GetByLesionid:
					_lesionid = ( values["Lesionid"] != null ) ? (int) EntityUtil.ChangeType(values["Lesionid"], typeof(int)) : (int)0;
					item = BbLesionlkpProvider.GetByLesionid(_lesionid);
					results = new TList<BbLesionlkp>();
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
			if ( SelectMethod == BbLesionlkpSelectMethod.Get || SelectMethod == BbLesionlkpSelectMethod.GetByLesionid )
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
				BbLesionlkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbLesionlkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbLesionlkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbLesionlkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbLesionlkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbLesionlkpDataSource class.
	/// </summary>
	public class BbLesionlkpDataSourceDesigner : ProviderDataSourceDesigner<BbLesionlkp, BbLesionlkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbLesionlkpDataSourceDesigner class.
		/// </summary>
		public BbLesionlkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbLesionlkpSelectMethod SelectMethod
		{
			get { return ((BbLesionlkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbLesionlkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbLesionlkpDataSourceActionList

	/// <summary>
	/// Supports the BbLesionlkpDataSourceDesigner class.
	/// </summary>
	internal class BbLesionlkpDataSourceActionList : DesignerActionList
	{
		private BbLesionlkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbLesionlkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbLesionlkpDataSourceActionList(BbLesionlkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbLesionlkpSelectMethod SelectMethod
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

	#endregion BbLesionlkpDataSourceActionList
	
	#endregion BbLesionlkpDataSourceDesigner
	
	#region BbLesionlkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbLesionlkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbLesionlkpSelectMethod
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
		/// Represents the GetByLesionid method.
		/// </summary>
		GetByLesionid
	}
	
	#endregion BbLesionlkpSelectMethod

	#region BbLesionlkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbLesionlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbLesionlkpFilter : SqlFilter<BbLesionlkpColumn>
	{
	}
	
	#endregion BbLesionlkpFilter

	#region BbLesionlkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbLesionlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbLesionlkpExpressionBuilder : SqlExpressionBuilder<BbLesionlkpColumn>
	{
	}
	
	#endregion BbLesionlkpExpressionBuilder	

	#region BbLesionlkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbLesionlkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbLesionlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbLesionlkpProperty : ChildEntityProperty<BbLesionlkpChildEntityTypes>
	{
	}
	
	#endregion BbLesionlkpProperty
}

