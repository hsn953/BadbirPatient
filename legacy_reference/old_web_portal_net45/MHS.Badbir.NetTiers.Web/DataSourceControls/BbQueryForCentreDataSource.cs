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
	/// Represents the DataRepository.BbQueryForCentreProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbQueryForCentreDataSourceDesigner))]
	public class BbQueryForCentreDataSource : ProviderDataSource<BbQueryForCentre, BbQueryForCentreKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreDataSource class.
		/// </summary>
		public BbQueryForCentreDataSource() : base(new BbQueryForCentreService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbQueryForCentreDataSourceView used by the BbQueryForCentreDataSource.
		/// </summary>
		protected BbQueryForCentreDataSourceView BbQueryForCentreView
		{
			get { return ( View as BbQueryForCentreDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbQueryForCentreDataSource control invokes to retrieve data.
		/// </summary>
		public BbQueryForCentreSelectMethod SelectMethod
		{
			get
			{
				BbQueryForCentreSelectMethod selectMethod = BbQueryForCentreSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbQueryForCentreSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbQueryForCentreDataSourceView class that is to be
		/// used by the BbQueryForCentreDataSource.
		/// </summary>
		/// <returns>An instance of the BbQueryForCentreDataSourceView class.</returns>
		protected override BaseDataSourceView<BbQueryForCentre, BbQueryForCentreKey> GetNewDataSourceView()
		{
			return new BbQueryForCentreDataSourceView(this, DefaultViewName);
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
	/// Supports the BbQueryForCentreDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbQueryForCentreDataSourceView : ProviderDataSourceView<BbQueryForCentre, BbQueryForCentreKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbQueryForCentreDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbQueryForCentreDataSourceView(BbQueryForCentreDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbQueryForCentreDataSource BbQueryForCentreOwner
		{
			get { return Owner as BbQueryForCentreDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbQueryForCentreSelectMethod SelectMethod
		{
			get { return BbQueryForCentreOwner.SelectMethod; }
			set { BbQueryForCentreOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbQueryForCentreService BbQueryForCentreProvider
		{
			get { return Provider as BbQueryForCentreService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbQueryForCentre> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbQueryForCentre> results = null;
			BbQueryForCentre item;
			count = 0;
			
			int _qid;
			int _centreid;
			int _queryStatusId;
			int _queryTypeId;

			switch ( SelectMethod )
			{
				case BbQueryForCentreSelectMethod.Get:
					BbQueryForCentreKey entityKey  = new BbQueryForCentreKey();
					entityKey.Load(values);
					item = BbQueryForCentreProvider.Get(entityKey);
					results = new TList<BbQueryForCentre>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbQueryForCentreSelectMethod.GetAll:
                    results = BbQueryForCentreProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbQueryForCentreSelectMethod.GetPaged:
					results = BbQueryForCentreProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbQueryForCentreSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbQueryForCentreProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbQueryForCentreProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbQueryForCentreSelectMethod.GetByQid:
					_qid = ( values["Qid"] != null ) ? (int) EntityUtil.ChangeType(values["Qid"], typeof(int)) : (int)0;
					item = BbQueryForCentreProvider.GetByQid(_qid);
					results = new TList<BbQueryForCentre>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case BbQueryForCentreSelectMethod.GetByCentreid:
					_centreid = ( values["Centreid"] != null ) ? (int) EntityUtil.ChangeType(values["Centreid"], typeof(int)) : (int)0;
					results = BbQueryForCentreProvider.GetByCentreid(_centreid, this.StartIndex, this.PageSize, out count);
					break;
				case BbQueryForCentreSelectMethod.GetByQueryStatusId:
					_queryStatusId = ( values["QueryStatusId"] != null ) ? (int) EntityUtil.ChangeType(values["QueryStatusId"], typeof(int)) : (int)0;
					results = BbQueryForCentreProvider.GetByQueryStatusId(_queryStatusId, this.StartIndex, this.PageSize, out count);
					break;
				case BbQueryForCentreSelectMethod.GetByQueryTypeId:
					_queryTypeId = ( values["QueryTypeId"] != null ) ? (int) EntityUtil.ChangeType(values["QueryTypeId"], typeof(int)) : (int)0;
					results = BbQueryForCentreProvider.GetByQueryTypeId(_queryTypeId, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == BbQueryForCentreSelectMethod.Get || SelectMethod == BbQueryForCentreSelectMethod.GetByQid )
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
				BbQueryForCentre entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbQueryForCentreProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbQueryForCentre> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbQueryForCentreProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbQueryForCentreDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbQueryForCentreDataSource class.
	/// </summary>
	public class BbQueryForCentreDataSourceDesigner : ProviderDataSourceDesigner<BbQueryForCentre, BbQueryForCentreKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreDataSourceDesigner class.
		/// </summary>
		public BbQueryForCentreDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbQueryForCentreSelectMethod SelectMethod
		{
			get { return ((BbQueryForCentreDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbQueryForCentreDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbQueryForCentreDataSourceActionList

	/// <summary>
	/// Supports the BbQueryForCentreDataSourceDesigner class.
	/// </summary>
	internal class BbQueryForCentreDataSourceActionList : DesignerActionList
	{
		private BbQueryForCentreDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbQueryForCentreDataSourceActionList(BbQueryForCentreDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbQueryForCentreSelectMethod SelectMethod
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

	#endregion BbQueryForCentreDataSourceActionList
	
	#endregion BbQueryForCentreDataSourceDesigner
	
	#region BbQueryForCentreSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbQueryForCentreDataSource.SelectMethod property.
	/// </summary>
	public enum BbQueryForCentreSelectMethod
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
		/// Represents the GetByQid method.
		/// </summary>
		GetByQid,
		/// <summary>
		/// Represents the GetByCentreid method.
		/// </summary>
		GetByCentreid,
		/// <summary>
		/// Represents the GetByQueryStatusId method.
		/// </summary>
		GetByQueryStatusId,
		/// <summary>
		/// Represents the GetByQueryTypeId method.
		/// </summary>
		GetByQueryTypeId
	}
	
	#endregion BbQueryForCentreSelectMethod

	#region BbQueryForCentreFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryForCentre"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryForCentreFilter : SqlFilter<BbQueryForCentreColumn>
	{
	}
	
	#endregion BbQueryForCentreFilter

	#region BbQueryForCentreExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryForCentre"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryForCentreExpressionBuilder : SqlExpressionBuilder<BbQueryForCentreColumn>
	{
	}
	
	#endregion BbQueryForCentreExpressionBuilder	

	#region BbQueryForCentreProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbQueryForCentreChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryForCentre"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryForCentreProperty : ChildEntityProperty<BbQueryForCentreChildEntityTypes>
	{
	}
	
	#endregion BbQueryForCentreProperty
}

