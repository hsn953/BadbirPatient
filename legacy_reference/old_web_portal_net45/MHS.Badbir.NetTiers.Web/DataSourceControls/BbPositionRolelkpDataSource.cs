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
	/// Represents the DataRepository.BbPositionRolelkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbPositionRolelkpDataSourceDesigner))]
	public class BbPositionRolelkpDataSource : ProviderDataSource<BbPositionRolelkp, BbPositionRolelkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPositionRolelkpDataSource class.
		/// </summary>
		public BbPositionRolelkpDataSource() : base(new BbPositionRolelkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbPositionRolelkpDataSourceView used by the BbPositionRolelkpDataSource.
		/// </summary>
		protected BbPositionRolelkpDataSourceView BbPositionRolelkpView
		{
			get { return ( View as BbPositionRolelkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbPositionRolelkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbPositionRolelkpSelectMethod SelectMethod
		{
			get
			{
				BbPositionRolelkpSelectMethod selectMethod = BbPositionRolelkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbPositionRolelkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbPositionRolelkpDataSourceView class that is to be
		/// used by the BbPositionRolelkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbPositionRolelkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbPositionRolelkp, BbPositionRolelkpKey> GetNewDataSourceView()
		{
			return new BbPositionRolelkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbPositionRolelkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbPositionRolelkpDataSourceView : ProviderDataSourceView<BbPositionRolelkp, BbPositionRolelkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPositionRolelkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbPositionRolelkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbPositionRolelkpDataSourceView(BbPositionRolelkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbPositionRolelkpDataSource BbPositionRolelkpOwner
		{
			get { return Owner as BbPositionRolelkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbPositionRolelkpSelectMethod SelectMethod
		{
			get { return BbPositionRolelkpOwner.SelectMethod; }
			set { BbPositionRolelkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbPositionRolelkpService BbPositionRolelkpProvider
		{
			get { return Provider as BbPositionRolelkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbPositionRolelkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbPositionRolelkp> results = null;
			BbPositionRolelkp item;
			count = 0;
			
			int _positionid;

			switch ( SelectMethod )
			{
				case BbPositionRolelkpSelectMethod.Get:
					BbPositionRolelkpKey entityKey  = new BbPositionRolelkpKey();
					entityKey.Load(values);
					item = BbPositionRolelkpProvider.Get(entityKey);
					results = new TList<BbPositionRolelkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPositionRolelkpSelectMethod.GetAll:
                    results = BbPositionRolelkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbPositionRolelkpSelectMethod.GetPaged:
					results = BbPositionRolelkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbPositionRolelkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbPositionRolelkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbPositionRolelkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbPositionRolelkpSelectMethod.GetByPositionid:
					_positionid = ( values["Positionid"] != null ) ? (int) EntityUtil.ChangeType(values["Positionid"], typeof(int)) : (int)0;
					item = BbPositionRolelkpProvider.GetByPositionid(_positionid);
					results = new TList<BbPositionRolelkp>();
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
			if ( SelectMethod == BbPositionRolelkpSelectMethod.Get || SelectMethod == BbPositionRolelkpSelectMethod.GetByPositionid )
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
				BbPositionRolelkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbPositionRolelkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbPositionRolelkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbPositionRolelkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbPositionRolelkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbPositionRolelkpDataSource class.
	/// </summary>
	public class BbPositionRolelkpDataSourceDesigner : ProviderDataSourceDesigner<BbPositionRolelkp, BbPositionRolelkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbPositionRolelkpDataSourceDesigner class.
		/// </summary>
		public BbPositionRolelkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPositionRolelkpSelectMethod SelectMethod
		{
			get { return ((BbPositionRolelkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbPositionRolelkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbPositionRolelkpDataSourceActionList

	/// <summary>
	/// Supports the BbPositionRolelkpDataSourceDesigner class.
	/// </summary>
	internal class BbPositionRolelkpDataSourceActionList : DesignerActionList
	{
		private BbPositionRolelkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbPositionRolelkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbPositionRolelkpDataSourceActionList(BbPositionRolelkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPositionRolelkpSelectMethod SelectMethod
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

	#endregion BbPositionRolelkpDataSourceActionList
	
	#endregion BbPositionRolelkpDataSourceDesigner
	
	#region BbPositionRolelkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbPositionRolelkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbPositionRolelkpSelectMethod
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
		/// Represents the GetByPositionid method.
		/// </summary>
		GetByPositionid
	}
	
	#endregion BbPositionRolelkpSelectMethod

	#region BbPositionRolelkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPositionRolelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPositionRolelkpFilter : SqlFilter<BbPositionRolelkpColumn>
	{
	}
	
	#endregion BbPositionRolelkpFilter

	#region BbPositionRolelkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPositionRolelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPositionRolelkpExpressionBuilder : SqlExpressionBuilder<BbPositionRolelkpColumn>
	{
	}
	
	#endregion BbPositionRolelkpExpressionBuilder	

	#region BbPositionRolelkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbPositionRolelkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbPositionRolelkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPositionRolelkpProperty : ChildEntityProperty<BbPositionRolelkpChildEntityTypes>
	{
	}
	
	#endregion BbPositionRolelkpProperty
}

