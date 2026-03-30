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
	/// Represents the DataRepository.BbQueryProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbQueryDataSourceDesigner))]
	public class BbQueryDataSource : ProviderDataSource<BbQuery, BbQueryKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryDataSource class.
		/// </summary>
		public BbQueryDataSource() : base(new BbQueryService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbQueryDataSourceView used by the BbQueryDataSource.
		/// </summary>
		protected BbQueryDataSourceView BbQueryView
		{
			get { return ( View as BbQueryDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbQueryDataSource control invokes to retrieve data.
		/// </summary>
		public BbQuerySelectMethod SelectMethod
		{
			get
			{
				BbQuerySelectMethod selectMethod = BbQuerySelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbQuerySelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbQueryDataSourceView class that is to be
		/// used by the BbQueryDataSource.
		/// </summary>
		/// <returns>An instance of the BbQueryDataSourceView class.</returns>
		protected override BaseDataSourceView<BbQuery, BbQueryKey> GetNewDataSourceView()
		{
			return new BbQueryDataSourceView(this, DefaultViewName);
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
	/// Supports the BbQueryDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbQueryDataSourceView : ProviderDataSourceView<BbQuery, BbQueryKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbQueryDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbQueryDataSourceView(BbQueryDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbQueryDataSource BbQueryOwner
		{
			get { return Owner as BbQueryDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbQuerySelectMethod SelectMethod
		{
			get { return BbQueryOwner.SelectMethod; }
			set { BbQueryOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbQueryService BbQueryProvider
		{
			get { return Provider as BbQueryService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbQuery> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbQuery> results = null;
			BbQuery item;
			count = 0;
			
			int _chid;
			System.Int32? _fupnumber_nullable;
			int _queryStatusId;
			int _queryTypeId;
			int _qid;
			int _centreid;
			System.Int32 _fupaeid;
			System.Int32? sp220_Patientid;
			System.Int32? sp217_Chid;
			System.Int32? sp218_Chid;
			System.Int32? sp219_Chid;
			System.Int32? sp223_Fupaeid;

			switch ( SelectMethod )
			{
				case BbQuerySelectMethod.Get:
					BbQueryKey entityKey  = new BbQueryKey();
					entityKey.Load(values);
					item = BbQueryProvider.Get(entityKey);
					results = new TList<BbQuery>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbQuerySelectMethod.GetAll:
                    results = BbQueryProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbQuerySelectMethod.GetPaged:
					results = BbQueryProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbQuerySelectMethod.Find:
					if ( FilterParameters != null )
						results = BbQueryProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbQueryProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbQuerySelectMethod.GetByQid:
					_qid = ( values["Qid"] != null ) ? (int) EntityUtil.ChangeType(values["Qid"], typeof(int)) : (int)0;
					item = BbQueryProvider.GetByQid(_qid);
					results = new TList<BbQuery>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case BbQuerySelectMethod.GetByChid:
					_chid = ( values["Chid"] != null ) ? (int) EntityUtil.ChangeType(values["Chid"], typeof(int)) : (int)0;
					results = BbQueryProvider.GetByChid(_chid, this.StartIndex, this.PageSize, out count);
					break;
				case BbQuerySelectMethod.GetByChidFupnumber:
					_chid = ( values["Chid"] != null ) ? (int) EntityUtil.ChangeType(values["Chid"], typeof(int)) : (int)0;
					_fupnumber_nullable = (System.Int32?) EntityUtil.ChangeType(values["Fupnumber"], typeof(System.Int32?));
					results = BbQueryProvider.GetByChidFupnumber(_chid, _fupnumber_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbQuerySelectMethod.GetByQueryStatusId:
					_queryStatusId = ( values["QueryStatusId"] != null ) ? (int) EntityUtil.ChangeType(values["QueryStatusId"], typeof(int)) : (int)0;
					results = BbQueryProvider.GetByQueryStatusId(_queryStatusId, this.StartIndex, this.PageSize, out count);
					break;
				case BbQuerySelectMethod.GetByQueryTypeId:
					_queryTypeId = ( values["QueryTypeId"] != null ) ? (int) EntityUtil.ChangeType(values["QueryTypeId"], typeof(int)) : (int)0;
					results = BbQueryProvider.GetByQueryTypeId(_queryTypeId, this.StartIndex, this.PageSize, out count);
					break;
				// FK
				case BbQuerySelectMethod.GetByCentreid:
					_centreid = ( values["Centreid"] != null ) ? (int) EntityUtil.ChangeType(values["Centreid"], typeof(int)) : (int)0;
					results = BbQueryProvider.GetByCentreid(_centreid, this.StartIndex, this.PageSize, out count);
					break;
				// M:M
				case BbQuerySelectMethod.GetByFupaeidFromBbAdverseEventQueryLink:
					_fupaeid = ( values["Fupaeid"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Fupaeid"], typeof(System.Int32)) : (int)0;
					results = BbQueryProvider.GetByFupaeidFromBbAdverseEventQueryLink(_fupaeid, this.StartIndex, this.PageSize, out count);
					break;
				// Custom
				case BbQuerySelectMethod.Query_GetByPatientID_ExcludeSolvedAndExcluded:
					sp220_Patientid = (System.Int32?) EntityUtil.ChangeType(values["Patientid"], typeof(System.Int32?));
					results = BbQueryProvider.Query_GetByPatientID_ExcludeSolvedAndExcluded(sp220_Patientid, StartIndex, PageSize);
					break;
				case BbQuerySelectMethod.Query_GetByChid_AllFeedback:
					sp217_Chid = (System.Int32?) EntityUtil.ChangeType(values["Chid"], typeof(System.Int32?));
					results = BbQueryProvider.Query_GetByChid_AllFeedback(sp217_Chid, StartIndex, PageSize);
					break;
				case BbQuerySelectMethod.Query_GetByChid_ExcludeSolvedAndExcluded:
					sp218_Chid = (System.Int32?) EntityUtil.ChangeType(values["Chid"], typeof(System.Int32?));
					results = BbQueryProvider.Query_GetByChid_ExcludeSolvedAndExcluded(sp218_Chid, StartIndex, PageSize);
					break;
				case BbQuerySelectMethod.Query_GetByChid_SolvedQueries:
					sp219_Chid = (System.Int32?) EntityUtil.ChangeType(values["Chid"], typeof(System.Int32?));
					results = BbQueryProvider.Query_GetByChid_SolvedQueries(sp219_Chid, StartIndex, PageSize);
					break;
				case BbQuerySelectMethod.Query_GetRelatedQueriesByFupAEID:
					sp223_Fupaeid = (System.Int32?) EntityUtil.ChangeType(values["Fupaeid"], typeof(System.Int32?));
					results = BbQueryProvider.Query_GetRelatedQueriesByFupAEID(sp223_Fupaeid, StartIndex, PageSize);
					break;
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
			if ( SelectMethod == BbQuerySelectMethod.Get || SelectMethod == BbQuerySelectMethod.GetByQid )
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
				BbQuery entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbQueryProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbQuery> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbQueryProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbQueryDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbQueryDataSource class.
	/// </summary>
	public class BbQueryDataSourceDesigner : ProviderDataSourceDesigner<BbQuery, BbQueryKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbQueryDataSourceDesigner class.
		/// </summary>
		public BbQueryDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbQuerySelectMethod SelectMethod
		{
			get { return ((BbQueryDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbQueryDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbQueryDataSourceActionList

	/// <summary>
	/// Supports the BbQueryDataSourceDesigner class.
	/// </summary>
	internal class BbQueryDataSourceActionList : DesignerActionList
	{
		private BbQueryDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbQueryDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbQueryDataSourceActionList(BbQueryDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbQuerySelectMethod SelectMethod
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

	#endregion BbQueryDataSourceActionList
	
	#endregion BbQueryDataSourceDesigner
	
	#region BbQuerySelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbQueryDataSource.SelectMethod property.
	/// </summary>
	public enum BbQuerySelectMethod
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
		/// Represents the GetByChid method.
		/// </summary>
		GetByChid,
		/// <summary>
		/// Represents the GetByChidFupnumber method.
		/// </summary>
		GetByChidFupnumber,
		/// <summary>
		/// Represents the GetByQueryStatusId method.
		/// </summary>
		GetByQueryStatusId,
		/// <summary>
		/// Represents the GetByQueryTypeId method.
		/// </summary>
		GetByQueryTypeId,
		/// <summary>
		/// Represents the GetByQid method.
		/// </summary>
		GetByQid,
		/// <summary>
		/// Represents the GetByCentreid method.
		/// </summary>
		GetByCentreid,
		/// <summary>
		/// Represents the GetByFupaeidFromBbAdverseEventQueryLink method.
		/// </summary>
		GetByFupaeidFromBbAdverseEventQueryLink,
		/// <summary>
		/// Represents the Query_GetByPatientID_ExcludeSolvedAndExcluded method.
		/// </summary>
		Query_GetByPatientID_ExcludeSolvedAndExcluded,
		/// <summary>
		/// Represents the Query_GetByChid_AllFeedback method.
		/// </summary>
		Query_GetByChid_AllFeedback,
		/// <summary>
		/// Represents the Query_GetByChid_ExcludeSolvedAndExcluded method.
		/// </summary>
		Query_GetByChid_ExcludeSolvedAndExcluded,
		/// <summary>
		/// Represents the Query_GetByChid_SolvedQueries method.
		/// </summary>
		Query_GetByChid_SolvedQueries,
		/// <summary>
		/// Represents the Query_GetRelatedQueriesByFupAEID method.
		/// </summary>
		Query_GetRelatedQueriesByFupAEID
	}
	
	#endregion BbQuerySelectMethod

	#region BbQueryFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQuery"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryFilter : SqlFilter<BbQueryColumn>
	{
	}
	
	#endregion BbQueryFilter

	#region BbQueryExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQuery"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryExpressionBuilder : SqlExpressionBuilder<BbQueryColumn>
	{
	}
	
	#endregion BbQueryExpressionBuilder	

	#region BbQueryProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbQueryChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbQuery"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryProperty : ChildEntityProperty<BbQueryChildEntityTypes>
	{
	}
	
	#endregion BbQueryProperty
}

