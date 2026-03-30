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
	/// Represents the DataRepository.BbCentreRegionlkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbCentreRegionlkpDataSourceDesigner))]
	public class BbCentreRegionlkpDataSource : ProviderDataSource<BbCentreRegionlkp, BbCentreRegionlkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCentreRegionlkpDataSource class.
		/// </summary>
		public BbCentreRegionlkpDataSource() : base(new BbCentreRegionlkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbCentreRegionlkpDataSourceView used by the BbCentreRegionlkpDataSource.
		/// </summary>
		protected BbCentreRegionlkpDataSourceView BbCentreRegionlkpView
		{
			get { return ( View as BbCentreRegionlkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbCentreRegionlkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbCentreRegionlkpSelectMethod SelectMethod
		{
			get
			{
				BbCentreRegionlkpSelectMethod selectMethod = BbCentreRegionlkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbCentreRegionlkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbCentreRegionlkpDataSourceView class that is to be
		/// used by the BbCentreRegionlkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbCentreRegionlkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbCentreRegionlkp, BbCentreRegionlkpKey> GetNewDataSourceView()
		{
			return new BbCentreRegionlkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbCentreRegionlkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbCentreRegionlkpDataSourceView : ProviderDataSourceView<BbCentreRegionlkp, BbCentreRegionlkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCentreRegionlkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbCentreRegionlkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbCentreRegionlkpDataSourceView(BbCentreRegionlkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbCentreRegionlkpDataSource BbCentreRegionlkpOwner
		{
			get { return Owner as BbCentreRegionlkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbCentreRegionlkpSelectMethod SelectMethod
		{
			get { return BbCentreRegionlkpOwner.SelectMethod; }
			set { BbCentreRegionlkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbCentreRegionlkpService BbCentreRegionlkpProvider
		{
			get { return Provider as BbCentreRegionlkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbCentreRegionlkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbCentreRegionlkp> results = null;
			BbCentreRegionlkp item;
			count = 0;
			
			System.Int32 _centreregid;

			switch ( SelectMethod )
			{
				case BbCentreRegionlkpSelectMethod.Get:
					BbCentreRegionlkpKey entityKey  = new BbCentreRegionlkpKey();
					entityKey.Load(values);
					item = BbCentreRegionlkpProvider.Get(entityKey);
					results = new TList<BbCentreRegionlkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbCentreRegionlkpSelectMethod.GetAll:
                    results = BbCentreRegionlkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbCentreRegionlkpSelectMethod.GetPaged:
					results = BbCentreRegionlkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbCentreRegionlkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbCentreRegionlkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbCentreRegionlkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbCentreRegionlkpSelectMethod.GetByCentreregid:
					_centreregid = ( values["Centreregid"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Centreregid"], typeof(System.Int32)) : (int)0;
					item = BbCentreRegionlkpProvider.GetByCentreregid(_centreregid);
					results = new TList<BbCentreRegionlkp>();
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
			if ( SelectMethod == BbCentreRegionlkpSelectMethod.Get || SelectMethod == BbCentreRegionlkpSelectMethod.GetByCentreregid )
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
				BbCentreRegionlkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbCentreRegionlkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbCentreRegionlkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbCentreRegionlkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbCentreRegionlkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbCentreRegionlkpDataSource class.
	/// </summary>
	public class BbCentreRegionlkpDataSourceDesigner : ProviderDataSourceDesigner<BbCentreRegionlkp, BbCentreRegionlkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbCentreRegionlkpDataSourceDesigner class.
		/// </summary>
		public BbCentreRegionlkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbCentreRegionlkpSelectMethod SelectMethod
		{
			get { return ((BbCentreRegionlkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbCentreRegionlkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbCentreRegionlkpDataSourceActionList

	/// <summary>
	/// Supports the BbCentreRegionlkpDataSourceDesigner class.
	/// </summary>
	internal class BbCentreRegionlkpDataSourceActionList : DesignerActionList
	{
		private BbCentreRegionlkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbCentreRegionlkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbCentreRegionlkpDataSourceActionList(BbCentreRegionlkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbCentreRegionlkpSelectMethod SelectMethod
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

	#endregion BbCentreRegionlkpDataSourceActionList
	
	#endregion BbCentreRegionlkpDataSourceDesigner
	
	#region BbCentreRegionlkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbCentreRegionlkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbCentreRegionlkpSelectMethod
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
		/// Represents the GetByCentreregid method.
		/// </summary>
		GetByCentreregid
	}
	
	#endregion BbCentreRegionlkpSelectMethod

	#region BbCentreRegionlkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbCentreRegionlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCentreRegionlkpFilter : SqlFilter<BbCentreRegionlkpColumn>
	{
	}
	
	#endregion BbCentreRegionlkpFilter

	#region BbCentreRegionlkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbCentreRegionlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCentreRegionlkpExpressionBuilder : SqlExpressionBuilder<BbCentreRegionlkpColumn>
	{
	}
	
	#endregion BbCentreRegionlkpExpressionBuilder	

	#region BbCentreRegionlkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbCentreRegionlkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbCentreRegionlkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCentreRegionlkpProperty : ChildEntityProperty<BbCentreRegionlkpChildEntityTypes>
	{
	}
	
	#endregion BbCentreRegionlkpProperty
}

